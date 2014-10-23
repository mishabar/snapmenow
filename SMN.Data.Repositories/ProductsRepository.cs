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

        public Product Get(string id)
        {
            return _collection.FindOne(Query<Product>.EQ(p => p.ID, id));
        }

        public void Update(Product product, string email)
        {
            Product item = _collection.FindOne(Query<Product>.EQ(p => p.ID, product.ID));
            if (item == null || (item.Email != email && email != ""))
                throw new UnauthorizedAccessException("Unauthorized");

            //if (item.CurrentSale != null)
            //    throw new InvalidOperationException("Cannot update a Product during Active Sale");

            _collection.Update(Query<Product>.EQ(p => p.ID, product.ID),
                Update<Product>.Set(p => p.Name, product.Name).Set(p => p.Description, product.Description)
                .Set(p => p.SKU, product.SKU).Set(p => p.MSRP, product.MSRP).Set(p => p.MinPrice, product.MinPrice)
                .Set(p => p.SnapPrice, product.SnapPrice).Set(p => p.ItemsAvailable, product.ItemsAvailable));
        }


        public void UpdateImage(string id, int i, string url)
        {
            Product product =  _collection.FindOne(Query<Product>.EQ(p => p.ID, id));
            product.Images[i] = url;
            _collection.Save(product);
        }


        public string[] Delete(string id, string email)
        {
            Product item = Get(id);
            if (item == null || (item.Email != email && email != ""))
                throw new UnauthorizedAccessException("Unauthorized");

            if (item.CurrentSale != null)
                throw new InvalidOperationException("Cannot delete a Product during Active Sale");

            _collection.Remove(Query<Product>.EQ(p => p.ID, id));

            return item.Images;
        }


        public void ScheduleSale(string id, DateTime start_on, bool start_now, int duration, int items, string email)
        {
            Product item = Get(id);
            if (item == null || (item.Email != email && email != ""))
                throw new UnauthorizedAccessException("Unauthorized");

            if (!start_now)
            {
                _collection.Update(Query<Product>.EQ(p => p.ID, id),
                    Update<Product>.Set(p => p.Schedule, new Schedule { StartOn = start_on, Duration = duration, Items = items }));
            }
            else
            {
                _collection.Update(Query<Product>.EQ(p => p.ID, id),
                    Update<Product>.Set(p => p.CurrentSale, new Sale
                    {
                        ID = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                        Items = items,
                        CurrentPrice = item.MSRP,
                        Snaps = new List<Snap>(),
                        ProductID = item.ID,
                        StartedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time"))
                    }));
            }
        }
    }
}
