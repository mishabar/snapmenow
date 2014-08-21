using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMN.Data;

namespace SMN.Services.Tokens
{
    public class InvoiceToken
    {
        public string ID { get; set; }
        public string UserSnapID { get; set; }
        public string ProductID { get; set; }
        public AddressToken ShippingAddress { get; set; }
        public AddressToken BillingAddress { get; set; }
    }

    public static class InvoiceTokenExtensions
    {
        public static InvoiceToken AsToken(this Invoice invoice)
        {
            return new InvoiceToken 
            {
                ID = invoice.ID,
                ProductID = invoice.ProductID,
                UserSnapID = invoice.UserSnapID,
                ShippingAddress = invoice.ShippingAddress == null ? new AddressToken() : invoice.ShippingAddress.AsToken(),
                BillingAddress = invoice.BillingAddress == null ? new AddressToken() : invoice.BillingAddress.AsToken()
            };
        }

        public static Invoice AsInvoice(this InvoiceToken token)
        {
            return new Invoice 
            {
                ID = token.ID,
                ProductID = token.ProductID,
                UserSnapID = token.UserSnapID,
                ShippingAddress = token.ShippingAddress != null ? token.ShippingAddress.AsAddress() : null,
                BillingAddress = token.BillingAddress != null ? token.BillingAddress.AsAddress() : null
            };
        }
    }
}
