﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Services.Tokens
{
    public class SaleToken
    {
        public int Items { get; set; }
        public DateTime EndsAt { get; set; }
        public float CurrentPrice { get; set; }
        public float Discount { get; set; }
        public int Snaps { get; set; }
        public string ID { get; set; }
    }
}
