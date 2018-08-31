using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FortuneSystem.Models.Recibos;

namespace FortuneSystem.Controllers
{
    
    public class RecibosController : Controller
    {
        public int variable;
        // GET: Recibos
        public ActionResult Index()
        {
            return View();
        }

        
      
        public ActionResult Datos_PO()
        {
            variable = 98;
            ViewData["venta"] = "EXITO?";
            return View("Datos_PO");
        }

       

    }
}