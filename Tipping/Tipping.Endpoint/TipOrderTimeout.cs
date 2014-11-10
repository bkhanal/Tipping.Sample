using System;

namespace Tipping.Endpoint
{
    public class TipOrderTimeout
    {
        public Guid MessageId { get; set; }

        public Guid OrderId { get; set; }
    }
}