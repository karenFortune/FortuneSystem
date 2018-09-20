﻿using FortuneSystem.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FortuneSystem.Controllers.Catalogos
{
    public class ClienteFinalController : Controller
    {
        // GET: ClienteFinal
        CatClienteFinalData objClienteFinal = new CatClienteFinalData();

        public ActionResult Index()
        {
            List<CatClienteFinal> listaClientesFinal = new List<CatClienteFinal>();
            listaClientesFinal = objClienteFinal.ListaClientesFinal().ToList();
            return View(listaClientesFinal);
        }

        [HttpGet]
        public ActionResult CrearClienteFinal()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearClienteFinal([Bind] CatClienteFinal clientesFinal)
        {
            if (ModelState.IsValid)
            {
                objClienteFinal.AgregarClientesFinal(clientesFinal);
                TempData["clienteOK"] = "Se registro correctamente el cliente final.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["clienteError"] = "No se pudo registrar el cliente final, intentelo más tarde.";
            }
            return View(clientesFinal);
        }

        [HttpGet]
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatClienteFinal clientesFinal = objClienteFinal.ConsultarListaClientesFinal(id);
            if (clientesFinal == null)
            {
                return View();
            }
            return View(clientesFinal);
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatClienteFinal clientes = objClienteFinal.ConsultarListaClientesFinal(id);
            if (clientes == null)
            {
                return View();
            }

            return View(clientes);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, [Bind] CatClienteFinal clientes)
        {
            if (id != clientes.CustomerFinal)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                objClienteFinal.ActualizarClienteFinal(clientes);
                TempData["clienteEditar"] = "Se modifico correctamente el cliente final.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["clienteEditarError"] = "No se pudo modificar el cliente final, intentelo más tarde.";
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

            CatClienteFinal clientes = objClienteFinal.ConsultarListaClientesFinal(id);

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
            objClienteFinal.EliminarClienteFinal(id);
            TempData["clienteEliminar"] = "Se elimino correctamente el cliente Final.";
            return RedirectToAction("Index");
        }



    }
}