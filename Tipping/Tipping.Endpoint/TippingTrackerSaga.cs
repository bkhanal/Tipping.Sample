using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Saga;
using Tipping.Messages;
using Tipping.Messages.Commands;

namespace Tipping.Endpoint
{
    public class TippingTrackerSaga :Saga<TippingTrackerSagaData>, 
        IAmStartedByMessages<TipForOrder>
        , IHandleMessages<ChangeTipForOrder>
        ,IHandleTimeouts<TipOrderTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TippingTrackerSagaData> mapper)
        {
            mapper.ConfigureMapping<TipForOrder>(m=>m.OrderId).ToSaga(y=>y.OrderId);
            mapper.ConfigureMapping<ChangeTipForOrder>(m => m.OrderId).ToSaga(y => y.OrderId);
        }

        
        public void Handle(TipForOrder message)
        {
            //Validation LOGIC! - some of this can go in the command class. - data validations..
            //is currency valid, tip amount valid, 
            //maybe business logic to have certain amount per customer or the user preference to not go beyond some number
            if (message.TipAmount < 1)
                throw new InvalidOperationException("Tip cannot be less than 1 dollar");
            Console.WriteLine("Tip order message:" + message.ToString());
            Data.OrderId = message.OrderId;
            Data.TipAmount = message.TipAmount;
            Data.TipCurrency = message.TipCurrency;
            Data.UserId = message.UserId;
            
            //TODO : Make this configurable, inject some interface for configuration management.
            RequestTimeout(TimeSpan.FromSeconds(60*2),new TipOrderTimeout{MessageId = Guid.NewGuid(),OrderId = message.OrderId});
        }

        public void Handle(ChangeTipForOrder message)
        {
            //Validation LOGIC! - some of this can go in the command class. - data validations..
            //is currency valid, tip amount valid, 
            //maybe business logic to make sure tipamount cannot be changed to certain value / currency.

            Console.WriteLine("Change tip message:" + message.ToString());
            Data.TipAmount = message.TipAmount;
            Data.TipCurrency = message.TipCurrency;
        }

        public void Timeout(TipOrderTimeout state)
        {
            Bus.SendLocal(new ProcessTipPayment
                {
                    MessageId = Guid.NewGuid(),
                    OrderId = Data.Id,
                    TipAmount = Data.TipAmount,
                    TipCurrency = Data.TipCurrency,
                    UserId = Data.UserId    
                });
            MarkAsComplete();
        }
    }
}
