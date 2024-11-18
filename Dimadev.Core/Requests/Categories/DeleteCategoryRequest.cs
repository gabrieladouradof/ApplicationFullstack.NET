using Dimadev.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories
{
    public class DeleteCategoryRequest : Request
    {
        public long Id { get; set; }
    }
}
