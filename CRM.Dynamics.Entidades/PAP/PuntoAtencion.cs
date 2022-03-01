using CRM.Dynamics.Entidades.Tipos;

namespace CRM.Dynamics.Entidades.PAP
{
    public class PuntoAtencion
    {
        /// <summary>
        /// Identificador GUID del punto de atención.
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Código del punto de atención
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre del punto de atención
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Dirección.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Correo electrónico.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Teléfono fijo.
        /// </summary>
        public string TelefonoFijo { get; set; }

        /// <summary>
        /// Número celular.
        /// </summary>
        public string Celular { get; set; }

        /// <summary>
        /// Municipio del punto de atención.
        /// </summary>
        public Municipio Municipio { get; set; }
    }
}
