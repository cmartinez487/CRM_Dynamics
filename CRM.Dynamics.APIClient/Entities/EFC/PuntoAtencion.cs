namespace CRM.Dynamics.APIClient.Entities.EFC
{
    /// <summary>
    /// Entidad que representa un punto de atención
    /// </summary>
    public class PuntoAtencion
    {
        /// <summary>
        /// Código del punto de atención
        /// </summary>
        public string efc_codigo { get; set; }

        /// <summary>
        /// Nombre del punto de atención
        /// </summary>
        public string efc_nombre { get; set; }

        /// <summary>
        /// Dirección.
        /// </summary>
        public string efc_direccion { get; set; }

        /// <summary>
        /// Correo electrónico.
        /// </summary>
        public string emailaddress { get; set; }

        /// <summary>
        /// Teléfono fijo.
        /// </summary>
        public string efc_telefonofijo { get; set; }

        /// <summary>
        /// Número celular.
        /// </summary>
        public string efc_celular { get;  set; }
    }

    /// <summary>
    /// Entidad de solo consulta de puntos de atención
    /// </summary>
    public class PuntoAtencionQuery : PuntoAtencion
    { 
        /// <summary>
        /// Identificador GUID del punto de atención.
        /// </summary>
        public string efc_puntoatencionid { get; set; }

        /// <summary>
        /// Propiedad expansiva que contiene los incidentes relacionados al punto de atención
        /// </summary>
        public object[] efc_efc_puntoatencion_incident_puntoatencionid { get; set; }

        /// <summary>
        /// Municipio del punto de atención.
        /// </summary>
        public Municipio efc_municipioid { get; set; }
    }
}
