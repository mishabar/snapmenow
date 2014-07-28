using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Services.Tokens
{
    public class SaleToken
    {
        public DateTime EndsAt { get; set; }
        public int CurrentPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
