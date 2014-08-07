using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace SMN.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private MongoCollection<Product> _collection;

        public ProductsRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<Product>("products");
        }

        public IEnumerable<Product> FindAllForRetailer(string retailerEmail)
        {
            return retailerEmail == null ?  _collection.FindAll().ToList() : _collection.Find(Query<Product>.EQ(p => p.Email, retailerEmail)).ToList();
        }

        public Product Create(Product product)
        {
            if (_collection.FindOne(Query.And(Query<Product>.EQ(p => p.Email, product.Email), Query<Product>.EQ(p => p.SKU, product.SKU))) == null)
            {
                _collection.Insert(product);
                return product;
            }
            return null;
        }
    }
}
