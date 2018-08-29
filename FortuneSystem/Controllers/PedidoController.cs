using FortuneSystem.Models.Pedidos;
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
        PedidosData objPedido = new PedidosData();
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
        public ActionResult CrearPO([Bind] Pedidos ordenCompra)
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