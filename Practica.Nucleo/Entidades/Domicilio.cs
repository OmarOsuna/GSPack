using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Entidades
{
    public class Domicilio : Persistent
    {
        public override int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Avenida { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }


        public static Domicilio ObtenerPorId(int id)
        {
            Domicilio u = new Domicilio();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(u.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    u = (crit.UniqueResult<Domicilio>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return u;
        }


    }
}
