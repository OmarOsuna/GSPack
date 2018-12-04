using Practica.Nucleo.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica.Web.Attribute
{
    public class ValidarSession: AuthorizeAttribute
    {
        public Perfil[] Roles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if (HttpContext.Current.Request.Cookies["ckSolicitud"] != null)
            {
                string roles = HttpContext.Current.Request.Cookies["ckSolicitud"].Values["perfilUsuario"].ToString();
                if (Roles != null)
                {
                    foreach (Perfil p in Roles)
                    {
                        if ((int)p == Int32.Parse(roles))
                        {
                            authorize = true;
                            break;
                        }
                    }
                }
            }
            return authorize;
        }

    }
}