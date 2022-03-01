namespace CRM.Dynamics.APIClient.Entities
{
    /// <summary>
    /// Clase que representa una cuenta o cliente judirico
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Identificador único del cliente
        /// </summary>
        public string accountid { get; set; }

        /// <summary>
        /// Referencia al tipo de documento. Sintáxis: "/efc_tipodocumentos([guid-tipodocumento])".
        /// </summary>
        public string efc_tipodocumentoid_ODATABIND { get; set; }

        /// <summary>
        /// Número de identificación del cliente
        /// </summary>
        public string efc_numerodocumento { get; set; }

        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Número de teléfono principal (Completo)
        /// </summary>
        public string telephone1 { get; set; }

        /// <summary>
        /// Número celular del cliente
        /// </summary>
        public string telephone2 { get; set; }

        /// <summary>
        /// Número de teléfono
        /// </summary>
        public string efc_numerotelefono { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        public string efc_direccion { get; set; }

        /// <summary>
        /// Correo electrónico
        /// </summary>
        public string emailaddress1 { get; set; }

        /// <summary>
        /// Referencia al municipio. Sintáxis: "/efc_municipios([guid-municipio])".
        /// </summary>
        public string efc_municipioid_ODATABIND { get; set; }

        /// <summary>
        /// Número de identificación o código de la cuenta para buscar e identificar rápidamente la cuenta en las vistas del sistema
        /// </summary>
        public string accountnumber { get; set; }        

    }

    public class AccountQuery : Account 
    {
        /// <summary>
        /// Tipo de documento
        /// </summary>
        public string _efc_tipodocumentoid_value { get; set; }
            
        /// <summary>
        /// Propiedad expansiva que contiene los incidentes del cliente
        /// </summary>
        public object[] incident_customer_accounts { get; set; }        
    }
}
