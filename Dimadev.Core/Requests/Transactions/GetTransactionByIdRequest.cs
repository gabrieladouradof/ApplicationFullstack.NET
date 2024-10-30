using Dimadev.Core.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Transactions
{
    public class GetTransactionByIdRequest : Request
    {
       public long Id { get; set; } 
    }
}
