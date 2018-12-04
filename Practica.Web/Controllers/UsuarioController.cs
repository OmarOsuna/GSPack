using Practica.Nucleo.Entidades;
using Practica.Nucleo.Enumeradores;
using Practica.Nucleo.Transporte;
using Practica.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica.Web.Controllers
{
    [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR })]
    public class UsuarioController : Controller
    {

        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult Add()
        {
            return PartialView("~/Views/Usuario/Add.cshtml");
        }

        public ActionResult Edit()
        {
            return PartialView("~/Views/Usuario/Edit.cshtml");
        }

        public ActionResult Pass()
        {
            return PartialView("~/Views/Usuario/Pass.cshtml");
        }



        public ActionResult Guardar(int id, string nombre, string correo, string cuenta, string password, int perfil)
        {
            ActionResult action = null;
            try
            {
                if (Usuario.Guardar(id, nombre, correo, cuenta, password, perfil))
                {
                    action = Content("true");
                }
                else
                {
                    action = Content("false");
                }
            }
            catch (Exception ex)
            {
                return Content("false");

            }
            return action;
        }
        
        public ActionResult Eliminar(int id)
        {
            ActionResult action = null;

            try
            {
                if (Usuario.Eliminar(id))
                {
                    action = Content("true");
                }
                else
                {
                    action = Content("false");
                }

            }
            catch (Exception ex)
            {
                return Content("false");

            }
            return action;
        }

        public ActionResult ObtenerPorId(int id)
        {
            Usuario u = new Usuario();
            try
            {
                u = Usuario.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return Json(u, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerTodos()
        {
            try
            {
                IList<UsuarioDTO> usuarios = Usuario.ObtenerTodos();
                return Json(new { data = usuarios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult CambiarPassword(int id, string password)
        {
            ActionResult action = null;
            try
            {
                if (Usuario.CambiarPassword(id, password))
                {
                    action = Content("true");
                }
                else
                {
                    action = Content("false");
                }
            }
            catch (Exception ex)
            {
                return Content("false");

            }
            return action;
        }


    }

    
}