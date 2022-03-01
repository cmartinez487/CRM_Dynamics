namespace CRM.Dynamics.APIClient.Entities
{
    /// <summary>
    ///  Clase que representa una nota adjunta
    /// </summary>
    public class Annotation
    {
        /// <summary>
        /// Título de la nota
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// Contenido de la nota
        /// </summary>
        public string notetext { get; set; }

        /// <summary>
        /// Contenido del archivo adjunto (base64)
        /// </summary>
        public string documentbody { get; set; }

        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// Tamaño del archivo adjunto en bytes
        /// </summary>
        public int filesize { get; set; }

        /// <summary>
        /// Tipo del archivo adjunto
        /// </summary>
        public string mimetype { get; set; }

        /// <summary>
        /// Nombre del objeto relacionado (incident)
        /// </summary>
        public string objecttypecode { get; set; }

        /// <summary>
        /// Referencia al incidente. Sintáxis: "/incidents([guid-incidente])"
        /// </summary>
        public string objectid_incident_ODATABIND { get; set; }

        /// <summary>
        ///  Fecha de creación
        /// </summary>
        public string createdon { get; set; }
    }
}
