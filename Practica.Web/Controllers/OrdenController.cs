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
    
    public class OrdenController : Controller
    {
        [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
        public ActionResult Index()
        {
            return View();
        }

        [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
        public ActionResult Add()
        {
            return PartialView("~/Views/Orden/Add.cshtml");
        }

        [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
        public ActionResult Edit()
        {
            return PartialView("~/Views/Orden/Edit.cshtml");
        }

        [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
        public ActionResult Vista()
        {
            return PartialView("~/Views/Orden/Vista.cshtml");
        }


        public ActionResult ListaStatus()
        {
            return PartialView("~/Views/Home/ListaStatus.cshtml");
        }

        public ActionResult Error()
        {
            return PartialView("~/Views/Orden/Error.cshtml");
        }

        public ActionResult Existe(string folio)
        {
            ActionResult action = null;
            try
            {
                if (Orden.Existe(folio))
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

        [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
        public ActionResult ObtenerTodosDTO()
        {
            try
            {
                IList<OrdenDTO> destinatarios = Orden.ObtenerTodosDTO();
                return Json(new { data = destinatarios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
        public ActionResult ObtenerTodos()
        {
            try
            {
                IList<Orden> ordenes = Orden.ObtenerTodos();
                return Json(new { data = ordenes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ObtenerStatus(int id)
        {
            try
            {
                IList<Estatu> status = Orden.ObtenerStatus(id);
                return Json(new { data = status }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
        public ActionResult ObtenerStatusDTO(int id)
        {
            try
            {
                IList<EstatuDTO> status = Orden.ObtenerStatusDTO(id);
                return Json(new { data = status }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ObtenerStatusDTOFolio(string folio)
        {
            try
            {
                IList<EstatuDTO> status = Orden.ObtenerStatusDTOFolio(folio);
                return Json(new { data = status }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [ValidarSession(Roles = new Perfil[] { Perfil.ADMINISTRADOR, Perfil.LECTURA })]
        public ActionResult ObtenerPorId(int id)
        {
            Orden orden = new Orden();
            try
            {
                orden = Orden.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return Json(orden, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ObtenerPorFolio(string folio)
        {
            Orden orden = new Orden();
            try
            {
                orden = Orden.ObtenerPorFolio(folio);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return Json(orden, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Guardar(int id, string nombreRemitente, string telefonoUnoRemitente, string telefonoDosRemitente, string telefonoTresRemitente, string correoRemitente, string rfcRemitente, string nombreDestinatario, string telefonoDestinatario, string correoDestinatario, string calleDomicilioD, string numeroDomicilioD, string avenidaDomicilioD, string cpDomicilioD, string coloniaDomicilioD, string estadoDomicilioD, string ciudadDomicilioD, string referenciaDomicilioD, string destinatarioDos, string tipoPaquete, string pesoPaquete, string tamanoPaquete, string descripcionPaquete, string fechaEntrega, string precioPaquete, int idRemitente, int idDestinatario)
        {
            ActionResult action = null;
            try
            {
                //int idEmpleado = Convert.ToInt32(Session["idEmpleado"].ToString());
                if (Orden.Guardar(id, nombreRemitente, telefonoUnoRemitente, telefonoDosRemitente, telefonoTresRemitente, correoRemitente, rfcRemitente, nombreDestinatario,
                    telefonoDestinatario, correoDestinatario, calleDomicilioD, avenidaDomicilioD, numeroDomicilioD, cpDomicilioD, coloniaDomicilioD, estadoDomicilioD,
                    ciudadDomicilioD, referenciaDomicilioD, tipoPaquete, pesoPaquete, tamanoPaquete, descripcionPaquete, fechaEntrega, precioPaquete,
                    destinatarioDos, 32, idRemitente, idDestinatario))
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
                if (Orden.Eliminar(id))
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

        public ActionResult ObtenerFolio()
        {
            ActionResult action = null;
            Orden.ObtenerFolio();
            return action;
        }

    }
}