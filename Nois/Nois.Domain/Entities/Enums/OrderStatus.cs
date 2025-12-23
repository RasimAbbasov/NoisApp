using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Domain.Entities.Enums
{
    public enum OrderStatus
    {
        Pending = 0,
        Paid = 1, 
        Shipped = 2, 
        Cancelled = 3
    }
}
