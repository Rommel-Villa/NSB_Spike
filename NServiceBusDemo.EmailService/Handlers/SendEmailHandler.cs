using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBusDemo.Messages.Commands;
using System.Threading;

namespace NServiceBusDemo.EmailService.Handlers
{
    public class SendEmailHandler : IHandleMessages<SendEmail>
    {
        public void Handle(SendEmail message)
        {
            Thread.Sleep(500);

            Console.WriteLine("Email: Email sent.");
        }
    }
}
