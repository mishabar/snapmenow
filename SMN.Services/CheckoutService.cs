using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMN.Data.Repositories;
using SMN.Services.Tokens;

namespace SMN.Services
{
    public class CheckoutService : ICheckoutService
    {
        private IUserSnapsRepository _userSnapsRepository;
        private IInvoicesRepository _invoicesRepository;

        public CheckoutService(IUserSnapsRepository userSnapsRepository, IInvoicesRepository invoicesRepository)
        {
            _userSnapsRepository = userSnapsRepository;
            _invoicesRepository = invoicesRepository;
        }

        public SnapToken GetSnap(string userID, string id)
        {
            return _userSnapsRepository.Get(userID, id).AsToken();
        }

        public InvoiceToken GetOrCreateInvoice(SnapToken snapToken)
        {
            return _invoicesRepository.GetOrCreate(snapToken.ID, snapToken.ProductID).AsToken();
        }


        public void SaveInvoice(InvoiceToken invoice)
        {
            _invoicesRepository.Save(invoice.AsInvoice());
        }
    }
}
