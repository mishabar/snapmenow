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
        void Update(Product product, string email);
        void UpdateImage(string id, int i, string url);
        string[] Delete(string id, string email);
        void ScheduleSale(string id, DateTime start_on, bool start_now, int duration, int items, string email);
    }
}
