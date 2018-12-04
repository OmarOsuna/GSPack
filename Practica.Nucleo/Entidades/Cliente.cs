using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Entidades
{
    public class Cliente : Persistent
    {
        public override int Id { get; set; }
        public string Nombre { get; set; }
        public string TelefonoUno { get; set; }
        public string TelefonoDos { get; set; }
        public string TelefonoTres { get; set; }
        public string Correo { get; set; }
        public string RFC { get; set; }


        //Guardar Y Editar
        public static bool Guardar(int id, string nombre, string telefonoUno, string telefonoDos, string telefonoTres, string correo, string rfc)
        {
            bool realizado = false;
            try
            {
                if (id.Equals("") || nombre.Equals("") || correo.Equals("") || telefonoUno.Equals("") || rfc.Equals(""))
                {
                    realizado = false;
                }
                else
                {
                    Cliente cliente = id == 0 ? new Cliente() : ObtenerPorId(id);
                    cliente.Nombre = nombre;
                    cliente.TelefonoUno = telefonoUno;
                    cliente.TelefonoDos = telefonoDos;
                    cliente.TelefonoTres = telefonoTres;
                    cliente.Correo = correo;
                    cliente.RFC = rfc;
                    if (id == 0)
                    {
                        cliente.Save();
                    }
                    else
                    {
                        cliente.Update();
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

        //Elimar
        public static bool Eliminar(int id)
        {
            bool realizado = false;
            try
            {
                Cliente cliente = new Cliente();
                cliente.Id = id;
                cliente.Delete();
                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return realizado;
        }

        //Obtener Todos los Clintes 
        public static IList<Cliente> ObtenerTodos()
        {
            IList<Cliente> clientes = new List<Cliente>();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(new Cliente().GetType());
                    clientes = crit.List<Cliente>();
                    session.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clientes;
        }
           
        //Busca por ID un cliente.
        public static Cliente ObtenerPorId(int id)
        {
            Cliente cl = new Cliente();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(cl.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    cl = (crit.UniqueResult<Cliente>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cl;
        }

    }
}
