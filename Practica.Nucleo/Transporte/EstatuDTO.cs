using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Transporte
{
    public class EstatuDTO
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
        public string TipoEstatu { get; set; }
        public string Lugar { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
}
