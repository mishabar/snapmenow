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

        SnapToken SnapProduct(string user, string id, out bool saleIsOver);

        bool LaunchSale(string id);

        IEnumerable<SnapToken> GetUserSnaps(string email);

        IEnumerable<SnapToken> GetSaleSnaps(string saleID);
    }
}
