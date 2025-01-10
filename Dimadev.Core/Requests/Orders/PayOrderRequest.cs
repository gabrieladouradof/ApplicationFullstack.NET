using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimadev.Core.Requests.Orders
{
    public class PayOrderRequest : Request
    {
        public long Id { get; set; }
        public string ExternalReference { get; set;} = string.Empty;
            
    }
}
