using CRM.Dynamics.AccesoDatos.Usuarios;
using CRM.Dynamics.APIClient;
using CRM.Dynamics.APIClient.Dictionaries;
using CRM.Dynamics.APIClient.Entities;
using CRM.Dynamics.APIClient.Entities.EFC;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.Entidades.Caso;
using CRM.Dynamics.Entidades.Clientes;
using CRM.Dynamics.WebApi.Handlers;
using CRM.Dynamics.WebApi.Helpers;
using CRM.Dynamics.WebApi.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRM.Dynamics.Areas.Dynamics365.Controllers
{
    public class CasoController : ApiController
    {
        /// <summary>
        /// Obtiene un caso por ID
        /// </summary>
        /// <param name="guid">Identificador del caso</param>
        /// <returns>Devuelve un caso</returns>
        [HttpGet]
        [Route("api/dynamics/caso/{guid}")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Get(string guid)
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();
                // Realiza solicitud al API de CRM Dynamics 365
                DynamicsResponse response = api.Get("incidents(" + guid + ")");
                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, JsonConvert.DeserializeObject(response.Message));
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Caso / Get", e.Message, TipoAuditoria.ERROR, guid);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Obtiene los casos de un cliente natural
        /// </summary>
        /// <param name="tipo">Tipo de identificación</param>
        /// <param name="identificacion">Número de identificación</param>
        /// <param name="numerocaso">Número CUN o ticket</param>
        /// <returns>Devuelve el listado de casos consultado</returns>
        [HttpGet]
        [Route("api/dynamics/caso/natural/{tipo}/{identificacion}/{numerocaso}")]
        [System.Web.Http.Authorize]
        public dynamic GetNatural(string tipo, string identificacion, string numerocaso)
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();
                string query;
                bool queryByTicket = !string.IsNullOrEmpty(numerocaso) && numerocaso != "null";
                bool clientejudirico = false;

                if (queryByTicket)
                {
                    query = string.Format("incidents?$filter=ticketnumber eq '{0}' or efc_numerocun eq '{0}'&$expand=customerid_contact($select=firstname,lastname,_efc_tipodocumentoid_value,efc_numerodocumento),customerid_account($select=name,_efc_tipodocumentoid_value,efc_numerodocumento)",
                        numerocaso);
                }
                else
                {
                    string tipoDoc = Dictionaries.GetTipoDocumento(tipo).ToUpper();
                    if (tipoDoc == "N")
                    {
                        clientejudirico = true;
                        query = string.Format("accounts?$select=name,_efc_tipodocumentoid_value,efc_numerodocumento&$expand=incident_customer_accounts($select=ticketnumber,efc_numerocun,statecode,statuscode,createdon,efc_fechavencimiento,efc_fechacierre,_efc_tiposolicitudid_value,_efc_motivosolicitudid_value,_efc_estadocun_value,_efc_subtiposolicitudid_value)&$filter=_efc_tipodocumentoid_value eq {0} and efc_numerodocumento eq '{1}'",
                        tipo,
                        identificacion);
                    }
                    else
                    {
                        query = string.Format("contacts?$select=firstname,lastname,_efc_tipodocumentoid_value,efc_numerodocumento&$expand=incident_customer_contacts($select=ticketnumber,efc_numerocun,statecode,statuscode,createdon,efc_fechavencimiento,efc_fechacierre,_efc_tiposolicitudid_value,_efc_motivosolicitudid_value,_efc_estadocun_value,_efc_subtiposolicitudid_value)&$filter=_efc_tipodocumentoid_value eq {0} and efc_numerodocumento eq '{1}'",
                            tipo,
                            identificacion);
                    }
                }

                // Realiza solicitud al API de CRM Dynamics 365
                DynamicsResponse response = api.Get(query);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ContactQuery contact = new ContactQuery();
                    AccountQuery account = new AccountQuery();
                    Contacto contacto = new Contacto();                    
                    IncidentQuery incident = new IncidentQuery();
                    List<IncidentQuery> incidents = new List<IncidentQuery>();

                    if (queryByTicket)
                    {
                        incident = DynamicsClient.GetEntity<IncidentQuery>(response.Message);
                        if (incident.incidentid != null)
                        {
                            incidents.Add(incident);
                            if (incident.customerid_contact != null)
                            {                                
                                contact = DynamicsClient.GetEntityFromProperty<ContactQuery>(incident.customerid_contact);
                            }
                            else if (incident.customerid_account != null)
                            {
                                clientejudirico = true;
                                account = DynamicsClient.GetEntityFromProperty<AccountQuery>(incident.customerid_account);                                
                            }
                        }
                    }
                    else
                    {
                        if (clientejudirico)
                        {                            
                            account = DynamicsClient.GetEntity<AccountQuery>(response.Message);
                            if (account.accountid != null)
                            {
                                incidents = DynamicsClient.GetEntityListFromProperty<IncidentQuery>(account.incident_customer_accounts);
                            }
                        }                        
                        else 
                        {                            
                            contact = DynamicsClient.GetEntity<ContactQuery>(response.Message);
                            if (contact.contactid != null)
                            {
                                incidents = DynamicsClient.GetEntityListFromProperty<IncidentQuery>(contact.incident_customer_contacts);
                            }
                        }
                    }

                    List<Caso> casoResponseList = new List<Caso>();

                    if (clientejudirico)
                    {
                        contacto = new Contacto
                        {
                            Nombres = account.name,
                            TipoDocumento = Dictionaries.GetTipoDocumento(account._efc_tipodocumentoid_value),
                            NumeroDocumento = account.efc_numerodocumento
                        };
                    }
                    else
                    {
                        contacto = new Contacto
                        {
                            Nombres = contact.firstname,
                            Apellidos = contact.lastname,
                            TipoDocumento = Dictionaries.GetTipoDocumento(contact._efc_tipodocumentoid_value),
                            NumeroDocumento = contact.efc_numerodocumento
                        };
                    }

                    foreach (IncidentQuery i in incidents)
                    {
                        casoResponseList.Add(new Caso
                        {
                            GUID = i.incidentid,
                            NumeroRadicado = i.ticketnumber,
                            NumeroCUN = i.efc_numerocun,
                            Contacto = contacto,
                            TipoSolicitud = Dictionaries.GetTipoSolicitud(i._efc_tiposolicitudid_value),
                            EstadoTramite = Dictionaries.GetStateCode(i.statecode),
                            SubEstadoTramite = Dictionaries.GetStatusCode(i.statuscode),
                            DescripcionEstadoTramite = Dictionaries.GetEstadoCUN(i._efc_estadocun_value),
                            TipoQueja = Dictionaries.GetSubTipoSolicitud(i._efc_subtiposolicitudid_value),
                            FechaCreacion = i.createdon,
                            FechaVencimiento = string.IsNullOrEmpty(i.efc_fechavencimiento) ? string.Empty : i.efc_fechavencimiento,
                            FechaRespuesta = string.IsNullOrEmpty(i.efc_fechacierre) ? string.Empty : i.efc_fechacierre
                        });
                    }

                    // Devuelve resultado de la solicitud
                    return casoResponseList;
                }

                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, JsonConvert.DeserializeObject(response.Message));
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Caso / GET", e.Message, TipoAuditoria.ERROR, numerocaso);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("Caso / GET", exception.ToString(), TipoAuditoria.ERROR, numerocaso);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }

        /// <summary>
        /// Obtiene los casos de un cliente natural
        /// </summary>
        /// <param name="tipo">Tipo de identificación</param>
        /// <param name="identificacion">Número de identificación</param>
        /// <param name="numerocaso">Número CUN o ticket</param>
        /// <returns>Devuelve el listado de casos consultado</returns>
        [HttpGet]
        [Route("api/dynamics/caso/puntoatencion/{codigoPAP}/{numerocaso}")]
        [System.Web.Http.Authorize]
        public dynamic GetPuntoAtencion(string codigoPAP, string numerocaso)
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();
                string query;
                bool consultaEspecifica = !String.IsNullOrEmpty(numerocaso) && numerocaso != "null";

                if (consultaEspecifica)
                {
                    query = String.Format("incidents?$filter=ticketnumber eq '{0}' or efc_numerocun eq '{0}'&$expand=efc_puntoatencionid($select=efc_nombre,efc_codigo)",
                        numerocaso);
                }
                else
                {
                    query = String.Format("efc_puntoatencions?$select=efc_nombre,efc_codigo&$filter=endswith(efc_codigo,'{0}')&$expand=efc_efc_puntoatencion_incident_puntoatencionid($select=ticketnumber,efc_numerocun,statecode,statuscode,createdon,efc_fechavencimiento,efc_fechacierre,_efc_tiposolicitudid_value,_efc_motivosolicitudid_value,_efc_estadocun_value,_efc_subtiposolicitudid_value)",
                        codigoPAP);
                }

                // Realiza solicitud al API de CRM Dynamics 365
                DynamicsResponse response = api.Get(query);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    List<IncidentQuery> incidents = new List<IncidentQuery>();
                    PuntoAtencionQuery pap = new PuntoAtencionQuery();

                    if (consultaEspecifica)
                    {
                        IncidentQuery incident = new IncidentQuery();
                        incident = DynamicsClient.GetEntity<IncidentQuery>(response.Message);
                        if (incident.incidentid != null)
                        {
                            incidents.Add(incident);
                            if (incident.efc_puntoatencionid != null)
                            {
                                pap = DynamicsClient.GetEntityFromProperty<PuntoAtencionQuery>(incident.efc_puntoatencionid);
                            }
                        }
                    }
                    else
                    {
                        pap = DynamicsClient.GetEntity<PuntoAtencionQuery>(response.Message);
                        if (pap.efc_puntoatencionid != null && pap.efc_efc_puntoatencion_incident_puntoatencionid != null)
                        {
                            incidents = DynamicsClient.GetEntityListFromProperty<IncidentQuery>(pap.efc_efc_puntoatencion_incident_puntoatencionid);
                        }
                    }

                    List<Caso> casoResponseList = new List<Caso>();

                    foreach (IncidentQuery i in incidents)
                    {
                        casoResponseList.Add(new Caso
                        {
                            GUID = i.incidentid,
                            NumeroRadicado = i.ticketnumber,
                            NumeroCUN = i.efc_numerocun,
                            TipoSolicitud = Dictionaries.GetTipoSolicitud(i._efc_tiposolicitudid_value),
                            EstadoTramite = Dictionaries.GetStateCode(i.statecode),
                            SubEstadoTramite = Dictionaries.GetStatusCode(i.statuscode),
                            DescripcionEstadoTramite = Dictionaries.GetEstadoCUN(i._efc_estadocun_value),
                            TipoQueja = Dictionaries.GetSubTipoSolicitud(i._efc_subtiposolicitudid_value),
                            FechaCreacion = i.createdon,
                            FechaVencimiento = string.IsNullOrEmpty(i.efc_fechavencimiento) ? string.Empty : i.efc_fechavencimiento,
                            FechaRespuesta = string.IsNullOrEmpty(i.efc_fechacierre) ? string.Empty : i.efc_fechacierre,
                            CodigoPuntoAtencion = string.IsNullOrEmpty(pap.efc_codigo) ? string.Empty : pap.efc_codigo,
                            NombrePuntoAtencion = string.IsNullOrEmpty(pap.efc_nombre) ? string.Empty : pap.efc_nombre
                        });
                    }

                    // Devuelve resultado de la solicitud
                    return casoResponseList;
                }

                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, JsonConvert.DeserializeObject(response.Message));
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Caso / GET", e.Message, TipoAuditoria.ERROR, numerocaso);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("Caso / GET", exception.ToString(), TipoAuditoria.ERROR, numerocaso);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }

        /// <summary>
        /// Método POST para crear casos de cliente natural
        /// </summary>
        /// <param name="caso">Caso</param>
        /// <returns>Devuelve resultado de la operación</returns>
        [HttpPost]
        [Route("api/dynamics/caso/natural")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Natural([FromBody]Caso caso)
        {
            LogHandlerCRM.Instance.Log("Caso / POST", string.Empty, TipoAuditoria.REQUEST, caso);

            try
            {
                // Valida tamaño del archivo adjunto
                if (caso.NotaAdjunta != null && caso.NotaAdjunta.TamanioArchivo > Config.GetInt("MaxAttachFileSize"))
                {
                    throw new DynamicsAPIException(String.Format("No se admiten archivos adjuntos de más de {0}MB.", Config.GetInt("MaxAttachFileSize") / 1000000));
                }

                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();

                // Consulta el cliente por tipo y número de documento
                string query = String.Format("contacts?$select=efc_numerodocumento,_efc_tipodocumentoid_value,fullname&$filter=_efc_tipodocumentoid_value eq {0} and efc_numerodocumento eq '{1}'&$count=true",
                    caso.Contacto.TipoDocumentoGUID,
                    caso.Contacto.NumeroDocumento);
                DynamicsResponse clientResponse = api.Get(query);
                // Obtiene la cantidad de registros encontrados
                int count = DynamicsClient.GetCount(clientResponse.Message);

                // Crea instancia del cliente recibido
                Contact newcontact = new Contact
                {
                    contactid = Guid.NewGuid().ToString(),
                    efc_tipodocumentoid_ODATABIND = "/efc_tipodocumentos(" + caso.Contacto.TipoDocumentoGUID + ")",
                    efc_numerodocumento = caso.Contacto.NumeroDocumento,
                    firstname = caso.Contacto.Nombres,
                    lastname = caso.Contacto.Apellidos,
                    efc_numerotelefono = caso.Contacto.TelefonoFijo,
                    mobilephone = caso.Contacto.TelefonoCelular,
                    efc_direccion = caso.Contacto.Direccion,
                    efc_municipioid_ODATABIND = "/efc_municipios(" + caso.Contacto.MunicipioGUID + ")",
                    emailaddress1 = caso.Contacto.CorreoElectronico,
                    efc_autorizadatospersonales = caso.AutorizaTratamientoDatosPersonales,
                    efc_espep = false,
                    efc_clasificacioncontacto = 3 // Preferencial
                };

                // Crea nueva instancia de cliente 
                Contact contact = new Contact();

                // Valida si el cliente ya existe en Dynamics 365
                if (count > 0)
                {
                    // Obtiene datos del cliente ya existente
                    contact = DynamicsClient.GetEntity<Contact>(clientResponse.Message);
                    newcontact.contactid = contact.contactid;
                    newcontact.efc_clasificacioncontacto = contact.efc_clasificacioncontacto;
                    newcontact.efc_espep = contact.efc_espep;
                    // Actualiza datos de contacto
                    DynamicsResponse clientPut = api.Put("contacts", contact.contactid, newcontact);
                }
                else
                {
                    // Crea cliente en Dynamics 365
                    DynamicsResponse clientPost = api.Post("contacts", newcontact, "efc_numerodocumento,_efc_tipodocumentoid_value,fullname");
                    // Se obtienen datos del cliente recién creado
                    contact = DynamicsClient.GetEntity<Contact>(clientPost.Message);
                }

                // Crea instancia del caso
                IncidentContact incident = new IncidentContact
                {
                    incidentid = Guid.NewGuid().ToString(),
                    efc_tiposolicitudid_ODATABIND = "/efc_tiposolicituds(" + caso.TipoSolicitudGUID + ")",
                    efc_tipodocumentoconsultar_ODATABIND = "/efc_tipodocumentos(" + caso.Contacto.TipoDocumentoGUID + ")",
                    efc_numerodocumentoconsultar = caso.Contacto.NumeroDocumento,
                    description = caso.Descripcion,
                    efc_mediorespuesta = caso.MedioRespuesta,
                    caseorigincode = 3, // Origen (Web)
                    casetypecode = 2, // TipoCaso (Problema)
                    statecode = 0, // CodigoEstado (Activo)
                    statuscode = 1, // CodigoEtapa (En Progreso)
                    incidentstagecode = 1, // EtapaCaso (Default)
                    prioritycode = 2, // CodigoPrioridad (Normal)
                    severitycode = 1, // CodigoGravedad (Default)
                    firstresponseslastatus = 1, // SLA Inicial (Default)                    
                    resolvebyslastatus = 1, // EstadoSLA (En Progreso)
                    efc_tipocliente = 5, // Tipo Cliente (Cliente Natural)
                    customerid_contact_ODATABIND = "/contacts(" + contact.contactid + ")"
                };

                // Crea el nuevo caso en Dynamics 365
                DynamicsResponse response = api.Post("incidents", incident, "title,ticketnumber,efc_numerocun");

                // Valida resultado de la operación
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    // Verifica si el caso tiene nota adjunta
                    if (caso.NotaAdjunta != null)
                    {
                        // Obtiene los datos del nuevo caso
                        Incident newIncident = DynamicsClient.GetEntity<Incident>(response.Message);
                        // Crea instancia de la nota adjunta
                        Annotation annotation = new Annotation
                        {
                            documentbody = caso.NotaAdjunta.ContenidoArchivo,
                            filename = caso.NotaAdjunta.NombreArchivo,
                            filesize = caso.NotaAdjunta.TamanioArchivo,
                            mimetype = caso.NotaAdjunta.MimeType,
                            notetext = caso.NotaAdjunta.Texto,
                            objecttypecode = "incident",
                            objectid_incident_ODATABIND = "/incidents(" + newIncident.incidentid + ")"
                        };

                        // Crea la nota adjunta en Dynamics 365
                        DynamicsResponse annotationResponse = api.Post("annotations", annotation);
                    }

                    IncidentQuery createdIncident = DynamicsClient.GetEntity<IncidentQuery>(response.Message);

                    Caso casoCreado = new Caso
                    {
                        GUID = createdIncident.incidentid,
                        NumeroRadicado = createdIncident.ticketnumber,
                        NumeroCUN = createdIncident.efc_numerocun
                    };

                    LogHandlerCRM.Instance.Log("Caso / POST", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                    return Request.CreateResponse(response.StatusCode, casoCreado);
                }
                else
                {
                    LogHandlerCRM.Instance.Log("Caso / POST", response.Message, TipoAuditoria.ERROR, caso);
                    return Request.CreateResponse(response.StatusCode, JsonConvert.DeserializeObject(response.Message));
                }
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Caso / POST", e.Message, TipoAuditoria.ERROR, caso);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("Caso / POST", exception.ToString(), TipoAuditoria.ERROR, caso);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }

        /// <summary>
        /// Método POST para crear casos desde punto de atención
        /// </summary>
        /// <param name="caso">Caso</param>
        /// <returns>Devuelve resultado de la operación</returns>
        [HttpPost]
        [Route("api/dynamics/caso/puntoatencion")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage PuntoAtencion([FromBody]Caso caso)
        {
            LogHandlerCRM.Instance.Log("Caso PAP / POST", string.Empty, TipoAuditoria.REQUEST, caso);

            try
            {
                // Valida tamaño del archivo adjunto
                if (caso.NotaAdjunta != null && caso.NotaAdjunta.TamanioArchivo > Config.GetInt("MaxAttachFileSize"))
                {
                    throw new DynamicsAPIException(String.Format("No se admiten archivos adjuntos de más de {0}MB.", Config.GetInt("MaxAttachFileSize") / 1000000));
                }

                // Consulta especialista de servicio
                Usuario usuario = DaoUsuario.Instance.ConsultarEspecialistaServicio(Dictionaries.GetTipoDocumento(caso.Contacto.TipoDocumentoGUID), caso.Contacto.NumeroDocumento);

                // Valida si existe el especialista de servicio
                if (usuario.USUcodigo == null)
                {
                    throw new DynamicsAPIException("El especialista de servicio no existe.");
                }

                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();

                // Guid de tipo relación 'Cajero' (entidad efc_relacionclientes)
                string TipoRelacionCajero = Config.GetString("TipoRelacionCajero");

                // Consulta contacto por tipo y número de documento, y filtra relaciones con el cliente de tipo cajero.TipoRelacionCajero
                string query = String.Format("contacts?$select=efc_numerodocumento,_efc_tipodocumentoid_value,fullname&$filter=_efc_tipodocumentoid_value eq {0} and efc_numerodocumento eq '{1}'&$count=true&$expand=efc_contact_efc_relacioncliente_contactoid($filter=_efc_tiporelacionid_value eq {2})",
                    caso.Contacto.TipoDocumentoGUID,
                    caso.Contacto.NumeroDocumento,
                    TipoRelacionCajero);
                DynamicsResponse contactResponse = api.Get(query);

                // Obtiene la cantidad de registros encontrados
                int count = DynamicsClient.GetCount(contactResponse.Message);

                // Crea nueva instancia de consulta del cliente 
                ContactQuery contact = new ContactQuery();

                // Valida si el contacto ya existe en Dynamics 365
                if (count > 0)
                {
                    // Obtiene datos del contacto
                    contact = DynamicsClient.GetEntity<ContactQuery>(contactResponse.Message);

                    // Valida si el contacto tiene relación-cliente de tipo Cajero (Valida si el especialista de servicio es un cajero)
                    if (contact.efc_contact_efc_relacioncliente_contactoid.Length == 0)
                    {
                        throw new DynamicsAPIException("El especialista de servicio indicado no está registrado como cajero en Dynamics 365.");
                    }
                }
                else
                {
                    throw new DynamicsAPIException("El especialista de servicio no existe en Dynamics 365.");
                }

                // Crea instancia del caso
                IncidentPAP incident = new IncidentPAP
                {
                    incidentid = Guid.NewGuid().ToString(),
                    efc_tiposolicitudid_ODATABIND = "/efc_tiposolicituds(" + caso.TipoSolicitudGUID + ")",
                    efc_tipodocumentoconsultar_ODATABIND = "/efc_tipodocumentos(" + caso.Contacto.TipoDocumentoGUID + ")",
                    efc_numerodocumentoconsultar = caso.Contacto.NumeroDocumento,
                    description = String.Format("Motivo: {0}\n\nNo. de operación correcto: {1}\n\n{2}", caso.MotivoDescripcion, caso.NumeroOperacionCorrecto, caso.Descripcion),
                    efc_mediorespuesta = caso.MedioRespuesta,
                    efc_numerooperacion = caso.NumeroOperacionErrado,
                    caseorigincode = 3, // Origen (Web)
                    casetypecode = 2, // TipoCaso (Problema)
                    statecode = 0, // CodigoEstado (Activo)
                    statuscode = 1, // CodigoEtapa (En Progreso)
                    incidentstagecode = 1, // EtapaCaso (Default)
                    prioritycode = 2, // CodigoPrioridad (Normal)
                    severitycode = 1, // CodigoGravedad (Default)
                    firstresponseslastatus = 1, // SLA Inicial (Default)
                    resolvebyslastatus = 1, // EstadoSLA (En Progreso)
                    efc_tipocliente = 6, // TipoCliente (Punto de Atención)
                    efc_puntoatencionid_ODATABIND = "/efc_puntoatencions(" + caso.PuntoAtencionGUID + ")",
                    efc_motivosolicitudid_ODATABIND = "/efc_motivosolicituds(" + caso.MotivoGUID + ")",
                    customerid_contact_ODATABIND = "/contacts(" + contact.contactid + ")"
                };

                // Crea el nuevo caso en Dynamics 365
                DynamicsResponse response = api.Post("incidents", incident, "title,ticketnumber,efc_numerocun");

                // Valida resultado de la operación
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    // Verifica si el caso tiene nota adjunta
                    if (caso.NotaAdjunta != null)
                    {
                        // Obtiene los datos del nuevo caso
                        Incident newIncident = DynamicsClient.GetEntity<Incident>(response.Message);
                        // Crea instancia de la nota adjunta
                        Annotation annotation = new Annotation
                        {
                            documentbody = caso.NotaAdjunta.ContenidoArchivo,
                            filename = caso.NotaAdjunta.NombreArchivo,
                            filesize = caso.NotaAdjunta.TamanioArchivo,
                            mimetype = caso.NotaAdjunta.MimeType,
                            notetext = caso.NotaAdjunta.Texto,
                            objecttypecode = "incident",
                            objectid_incident_ODATABIND = "/incidents(" + newIncident.incidentid + ")"
                        };

                        // Crea la nota adjunta en Dynamics 365
                        DynamicsResponse annotationResponse = api.Post("annotations", annotation);
                    }

                    IncidentQuery createdIncident = DynamicsClient.GetEntity<IncidentQuery>(response.Message);

                    Caso casoCreado = new Caso
                    {
                        GUID = createdIncident.incidentid,
                        NumeroRadicado = createdIncident.ticketnumber,
                        NumeroCUN = createdIncident.efc_numerocun
                    };

                    LogHandlerCRM.Instance.Log("Caso PAP / POST", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                    return Request.CreateResponse(response.StatusCode, casoCreado);
                }
                else
                {
                    LogHandlerCRM.Instance.Log("Caso PAP / POST", response.Message, TipoAuditoria.ERROR, caso);
                    return Request.CreateResponse(response.StatusCode, JsonConvert.DeserializeObject(response.Message));
                }
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Caso PAP / POST", e.Message, TipoAuditoria.ERROR, caso);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("Caso PAP / POST", exception.ToString(), TipoAuditoria.ERROR, caso);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }

        /// <summary>
        /// Método PUT que actualiza un campo especifico de un caso
        /// </summary>
        /// <param name="guid">Identificador del caso</param>
        /// <param name="field">Campo a actualizar</param>
        /// <param name="newvalue">Valor de actualización</param>
        /// <returns>Devuelve resultado de la operación</returns>
        [HttpPut]
        [Route("api/dynamics/caso/{guid}/{field}/{newvalue}")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Put(string guid, string field, string newvalue)
        {
            string param = guid + " " + field + " " + newvalue;
            LogHandlerCRM.Instance.Log("Caso / PUT", string.Empty, TipoAuditoria.REQUEST, param);

            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();

                // Operación PUT
                DynamicsResponse response = api.Put("incidents", guid, field, newvalue);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                {
                    LogHandlerCRM.Instance.Log("Caso / PUT", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    LogHandlerCRM.Instance.Log("Caso / PUT", response.Message, TipoAuditoria.ERROR, param);
                    return Request.CreateResponse(response.StatusCode, JsonConvert.DeserializeObject(response.Message));
                }
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Caso / PUT", e.Message, TipoAuditoria.ERROR, param);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Método PUT aque actualiza un caso completo
        /// </summary>
        /// <param name="guid">Identificador del caso</param>
        /// <param name="field">Campo a actualizar</param>
        /// <returns>Devuelve resultado de la operación</returns>
        [HttpPut]
        [Route("api/dynamics/caso/{guid}")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Put([FromBody]Incident incident, string guid)
        {
            LogHandlerCRM.Instance.Log("Caso / PUT", string.Empty, TipoAuditoria.REQUEST, incident);

            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();

                // Operación PUT                
                DynamicsResponse response = api.Put("incidents", guid, incident);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                {
                    LogHandlerCRM.Instance.Log("Caso / PUT", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    LogHandlerCRM.Instance.Log("Caso / PUT", response.Message, TipoAuditoria.ERROR, incident);
                    return Request.CreateResponse(response.StatusCode, JsonConvert.DeserializeObject(response.Message));
                }
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Caso / PUT", e.Message, TipoAuditoria.ERROR, incident);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Método DELETE que elimina un caso
        /// </summary>        
        /// <param name="guid">Identificador del caso</param>
        /// <returns>Devuelve resultado de la operación</returns>
        [HttpDelete]
        [Route("api/dynamics/caso/{guid}")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Delete(string guid)
        {
            LogHandlerCRM.Instance.Log("Caso / DELETE", string.Empty, TipoAuditoria.REQUEST, guid);

            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();

                // Operación DELETE
                DynamicsResponse response = api.Delete("incidents", guid);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                {
                    LogHandlerCRM.Instance.Log("Caso / DELETE", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    LogHandlerCRM.Instance.Log("Caso / DELETE", response.Message, TipoAuditoria.ERROR, guid);
                    return Request.CreateResponse(response.StatusCode, JsonConvert.DeserializeObject(response.Message));
                }
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Caso / DELETE", e.Message, TipoAuditoria.ERROR, guid);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
