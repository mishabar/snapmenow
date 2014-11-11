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
        private IProductsService _productService;

        public SalesController(ISalesService salesService, IEmailService emailService, IProductsService productService)
        {
            _salesService = salesService;
            _emailService = emailService;
            _productService = productService;
        }

        // GET: Sales
        public ActionResult Index()
        {
            IEnumerable<ProductToken> sales = _salesService.GetActiveSales();

            return View(sales);
        }

        // GET: Soon
        public ActionResult Soon()
        {
            IEnumerable<ProductToken> sales = _salesService.GetUpcomingSales();

            return View(sales);
        }

        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
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
                bool saleIsOver = false;
                SnapToken token = _salesService.SnapProduct(email, id, out saleIsOver);
                if (token != null)
                {
                    ProductToken product = _productService.Get(id);
                    ViewRenderer renderer = new ViewRenderer();
                    string body = renderer.RenderViewToString("~/Views/Email/Snap.cshtml",
                        new Dictionary<string, object> { 
                        { "Title", token.ProductName }, 
                        { "Price", token.Price }, 
                        { "ID", id },
                        { "EndsAt", product.CurrentSale == null ? new DateTime?() : new DateTime?(product.CurrentSale.EndsAt) },
                        { "Image", product.Images[0] },
                        { "MSRP", product.MSRP },
                        { "Discount", (1M - (decimal)token.Price / (decimal)product.MSRP) * 100M }});
                    _emailService.Send(email, "You have just Snapped a " + token.ProductName, body);

                    if (saleIsOver)
                    {
                        body = null;
                        foreach (var snap in _salesService.GetSaleSnaps(token.SaleID))
                        {
                            if (body == null)
                            {
                                body = renderer.RenderViewToString("~/Views/Email/SaleEnded.cshtml",
                                    new Dictionary<string, object> { 
                                    { "Title", token.ProductName }, 
                                    { "Price", snap.FinalPrice }, 
                                    { "ID", snap.ID } ,
                                    { "Image", product.Images[0] },
                                    { "MSRP", product.MSRP },
                                    { "Discount", (1M - (decimal)token.Price / (decimal)product.MSRP) * 100M }});
                            }
                            _emailService.Send(email, token.ProductName + " sale has just ended", body);
                        }
                    }
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
                return Json(new
                {
                    status = "active",
                    price = string.Format("{0:c}", token.CurrentSale.CurrentPrice),
                    discount = Math.Round(token.CurrentSale.Discount, 2).ToString() + "%",
                    snaps = token.CurrentSale.Snaps
                }, JsonRequestBehavior.AllowGet);
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