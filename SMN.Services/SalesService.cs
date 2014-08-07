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
        private static Dictionary<string, object> _locks;

        static SalesService()
        {
            _locks = new Dictionary<string, object>();
        }

        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public IEnumerable<Tokens.ProductToken> GetActiveSales()
        {
            return _salesRepository.GetActive().Select(p => p.AsToken(null));
        }

        public ProductToken GetActiveSale(string id, string email)
        {
            return _salesRepository.GetActive(id).AsToken(email);
        }

        public bool SnapProduct(string user, string id)
        {
            bool result = false;
            Product product = _salesRepository.GetActive(id);
            if (product.CurrentSale == null)
                return result;

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
                result = _salesRepository.SnapProduct(user, product);
            }
            return result;
        }


        public bool LaunchSale(string id)
        {
            return _salesRepository.StartSale(id);
        }
    }
}
