using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace SMN.Data.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private MongoCollection<Product> _productsCollection;
        private MongoCollection<Sale> _salesCollection;
        private MongoCollection<UserSnap> _userSnapsCollection;

        public SalesRepository(MongoDatabase db)
        {
            _productsCollection = db.GetCollection<Product>("products");
            _salesCollection = db.GetCollection<Sale>("sales");
            _userSnapsCollection = db.GetCollection<UserSnap>("user_snaps");
        }

        public IEnumerable<Product> GetActive()
        {
            return _productsCollection.Find(Query<Product>.NE(p => p.CurrentSale, null));
        }

        public Product GetActive(string id)
        {
            return _productsCollection.FindOne(Query<Product>.EQ(p => p.ID, id));
        }


        public Snap SnapProduct(string user, Product product)
        {
            Snap snap = null;
            if (product.ItemsAvailable > 0 && product.CurrentSale != null)
            {
                int currentPrice = product.CurrentSale.CurrentPrice;
                product.ItemsAvailable--;
                snap = new Snap { SnappedAt = DateTime.Now, Price = product.CurrentSale.CurrentPrice, ProductID = product.ID, UserID = user, FinalPrice = 0 };
                product.CurrentSale.Snaps.Add(snap);
                product.CurrentSale.CurrentPrice -= product.SnapPrice;
                _productsCollection.Save(product);

                if (product.ItemsAvailable == 0 || product.CurrentSale.CurrentPrice <= product.MinPrice)
                {
                    // End sale
                    snap.FinalPrice = currentPrice;
                    product.CurrentSale.EndedAt = DateTime.Now;
                    _salesCollection.Insert(product.CurrentSale);
                    _userSnapsCollection.Update(Query<UserSnap>.EQ(s => s.SaleID, product.CurrentSale.ID), 
                        Update<UserSnap>.Set(s => s.FinalPrice, currentPrice).Set(s => s.Status, "Awaiting Checkout"), new MongoUpdateOptions { Flags = UpdateFlags.Multi });
                    product.CurrentSale = null;
                    _productsCollection.Save(product);
                }

                return snap;
            }

            return snap;
        }


        public bool StartSale(string productId)
        {
            Product product = _productsCollection.FindOne(Query<Product>.EQ(p => p.ID, productId));
            if (product != null && product.ItemsAvailable > 0 && product.CurrentSale == null)
            {
                Sale sale = new Sale
                {
                    ProductID = productId,
                    CurrentPrice = (int)(product.MSRP * 0.9),
                    StartedAt = DateTime.Now,
                    ID = ObjectId.GenerateNewId().ToString()
                };
                product.CurrentSale = sale;
                _productsCollection.Save(product);

                return true;
            }
            return false;
        }


        public Sale GetSale(string saleID)
        {
            return _salesCollection.FindOne(Query<Sale>.EQ(s => s.ID, saleID));
        }


        public IEnumerable<Product> GetUpcoming()
        {
            return _productsCollection.Find(Query<Product>.NE(p => p.Schedule, null));
        }
    }
}
