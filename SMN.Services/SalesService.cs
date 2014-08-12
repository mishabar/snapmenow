using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SMN.Data;
using SMN.Data.Repositories;
using SMN.Services.Tokens;

namespace SMN.Services
{
    public class SalesService : ISalesService
    {
        private ISalesRepository _salesRepository;
        private IUserSnapsRepository _userSnapsRepository;
        private static Dictionary<string, object> _locks;

        static SalesService()
        {
            _locks = new Dictionary<string, object>();
        }

        public SalesService(ISalesRepository salesRepository, IUserSnapsRepository userSnapsRepository)
        {
            _salesRepository = salesRepository;
            _userSnapsRepository = userSnapsRepository;
        }

        public IEnumerable<Tokens.ProductToken> GetActiveSales()
        {
            return _salesRepository.GetActive().Select(p => p.AsToken(null));
        }

        public ProductToken GetActiveSale(string id, string email)
        {
            return _salesRepository.GetActive(id).AsToken(email);
        }

        public SnapToken SnapProduct(string user, string id, out bool saleIsOver)
        {
            SnapToken token = null;
            saleIsOver = false;
            Product product = _salesRepository.GetActive(id);
            if (product.CurrentSale == null)
                return token;

            object obj = null;
            lock (_locks)
            {
                if (!_locks.TryGetValue(id, out obj))
                {
                    obj = new object();
                    _locks.Add(id, obj);
                }
            }
            lock (obj)
            {
                string currentSaleID = product.CurrentSale.ID;
                Snap snap = _salesRepository.SnapProduct(user, product);
                if (snap != null)
                {
                    saleIsOver = (product.CurrentSale == null);
                    UserSnap usnap = new UserSnap(snap, product.Name, currentSaleID, (saleIsOver ? "Awaiting Checkout" : "Snapped"));
                    _userSnapsRepository.Insert(usnap);
                    token = usnap.AsToken();
                    token.ProductName = product.Name;
                    token.SaleID = currentSaleID;
                }
            }
            return token;
        }


        public bool LaunchSale(string id)
        {
            return _salesRepository.StartSale(id);
        }


        public IEnumerable<SnapToken> GetUserSnaps(string email)
        {
            return _userSnapsRepository.GetUserSnaps(email).Select(s => s.AsToken());
        }


        public IEnumerable<SnapToken> GetSaleSnaps(string saleID)
        {
            return _userSnapsRepository.GetSaleSnaps(saleID).Select(s => s.AsToken());
        }
    }
}
