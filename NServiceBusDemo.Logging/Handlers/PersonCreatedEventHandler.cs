using System;
using System.Threading;
using NServiceBus;
using NServiceBusDemo.Messages.Events;

namespace NServiceBusDemo.Logging.Handlers
{
    public class PersonCreatedEventHandler : IHandleMessages<IPersonCreated>
    {
        public void Handle(IPersonCreated message)
        {
            Thread.Sleep(500);

            Console.WriteLine("Logging: Person created event logged.");
        }
    }
}
