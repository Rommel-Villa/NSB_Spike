using Autofac;
using NServiceBus.Features;

namespace NServiceBusDemo.Persistence
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
//            var builder = new ContainerBuilder();
//            var container = builder.Build();
//            configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));

            configuration.UseTransport<AzureStorageQueueTransport>();
            configuration.UsePersistence<AzureStoragePersistence>();
            configuration.UseSerialization<JsonSerializer>();


            configuration.DisableFeature<Sagas>();
//            configuration.DisableFeature<TimeoutManager>();

            configuration.Conventions().DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusDemo.Messages.Commands"));
            configuration.Conventions().DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusDemo.Messages.Events"));
            configuration.Conventions().DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusDemo.Messages.Responses"));
        }
    }
}
