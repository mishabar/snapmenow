using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMN.Services.Tokens;

namespace SMN.Web.Models
{
    public class ReviewModel
    {
        public int Step { get; set; }
        public string ID { get; set; }
        public SnapToken UserSnap { get; set; }
        public ProductToken Product { get; set; }
    }
}