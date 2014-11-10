using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Tipping.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {

            var configuration = new BusConfiguration();
            var bus = Bus.Create(configuration);
            bus.Start();
                
            
            Console.WriteLine("Press 1 to send TipForOrder command. ");
            Console.WriteLine("Press 2 to send ChangeTipForOrder command. ");
            Console.WriteLine("Press 3 to quit");
            while (true)
            {
                var input = Console.ReadKey();
                switch input
                       case "1":
                {
                }

            }

        }

        //private static IStartableBus CreateBus()
        //{
          
        //}
    }
}
