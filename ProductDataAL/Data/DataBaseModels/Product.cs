﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataAL.Data.DataBaseModels
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public string? Description { get; set; }
        public string? Image { get; set; }

    }
}
