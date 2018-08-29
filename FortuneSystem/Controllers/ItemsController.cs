using FortuneSystem.Models.DescripcionItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FortuneSystem.Controllers
{
    public class ItemsController : Controller
    {
        DescripcionItemData objItems = new DescripcionItemData();
        // GET: Items
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CrearItems()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearItems([Bind] DescripcionItem descItem)
        {
            if (ModelState.IsValid)
            {
                objItems.AgregarItems(descItem);
                return RedirectToAction("CrearItems");
            }
            return View(descItem);
        }
    }
}