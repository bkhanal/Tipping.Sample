using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus.Testing;
using NUnit.Framework;
using Tipping.Messages.Commands;

namespace Tipping.Endpoint.Tests
{
    [TestFixture]
    public class TextFixtureSagaTests
    {
        [SetUp]
        public void Init()
        {
            Test.Initialize();
        }
        [Test]
        public void Run()
        {

            Test.Saga<TippingTrackerSaga>()
                //.ExpectReplyToOrginator<MyResponse>()
                .ExpectTimeoutToBeSetIn<TipForOrder>((state, span) => span == TimeSpan.FromMinutes(1))
                .When(s => s.Handle(new ChangeTipForOrder()))

                .WhenSagaTimesOut()
                .AssertSagaCompletionIs(true);
            //.ExpectSend<ProcessTipPayment>(x=>x.TipAmount==Data );
        }
    }

}
