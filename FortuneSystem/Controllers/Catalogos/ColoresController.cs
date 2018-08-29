using FortuneSystem.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FortuneSystem.Controllers.Catalogos
{
    public class ColoresController : Controller
    {
        CatColoresData objColores = new CatColoresData();
        // GET: Colores
        public ActionResult Index()
        {
            List<CatColores> listaColores = new List<CatColores>();
            listaColores = objColores.ListaColores().ToList();
            return View(listaColores);
        }

        [HttpGet]
        public ActionResult CrearColor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearColor([Bind] CatColores colores)
        {
            if (ModelState.IsValid)
            {
                objColores.AgregarColores(colores);
                return RedirectToAction("Index");
            }
            return View(colores);
        }

        [HttpGet]
        public ActionResult Detalles(int? id)
        {
            if(id == null)
            {
                return View();
            }

            CatColores colores = objColores.ConsultarListaColores(id);
            if(colores == null)
            {
                return View();
            }
            return View(colores);
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatColores colores = objColores.ConsultarListaColores(id);
            if (colores == null)
            {
                return View();
            }

            return View(colores);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, [Bind] CatColores colores)
        {
            if (id != colores.IdColor)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                objColores.ActualizarColores(colores);
                return RedirectToAction("Index");
            }
            return View(colores);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatColores colores = objColores.ConsultarListaColores(id);

            if (colores == null)
            {
                return View();
            }
            return View(colores);

        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfimacionEliminar(int? id)
        {
            objColores.EliminarColores(id);
            return RedirectToAction("Index");
        }



    }
}