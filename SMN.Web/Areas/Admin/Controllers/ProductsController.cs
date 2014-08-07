using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SMN.Services;
using SMN.Services.Tokens;

namespace SMN.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator,Retailer")]
    public class ProductsController : Controller
    {
        private IProductsService _productsService;
        private ISalesService _salesService;

        public ProductsController(IProductsService productsService, ISalesService salesService)
        {
            _productsService = productsService;
            _salesService = salesService;
        }

        // GET: Admin/Products
        public ActionResult Index()
        {
            IEnumerable<ProductToken> products = _productsService.GetAllForRetailer((User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value);
            return View(products);
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(string sku)
        {
            return View();
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ProductToken token = new ProductToken { Email = User.Identity.Name };
            return View(token);
        }

        // POST: Admin/Products/Create
        [HttpPost]
        public ActionResult Create(ProductToken token)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (false && Request.Files.Count == 0)
                    {
                        ModelState.AddModelError("", "Please select at least one image");
                        return View(token);
                    }
                    _productsService.CreateProduct(token, Request.Files);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(token);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult LaunchSale(string id)
        {
            if (_salesService.LaunchSale(id)) 
            {
                return RedirectToAction("Index");
            }
            return Redirect(Url.Action("Index") + "?failed=true");
        }
    }
}
