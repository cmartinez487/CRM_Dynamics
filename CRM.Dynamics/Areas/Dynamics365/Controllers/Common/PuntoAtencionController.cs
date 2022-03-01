using CRM.Dynamics.APIClient;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.WebApi.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrmAPI = CRM.Dynamics.APIClient.Entities.EFC;
using WebAPI = CRM.Dynamics.Entidades;

namespace CRM.Dynamics.Areas.Dynamics365.Controllers.Common
{
    public class PuntoAtencionController : ApiController
    {
        /// <summary>
        /// Obtiene los puntos de atención que coincidan por código
        /// </summary>        
        /// <returns>Puntos de atención</returns>
        [HttpGet]
        [Route("api/dynamics/puntoatencion/{code}")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Get(string code)
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();
                // Realiza solicitud al API de CRM Dynamics 365
                DynamicsResponse response = api.Get("efc_puntoatencions?$select=efc_codigo,efc_nombre,efc_direccion,emailaddress,efc_telefonofijo,efc_celular&$filter=endswith(efc_codigo,'" + code + "')&$expand=efc_municipioid($select=efc_codigo,efc_nombre)");

                // Obtiene el listado de puntos de atención
                List<CrmAPI.PuntoAtencionQuery> puntosAtencion = DynamicsClient.GetEntityList<CrmAPI.PuntoAtencionQuery>(response.Message);

                // Adapta respuesta a tipos WebAPI
                List<WebAPI.PAP.PuntoAtencion> responseList = new List<WebAPI.PAP.PuntoAtencion>();
                foreach (CrmAPI.PuntoAtencionQuery i in puntosAtencion)
                {
                    var pap = new WebAPI.PAP.PuntoAtencion
                    {
                        GUID = i.efc_puntoatencionid,
                        Codigo = i.efc_codigo,
                        Nombre = i.efc_nombre,
                        Direccion = i.efc_direccion,
                        Correo = i.emailaddress,
                        TelefonoFijo = i.efc_telefonofijo,
                        Celular = i.efc_celular
                    };

                    if (i.efc_municipioid != null)
                    {
                        pap.Municipio = new WebAPI.Tipos.Municipio
                        {
                            GUID = i.efc_municipioid.efc_municipioid,
                            Codigo = i.efc_municipioid.efc_codigo,
                            Nombre = i.efc_municipioid.efc_nombre
                        };
                    }
                    responseList.Add(pap);
                }

                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, responseList);
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("PuntoAtencion / GET", e.Message, TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("PuntoAtencion / GET", exception.ToString(), TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
