﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimadev.Core.Requests.Orders
{
    public class GetOrderByNumberRequest : Request
    {
        public string Number { get; set; } = string.Empty;
    }
}
