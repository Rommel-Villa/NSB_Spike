using Autofac;
using NServiceBus.Features;

namespace NServiceBusDemo.Persistence
{
    using NServiceBus;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint,AsA_Server
    {
        public void Customize(BusConfiguration configuration)
        {
            var builder = new ContainerBuilder();


            var container = builder.Build();

            configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));

            configuration.UseTransport<AzureStorageQueueTransport>();
            configuration.AzureConfigurationSource();
            configuration.UsePersistence<AzureStoragePersistence>();
            configuration.UseSerialization<JsonSerializer>();


            configuration.DisableFeature<Sagas>();
            configuration.DisableFeature<TimeoutManager>();

            configuration.Conventions().DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusDemo.Messages.Commands"));
            configuration.Conventions().DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusDemo.Messages.Events"));
            configuration.Conventions().DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusDemo.Messages.Responses"));
        }
    }
}
