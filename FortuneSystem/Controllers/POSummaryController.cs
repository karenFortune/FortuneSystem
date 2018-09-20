﻿using FortuneSystem.Models.Catalogos;
using FortuneSystem.Models.Item;
using FortuneSystem.Models.Items;
using FortuneSystem.Models.Pedidos;
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
        PedidosData objPedido = new PedidosData();
        ItemTallaData objTalla = new ItemTallaData();
        CatTallaItemData objTallas = new CatTallaItemData();

        public int IdPedido;
        // GET: POSummary
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CrearItems()
        {

            // ListaGenero(summary);
            PedidosController pedidos = new PedidosController();
            //pedidos.ObtenerListas();
            string genero = "";
            ListarTallasPorGenero(genero);
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


        [HttpGet]
        public ActionResult RegistrarItem([Bind] POSummary descItem, string EstiloItem, string IdColor, int Cantidad, float Precio, string IdGenero, int IdTela, string TipoCamiseta)
        {
            int PedidosId = objPedido.Obtener_Utlimo_po();
            descItem.PedidosId = PedidosId;                           
           // objItems.AgregarItems(descItem);           

            return View(descItem);
        }

        public ActionResult CreateColor(POSummary descItem)
        {
            if (ModelState.IsValid)
            {
                string colorEstilo = descItem.CatColores.CodigoColor;
                string  colorDesc = descItem.CatColores.DescripcionColor;
                return View();
            }
            else
                return View("Index");
        }

        [HttpPost]
        public JsonResult Autocomplete_Item_Estilo(string keyword)
        {
            POSummary summary = new POSummary();
            List<ItemDescripcion> listaItems = summary.ListaItems;
            listaItems = objItemsDes.ListaItems().ToList();
            var ItemLista = (from N in listaItems
                             where N.ItemEstilo.StartsWith(keyword.ToUpper())
                             select new { Estilo = N.ItemEstilo, Descr = N.Descripcion, Id=N.ItemId});
            return Json(ItemLista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Obtener_Lista_Tallas(List<string> ListTalla)
        {
            ItemTalla tallaItem = new ItemTalla();
            List<string> tallas = ListTalla[0].Split('*').ToList();
            List<string> cantidad = ListTalla[1].Split('*').ToList();
            List<string> extras = ListTalla[2].Split('*').ToList();
            List<string> ejemplos = ListTalla[3].Split('*').ToList();
            int i = 0;
            foreach (var item in tallas)
            {
                i++; 
            }

            i = i - 1;
           for(int v = 0; v < i; v++)
            {
                tallaItem.Talla = tallas[v];

                string cantidadT = cantidad[v];
                if (cantidadT == "")
                {
                    cantidadT = "0";
                }
                tallaItem.Cantidad = Int32.Parse(cantidadT);

                string extraT = extras[v];
                if(extraT == "")
                {
                    extraT = "0";
                }
                tallaItem.Extras = Int32.Parse(extraT);

                string ejemploT = ejemplos[v];
                if (ejemploT == "")
                {
                    ejemploT = "0";
                }
                tallaItem.Ejemplos= Int32.Parse(ejemploT);

                tallaItem.IdSummary = objItems.Obtener_Utlimo_Item();

                //objTalla.RegistroTallas(tallaItem);
        
                
            }
            return Json("0", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Autocomplete_Item_Desc(string keyword, string estilo)
        {
            POSummary summary = new POSummary();
            List<ItemDescripcion> listaItems = summary.ListaItems;
            listaItems = objItemsDes.ListaItems().ToList();
            var ItemLista = (from N in listaItems
                             where N.ItemEstilo.StartsWith(keyword.ToUpper())
                             select new {
                                 label = N.ItemEstilo,
                                 val = N.ItemEstilo,
                                 descripcion = N.Descripcion,
                                 id= N.ItemId
                             });
            return Json(ItemLista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Autocomplete_Color(string keyword)
        {
            POSummary summary = new POSummary();
            List<CatColores> listaColores = summary.ListaColores;
            listaColores = objColores.ListaColores().ToList();
            var Colores = (from N in listaColores
                           where N.CodigoColor.StartsWith(keyword.ToUpper())
                             select new { N.CodigoColor, Color=N.DescripcionColor, Id=N.IdColor});
            return Json(Colores, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Autocomplete_Talla(string keyword)
        {
            POSummary summary = new POSummary();
            List<CatTallaItem> listTallas = summary.ListaTallas;
            listTallas = objTallas.ListaTallas().ToList();
            var TallaLista = (from N in listTallas
                             where N.Talla.StartsWith(keyword.ToUpper())
                             select new { N.Talla});
            return Json(TallaLista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ListarTallasPorGenero(string Genero)
        {
                POSummary summary = new POSummary();
                List<CatGenero> listaGenero = summary.ListarTallasPorGenero;
                listaGenero = objGenero.ListarTallasPorGenero(Genero).ToList();
                summary.ListarTallasPorGenero = listaGenero;

                return View(summary);       
                        
        }

        public JsonResult List(string Genero)
        {
            POSummary summary = new POSummary();
            List<CatGenero> listaGenero = summary.ListarTallasPorGenero;
            listaGenero = objGenero.ListarTallasPorGenero(Genero).ToList();
            summary.ListarTallasPorGenero = listaGenero;
            return Json(listaGenero, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public ActionResult ListarTallasPorGenero(POSummary descItem)
        {
            return View();
        }




    }
}