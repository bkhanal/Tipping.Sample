using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NServiceBus;
using NServiceBus.Persistence;
using Tipping.Messages;
using Tipping.Messages.Commands;

namespace Tipping.TestHarness.UI
{
    public partial class Form1 : Form
    {
        private IStartableBus bus;
        private Guid orderId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();
        public Form1()
        {
            InitializeComponent();
            var configuration = new BusConfiguration();
            configuration.UsePersistence<RavenDBPersistence>();
            var conventionsBuilder = configuration.Conventions();
            conventionsBuilder.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Tipping") && t.Namespace.EndsWith("Commands"));
            conventionsBuilder.DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Tipping") && t.Namespace.EndsWith("Events"));

            
            configuration.ScanAssembliesInDirectory(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
             bus = Bus.Create(configuration);
            bus.Start();
                
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var message = new TipForOrder
                {
                    MessageId = Guid.NewGuid(),
                    OrderId = orderId,
                    TipAmount = Convert.ToDouble(textBox1.Text),
                    UserId = userId,
                    TipCurrency = "$"
                };
            //DONOT do this.
            bus.Send("tipping.endpoint",message);

            //bus.Send(message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var message = (new ChangeTipForOrder()
            {
                MessageId = Guid.NewGuid(),
                OrderId = orderId,
                TipAmount = Convert.ToDouble(textBox2.Text),
                UserId = userId,
                TipCurrency = "$"
            });
            //DONOT DO THIS
            bus.Send("tipping.endpoint", message);


            //bus.Send(message);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var message = new TipForOrder
            {
                MessageId = Guid.NewGuid(),
                OrderId = orderId,
                TipAmount = Convert.ToDouble(textBox1.Text),
                UserId = userId,
                TipCurrency = "$"
            };
            //DONOT do this.
            //bus.Send("tipping.endpoint", message);

            bus.Send(message);
        }
    }
}
