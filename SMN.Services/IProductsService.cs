using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SMN.Services
{
    public interface IProductsService
    {
        IEnumerable<Tokens.ProductToken> GetAllForRetailer(string retailerEmail);

        string Create(Tokens.ProductToken token);

        Tokens.ProductToken Get(string id);

        void Update(Tokens.ProductToken token, string email);

        void Delete(string id, string email);

        void UpdateImage(string id, HttpPostedFileBase image, int i);

        void ScheduleSale(string id, DateTime start_on, bool start_now, int duration, int items, string email);
    }
}
