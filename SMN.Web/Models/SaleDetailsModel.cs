using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMN.Services.Tokens;

namespace SMN.Web.Models
{
    public class SaleDetailsModel
    {
        public SMN.Services.Tokens.ProductToken Product { get; set; }
        public SnapToken MySnap { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
    }
}