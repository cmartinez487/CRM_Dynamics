namespace CRM.Dynamics.Entidades.Clientes
{
    public class Contacto
    {
        /// <summary>
        /// Identificador GUID del tipo de documento del contacto.
        /// </summary>
        public string TipoDocumentoGUID { get; set; }

        /// <summary>
        /// Tipo de documento del contacto.
        /// </summary>
        public string TipoDocumento { get; set; }

        /// <summary>
        /// Número de identificación del contacto.
        /// </summary>
        public string NumeroDocumento { get; set; }

        /// <summary>
        /// Nombre(s) del contacto.
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del contacto.
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Número de teléfono fijo del contacto
        /// </summary>
        public string TelefonoFijo { get; set; }

        /// <summary>
        /// Número de celular del contacto.
        /// </summary>
        public string TelefonoCelular { get; set; }

        /// <summary>
        /// Direccíón del contacto.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Identificador GUID del Municipio de residencia del contacto.
        /// </summary>
        public string MunicipioGUID { get; set; }

        /// <summary>
        /// Nombre del municipio de residencia del contacto.
        /// </summary>
        public string Municipio { get; set; }

        /// <summary>
        /// Correo electrónico del contacto.
        /// </summary>
        public string CorreoElectronico { get; set; }
    }
}
