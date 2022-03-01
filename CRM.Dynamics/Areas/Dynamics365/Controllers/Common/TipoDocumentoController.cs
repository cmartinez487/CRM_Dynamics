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
    public class TipoDocumentoController : ApiController
    {
        /// <summary>
        /// Obtiene el listado de tipos de documento
        /// </summary>        
        /// <returns>Listado de tipos de documento</returns>
        [HttpGet]
        [Route("api/dynamics/tipodocumento")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Get()
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();
                // Realiza solicitud al API de CRM Dynamics 365
                DynamicsResponse response = api.Get("efc_tipodocumentos?$select=efc_codigo,efc_nombre,efc_aplicaparacliente,efc_tipodocumentoid");

                // Obtiene el listado de tipo de documentos
                List<CrmAPI.TipoDocumento> tiposDocumento = DynamicsClient.GetEntityList<CrmAPI.TipoDocumento>(response.Message);

                // Adapta respuesta a tipos WebAPI
                List<WebAPI.TipoDocumento> responseList = new List<WebAPI.TipoDocumento>();
                foreach (CrmAPI.TipoDocumento i in tiposDocumento)
                {
                    responseList.Add(new WebAPI.TipoDocumento
                    {
                        GUID = i.efc_tipodocumentoid,
                        Codigo = i.efc_codigo,
                        Nombre = i.efc_nombre
                    });
                }

                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, responseList);
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("TipoDocumento / GET", e.Message, TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("TipoDocumento / GET", exception.ToString(), TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
