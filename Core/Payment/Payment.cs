using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payment
{
    public abstract class Payment
    {
        Order Order { get; set; }

        protected Payment(Order order)
        {
            Order = order;
        }
    }
}
