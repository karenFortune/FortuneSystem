using FortuneSystem.Models.Pedido;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FortuneSystem.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        PedidoData objPedido = new PedidoData();
        /* public ActionResult Index()
         {
             List<Pedido> listaPedidos = new List<Pedido>();
             listaPedidos = objPedido.ListaOrdenCompra().ToList();
             return View(listaPedidos);
         }*/
     

        [HttpGet]
        public ActionResult CrearPO()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPO([Bind] Pedido ordenCompra)
        {
            if (ModelState.IsValid)
            {
                objPedido.AgregarPO(ordenCompra);
                return RedirectToAction("CrearPO");
            }
            return View(ordenCompra);
        }
    }
}