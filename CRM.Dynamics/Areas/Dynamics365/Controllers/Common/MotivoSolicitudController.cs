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
    [Route("api/dynamics/motivosolicitud")]
    public class MotivoSolicitudController : ApiController
    {
        /// <summary>
        /// Obtiene el listado de motivos de solicitud
        /// </summary>        
        /// <returns>Listado de motivios de solicitud</returns>
        [HttpGet]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Get()
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();
                // Realiza solicitud al API de CRM Dynamics 365
                DynamicsResponse response = api.Get("efc_motivosolicituds?$select=efc_codigo,efc_nombre");

                // Obtiene el listado de motivos
                List<CrmAPI.MotivoSolicitud> motivosSolicitud = DynamicsClient.GetEntityList<CrmAPI.MotivoSolicitud>(response.Message);

                // Adapta respuesta a tipos WebAPI
                List<WebAPI.MotivoSolicitud> responseList = new List<WebAPI.MotivoSolicitud>();
                foreach (CrmAPI.MotivoSolicitud i in motivosSolicitud)
                {
                    responseList.Add(new WebAPI.MotivoSolicitud
                    {
                        GUID = i.efc_motivosolicitudid,
                        Codigo = i.efc_codigo,
                        Nombre = i.efc_nombre
                    });
                }

                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, responseList);
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("MotivoSolicitud / GET", e.Message, TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("MotivoSolicitud / GET", exception.ToString(), TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
