using FortuneSystem.Models.Catalogos;
using FortuneSystem.Models.Item;
using FortuneSystem.Models.Pedidos;
using FortuneSystem.Models.POSummary;
using FortuneSystem.Models.Revisiones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FortuneSystem.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedido
        PedidosData objPedido = new PedidosData();
        CatClienteData objCliente = new CatClienteData();
        CatClienteFinalData objClienteFinal = new CatClienteFinalData();
        CatStatusData objEstados = new CatStatusData();
        CatGeneroData objGenero = new CatGeneroData();
        DescripcionItemData objItems = new DescripcionItemData();
        CatTelaData objTela = new CatTelaData();
        CatTipoCamisetaData objTipoC = new CatTipoCamisetaData();
        ItemTallaData objTallas = new ItemTallaData();
        RevisionesData objRevision = new RevisionesData();
        public int estado;
        public int IdPO;
        public int pedidos;
    
        public ActionResult Index()
         {
            List<OrdenesCompra> listaPedidos= new List<OrdenesCompra>();
             listaPedidos = objPedido.ListaOrdenCompra().ToList();
             return View(listaPedidos);
         }

      /*  [ChildActionOnly]
        public ActionResult StudentList()
        {
            lista = objPedido.ListaOrdenCompra().ToList();
            return PartialView(lista);
        }*/
        [HttpPost]
        public JsonResult Lista_Estilos_PO(int? id)
        {

            List<POSummary> listaItems = objItems.ListaItemsPorPO(id).ToList();
      
            var result = Json(new { listaItem = listaItems});
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult Listado_Estilos_PO()
        {
            int? id = Convert.ToInt32(Convert.ToInt32(Session["id_pedido"]));
         
            List<POSummary> listaItems = objItems.ListaItemsPorPO(id).ToList();

            return PartialView(listaItems);
        }

        [ChildActionOnly]
        public ActionResult Listado_Tallas_Estilo(int? id)
        {
            List<ItemTalla> listaTallas = objTallas.ListaTallasPorEstilo(id).ToList();

            return PartialView(listaTallas);
        }

        [HttpPost]
        public JsonResult Lista_Tallas_Estilo(int? id)
        {
            List<ItemTalla> listaTallas = objTallas.ListaTallasPorEstilo(id).ToList();
            string estilo = "";
            foreach (var item in listaTallas)
            {
                estilo = item.Estilo;

            }
            var result = Json(new { listaTalla = listaTallas, estilos = estilo });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CrearPO()
        {
            OrdenesCompra pedido = new OrdenesCompra();
            POSummary summary = new POSummary();
            ListasClientes(pedido);
            ListaEstados(pedido);
            ListaGenero(summary);
            ListaTela(summary);
            ListaTipoCamiseta(summary);
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPO([Bind] OrdenesCompra ordenCompra)
        {
            if (ModelState.IsValid)
            {
                ObtenerIdClientes(ordenCompra);
                ListaEstados(ordenCompra);
                //objPedido.AgregarPO(pedido);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return View();
            }

            OrdenesCompra pedido = objPedido.ConsultarListaPO(id);
            pedido.CatCliente = objCliente.ConsultarListaClientes(pedido.Cliente);
            pedido.CatClienteFinal = objClienteFinal.ConsultarListaClientesFinal(pedido.ClienteFinal);
            pedido.IdPedido =Convert.ToInt32(id);
            
            if (pedido == null)
            {
                return View();
            }
            return View(pedido);
        }

        [HttpGet]
        public ActionResult ObtenerPORevision(int? id)
        {
            OrdenesCompra pedido = objPedido.ConsultarListaPO(id);
            SeleccionarClientes(pedido);
            SeleccionarClienteFinal(pedido);
            string rev = pedido.PO + "-REV1";
            pedido.PO = rev.Replace(" ", "");
            pedido.IdPedido = Convert.ToInt32(id);
            Session["id_pedido"] = id;
            ObtenerEstadoRevisado(pedido);

            //objPedido.AgregarPO(pedido);
            int PedidosId = objPedido.Obtener_Utlimo_po();
            Session["idPedidoNuevo"] = PedidosId;
            Revisiones revisionPO = new Revisiones() {
                IdPedido = Convert.ToInt32(Session["id_pedido"]),
                IdRevisionPO = Convert.ToInt32(Session["idPedidoNuevo"]),
                FechaRevision = DateTime.Today,
                IdStatus = pedido.IdStatus

            };
            //objRevision.AgregarRevisionesPO(revisionPO);
           
           
         
            return View();
        }

        [HttpGet]
        public ActionResult Revision(int? id)
        {
            POSummary summary = new POSummary();
            ListaGenero(summary);
            ListaTela(summary);
            ListaTipoCamiseta(summary);
            if (id == null)
            {
                return View();
            }
            OrdenesCompra pedidos = new OrdenesCompra();
            ListasClientes(pedidos);
            

            if(id != null)
            {
                RegistrarRevisionPO(pedidos);
            }   


            if (pedidos == null)
            {
                return View();
            }

            return View(pedidos);

        }
        [HttpPost]
        public ActionResult RegistrarRevisionPO([Bind] OrdenesCompra pedido)
        {
            List<POSummary> listaItems = objItems.ListaItemsPorPO(pedido.IdPedido).ToList();


           // List<ItemTalla> listaTallas = objTallas.ListaTallasPorEstilo(id).ToList();
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Revision(int id, [Bind] OrdenesCompra pedido)
        {
            string cliente = Request.Form["Nombre"].ToString();
            pedido.Cliente = Int32.Parse(cliente);

            string clienteFinal = Request.Form["NombreCliente"].ToString();
            pedido.ClienteFinal = Int32.Parse(clienteFinal);
            /*if (id != pedido.IdPedido)
            {
                return View();
            }*/
            /* if (ModelState.IsValid)
             {
                 objPedido.ActualizarPedidos(pedido);
                 TempData["pedidoRevision"] = "Se registro correctamente la revisión de la orden de compra .";
                 return RedirectToAction("Index");
             }
             else
             {
                 TempData["pedidoRevisionError"] = "No se pudo registrar la revisión de la orden de compra, intentelo más tarde.";
             }*/
            return View(pedido);
        }

        [HttpGet]
        public ActionResult EditarEstilo(int? id)
        {
            if (id == null)
            {
                return View();
            }


            POSummary items = new POSummary();
            ListaGenero(items);
            ListaTela(items);
            ListaTipoCamiseta(items);


            if (items == null)
            {

                return View();
            }

            return PartialView(items);

        }

        [HttpPost]     
        public ActionResult RegistrarPO([Bind] OrdenesCompra ordenCompra,string po, int VPO, DateTime FechaCancel, DateTime FechaOrden, int Cliente, int Clientefinal, int TotalUnidades)
        {
            ListaEstados(ordenCompra);                          
           // objPedido.AgregarPO(ordenCompra);           
            
            return View(ordenCompra);
        }

        public void SeleccionarClientes(OrdenesCompra pedido)
        {
            List<CatCliente> listaClientes = pedido.LCliente;
            listaClientes = objCliente.ListaClientes().ToList();
            pedido.CatCliente = objCliente.ConsultarListaClientes(pedido.Cliente);
            pedido.CatCliente.Customer = pedido.Cliente;
            ViewBag.listCliente = new SelectList(listaClientes, "Customer", "Nombre", pedido.Cliente);


        }

        public void SeleccionarClienteFinal(OrdenesCompra pedido)
        {
        
            List<CatClienteFinal> listaClientesFinal = pedido.LClienteFinal;
            listaClientesFinal = objClienteFinal.ListaClientesFinal().ToList();
            pedido.CatClienteFinal = objClienteFinal.ConsultarListaClientesFinal(pedido.ClienteFinal);
            pedido.CatClienteFinal.CustomerFinal = pedido.ClienteFinal;
            ViewBag.listClienteFinal = new SelectList(listaClientesFinal, "CustomerFinal", "NombreCliente", pedido.ClienteFinal);

        }

        public void ListasClientes(OrdenesCompra pedido)
        {
            List<CatCliente> listaClientes = pedido.ListaClientes;
            listaClientes = objCliente.ListaClientes().ToList();

            ViewBag.listCliente = new SelectList(listaClientes, "Customer", "Nombre", pedido.Cliente);

            List<CatClienteFinal> listaClientesFinal = pedido.ListaClientesFinal;
            listaClientesFinal = objClienteFinal.ListaClientesFinal().ToList();
            ViewBag.listClienteFinal = new SelectList(listaClientesFinal, "CustomerFinal", "NombreCliente", pedido.ClienteFinal);
        }

        public void ObtenerIdClientes(OrdenesCompra pedido)
        {
            string cliente = Request.Form["listCliente"].ToString();
            pedido.Cliente = Int32.Parse(cliente);
            pedido.CatCliente = objCliente.ConsultarListaClientes(pedido.Cliente);


            string clienteFinal = Request.Form["listClienteFinal"].ToString();
            pedido.ClienteFinal = Int32.Parse(clienteFinal);
            pedido.CatClienteFinal = objClienteFinal.ConsultarListaClientesFinal(pedido.ClienteFinal);



        }

        public void ListaGenero(POSummary summary)
        {
            List<CatGenero> listaGenero = summary.ListaGeneros;
            listaGenero = objGenero.ListaGeneros().ToList();

            ViewBag.listGenero = new SelectList(listaGenero, "GeneroCode", "Genero", summary.IdGenero);

        }

        public void ListaTela(POSummary summary)
        {
            List<CatTela> listaTela = summary.ListaTelas;
            listaTela = objTela.ListaTela().ToList();

            ViewBag.listTela = new SelectList(listaTela, "Id_Tela", "Tela", summary.IdTela);

        }

        public void ListaTipoCamiseta(POSummary summary)
        {
            List<CatTipoCamiseta> listaTipoCamiseta = summary.ListaTipoCamiseta;
            listaTipoCamiseta = objTipoC.ListaTipoCamiseta().ToList();

            ViewBag.listTipoCamiseta = new SelectList(listaTipoCamiseta, "TipoProducto", "DescripcionTipo", summary.TipoCamiseta);

        }

        public void ListaEstados(OrdenesCompra pedido)
        {
            List<CatStatus> listaEstados = pedido.ListaCatStatus;
            listaEstados = objEstados.ListarEstados().ToList();

            ViewBag.listEstados = new SelectList(listaEstados, "IdStatus", "Estado", pedido.IdStatus);
            foreach (var item in listaEstados)
            {
                if(item.IdStatus == 1)
                {
                    pedido.IdStatus = item.IdStatus;
                }
               
            }

        }
        public void ObtenerEstadoRevisado(OrdenesCompra pedido)
        {
            List<CatStatus> listaEstados = pedido.ListaCatStatus;
            listaEstados = objEstados.ListarEstados().ToList();

            ViewBag.listEstados = new SelectList(listaEstados, "IdStatus", "Estado", pedido.IdStatus);
            foreach (var item in listaEstados)
            {
                if (item.IdStatus == 3)
                {
                    pedido.IdStatus = item.IdStatus;
                }

            }

        }


    }
}