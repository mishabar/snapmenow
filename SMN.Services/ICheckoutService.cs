using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Services
{
    public interface ICheckoutService
    {
        Tokens.SnapToken GetSnap(string userID, string id);

        Tokens.InvoiceToken GetOrCreateInvoice(Tokens.SnapToken snapToken);

        void SaveInvoice(Tokens.InvoiceToken invoice);
    }
}
