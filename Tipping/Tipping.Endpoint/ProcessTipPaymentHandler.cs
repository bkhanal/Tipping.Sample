using System;
using NServiceBus;
using Tipping.Messages;
using Tipping.Messages.Commands;

namespace Tipping.Endpoint
{
    public class ProcessTipPaymentHandler : IHandleMessages<ProcessTipPayment>
    {
        public void Handle(ProcessTipPayment message)
        {
            Console.WriteLine("Process tip message" + message.ToString());
        }
    }
}