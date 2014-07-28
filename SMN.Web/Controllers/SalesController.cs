using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMN.Services;
using SMN.Services.Tokens;

namespace SMN.Web.Controllers
{
    public class SalesController : Controller
    {
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
            return View(_salesService.GetActiveSale(id));
        }
    }
}