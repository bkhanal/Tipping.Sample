using System.Reflection;
using NServiceBus;
using NServiceBus.Persistence;

namespace Tipping.Endpoint
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<RavenDBPersistence>();
            var conventionsBuilder = configuration.Conventions();
            conventionsBuilder.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Tipping") && t.Namespace.EndsWith("Commands"));
            conventionsBuilder.DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Tipping") && t.Namespace.EndsWith("Events"));
            configuration.ScanAssembliesInDirectory
                (System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            
            configuration.AssembliesToScan(AllAssemblies.Matching("AI.").
                And("Performant.").And("NServiceBus.").Except("test"));
        }
    }
}
