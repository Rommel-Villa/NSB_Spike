using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBusDemo.Common.Model;

namespace NServiceBusDemo.Messages.Commands
{
    public class CreatePerson
    {
        public Person Person { get; set; }
    }
}
