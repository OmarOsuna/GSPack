using NHibernate;
using NHibernate.Criterion;
using Practica.Nucleo.Enumeradores;
using Practica.Nucleo.Negocio;
using Practica.Nucleo.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica.Nucleo.Entidades
{
    public class Orden : Persistent
    {
        public override int Id { get; set; }
        public string Folio { get; set; }
        public int NumeroFolio { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime HoraSalida { get; set; }
        public DateTime FechaEntrega { get; set; }
        public Cliente Cliente { get; set; }
        public Destinatario Destinatario { get; set; }
        public Usuario Empleado { get; set; }
        public Paquete Paquete { get; set; }
        public string Precio { get; set; }
        public string DestinatarioDos { get; set; }
        public IList<Estatu> Estatus { get; set; }




        public static IList<Orden> ObtenerTodos()
        {
            IList<Orden> ordenes = new List<Orden>();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(new Orden().GetType());
                    ordenes = crit.List<Orden>();                    
                    session.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ordenes;
        }

        public static IList<OrdenDTO> ObtenerTodosDTO()
        {
            IList<OrdenDTO> ordenesDTO = new List<OrdenDTO>();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(new Orden().GetType());

                    IList<Orden> ordenes = crit.List<Orden>();
                    foreach (Orden orden in ordenes)
                    {
                        OrdenDTO ordenDTO = new OrdenDTO();
                        ordenDTO.Id = orden.Id;
                        ordenDTO.Folio = orden.Folio;
                        ordenDTO.FechaHoraSalida = orden.FechaSalida.ToString();
                        ordenDTO.FechaEntrega = orden.FechaEntrega.ToString();
                        ordenDTO.Remitente = orden.Cliente.Nombre;
                        ordenDTO.Destinatario = orden.Destinatario.Nombre;
                        ordenDTO.Precio = "<span class='badge bg-green'>" + "$" + orden.Precio + "</span>";
                        ordenDTO.DomicilioEntrega = "Col. " + orden.Destinatario.Domicilio.Colonia + ", Calle " + orden.Destinatario.Domicilio.Calle + " #" + orden.Destinatario.Domicilio.Numero;
                        ordenesDTO.Add(ordenDTO);
                    }
                    session.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ordenesDTO;
        }

        public static Orden ObtenerPorId(int id)
        {
            Orden ord = new Orden();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(ord.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    ord = (crit.UniqueResult<Orden>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ord;
        }

        public static Orden ObtenerPorFolio(string folio)
        {
            Orden ord = new Orden();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(ord.GetType());
                    crit.Add(Expression.Eq("Folio", folio));
                    ord = (crit.UniqueResult<Orden>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ord;
        }

        public static IList<Estatu> ObtenerStatus(int id)
        {
            IList<Estatu> estatus = new List<Estatu>();
            Orden ord = new Orden();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(ord.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    ord = (crit.UniqueResult<Orden>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estatus = ord.Estatus;
        }

        public static IList<EstatuDTO> ObtenerStatusDTO(int id)
        {
            IList<EstatuDTO> estatus = new List<EstatuDTO>();
            Orden ord = new Orden();           
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(ord.GetType());
                    crit.Add(Expression.Eq("Id", id));
                    ord = (crit.UniqueResult<Orden>());
                    var result = ord.Estatus.OrderByDescending(a => a.Fecha).ToList<Estatu>();

                    foreach (var estatu in result)
                    {
                        EstatuDTO estatuDTO = new EstatuDTO();
                        estatuDTO.Id = estatu.Id;
                        estatuDTO.Fecha = String.Format("{0:d/M/yyyy HH:mm:ss}", estatu.Fecha);
                        estatuDTO.Descripcion = estatu.Descripcion;
                        if (Convert.ToString(estatu.TipoEstatu).Equals("PENDIENTE"))
                        {
                            estatuDTO.TipoEstatu = "<span class='badge bg-blue'>Pendiente</span>";

                        }
                        if (Convert.ToString(estatu.TipoEstatu).Equals("ENTREGADO"))
                        {
                            estatuDTO.TipoEstatu = "<span class='badge bg-green'>Entregado</span>";

                        }
                        if (Convert.ToString(estatu.TipoEstatu).Equals("CANCELADO"))
                        {
                            estatuDTO.TipoEstatu = "<span class='badge bg-red'>Cancelado</span>";

                        }
                        estatuDTO.Lugar = estatu.Ciudad + " " + estatu.Estado;
                        estatuDTO.Longitud = estatu.Longitud;
                        estatuDTO.Latitud = estatu.Latitud;

                        estatus.Add(estatuDTO);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return estatus;
        }

        public static IList<EstatuDTO> ObtenerStatusDTOFolio(string folio)
        {
            IList<EstatuDTO> estatus = new List<EstatuDTO>();
            Orden ord = new Orden();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(ord.GetType());
                    crit.Add(Expression.Eq("Folio", folio));
                    ord = (crit.UniqueResult<Orden>());
                    if (ord != null)
                    {
                        var result = ord.Estatus.OrderByDescending(a => a.Fecha).ToList<Estatu>();

                        foreach (var estatu in result)
                        {
                            EstatuDTO estatuDTO = new EstatuDTO();
                            estatuDTO.Id = estatu.Id;
                            estatuDTO.Fecha = String.Format("{0:d/M/yyyy HH:mm:ss}", estatu.Fecha);
                            estatuDTO.Descripcion = estatu.Descripcion;
                            if (Convert.ToString(estatu.TipoEstatu).Equals("PENDIENTE"))
                            {
                                estatuDTO.TipoEstatu = "<span class='badge bg-blue'>Pendiente</span>";

                            }
                            if (Convert.ToString(estatu.TipoEstatu).Equals("ENTREGADO"))
                            {
                                estatuDTO.TipoEstatu = "<span class='badge bg-green'>Entregado</span>";

                            }
                            if (Convert.ToString(estatu.TipoEstatu).Equals("CANCELADO"))
                            {
                                estatuDTO.TipoEstatu = "<span class='badge bg-red'>Cancelado</span>";

                            }
                            estatuDTO.Lugar = estatu.Ciudad + " " + estatu.Estado;
                            estatuDTO.Longitud = estatu.Longitud;
                            estatuDTO.Latitud = estatu.Latitud;

                            estatus.Add(estatuDTO);
                        }

                    }                    

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return estatus;
        }

        public static string ObtenerFolio()
        {
            string folio = "";
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    int numeroFolioDB = ObtenerNumeroFolioConsecutivo();
                    int iteraciones = 6 - Convert.ToString(numeroFolioDB).Length;
                    string ceros = "";
                    for (int i = 0; i < iteraciones; i++)
                    {
                        ceros += "0";
                    }
                    numeroFolioDB++;
                    string anio = Convert.ToString(DateTime.Now.Year);
                    folio = ceros + numeroFolioDB + anio;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return folio;
        }

        public static int ObtenerNumeroFolioConsecutivo()
        {
            int numeroFolio = 0;
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    numeroFolio = session.CreateSQLQuery("SELECT numeroFolio FROM orden ORDER BY id DESC LIMIT 1;").UniqueResult<int>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return numeroFolio;
        }

        public static bool Existe(string folio)
        {
            bool existe = false;
            Orden ord = new Orden();
            try
            {
                using (ISession session = Persistent.SessionFactory.OpenSession())
                {
                    ICriteria crit = session.CreateCriteria(ord.GetType());
                    crit.Add(Expression.Eq("Folio", folio));
                    ord = (crit.UniqueResult<Orden>());

                    if (ord != null)
                    {
                        existe = true;
                    }
                    else
                    {
                        existe = false;
                    }

                }
               
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return existe;

        }
               
        public static bool Guardar(int id, string nombreRemitente, string telefonoUnoRemitente, string telefonoDosRemitente, string telefonoTresRemitente, string correoRemitente, string rfcRemitente, string nombreDestinatario, string telefonoDestinatario, string correoDestinatario, string calleDomicilioD, string avenidaDomicilioD, string numeroDomicilioD, string cpDomicilioD, string coloniaDomicilioD, string estadoDomicilioD, string ciudadDomicilioD, string referenciaDomicilioD, string tipoPaquete, string pesoPaquete, string tamanoPaquete, string descripcionPaquete, string fechaEntrega, string precioPaquete, string destinatarioDos, int idEmpleado, int idRemitente, int idDestinatario)
        {
            bool realizado = false;
            try
            {
                if (id.Equals("") || nombreRemitente.Equals("") || telefonoUnoRemitente.Equals("") || correoRemitente.Equals("") || rfcRemitente.Equals("") || nombreDestinatario.Equals("") || telefonoDestinatario.Equals("")||
                    correoDestinatario.Equals("") || calleDomicilioD.Equals("") || numeroDomicilioD.Equals("")||
                    cpDomicilioD.Equals("") || coloniaDomicilioD.Equals("") || estadoDomicilioD.Equals("") || ciudadDomicilioD.Equals("") ||
                    referenciaDomicilioD.Equals("") || tipoPaquete.Equals("") || descripcionPaquete.Equals("") || fechaEntrega.Equals("") || precioPaquete.Equals(""))
                {
                    realizado = false;
                }
                else
                {



                    Orden orden = id == 0 ? new Orden() : ObtenerPorId(id);

                    if (id != 0)
                    {
                        idDestinatario = orden.Destinatario.Id;
                        idRemitente = orden.Cliente.Id;

                    }
                    orden.FechaEntrega = Convert.ToDateTime(fechaEntrega);
                    orden.Precio = precioPaquete;
                    orden.DestinatarioDos = destinatarioDos;
                    orden.FechaSalida = DateTime.Now;
                    orden.HoraSalida = DateTime.Now;


                    Cliente remitente = new Cliente();
                    if (idDestinatario == 0)
                    {
                        remitente.Nombre = nombreRemitente;
                        remitente.TelefonoUno = telefonoUnoRemitente;
                        remitente.TelefonoDos = telefonoDosRemitente;
                        remitente.TelefonoTres = telefonoTresRemitente;
                        remitente.Correo = correoRemitente;
                        remitente.RFC = rfcRemitente;

                    } else
                    {
                        remitente = Cliente.ObtenerPorId(idRemitente);
                    }                  
                    
                    orden.Cliente = remitente;


                    Destinatario destinatario = new Destinatario();

                    if (idDestinatario == 0)
                    {
                        destinatario.Nombre = nombreDestinatario;
                        destinatario.Telefono = telefonoDestinatario;
                        destinatario.Correo = correoDestinatario;

                        Domicilio domicilio = new Domicilio();
                        domicilio.Calle = calleDomicilioD;
                        domicilio.Avenida = avenidaDomicilioD;
                        domicilio.Colonia = cpDomicilioD;
                        domicilio.Ciudad = ciudadDomicilioD;
                        domicilio.Estado = estadoDomicilioD;
                        domicilio.Referencia = referenciaDomicilioD;
                        domicilio.CodigoPostal = cpDomicilioD;
                        domicilio.Numero = numeroDomicilioD;
                        destinatario.Domicilio = domicilio;
                    } else
                    {
                        destinatario = Destinatario.ObtenerPorId(idDestinatario);
                    }
                    

                    orden.Destinatario = destinatario;

                    Usuario empleado = new Usuario();
                    empleado = Usuario.ObtenerPorId(idEmpleado);

                    orden.Empleado = empleado;

                    Paquete paquete = new Paquete();
                    paquete.Peso = Convert.ToDouble(pesoPaquete);
                    paquete.Tamano = tamanoPaquete;
                    paquete.TipoEnvio = tipoPaquete;
                    paquete.Descripcion = descripcionPaquete;

                    orden.Paquete = paquete;

                    if (id == 0)
                    {
                        IList<Estatu> estatus = new List<Estatu>();
                        orden.Folio = ObtenerFolio();
                        orden.NumeroFolio = ObtenerNumeroFolioConsecutivo() + 1;
                        Estatu estatu = new Estatu();
                        estatu.Fecha = DateTime.Now;
                        estatu.Descripcion = "Procesado en oficinas GSPack";
                        estatu.TipoEstatu = (TipoEstatu) 1;
                        estatu.Ciudad = "Guaymas";
                        estatu.Estado = "Sonora";
                        estatu.Latitud = "27.925340417122968";
                        estatu.Longitud = "-110.9110737194689";
                        estatus.Add(estatu);
                        orden.Estatus = estatus;
                        string domicilio = "#" + orden.Destinatario.Domicilio.Numero + " " + orden.Destinatario.Domicilio.Calle + " " + orden.Destinatario.Domicilio.Colonia;
                        string cec = orden.Destinatario.Domicilio.Ciudad + " " + orden.Destinatario.Domicilio.Estado + " CP" + orden.Destinatario.Domicilio.CodigoPostal;
                        Email.EnviarEmail(orden.Destinatario.Correo, orden.Destinatario.Nombre, orden.FechaSalida.ToString(), orden.FechaEntrega.ToString(), orden.Folio, domicilio, orden.Destinatario.Domicilio.Referencia, cec, orden.Destinatario.Telefono);
                        orden.Save();
                    }
                    else
                    {
                        orden.Update();
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
                Orden orden = new Orden();
                orden.Id = id;
                orden.Delete();
                realizado = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return realizado;
        }
        
    }
}
