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
    [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
    public class DestinatarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return PartialView("~/Views/Destinatario/Add.cshtml");
        }

        public ActionResult Edit()
        {
            return PartialView("~/Views/Destinatario/Edit.cshtml");
        }



        public ActionResult Guardar(int id, string nombre, string correo, string telefono, string calle, string numero, string avenida, string codigoPostal, string colonia, string estado, string ciudad, string referencia)
        {
            ActionResult action = null;
            try
            {
                if (Destinatario.Guardar(id, nombre, correo, telefono, calle, numero, avenida, codigoPostal, colonia, estado, ciudad, referencia))
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
                if (Destinatario.Eliminar(id))
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


        public ActionResult ObtenerTodos()
        {
            try
            {
                IList<DestinatarioDTO> destinatarios = Destinatario.ObtenerTodos();
                return Json(new { data = destinatarios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        public ActionResult ObtenerPorId(int id)
        {
            Destinatario dtr = new Destinatario();
            try
            {
                dtr = Destinatario.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return Json(dtr, JsonRequestBehavior.AllowGet);
        }




    }
}