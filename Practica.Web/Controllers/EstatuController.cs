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
    public class EstatuController : Controller
    {
        // GET: Estatu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return PartialView("~/Views/Estatu/Add.cshtml");
        }

        public ActionResult Edit()
        {
            return PartialView("~/Views/Estatu/Edit.cshtml");
        }

        public ActionResult Map()
        {
            return PartialView("~/Views/Estatu/Map.cshtml");
        }

        public ActionResult Guardar(int id, string descripcion, int tipoEstatu, string ciudad, string estado, string latitud, string longitud, int idOrden)
        {
            ActionResult action = null;
            try
            {
                if (Estatu.Guardar(id, descripcion, tipoEstatu, ciudad, estado, latitud, longitud, idOrden))
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
                if (Estatu.Eliminar(id))
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
            Estatu est = new Estatu();
            try
            {
                est = Estatu.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return Json(est, JsonRequestBehavior.AllowGet);
        }

    }
}