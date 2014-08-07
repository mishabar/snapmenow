using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMN.Services.Tokens;

namespace SMN.Services
{
    public interface ISalesService
    {
        IEnumerable<Tokens.ProductToken> GetActiveSales();

        ProductToken GetActiveSale(string id, string email);

        bool SnapProduct(string user, string id);
    }
}
