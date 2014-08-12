using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace SMN.Data.Repositories
{
    public class UserSnapsRepository : IUserSnapsRepository
    {
        private MongoCollection<UserSnap> _collection;

        public UserSnapsRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<UserSnap>("user_snaps");
        }

        public IEnumerable<UserSnap> GetUserSnaps(string email)
        {
            return _collection.Find(Query<UserSnap>.EQ(s => s.UserID, email)).OrderByDescending(s => s.SnappedAt);
        }

        public void Insert(UserSnap userSnap)
        {
            _collection.Save(userSnap);
        }

        public IEnumerable<UserSnap> GetSaleSnaps(string saleID)
        {
            return _collection.Find(Query<UserSnap>.EQ(s => s.SaleID, saleID));
        }
    }
}
