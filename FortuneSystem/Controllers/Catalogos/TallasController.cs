using FortuneSystem.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FortuneSystem.Controllers.Catalogos
{
    public class TallasController : Controller
    {
        // GET: Tallas
        CatTallaItemData objTalla = new CatTallaItemData();
        public ActionResult Index()
        {
            List<CatTallaItem> listaTalla = new List<CatTallaItem>();
            listaTalla = objTalla.ListaTallas().ToList();
            return View(listaTalla);
        }

        [HttpGet]
        public ActionResult CrearTalla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearTalla([Bind] CatTallaItem tallas)
        {
            if (ModelState.IsValid)
            {
                objTalla.AgregarTallas(tallas);
                return RedirectToAction("Index");
            }
            return View(tallas);
        }

        [HttpGet]
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatTallaItem tallas = objTalla.ConsultarListaTallas(id);
            if (tallas == null)
            {
                return View();
            }
            return View(tallas);
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatTallaItem tallas = objTalla.ConsultarListaTallas(id);
            if (tallas == null)
            {
                return View();
            }

            return View(tallas);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, [Bind] CatTallaItem tallas)
        {
            if (id != tallas.Id)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                objTalla.ActualizarTallas(tallas);
                return RedirectToAction("Index");
            }
            return View(tallas);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatTallaItem tallas = objTalla.ConsultarListaTallas(id);


            if (tallas == null)
            {
                return View();
            }
            return View(tallas);

        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfimacionEliminar(int? id)
        {
            objTalla.EliminarTallas(id);
            return RedirectToAction("Index");
        }



    }
}