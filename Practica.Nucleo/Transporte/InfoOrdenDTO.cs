using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Transporte
{
    public class InfoOrdenDTO
    {
        public int Id { get; set; }
        public string FechaSalida { get; set; }
        public string FechaEntrega { get; set; }
        public string Costo { get; set; }
        public string Remitente { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
    }
}
