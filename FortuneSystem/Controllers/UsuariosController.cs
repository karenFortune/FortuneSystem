﻿using FortuneSystem.Models.Roles;
using FortuneSystem.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FortuneSystem.Controllers
{
    public class UsuariosController : Controller
    {
        CatUsuarioData objCatUser= new CatUsuarioData();
        CatRolesData objCaRoles = new CatRolesData();
        // GET: Usuarios
        public ActionResult Index()
        {
          //  Roles
            List<CatUsuario> listaUsuarios = new List<CatUsuario>();
            listaUsuarios = objCatUser.ListaUsuarios().ToList();

            return View(listaUsuarios);
        }



        [HttpGet]
        public ActionResult CrearUsuario()
        {
            CatUsuario usuario = new CatUsuario();

            List<CatRoles> listaRoles = usuario.ListaRoles;
            listaRoles = objCaRoles.ListaRoles().ToList();
            usuario.Cargo = 1;
            ViewBag.listRoles = new SelectList(listaRoles, "Id", "rol", usuario.Cargo);
         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearUsuario([Bind] CatUsuario usuario)
        {
            if(ModelState.IsValid)
            {
   

                objCatUser.AgregarUsuarios(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return View();
            }

            CatUsuario usuario = objCatUser.ConsultarListaUsuarios(id);
            if (usuario == null)
            {
                return View();
            }
            return View(usuario);


        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return View();
            }

            CatUsuario usuario = objCatUser.ConsultarListaUsuarios(id);
            if(usuario == null)
            {
                return View();
            }

            return View(usuario);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind] CatUsuario usuarios)
        {
            if (id != usuarios.Id)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                objCatUser.ActualizarUsuarios(usuarios);
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return View();
            }

            CatUsuario usuarios = objCatUser.ConsultarListaUsuarios(id);

            if(usuarios == null)
            {
                return View();
            }
            return View(usuarios);

        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfimacionEliminar(int? id)
        {
            objCatUser.EliminarUsuario(id);
            return RedirectToAction("Index");
        }

    }
}