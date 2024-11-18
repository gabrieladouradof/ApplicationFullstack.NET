using Dimadev.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Transactions
{
    public class GetTransactionByPeriodRequest : PagedRequest
    {
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
    }
}
