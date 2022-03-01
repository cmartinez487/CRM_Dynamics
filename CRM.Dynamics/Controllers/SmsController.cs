using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Dynamics.WebApi.Resource;
using CRM.Dynamics.WebApi.Handlers;
using CRM.Dynamics.AccesoDatos.SMS;
using CRM.Dynamics.Entidades.Sms;
using CRM.Dynamics.Entidades;
using Newtonsoft.Json;

namespace CRM.Dynamics.WebApi.Controllers
{
    public class SmsController : ApiController
    {
        /// <summary>
        /// Consulta de mensajes por numero de ceuluar
        /// </summary>
        /// <returns>devuelve un listado de clientes</returns>
        // GET: api/ConsultarSms
        [Authorize]
        public dynamic Get(Int64 Celular)
        {
            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            SmsResponse sms = new SmsResponse();
            sms.Celular = Celular;
            LogHandlerCRM.Instance.Log("ConsultarSms / Get", string.Empty, TipoAuditoria.REQUEST, sms);

            try
            {                
                List<SmsResponse> ListSMS = DaoSMS.Instance.ConsultarSms(Celular);
                LogHandlerCRM.Instance.Log("ConsultarSms / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, ListSMS);                

                return ListSMS;
            }
            catch (Exception e)
            {
                Auditoria.Api = "Sms / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Parametros = JsonConvert.SerializeObject(sms, Formatting.Indented);

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }

        }

        /// <summary>
        /// Metodo POST para la insercion de nuevos Sms
        /// </summary>
        /// <param name="sms">Modelo</param>
        // POST: api/Sms
        [Authorize]
        public HttpResponseMessage Post([FromBody]SmsRequest sms)
        {
            LogHandlerCRM.Instance.Log("Sms / Post", string.Empty, TipoAuditoria.REQUEST, sms);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "Sms / Post";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(sms, Formatting.Indented);

            try
            {
                if (sms.TipoIdentificacion != "" && sms.Identificacion != 0 && sms.Celular  != 0)
                {                    
                    DaoSMS.Instance.InsertarSms(sms);
                    LogHandlerCRM.Instance.Log("Sms / Post", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);

                    return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
                }

                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorClieRes1.ToString());
                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorClieRes1.ToString());
                }
            }
            catch (Exception e)
            {
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }

        }
    }
}