using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMN.Data.Repositories;
using SMN.Services.Tokens;

namespace SMN.Services
{
    public class SalesService : ISalesService
    {
        private ISalesRepository _salesRepository;

        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public IEnumerable<Tokens.ProductToken> GetActiveSales()
        {
            return _salesRepository.GetActive().Select(p => p.AsToken());
        }

        public ProductToken GetActiveSale(string id)
        {
            return _salesRepository.GetActive(id).AsToken();
        }
    }
}
