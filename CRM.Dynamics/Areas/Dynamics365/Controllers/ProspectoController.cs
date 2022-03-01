using CRM.Dynamics.APIClient;
using CRM.Dynamics.APIClient.Entities;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.Entidades.Clientes;
using CRM.Dynamics.WebApi.Handlers;
using CRM.Dynamics.WebApi.Resource;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRM.Dynamics.WebApi.Areas.Dynamics365.Controllers
{
    public class ProspectoController : ApiController
    {
        /// <summary>
        /// Método POST para crear prospectos
        /// </summary>
        /// <param name="prospecto">Prospecto</param>
        /// <returns>Devuelve resultado de la operación</returns>
        [HttpPost]
        [Route("api/dynamics/prospecto")]
        [Authorize]
        public HttpResponseMessage Post([FromBody]Prospecto prospecto)
        {
            LogHandlerCRM.Instance.Log("Prospecto / POST", string.Empty, TipoAuditoria.REQUEST, prospecto);

            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();

                // Consulta el prospecto por tipo y número de documento
                string query = string.Format("leads?$select=efc_numerodocumento,_efc_tipodocumentoid_value,fullname&$filter=_efc_tipodocumentoid_value eq {0} and efc_numerodocumento eq '{1}'&$count=true",
                    prospecto.TipoDocumentoGUID,
                    prospecto.NumeroDocumento);

                DynamicsResponse leadGet = api.Get(query);
                // Obtiene la cantidad de registros encontrados
                int count = DynamicsClient.GetCount(leadGet.Message);

                // Valida si el propecto ya existe en Dynamics 365
                if (count == 0)
                {
                    // Crea instancia del prospecto
                    Lead newlead = new Lead
                    {
                        leadid = Guid.NewGuid().ToString(),
                        efc_tipodocumentoid_ODATABIND = "/efc_tipodocumentos(" + prospecto.TipoDocumentoGUID + ")",
                        efc_numerodocumento = prospecto.NumeroDocumento,
                        firstname = prospecto.Nombres,
                        lastname = prospecto.Apellidos,
                        efc_numerotelefono = prospecto.TelefonoFijo,
                        mobilephone = prospecto.TelefonoCelular,
                        efc_direccion = prospecto.Direccion,
                        efc_municipioid_ODATABIND = "/efc_municipios(" + prospecto.MunicipioGUID + ")",
                        emailaddress1 = prospecto.CorreoElectronico,
                        efc_digitoverificacion = prospecto.DigitoVerificacion,
                        companyname = prospecto.RazonSocial,
                        efc_tipocliente = prospecto.TipoProspecto
                    };

                    // Crea prospecto en Dynamics 365
                    DynamicsResponse leadPost = api.Post("leads", newlead);

                    if (leadPost.StatusCode == HttpStatusCode.NoContent || leadPost.StatusCode == HttpStatusCode.Created)
                    {
                        LogHandlerCRM.Instance.Log("Prospecto / POST", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, prospecto);
                        return Request.CreateResponse(HttpStatusCode.OK, DynamicsClient.BuildJsonOK(ResourceMensaje.SuccessMessage));
                    }
                    else
                    {
                        LogHandlerCRM.Instance.Log("Prospecto / POST", leadPost.Message, TipoAuditoria.ERROR, prospecto);
                        return Request.CreateResponse(HttpStatusCode.BadRequest, JsonConvert.DeserializeObject(leadPost.Message));
                    }
                }
                else
                {
                    LogHandlerCRM.Instance.Log("Prospecto / POST", "El prospecto ya existe.", TipoAuditoria.ERROR, prospecto);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, DynamicsClient.BuildJsonError("El prospecto ya existe."));
                }
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Prospecto / POST", e.Message, TipoAuditoria.ERROR, prospecto);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("Prospecto / POST", exception.ToString(), TipoAuditoria.ERROR, prospecto);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
