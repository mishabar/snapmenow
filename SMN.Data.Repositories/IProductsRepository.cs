using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Data.Repositories
{
    public interface IProductsRepository
    {
        IEnumerable<Product> FindAllForRetailer(string retailerEmail);
        Product Create(Product product);
        Product Get(string id);
        void Update(Product product);
    }
}
