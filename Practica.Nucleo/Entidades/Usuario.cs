using NHibernate;
using NHibernate.Criterion;
using Practica.Nucleo.Enumeradores;
using Practica.Nucleo.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Entidades
{
    public class Usuario : Persistent
    {
        public override int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Cuenta { get; set; }
        public string Password { get; set; }
        public Perfil Perfil { get; set; }


        public static bool Guardar(int id, string nombre, string correo, string cuenta, string password, int perfil)
        {
            bool realizado = false;
            try
            {

                if (nombre != "" || correo != "" || cuenta != "" || password != "" || perfil != 0)
                {
                    Usuario u = new Usuario();
                    Usuario user = id == 0 ? new Usuario() : ObtenerPorId(id);
                    user.Nombre = nombre;
                    user.Correo = correo;
                    user.Cuenta = cuenta;
                    user.Perfil = (Perfil)perfil;

                    if (id == 0)
                    {
                        using (ISession session = Persistent.SessionFactory.OpenSession())
                        {

                            ICriteria crit = session.CreateCriteria(u.GetType());
                            crit.Add(Expression.Eq("Correo", correo));
                            u = (crit.UniqueResult<Usuario>());
                            if (u == null)
                            {
                                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                                {
                                    UTF8Encoding utf8 = new UTF8Encoding();
                                    byte[] data = md5.ComputeHash(utf8.GetBytes(password));
                                    user.Password = Convert.ToBase64String(data);
                                    user.Save();
                                    realizado = true;

                                }

                            }
                            else
                            {
                                realizado = false;
                            }

                        }

                    }
                    else
                    {
                        using (ISession session = Persistent.SessionFactory.OpenSession())
                        {

                            ICriteria crit = session.CreateCriteria(u.GetType());
                            crit.Add(Expression.Eq("Correo", correo));
                            u = (crit.UniqueResult<Usuario>());

                            if (u == null)
                            {
                                user.Update();
                                realizado = true;
                            }
                            else
                            {
                                if (u.Id == id)
                                {
                                    user.Update();
                                    realizado = true;

                                }
                                else
                                {
                                    realizado = false;
                                }
                            }

                        }


                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return realizado;
        }

        public static bool Eliminar(int id)
        {
            bool realizado = false;
            try
            {
                Usuario u = new Usuario();
                u.Id = id;
                u.Delete();
                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return realizado;
        }

        public static bool CambiarPassword(int id, string password)
        {
            bool realizado = false;
            try
            {
                Usuario u = id == 0 ? new Usuario() : ObtenerPorId(id);

                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    UTF8Encoding utf8 = new UTF8Encoding();
                    byte[] data = md5.ComputeHash(utf8.GetBytes(password));
                    u.Password = Convert.ToBase64String(data);
                    u.Update();
                    realizado = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return realizado;
        }

        public static IList<UsuarioDTO> ObtenerTodos()
        {
            IList<UsuarioDTO> usuarios = new List<UsuarioDTO>();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(new Usuario().GetType());

                    IList<Usuario> us = crit.List<Usuario>();
                    foreach (Usuario item in us)
                    {
                        UsuarioDTO usdto = new UsuarioDTO();
                        usdto.Id = item.Id;
                        usdto.Nombre = item.Nombre;
                        usdto.Correo = item.Correo;
                        usdto.Cuenta = item.Cuenta;

                        if (Convert.ToString(item.Perfil).Equals("ADMINISTRADOR"))
                        {
                            usdto.Perfil = "<span class='badge bg-blue'>Administrador</span>";

                        }
                        else
                        {
                            usdto.Perfil = "<span class='badge bg-green'>Lectura</span>";
                        }

                        usuarios.Add(usdto);
                    }
                    session.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usuarios;
        }

        public static Usuario ObtenerPorId(int id)
        {
            Usuario u = new Usuario();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(u.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    u = (crit.UniqueResult<Usuario>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return u;
        }

        public static Usuario ObtenerPorLogin(string cuenta, string password)
        {
            Usuario user = new Usuario();
            string hashPassword;
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(password));
                hashPassword = Convert.ToBase64String(data);
            }
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(user.GetType());
                    crit.Add(Expression.Eq("Correo", cuenta));
                    crit.Add(Expression.Eq("Password", hashPassword));
                    user = (crit.UniqueResult<Usuario>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

    }
}

