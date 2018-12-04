using Practica.Nucleo.Entidades;
using Practica.Nucleo.Enumeradores;
using Practica.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica.Web.Controllers
{
    [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
    public class ClienteController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return PartialView("~/Views/Cliente/Add.cshtml");
        }

        public ActionResult Edit()
        {
            return PartialView("~/Views/Cliente/Edit.cshtml");
        }



        public ActionResult Guardar(int id, string nombre, string telefonoUno, string telefonoDos, string telefonoTres, string correo, string rfc)
        {
            ActionResult action = null;
            try
            {
                if (Cliente.Guardar(id, nombre, telefonoUno, telefonoDos, telefonoTres, correo, rfc))
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
                if (Cliente.Eliminar(id))
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
            Cliente u = new Cliente();
            try
            {
                u = Cliente.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return Json(u, JsonRequestBehavior.AllowGet);
        }

        [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR })]
        public ActionResult ObtenerTodos()
        {
            try
            {
                IList<Cliente> clientes = Cliente.ObtenerTodos();
                return Json(new { data = clientes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

    }
}