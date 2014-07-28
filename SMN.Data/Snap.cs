using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Data
{
    public class Snap
    {
        public string ProductID { get; set; }
        public string UserID { get; set; }
        public int Price { get; set; }
        public DateTime SnappedAt { get; set; }
        public int FinalPrice { get; set; }
    }
}
