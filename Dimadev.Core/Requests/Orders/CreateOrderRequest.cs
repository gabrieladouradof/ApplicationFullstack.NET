using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimadev.Core.Requests.Orders
{
    public class CreateOrderRequest : Request
    {
        public long ProductId { get; set; }
        public long? VoucherId { get; set; }
    }
}
