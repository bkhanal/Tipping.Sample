using System;

namespace Tipping.Messages.Commands
{
    public class ProcessTipPayment
    {
        public Guid MessageId { get; set; }
        public Guid OrderId { get; set; }
        public double TipAmount { get; set; }
        public string TipCurrency { get; set; }
        public Guid UserId { get; set; }

        public override string ToString()
        {
            return string.Format("Order Id:{0}, TipAmount:{1}, TipCurrency:{2}, UserId:{3}", OrderId, TipAmount,
                                 TipCurrency, UserId);
        }
    }
}