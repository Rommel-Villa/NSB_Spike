using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBusDemo.Messages.Events;
using System.Threading;

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
