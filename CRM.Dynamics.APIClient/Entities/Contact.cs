namespace CRM.Dynamics.APIClient.Entities
{
    /// <summary>
    /// Entidad que representa un contacto o cliente natural
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Identificador único del cliente natural
        /// </summary>
        public string contactid { get; set; }

        /// <summary>
        /// Nombres del contacto
        /// </summary>
        public string firstname { get; set; }

        /// <summary>
        /// Apellidos del contacto
        /// </summary>
        public string lastname { get; set; }

        /// <summary>
        /// Referencia al tipo de documento. Sintáxis: "/efc_tipodocumentos([guid-tipodocumento])".
        /// </summary>
        public string efc_tipodocumentoid_ODATABIND { get; set; }

        /// <summary>
        /// Número de identificación del cliente
        /// </summary>
        public string efc_numerodocumento { get; set; }

        /// <summary>
        /// Tipo de contacto
        /// </summary>
        public int efc_clasificacioncontacto { get; set; }

        /// <summary>
        /// Autoriza tratamiento de datos personales
        /// </summary>
        public int efc_autorizadatospersonales { get; set; }

        /// <summary>
        /// Indica si es persona expuesta públicamente
        /// </summary>
        public bool efc_espep { get; set; }

        /// <summary>
        /// Correo electrónico
        /// </summary>
        public string emailaddress1 { get; set; }

        /// <summary>
        /// Teléfono celular
        /// </summary>
        public string mobilephone { get; set; }

        /// <summary>
        /// Teléfono fijo
        /// </summary>
        public string efc_numerotelefono { get; set; }

        /// <summary>
        /// Referencia al municipio. Sintáxis: "/efc_municipios([guid-municipio])".
        /// </summary>
        public string efc_municipioid_ODATABIND { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        public string efc_direccion { get; set; }
    }

    /// <summary>
    /// Entidad de sólo consulta de contactos
    /// </summary>
    public class ContactQuery : Contact
    {
        /// <summary>
        /// Propiedad expansiva que contiene los incidentes del contacto
        /// </summary>
        public object[] incident_customer_contacts { get; set; }

        /// <summary>
        /// Propiedad expansiva que contiene las relaciones con el cliente
        /// </summary>
        public object[] efc_contact_efc_relacioncliente_contactoid { get; set; }

        /// <summary>
        /// Tipo de documento
        /// </summary>
        public string _efc_tipodocumentoid_value { get; set; }

        /// <summary>
        /// Propiedad de navegación que contiene el municipio asociado al contacto
        /// </summary>
        public object efc_municipioid { get; set; }
    }
}
