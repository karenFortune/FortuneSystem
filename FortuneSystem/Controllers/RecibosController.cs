using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FortuneSystem.Models;
using FortuneSystem.Models.Recibos;

namespace FortuneSystem.Controllers
{
    public class RecibosController : Controller
    {
        // GET: Recibos
        public ActionResult Index()
        {
            DatosRecibos r = new DatosRecibos();
            ViewData["lista_pos"]=r.Llenar_lista_autocompletado();
            return View();
        }

        public ActionResult Recibo_general()
        {
            /*string idpo = Request.Form["caja_po"].ToString();
            int total = Convert.ToInt32(Request.Form["caja_total_recibidas"]);
            DatosRecibos r = new DatosRecibos();
            r.Registrar_alta(idpo, total);    */

            string idpo = Request.Form["caja_po"].ToString();
            DatosRecibos r = new DatosRecibos();
            r.Buscar_orden_compra();
            return View("Index");
           
        }

       



    }
}