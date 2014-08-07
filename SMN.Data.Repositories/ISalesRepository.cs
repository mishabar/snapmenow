using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Data.Repositories
{
    public interface ISalesRepository
    {
        IEnumerable<Product> GetActive();

        Product GetActive(string id);

        bool SnapProduct(string user, Product product);

        bool StartSale(string productId);
    }
}
