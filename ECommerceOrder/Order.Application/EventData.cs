using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application
{
    public class EventData
    {
        public int ProductId { get; set; }
        public required string OperationType { get; set; }
    }
}
