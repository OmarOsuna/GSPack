using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Transporte
{
    public class OrdenDTO
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public string FechaHoraSalida{ get; set; }
        public string FechaEntrega { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }       
        public string Precio { get; set; }
        public string DomicilioEntrega { get; set; }

    }
}
