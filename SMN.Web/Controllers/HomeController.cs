using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMN.Services;
using SMN.Web.Models;

namespace SMN.Web.Controllers
{
    public class HomeController : Controller
    {
        private IEmailService _emailService;

        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult HowItWorks()
        {
            return View();
        }

        public PartialViewResult FirstTime()
        {
            return PartialView("_FirstTime");
        }

        public ActionResult Retailer()
        {
            return View(new RetailerContactModel());
        }

        [HttpPost]
        public ActionResult Retailer(RetailerContactModel model)
        {
            if (ModelState.IsValid)
            {
                _emailService.Send("barmic@gmail.com", "Snap Me Now - Retailer Program Contact", string.Format("{0}<br/><br/>{1}", model.Name, model.Message));
                model.Status = "Sent";
            }
            return View(model);
        }
    }
}