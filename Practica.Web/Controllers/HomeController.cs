using Practica.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaStatus()
        {
            return PartialView("~/Views/Home/ListaStatus.cshtml");
        }

        public ActionResult Map()
        {
            return PartialView("~/Views/Home/Map.cshtml");
        }



        public ActionResult Login()
        {
            try
            {
                if (Session != null)
                {
                    Session.Clear();
                    Session.Abandon();
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Response.Cookies["ckSolicitud"].Expires = DateTime.Now.AddDays(-1);
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
                          
        [HttpPost]
        public ActionResult Login(FormCollection model)
        {
            ActionResult action = null;
            try
            {
                string cuenta = model["cuenta"].Trim();
                string password = model["password"].Trim();
                Usuario u = Usuario.ObtenerPorLogin(cuenta, password);
                if (u != null)
                {

                    HttpCookie ck = new HttpCookie("ckSolicitud");
                    ck.Values.Add("correoUsuario", u.Correo);
                    ck.Values.Add("nombreUsuario", u.Nombre);
                    ck.Values.Add("perfilUsuario", ((int)u.Perfil).ToString());
                    ck.Expires = DateTime.Now.AddHours(8);
                    Response.Cookies.Add(ck);



                    Session["usuarioId"] = u.Id;
                    Session["usuarioCuenta"] = u.Cuenta;
                    Session["usuarioCorreo"] = u.Correo;
                    Session["usuarioNombre"] = u.Nombre;
                    Session["usuarioRol"] = u.Perfil;

                    action = RedirectToAction("Index", "Orden");
                }
                else
                {
                    ViewBag.Message = "Los datos de usuario ingresado son incorrectos";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return action;
        }


    }
}