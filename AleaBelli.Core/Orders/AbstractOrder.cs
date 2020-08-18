using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AleaBelli.Core.Orders
{
    public enum OrderTarget
    {
        InvalidOrder = 0,
        Unit = 1,
    }

    public class AbstractOrder
    {
        public OrderTarget OrderTarget { get; set; }
    }
}
