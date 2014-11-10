using System;
using NServiceBus.Saga;

namespace Tipping.Endpoint
{
    public class TippingTrackerSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
        [Unique]
        public Guid OrderId { get; set; }

        public double TipAmount { get; set; }

        public string TipCurrency { get; set; }

        public Guid UserId { get; set; }
    }
}