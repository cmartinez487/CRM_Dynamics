namespace CRM.Dynamics.APIClient.Entities.EFC
{
    /// <summary>
    /// Clase que representa cualquier entidad de tipo creada por efecty para dynamics
    /// </summary>
    public class Tipo
    {
        /// <summary>
        /// Código del registro
        /// </summary>
        public string efc_codigo { get; set; }

        /// <summary>
        /// Nombre del registro
        /// </summary>
        public string efc_nombre { get; set; }
    }

    /// <summary>
    /// Entidad Tipo Documento
    /// </summary>
    public class TipoDocumento : Tipo
    {
        /// <summary>
        /// Identificador del tipo de documento
        /// </summary>
        public string efc_tipodocumentoid { get; set; }
    }

    /// <summary>
    /// Entidad Estado CUN
    /// </summary>
    public class EstadoCUN : Tipo
    {
        /// <summary>
        /// Identificador del estado CUN
        /// </summary>
        public string efc_estadocunid { get; set; }
    }

    /// <summary>
    /// Entidad Tipo Solicitud
    /// </summary>
    public class TipoSolicitud : Tipo
    {
        /// <summary>
        /// Identificador del tipo de solicitud
        /// </summary>
        public string efc_tiposolicitudid { get; set; }
    }

    /// <summary>
    /// Entidad Sub Tipo Solicitud
    /// </summary>
    public class SubTipoSolicitud : Tipo
    {
        /// <summary>
        /// Identificador del sub tipo de solicitud
        /// </summary>
        public string efc_subtiposolicitudid { get; set; }
    }

    /// <summary>
    /// Entidad Motivo de Solicitud
    /// </summary>
    public class MotivoSolicitud : Tipo
    {
        /// <summary>
        /// Identificador GUID del motivo de la solicitud.
        /// </summary>
        public string efc_motivosolicitudid { get; set; }
    }
}
