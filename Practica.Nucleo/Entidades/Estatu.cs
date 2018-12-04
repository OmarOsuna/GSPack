using NHibernate;
using NHibernate.Criterion;
using Practica.Nucleo.Enumeradores;
using Practica.Nucleo.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Entidades
{
    public class Estatu : Persistent
    {
        public override int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public TipoEstatu TipoEstatu { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }


        public static bool Guardar(int id, string descripcion, int tipoEstatu, string ciudad, string estado, string latitud, string longitud, int idOrden)
        {
            bool realizado = false;
            try
            {
                if (id.Equals("") || descripcion.Equals("") || tipoEstatu == 0 || ciudad.Equals("") || estado.Equals("") || latitud.Equals("") || longitud.Equals(""))
                {
                    realizado = false;

                }
                else
                {

                    Orden orden = Orden.ObtenerPorId(idOrden);
                    IList<Estatu> estatus = orden.Estatus;


                    if (id == 0)
                    {
                        Estatu estatu = new Estatu();
                        estatu.Fecha = DateTime.Now;
                        estatu.Descripcion = descripcion;
                        estatu.TipoEstatu = (TipoEstatu) tipoEstatu;
                        estatu.Ciudad = ciudad;
                        estatu.Estado = estado;
                        estatu.Latitud = latitud;
                        estatu.Longitud = longitud;
                        estatus.Add(estatu);
                        orden.Estatus = estatus;
                       
                        orden.Update();
                    } else
                    {
                        foreach (Estatu est in estatus)
                        {
                            if (est.Id == id)
                            {
                                est.Descripcion = descripcion;
                                est.TipoEstatu = (TipoEstatu)tipoEstatu;
                                est.Ciudad = ciudad;
                                est.Estado = estado;
                                est.Latitud = latitud;
                                est.Longitud = longitud;
                                orden.Estatus = estatus;
                                orden.Update();
                            }
                        }

                        

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
                Estatu estatu = new Estatu();
                estatu.Id = id;
                estatu.Delete();
                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return realizado;
        }

        public static Estatu ObtenerPorId(int id)
        {
            Estatu est = new Estatu();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(est.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    est = (crit.UniqueResult<Estatu>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return est;
        }
    }
}
