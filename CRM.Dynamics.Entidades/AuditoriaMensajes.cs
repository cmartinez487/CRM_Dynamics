namespace CRM.Dynamics.Entidades
{
    public class AuditoriaMensajes
    {
        /// <summary>
        /// ErrorID
        /// </summary>
        public string ErrorID { get; set; }

        /// <summary>
        /// Api
        /// </summary>
        public string Api { get; set; }

        /// <summary>
        /// Parametros
        /// </summary>
        public string Parametros { get; set; }

        /// <summary>
        /// Mensaje
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>
        /// Tipo: Error, request, response
        /// </summary>
        public TipoAuditoria Tipo { get; set; }
    }

    public enum TipoAuditoria
    {
        ERROR = 1,
        REQUEST = 2,
        RESPONSE = 3
    }
}
