using NServiceBus.Features;

namespace NServiceBusDemo.EmailService
{
    using Autofac;
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            // TODO: register types here

            var container = builder.Build();

            configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));

            
            configuration.UseTransport<AzureStorageQueueTransport>()
                .ConnectionString("UseDevelopmentStorage=true");
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
