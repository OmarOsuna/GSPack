using NHibernate;
using NHibernate.Criterion;
using Practica.Nucleo.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Entidades
{
    public class Destinatario : Persistent
    {
        public override int Id { get; set; }
        public string Nombre { get; set; }
        public Domicilio Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }



        public static bool Guardar(int id, string nombre, string correo, string telefono, string calle, string numero, string avenida, string codigoPostal, string colonia, string estado, string ciudad, string referencia)
        {
            bool realizado = false;
            try
            {
                if (nombre.Equals("") || correo.Equals("") || telefono.Equals("") || calle.Equals("") || numero.Equals("") || codigoPostal.Equals("") || colonia.Equals("") || estado.Equals("") || ciudad.Equals("") || referencia.Equals("") )
                {
                    realizado = false;
                }
                else
                {
                    Destinatario destinatario = id == 0 ? new Destinatario() : ObtenerPorId(id);
                    destinatario.Nombre = nombre;
                    destinatario.Correo = correo;
                    destinatario.Telefono = telefono;

                    Domicilio domicilio = new Domicilio();
                    domicilio.Calle = calle;
                    domicilio.Numero = numero;
                    domicilio.Avenida = avenida;
                    domicilio.CodigoPostal = codigoPostal;
                    domicilio.Colonia = colonia;
                    domicilio.Ciudad = ciudad;
                    domicilio.Estado = estado;
                    domicilio.Referencia = referencia;


                    destinatario.Domicilio = domicilio;

                    if (id == 0)
                    {
                        destinatario.Save();
                    }
                    else
                    {
                        destinatario.Update();
                    }
                    realizado = true;
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
                Destinatario destinatario = new Destinatario();
                destinatario.Id = id;
                destinatario.Delete();
                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return realizado;
        }


        public static Destinatario ObtenerPorId(int id)
        {
            Destinatario u = new Destinatario();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(u.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    u = (crit.UniqueResult<Destinatario>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return u;
        }


        public static IList<DestinatarioDTO> ObtenerTodos()
        {
            IList<DestinatarioDTO> destinatarios = new List<DestinatarioDTO>();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(new Destinatario().GetType());

                    IList<Destinatario> dest = crit.List<Destinatario>();
                    foreach (Destinatario item in dest)
                    {
                        DestinatarioDTO destdto = new DestinatarioDTO();
                        destdto.Id = item.Id;
                        destdto.Nombre = item.Nombre;
                        destdto.Domicilio = "Col. " + item.Domicilio.Colonia + ", Calle " + item.Domicilio.Calle + " #" + item.Domicilio.Numero;
                        destdto.Telefono = item.Telefono;
                        destdto.Correo = item.Correo;                     
                        destinatarios.Add(destdto);
                    }
                    session.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return destinatarios;
        }


    }
}
