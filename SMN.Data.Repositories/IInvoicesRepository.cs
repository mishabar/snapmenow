using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Data.Repositories
{
    public interface IInvoicesRepository
    {
        Invoice GetOrCreate(string userSnapId, string productId);

        void Save(Invoice invoice);
    }
}
