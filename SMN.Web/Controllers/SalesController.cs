using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SMN.Services;
using SMN.Services.Tokens;
using SMN.Web.Helpers;
using SMN.Web.Models;

namespace SMN.Web.Controllers
{
    public class SalesController : Controller
    {
        private static object _locker = new object();
        private ISalesService _salesService;
        private IEmailService _emailService;

        public SalesController(ISalesService salesService, IEmailService emailService)
        {
            _salesService = salesService;
            _emailService = emailService;
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
                SnapToken token = _salesService.SnapProduct(email, id);
                if (token != null)
                {
                    ViewRenderer renderer = new ViewRenderer();
                    string body = renderer.RenderViewToString("~/Views/Email/Snap.cshtml", 
                        new Dictionary<string, object> { { "Title", token.ProductName }, { "Price", token.Price }, { "ID", id } });
                    _emailService.Send(email, "You have just Snapped a " + token.ProductName, body);
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
                return Json(new { 
                    status = "active", 
                    price = string.Format("{0:c}", token.CurrentSale.CurrentPrice),
                    discount = Math.Round(token.CurrentSale.Discount, 2).ToString() + "%",
                    snaps = token.CurrentSale.Snaps }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = "inactive" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Prices()
        {
            IEnumerable<ProductToken> tokens = _salesService.GetActiveSales();
            return Json(tokens.Select(t => new { status = "active", id = t.ID, price = string.Format("{0:c}", t.CurrentSale.CurrentPrice), snaps = t.CurrentSale.Snaps }), JsonRequestBehavior.AllowGet);
        }
    }
}