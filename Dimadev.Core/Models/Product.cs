using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimadev.Core.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
       // public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;   
        public bool IsActive { get; set; }
        public decimal Price { get; set; }

    }
}
