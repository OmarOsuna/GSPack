using NHibernate;
using NHibernate.Criterion;
using Practica.Nucleo.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Entidades
{
    public class Paquete : Persistent
    {
        public override int Id { get; set; }
        public double Peso { get; set; }
        public string Tamano { get; set; }
        public string TipoEnvio { get; set; }
        public string Descripcion { get; set; }


        public static Paquete ObtenerPorId(int id)
        {
            Paquete pq = new Paquete();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(pq.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    pq = (crit.UniqueResult<Paquete>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pq;
        }
    }
}
