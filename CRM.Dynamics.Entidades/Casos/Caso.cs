using CRM.Dynamics.Entidades.Clientes;
using Nota = CRM.Dynamics.Entidades.NotaAdjunta;

namespace CRM.Dynamics.Entidades.Caso
{
    /// <summary>
    /// Clase que representa una caso (PQR)
    /// </summary>
    public class Caso
    {
        /// <summary>
        /// Identificador único del caso
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Número del radicado
        /// </summary>
        public string NumeroRadicado { get; set; }

        /// <summary>
        /// Código numérico único
        /// </summary>
        public string NumeroCUN { get; set; }

        /// <summary>
        /// Identificador GUID del tipo de solicitud.
        /// </summary>
        public string TipoSolicitudGUID { get; set; }

        /// <summary>
        /// Tipo de solicitud. (Petición, Queja, Indemnización, Sugerencia).
        /// </summary>
        public string TipoSolicitud { get; set; }       

        /// <summary>
        /// Información adicional para describir el caso.
        /// </summary>
        public string Descripcion { get; set; }
               
        /// <summary>
        /// Estado del trámite
        /// </summary>
        public string EstadoTramite { get; set; }

        /// <summary>
        /// Estado del trámite SIC
        /// </summary>
        public string SubEstadoTramite { get; set; }

        /// <summary>
        /// Descripción del estado del trámite
        /// </summary>
        public string DescripcionEstadoTramite { get; set; }

        /// <summary>
        /// Indica el tipo de caso: 1: Question, 2: Problem, 3:	Request
        /// </summary>
        public int TipoCaso { get; set; }

        /// <summary>
        /// Tipo de queja
        /// </summary>
        public string TipoQueja { get; set; }

        /// <summary>
        /// Fecha de creación
        /// </summary>
        public string FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de vencimiento
        /// </summary>
        public string FechaVencimiento { get; set; }

        /// <summary>
        /// Fecha de respuesta del caso
        /// </summary>
        public string FechaRespuesta { get; set; }

        /// <summary>
        /// Identificador GUID del punto de atención
        /// </summary>
        public string PuntoAtencionGUID { get; set; }

        /// <summary>
        /// Código del punto de atención
        /// </summary>
        public string CodigoPuntoAtencion { get; set; }

        /// <summary>
        /// Nombre del punto de atención
        /// </summary>
        public string NombrePuntoAtencion { get; set; }
        
        /// <summary>
        /// Medio de respuesta: 1:correo, 2:llamada, 3:envío físico
        /// </summary>
        public int MedioRespuesta { get; set; }

        /// <summary>
        /// Indica si el cliente autoriza el tratamiento de datos personales.
        /// </summary>
        public int AutorizaTratamientoDatosPersonales { get; set; }

        /// <summary>
        /// Indica el origen del caso: 1:phone, 2:email, 3:web, 2483:facebook, 3986: twitter
        /// </summary>
        public int Origen { get; set; }

        /// <summary>
        /// Selecciona el estado del caso.
        /// </summary>
        public int CodigoEtapa { get; set; }

        /// <summary>
        /// Selecciona la etapa actual del caso para ayudar a los miembros del equipo de servicio cuando revisan o transfieren un caso.
        /// </summary>
        public int EtapaCaso { get; set; }

        /// <summary>
        /// Descripción del motivo de la PQRS
        /// </summary>
        public string MotivoDescripcion { get; set; }

        /// <summary>
        /// Identificador GUID del motivo de la PQRS.
        /// </summary>
        public string MotivoGUID { get; set; }

        /// <summary>
        /// Número de operación de recaudo errado
        /// </summary>
        public string NumeroOperacionErrado { get; set; }

        /// <summary>
        /// Número de operación de recaudo correcto
        /// </summary>
        public string NumeroOperacionCorrecto { get; set; }

        /// <summary>
        /// Contacto que radica el caso.
        /// </summary>
        public Contacto Contacto { get; set; }

        /// <summary>
        /// Nota adjunta al caso
        /// </summary>
        public Nota.NotaAdjunta NotaAdjunta { get; set; }

    }
}
