using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NServiceBusDemo.Common.Model;

namespace NServiceBusDemo.Messages.Events
{
    public interface IPersonCreated
    {
        Person Person { get; set; }
    }
}
