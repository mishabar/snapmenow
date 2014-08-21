using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMN.Services.Tokens;

namespace SMN.Web.Models
{
    public class BillingModel
    {
        public int Step { get; set; }
        public string ID { get; set; }
        public AddressToken Address { get; set; }
        public AddressToken ShippingAddress { get; set; }
    }
}