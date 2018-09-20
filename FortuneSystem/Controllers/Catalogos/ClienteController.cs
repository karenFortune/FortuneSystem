﻿using FortuneSystem.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FortuneSystem.Controllers.Catalogos
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        CatClienteData objCliente = new CatClienteData();
        public ActionResult Index()
        {
            List<CatCliente> listaClientes = new List<CatCliente>();
            listaClientes = objCliente.ListaClientes().ToList();
            return View(listaClientes);
        }

        [HttpGet]
        public ActionResult CrearCliente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearCliente([Bind] CatCliente clientes)
        {
            if (ModelState.IsValid)
            {
                objCliente.AgregarClientes(clientes);
                TempData["clienteOK"] = "Se registro correctamente el cliente.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["clienteError"] = "No se pudo registrar el cliente, intentelo más tarde.";
            }
            return View(clientes);
        }

        [HttpGet]
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatCliente clientes = objCliente.ConsultarListaClientes(id);
            if (clientes == null)
            {
                return View();
            }
            return View(clientes);
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatCliente clientes = objCliente.ConsultarListaClientes(id);
            if (clientes == null)
            {
                return View();
            }

            return View(clientes);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, [Bind] CatCliente clientes)
        {
            if (id != clientes.Customer)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                objCliente.ActualizarCliente(clientes);
                TempData["clienteEditar"] = "Se modifico correctamente el cliente.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["clienteEditarError"] = "No se pudo modificar el cliente, intentelo más tarde.";
            }
            return View(clientes);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatCliente clientes = objCliente.ConsultarListaClientes(id);

            if (clientes == null)
            {
                return View();
            }
            return View(clientes);

        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfimacionEliminar(int? id)
        {
            objCliente.EliminarCliente(id);
            TempData["clienteEliminar"] = "Se elimino correctamente el cliente.";
            return RedirectToAction("Index");
        }



    }
}