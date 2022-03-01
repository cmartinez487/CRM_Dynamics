namespace CRM.Dynamics.Entidades.Tipos
{
    public class MotivoSolicitud
    {
        /// <summary>
        /// Identificador GUID del motivo de la solicitud.
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Código del motivo de solicitud.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre del motivo de solicitud.
        /// </summary>
        public string Nombre { get; set; }
    }
}
