using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SMN.Services;
using SMN.Services.Tokens;
using SMN.Web.Models;

namespace SMN.Web.Controllers
{
    public class SalesController : Controller
    {
        private static object _locker = new object();
        private ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        // GET: Sales
        public ActionResult Index()
        {
            IEnumerable<ProductToken> sales = _salesService.GetActiveSales();

            return View(sales);
        }

        public ActionResult Details(string id)
        {
            string email = null;
            if (User.Identity.IsAuthenticated)
            {
                email = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
            }
            ProductToken product = _salesService.GetActiveSale(id, email);

            return View(new SaleDetailsModel
            {
                Product = product
            });
        }

        [Authorize]
        public ActionResult Snap(string id)
        {
            string email = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
            lock (_locker)
            {
                if (_salesService.SnapProduct(email, id))
                {
                    return Redirect(Url.RouteUrl("SaleDetails", new { id = id }) + "?snapped=true");
                }
            }

            return View("Details", new SaleDetailsModel
            {
                Product = _salesService.GetActiveSale(id, email)
            });
        }

        public JsonResult Price(string id)
        {
            ProductToken token = _salesService.GetActiveSale(id, null);
            if (token.CurrentSale != null)
            {
                return Json(new { price = token.CurrentSale.CurrentPrice, snaps = token.CurrentSale.Snaps }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { error = "Sale is not active" }, JsonRequestBehavior.AllowGet);
        }
    }
}