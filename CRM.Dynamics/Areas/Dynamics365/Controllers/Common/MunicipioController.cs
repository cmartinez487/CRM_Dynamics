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
    public class MunicipioController : ApiController
    {
        /// <summary>
        /// Obtiene el listado de municipio que conicidan con los caracteres enviados
        /// </summary>        
        /// <returns>Listado de municipio</returns>
        [HttpGet]
        [Route("api/dynamics/municipio/{name}")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Get(string name)
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();
                // Realiza solicitud al API de CRM Dynamics 365
                DynamicsResponse response = api.Get("efc_municipios?$select=efc_codigo,efc_nombre,efc_municipioid&$expand=efc_departamentoid($select=efc_codigo,efc_nombre),efc_pais($select=efc_codigo,efc_nombre)&$filter=startswith(efc_nombre,'" + name + "')&$top=15");

                // Obtiene el listado de municipios
                List<CrmAPI.Municipio> municipios = DynamicsClient.GetEntityList<CrmAPI.Municipio>(response.Message);

                // Adapta respuesta a tipos WebAPI
                List<WebAPI.Municipio> responseList = new List<WebAPI.Municipio>();
                foreach (CrmAPI.Municipio i in municipios)
                {
                    responseList.Add(new WebAPI.Municipio
                    {
                        GUID = i.efc_municipioid,
                        Codigo = i.efc_codigo,
                        Nombre = i.efc_nombre,
                        Departamento = new WebAPI.Departamento
                        {
                            GUID = i.efc_departamentoid.efc_departamentoid,
                            Codigo = i.efc_departamentoid.efc_codigo,
                            Nombre = i.efc_departamentoid.efc_nombre
                        },
                        Pais = new WebAPI.Pais
                        {
                            GUID = i.efc_pais.efc_paisid,
                            Codigo = i.efc_pais.efc_codigo,
                            Nombre = i.efc_pais.efc_nombre
                        }
                    });
                }

                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, responseList);
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Municipio / GET", e.Message, TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("Municipio / GET", exception.ToString(), TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }

        /// <summary>
        /// Obtiene el listado de municipio que conicidan con los caracteres enviados
        /// </summary>        
        /// <returns>Listado de municipio</returns>
        [HttpGet]
        [Route("api/dynamics/municipio/getbyid/{guid}")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage GetById(string guid)
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();
                // Realiza solicitud al API de CRM Dynamics 365                
                DynamicsResponse response = api.Get("efc_municipios(" + guid + ")?$select=efc_codigo,efc_nombre,efc_municipioid&$expand=efc_departamentoid($select=efc_codigo,efc_nombre),efc_pais($select=efc_codigo,efc_nombre)");

                // Obtiene el listado de municipios
                CrmAPI.Municipio i = DynamicsClient.GetEntity<CrmAPI.Municipio>(response.Message);

                // Adapta respuesta a tipos WebAPI
                List<WebAPI.Municipio> responseList = new List<WebAPI.Municipio>();
                if (i != null)
                {
                    responseList.Add(new WebAPI.Municipio
                    {
                        GUID = i.efc_municipioid,
                        Codigo = i.efc_codigo,
                        Nombre = i.efc_nombre,
                        Departamento = new WebAPI.Departamento
                        {
                            GUID = i.efc_departamentoid.efc_departamentoid,
                            Codigo = i.efc_departamentoid.efc_codigo,
                            Nombre = i.efc_departamentoid.efc_nombre
                        },
                        Pais = new WebAPI.Pais
                        {
                            GUID = i.efc_pais.efc_paisid,
                            Codigo = i.efc_pais.efc_codigo,
                            Nombre = i.efc_pais.efc_nombre
                        }
                    });
                }

                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, responseList);
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Municipio / GETBYID", e.Message, TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("Municipio / GETBYID", exception.ToString(), TipoAuditoria.ERROR);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
