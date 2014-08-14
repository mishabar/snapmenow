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
    [Authorize]
    public class CheckoutController : Controller
    {
        private ICheckoutService _checkoutService;
        private IProductsService _productsService;

        public CheckoutController(ICheckoutService checkoutService, IProductsService productsService)
        {
            _checkoutService = checkoutService;
            _productsService = productsService;
        }

        // GET: Checkout
        public ActionResult Details(string id)
        {
            return SnapDetails(id);
        }

        [HttpPost]
        public ActionResult Details(string id, int step, string action)
        {
            switch (step)
            {
                case 1: // move to address
                    return AddressDetails(id);
                case 2:
                    return (action == "prev") ? SnapDetails(id) : BillingDetails(id);
                case 3:
                    return (action == "prev") ? AddressDetails(id) : ReviewDetails(id);
                case 4:
                    return (action == "prev") ? BillingDetails(id) : TrySubmitOrder(id);
            }
            return SnapDetails(id);
        }

        private ActionResult TrySubmitOrder(string id)
        {
            string email = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
            SnapToken snap = _checkoutService.GetSnap(email, id);
            ProductToken product = _productsService.Get(snap.ProductID);

            return View("Details", new CheckoutModel { Step = 1, UserSnap = snap, Product = product });
        }

        private ActionResult ReviewDetails(string id)
        {
            string email = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
            SnapToken snap = _checkoutService.GetSnap(email, id);
            ProductToken product = _productsService.Get(snap.ProductID);

            return View("ReviewDetails", new ReviewModel { Step = 4, ID = id, UserSnap = snap, Product = product });
        }

        private ActionResult SnapDetails(string id)
        {
            string email = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
            SnapToken snap = _checkoutService.GetSnap(email, id);
            ProductToken product = _productsService.Get(snap.ProductID);

            return View("Details", new CheckoutModel { Step = 1, UserSnap = snap, Product = product });
        }

        private ActionResult BillingDetails(string id)
        {
            string email = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
            SnapToken snap = _checkoutService.GetSnap(email, id);

            return View("BillingDetails", new BillingModel { Step = 3, ID = id /*Billing = snap.Billing*/ });
        }

        private ActionResult AddressDetails(string id)
        {
            string email = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
            SnapToken snap = _checkoutService.GetSnap(email, id);

            return View("AddressDetails", new AddressModel { Step = 2, ID = id, Address = snap.Address });
        }
    }
}