using FortuneSystem.Models.Catalogos;
using FortuneSystem.Models.Pedidos;
using FortuneSystem.Models.POSummary;
using System;
using System.Collections.Generic;
using System.Data;
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
        public int estado;
        /* public ActionResult Index()
         {
             List<Pedido> listaPedidos = new List<Pedido>();
             listaPedidos = objPedido.ListaOrdenCompra().ToList();
             return View(listaPedidos);
         }*/



        [HttpGet]
        public ActionResult CrearPO()
        {
            OrdenesCompra pedido = new OrdenesCompra();
            POSummary summary = new POSummary();
            ListasClientes(pedido);
            ListaEstados(pedido);
            ListaGenero(summary);

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
               // ordenCompra.IdStatus = estado;
                objPedido.AgregarPO(ordenCompra);
                return RedirectToAction("CrearPO");
            }
            return View(ordenCompra);
        }

        public void ListasClientes(OrdenesCompra pedido)
        {
            List<CatCliente> listaClientes = pedido.ListaClientes;
            listaClientes = objCliente.ListaClientes().ToList();

            ViewBag.listCliente = new SelectList(listaClientes, "Customer", "Nombre", pedido.Cliente);

            List<CatClienteFinal> listaClientesFinal = pedido.ListaClientesFinal;
            listaClientesFinal = objClienteFinal.ListaClientesFinal().ToList();
            ViewBag.listClienteFinal = new SelectList(listaClientesFinal, "CustomerFinal", "Nombre", pedido.ClienteFinal);
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

            ViewBag.listGenero = new SelectList(listaGenero, "IdGender", "Genero", summary.IdGenero);

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
    }
}