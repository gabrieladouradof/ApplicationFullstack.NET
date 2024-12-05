using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimadev.Core.Models.Reports
{
    public record ExpensesByCategory(string UserId, string Category, int Year, decimal incomes);

}
