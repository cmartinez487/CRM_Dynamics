namespace CRM.Dynamics.APIClient.Entities
{
    public class Lead
    {
        /// <summary>
        /// Indetificador del prospecto
        /// </summary>
        public string leadid { get; set; }

        /// <summary>
        /// Nombre de la empresa
        /// </summary>
        public string companyname { get; set; }

        /// <summary>
        /// Referencia al tipo de documento. Sintáxis: "/efc_tipodocumentos([guid-tipodocumento])".
        /// </summary>
        public string efc_tipodocumentoid_ODATABIND { get; set; }

        /// <summary>
        /// Número de documento
        /// </summary>
        public string efc_numerodocumento { get; set; }

        /// <summary>
        /// Dígito de verificación
        /// </summary>
        public string efc_digitoverificacion { get; set; }

        /// <summary>
        /// Referencia al municipio. Sintáxis: "/efc_municipios([guid-municipio])".
        /// </summary>
        public string efc_municipioid_ODATABIND { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        public string efc_direccion { get; set; }

        /// <summary>
        /// Número fijo
        /// </summary>
        public string efc_numerotelefono { get; set; }

        /// <summary>
        /// Número celular
        /// </summary>
        public string mobilephone { get; set; }

        /// <summary>
        /// Correo electrónico
        /// </summary>
        public string emailaddress1 { get; set; }

        /// <summary>
        /// Nombres
        /// </summary>
        public string firstname { get; set; }

        /// <summary>
        /// Apellidos
        /// </summary>
        public string lastname { get; set; }

        /// <summary>
        /// Tipo de prospecto. 1. Cliente Corporativo; 2. Retail; 3. Canal Corporativo; 4. Sombrilla; 5. Giro Empresarial Web; 6. Cliente jurídico
        /// </summary>
        public int efc_tipocliente { get; set; }
    }
}
