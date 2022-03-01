namespace CRM.Dynamics.Entidades.Clientes
{
    public class Prospecto
    {
        /// <summary>
        /// Razon social o nombre de compañia.
        /// </summary>
        public string RazonSocial { get; set; }

        /// <summary>
        /// Identificador GUID del tipo de documento.
        /// </summary>
        public string TipoDocumentoGUID { get; set; }

        /// <summary>
        /// Número de identificación del cliente.
        /// </summary>
        public string NumeroDocumento { get; set; }

        /// <summary>
        /// Dígito de verificación.
        /// </summary>
        public string DigitoVerificacion { get; set; }

        /// <summary>
        /// Identificador GUID del municipio.
        /// </summary>
        public string MunicipioGUID { get; set; }

        /// <summary>
        /// Dirección.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Número de teléfono fijo.
        /// </summary>
        public string TelefonoFijo { get; set; }

        /// <summary>
        /// Número de celular.
        /// </summary>
        public string TelefonoCelular { get; set; }

        /// <summary>
        /// Correo electrónico
        /// </summary>
        public string CorreoElectronico { get; set; }

        /// <summary>
        /// Nombre(s) del prospecto.
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del propecto.
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Tipo de prospecto. 1. Cliente Corporativo; 2. Retail; 3. Canal Corporativo; 4. Sombrilla; 5. Giro Empresarial Web; 6. Cliente jurídico
        /// </summary>
        public int TipoProspecto { get; set; }
    }
}
