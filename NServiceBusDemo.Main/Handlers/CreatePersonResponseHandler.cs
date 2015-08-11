using System;
using System.Threading;
using NServiceBus;
using NServiceBusDemo.Messages.Commands;
using NServiceBusDemo.Messages.Responses;

namespace NServiceBusDemo.Main.Handlers
{
    public class CreatePersonResponseHandler : IHandleMessages<CreatePersonResponse>
    {
        private readonly IBus bus;

        public CreatePersonResponseHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(CreatePersonResponse message)
        {
            Thread.Sleep(500);

            Console.WriteLine("Main: Person created.");

            bus.Send<SendEmail>(c =>
            {
                c.From = "me";
                c.To = "you";
                c.Subject = "Person created";
                c.Body = "A new person has been created!";
            });
        }
    }
}
