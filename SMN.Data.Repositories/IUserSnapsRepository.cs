using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMN.Data.Repositories
{
    public interface IUserSnapsRepository
    {
        IEnumerable<UserSnap> GetUserSnaps(string email);

        void Insert(UserSnap userSnap);

        IEnumerable<UserSnap> GetSaleSnaps(string saleID);

        UserSnap Get(string userID, string id);
    }
}
