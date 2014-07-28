using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace SMN.Data.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private MongoCollection<Product> _productsCollection;
        private MongoCollection<Sale> _salesCollection;

        public SalesRepository(MongoDatabase db)
        {
            _productsCollection = db.GetCollection<Product>("products");
            _salesCollection = db.GetCollection<Sale>("sales");
        }

        public IEnumerable<Product> GetActive()
        {
            //Product p1 = _productsCollection.FindOne();
            //Sale s1 = new Sale { ProductID = p1.ID, StartedAt = DateTime.Now };
            //_salesCollection.Insert(s1);
            //p1.CurrentSale = new BasicSaleData { CurrentPrice = (int)(p1.MSRP * 0.75), EndsAt = DateTime.Now.AddHours(24), ID = s1.ID  };
            //_productsCollection.Save(p1);
            return _productsCollection.Find(Query<Product>.Exists(p => p.CurrentSale));
        }

        public Product GetActive(string id)
        {
            return _productsCollection.FindOne(Query<Product>.EQ(p => p.ID, id));
        }
    }
}
