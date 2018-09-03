using FortuneSystem.Models.Catalogos;
using FortuneSystem.Models.Items;
using FortuneSystem.Models.POSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FortuneSystem.Controllers
{
    public class POSummaryController : Controller
    {
        DescripcionItemData objItems= new DescripcionItemData();
        CatColoresData objColores = new CatColoresData();
        CatGeneroData objGenero = new CatGeneroData();
        ItemDescripcionData objItemsDes = new ItemDescripcionData();
        // GET: POSummary
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CrearItems()
        {
            POSummary summary = new POSummary();
           // ListaGenero(summary);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearItems([Bind] POSummary descItem)
        {
            if (ModelState.IsValid)
            {
                objItems.AgregarItems(descItem);
                return RedirectToAction("CrearItems");
            }
            return View(descItem);
        }

        [HttpPost]
        public JsonResult Autocomplete_Item_Estilo(string keyword)
        {
            POSummary summary = new POSummary();
            List<ItemDescripcion> listaItems = summary.ListaItems;
            listaItems = objItemsDes.ListaItems().ToList();
            //Searching records from list using LINQ query  
            var ItemLista = (from N in listaItems
                             where N.ItemEstilo.StartsWith(keyword.ToUpper())
                             select new { N.ItemEstilo});
            return Json(ItemLista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Autocomplete_Item_Desc(string keyword, string estilo)
        {
            POSummary summary = new POSummary();
            List<ItemDescripcion> listaItems = summary.ListaItems;
            listaItems = objItemsDes.ListaItems().ToList();
            //Searching records from list using LINQ query  
            var ItemLista = (from N in listaItems
                             where N.ItemEstilo.StartsWith(keyword.ToUpper())
                             select new { N.ItemEstilo });
            return Json(ItemLista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Autocomplete_Color(string keyword)
        {
            POSummary summary = new POSummary();
            List<CatColores> listaColores = summary.ListaColores;
            listaColores = objColores.ListaColores().ToList();
            //Searching records from list using LINQ query  
            var Colores = (from N in listaColores
                           where N.CodigoColor.StartsWith(keyword.ToUpper())
                             select new { N.CodigoColor });
            return Json(Colores, JsonRequestBehavior.AllowGet);
        }

        private void Verificar_Item(string cadena)
        {
            string texto = VerificarItem(cadena);
          //  txtItemDes.Text = texto;
        }

        public string VerificarItem(string cadena)
        {
            string texto = objItems.Verificar_Item_CD(cadena);
            return texto;
        }




    }
}