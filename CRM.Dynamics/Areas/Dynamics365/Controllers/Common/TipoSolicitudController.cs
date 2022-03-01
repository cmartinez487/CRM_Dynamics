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
using WebAPI = CRM.Dynamics.Entidades.Tipos;

namespace CRM.Dynamics.Areas.Dynamics365.Controllers.Common
{
    [Route("api/dynamics/tiposolicitud")]
    public class TipoSolicitudController : ApiController
    {
        /// <summary>
        /// Obtiene el listado de tipos de solicitud
        /// </summary>        
        /// <returns>Listado de tipos de solicitud</returns>
        [HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Get()
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();
                // Realiza solicitud al API de CRM Dynamics 365
                DynamicsResponse response = api.Get("efc_tiposolicituds?$select=efc_codigo,efc_nombre,efc_tiposolicitudid");

                // Obtiene el listado de tipos de solicitud
                List<CrmAPI.TipoSolicitud> tiposSolicitud = DynamicsClient.GetEntityList<CrmAPI.TipoSolicitud>(response.Message);

                // Adapta respuesta a tipos WebAPI
                List<WebAPI.TipoSolicitud> responseList = new List<WebAPI.TipoSolicitud>();
                foreach (CrmAPI.TipoSolicitud i in tiposSolicitud)
                {
                    responseList.Add(new WebAPI.TipoSolicitud
                    {
                        GUID = i.efc_tiposolicitudid,
                        Codigo = i.efc_codigo,
                        Nombre = i.efc_nombre
                    });
                }

                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, responseList);
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("TipoSolicitud / GET", e.Message, TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("TipoSolicitud / GET", exception.ToString(), TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
