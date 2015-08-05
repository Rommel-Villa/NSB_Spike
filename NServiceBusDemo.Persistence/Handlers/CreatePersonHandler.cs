using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBusDemo.Messages.Commands;
using NServiceBusDemo.Messages.Events;
using NServiceBusDemo.Messages.Responses;

namespace NServiceBusDemo.Persistence.Handlers
{
    public class CreatePersonHandler : IHandleMessages<CreatePerson>
    {
        private readonly IBus bus;

        public CreatePersonHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(CreatePerson message)
        {
            Thread.Sleep(500);

            Console.WriteLine("Persistence: Person created.");

            // send response
            bus.Reply<CreatePersonResponse>(r => { r.Id = Guid.NewGuid(); });

            // publish event
            bus.Publish<IPersonCreated>(e => { e.Person = message.Person; });
        }
    }
}
