using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace SMN.Data.Repositories
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private MongoCollection<Invoice> _collection;

        public InvoicesRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<Invoice>("invoices");
        }

        public Invoice GetOrCreate(string userSnapId, string productId)
        {
            Invoice invoice = _collection.FindOne(Query.And(Query<Invoice>.EQ(i => i.UserSnapID, userSnapId), Query<Invoice>.EQ(i => i.ProductID, productId)));
            if (invoice == null)
            {
                invoice = new Invoice { ProductID = productId, UserSnapID = userSnapId };
                _collection.Save(invoice);
            }

            return invoice;
        }

        public void Save(Invoice invoice)
        {
            _collection.Save(invoice);
        }
    }
}
