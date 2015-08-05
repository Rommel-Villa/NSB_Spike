using System.Dynamic;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence;
using NServiceBusDemo.Common.Model;
using NServiceBusDemo.Messages.Commands;

namespace NServiceBusDemo.Main
{
    public class Program
    {
        private readonly IBus bus;

        public Program(IBus bus)
        {
            this.bus = bus;
        }

        public void Run()
        {
            while (true)
            {
                Console.Write("> ");

                var input = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (input.Length < 1) { continue; }

                var command = input[0].Trim().ToLower();

                if ("quit" == command) { break; }

                switch (command)
                {
                    case "create":
                        bus.Send<CreatePerson>(c =>
                        {
                            c.Person = new Person
                            {
                                Name = input.Length > 1 ? input[1] : string.Empty,
                                Age = input.Length > 2 ? int.Parse(input[2]) : 0,
                                Phone = input.Length > 3 ? input[3] : string.Empty
                            };
                        });
                        break;
                    default:
                        Console.WriteLine("???");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Program>();

            var container = builder.Build();

            var configuration = new BusConfiguration();

            configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));

            configuration.UseTransport<AzureStorageQueueTransport>();
            configuration.UsePersistence<AzureStoragePersistence>();
            configuration.UseSerialization<JsonSerializer>();

            configuration.DisableFeature<Sagas>();
            configuration.DisableFeature<TimeoutManager>();

            configuration.Conventions().DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusDemo.Messages.Commands"));
            configuration.Conventions().DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusDemo.Messages.Events"));
            configuration.Conventions().DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("NServiceBusDemo.Messages.Responses"));

            Bus.Create(configuration).Start();

            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<Program>().Run();
            }
        }
    }
}
