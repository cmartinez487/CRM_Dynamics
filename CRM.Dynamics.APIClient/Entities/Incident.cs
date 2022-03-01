namespace CRM.Dynamics.APIClient.Entities
{
    /// <summary>
    /// Entidad que representa un caso (PQR)
    /// </summary>
    public class Incident
    {
        /// <summary>
        /// Actividades completas.
        /// </summary>
        public bool activitiescomplete { get; set; }

        /// <summary>
        /// Detalla si el perfil está bloqueado o no.
        /// </summary>
        public bool blockedprofile { get; set; }

        /// <summary>
        /// Indica el origen del caso: 1:phone, 2:email, 3:web, 2483:facebook, 3986: twitter
        /// </summary>
        public int? caseorigincode { get; set; }

        /// <summary>
        /// Indica el tipo de caso para identificar el incidente y usar en el enrutamiento y análisis de casos.
        /// </summary>
        public int? casetypecode { get; set; }

        /// <summary>
        /// Consultar correo electrónico.
        /// </summary>
        public bool checkemail { get; set; }

        /// <summary>
        /// Indica si el representante de servicio al cliente se ha comunicado con el cliente o no.
        /// </summary>
        public bool customercontacted { get; set; }

        /// <summary>
        /// Muestra si los términos de los derechos asociados se deben disminuir o no.
        /// </summary>
        public bool decremententitlementterm { get; set; }

        /// <summary>
        /// Información adicional para describir el caso.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Muestra la tasa de conversión de la moneda del registro. El tipo de cambio se utiliza para convertir todos los campos de moneda local a la moneda predeterminada del sistema.
        /// </summary>
        public decimal? exchangerate { get; set; }

        /// <summary>
        /// Indica si se ha enviado la primera respuesta.
        /// </summary>
        public bool firstresponsesent { get; set; }

        /// <summary>
        /// Muestra el estado del tiempo de respuesta inicial para el caso de acuerdo con los términos del SLA.
        /// </summary>
        public int? firstresponseslastatus { get; set; }

        /// <summary>
        /// Tarea de seguimiento creada.
        /// </summary>
        public bool followuptaskcreated { get; set; }

        /// <summary>
        /// Número de secuencia de la importación que creó este registro.
        /// </summary>
        public int? importsequencenumber { get; set; }

        /// <summary>
        /// Identificador único del caso.
        /// </summary>
        public string incidentid { get; set; }

        /// <summary>
        /// Selecciona la etapa actual del caso para ayudar a los miembros del equipo de servicio cuando revisan o transfieren un caso.
        /// </summary>
        public int? incidentstagecode { get; set; }

        /// <summary>
        /// Esta decrementanto (Sólo para uso del sistema).
        /// </summary>
        public bool isdecrementing { get; set; }

        /// <summary>
        /// Indica si el caso ha sido escalado.
        /// </summary>
        public bool isescalated { get; set; }

        /// <summary>
        /// Indica si el incidente se ha fusionado con otro incidente.
        /// </summary>
        public bool merged { get; set; }

        /// <summary>
        /// Seleccione la prioridad para que los clientes preferidos o los problemas críticos se manejen rápidamente.
        /// </summary>
        public int? prioritycode { get; set; }

        /// <summary>
        /// Muestra el estado del tiempo de resolución para el caso de acuerdo con los términos del SLA.
        /// </summary>
        public int? resolvebyslastatus { get; set; }

        /// <summary>
        /// Indica si el incidente se ha enviado a la cola o no.
        /// </summary>
        public bool routecase { get; set; }

        /// <summary>
        /// Seleccione la etapa, en el proceso de resolución de casos, en la que se encuentra el caso.
        /// </summary>
        public int? servicestage { get; set; }

        /// <summary>
        /// Seleccione la gravedad del caso para indicar el impacto del incidente en el cliente.
        /// </summary>
        public int? severitycode { get; set; }

        /// <summary>
        /// Muestra si el caso está activo, resuelto o cancelado. Los casos resueltos y cancelados son de solo lectura y no se pueden editar a menos que se reactiven.
        /// </summary>
        public int statecode { get; set; }

        /// <summary>
        /// Selecciona el estado del caso.
        /// </summary>
        public int statuscode { get; set; }

        /// <summary>
        /// Número de versión de regla de zona horaria (Sólo para uso interno).
        /// </summary>
        public int? timezoneruleversionnumber { get; set; }

        /// <summary>
        /// Escriba un asunto o un nombre descriptivo, como la solicitud, el problema o el nombre de la empresa, para identificar el caso en las vistas de Microsoft Dynamics 365.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Número de versión.
        /// </summary>
        public int? versionnumber { get; set; }

        /// <summary>
        /// Referencia al tipo de documento. Sintáxis: "/efc_tipodocumentos([guid-tipodocumento])".
        /// </summary>
        public string efc_tipodocumentoconsultar_ODATABIND { get; set; }

        /// <summary>
        /// Número documento cliente
        /// </summary>
        public string efc_numerodocumentoconsultar { get; set; }

        /// <summary>
        /// Referencia al tipo de solicitud. Sintáxis: "/efc_tiposolicituds([guid-tiposolicitud])".
        /// </summary>
        public string efc_tiposolicitudid_ODATABIND { get; set; }

        /// <summary>
        /// Medio de respuesta: 1:correo, 2:llamada, 3:envío físico
        /// </summary>
        public int? efc_mediorespuesta { get; set; }

        /// <summary>
        /// Número de operación
        /// </summary>
        public string efc_numerooperacion { get; set; }

        /// <summary>
        /// Tipo de cliente que genera el caso
        /// </summary>
        public int? efc_tipocliente { get; set; }

    }

    /// <summary>
    /// Representa un caso creado por una persona natural
    /// </summary>
    public class IncidentContact : Incident
    {
        /// <summary>
        /// Referencia al contacto. Sintáxis: "/contacts([guid-contacto])"
        /// </summary>
        public string customerid_contact_ODATABIND { get; set; }
    }

    /// <summary>
    /// Representa un caso creado desde un punto de atención
    /// </summary>
    public class IncidentPAP : IncidentContact
    {
        /// <summary>
        /// Referencia al punto de atención. Sintáxis: "/efc_puntoatencion([guid-puntoatencion])".
        /// </summary>
        public string efc_puntoatencionid_ODATABIND { get; set; }

        /// <summary>
        /// Referencia al motivo de solicitud. Sintáxis: "/efc_motivosolicituds([guid-motivosolicitud])".
        /// </summary>
        public string efc_motivosolicitudid_ODATABIND { get; set; }
    }

    /// <summary>
    /// Entidad de solo consulta de casos
    /// </summary>
    public class IncidentQuery : Incident
    {
        /// <summary>
        /// Número de radicado
        /// </summary>
        public string ticketnumber { get; set; }

        /// <summary>
        /// Fecha de vencimiento
        /// </summary>
        public string efc_fechavencimiento { get; set; }

        /// <summary>
        /// Código único numérico
        /// </summary>
        public string efc_numerocun { get; set; }

        /// <summary>
        ///  Fecha de creación
        /// </summary>
        public string createdon { get; set; }

        /// <summary>
        /// Motivo de solicitud (Guid)
        /// </summary>
        public string _efc_motivosolicitudid_value { get; set; }

        /// <summary>
        /// Tipo de solicitud (Guid)
        /// </summary>
        public string _efc_tiposolicitudid_value { get; set; }

        /// <summary>
        /// Descripción del estado
        /// </summary>
        public string _efc_estadocun_value { get; set; }

        /// <summary>
        /// Tipo de queja
        /// </summary>
        public string _efc_subtiposolicitudid_value { get; set; }

        /// <summary>
        /// Estado aprobación
        /// </summary>
        public int? efc_estadoaprobacion { get; set; }

        /// <summary>
        /// Propiedad expansiva que contiene los puntos de atención relacionados
        /// </summary>
        public object efc_puntoatencionid { get; set; }

        /// <summary>
        /// Propiedad expansiva que contiene el contacto asociado relacionado
        /// </summary>
        public object customerid_contact { get; set; }

        /// <summary>
        /// Propiedad expansiva que contiene el cliente asociado relacionado
        /// </summary>
        public object customerid_account { get; set; }

        /// <summary>
        /// Fecha de cierre del caso
        /// </summary>
        public string efc_fechacierre { get; set; }

        /// <summary>
        /// Propiedad expansiva que contiene las notas adjuntas al caso
        /// </summary>
        public object[] Incident_Annotation { get; set; }
    }

}
