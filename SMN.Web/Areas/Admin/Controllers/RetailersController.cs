using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMN.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="Administrator")]
    public class RetailersController : Controller
    {
        // GET: Admin/Retailers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Retailers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Retailers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Retailers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Retailers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Retailers/Edit/5
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

        // GET: Admin/Retailers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Retailers/Delete/5
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
    }
}
