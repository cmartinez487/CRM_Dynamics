using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Dynamics.Entidades.Colaboradores;
using CRM.Dynamics.AccesoDatos.Colaboradores;
using CRM.Dynamics.WebApi.Resource;
using CRM.Dynamics.WebApi.Handlers;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.Comun;
using Newtonsoft.Json;

namespace CRM.Dynamics.WebApi.Controllers.Colaboradores
{
    public class RepresentantesController : ApiController
    {
        /// <summary>
        /// Metodo GET para consulta de Representantes legales
        /// </summary>
        /// <returns>devuelve un listado de Representantes</returns>
        // GET: api/ClientesCorporativos
        [Authorize]
		public dynamic Get(string tipoid, string identificacion)
        {
            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            RepresentanteLegal representante = new RepresentanteLegal();
            representante.REPLEGtipoid = tipoid;
            representante.REPLEGidentificacion = identificacion;

            LogHandlerCRM.Instance.Log("Representante / Get", string.Empty, TipoAuditoria.REQUEST, representante);

            try
            {
                List<RepresentanteLegal> Representante = DaoRepresentateLegal.Instance.ConsultarRepresentantes(tipoid, identificacion);
                LogHandlerCRM.Instance.Log("Representante / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, Representante);

                return Representante;
            }
            catch (Exception e)
            {
                Auditoria.Api = "Representante / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Parametros = JsonConvert.SerializeObject(representante, Formatting.Indented);

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

		/// <summary>
		/// Metodo POST para la insercion de nuevos Representante
		/// </summary>
		/// <param name="representante">Modelo</param>
		// POST: api/Representantes
		[Authorize]
		public HttpResponseMessage Post([FromBody]RepresentanteLegal representante)
        {
            LogHandlerCRM.Instance.Log("Representante / Post", string.Empty, TipoAuditoria.REQUEST, representante);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "Representante / Post";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(representante, Formatting.Indented);            

            try
            {
                if ((representante.REPLEGtipoid != null && representante.REPLEGtipoid != "") && (representante.REPLEGidentificacion != null && representante.REPLEGidentificacion != ""))
                {                    
                    DaoRepresentateLegal.Instance.InsertarRepresentante(representante);
                    LogHandlerCRM.Instance.Log("Representante / Post", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
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
                Auditoria.Parametros = JsonConvert.SerializeObject(representante, Formatting.Indented);
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

		/// <summary>
		/// Metodo PUT para la actualizacion de Clientes
		/// </summary>
		/// <param name="cliente">Modelo</param>
		// PUT: api/ClientesCorporativos/5
		[Authorize]
		public HttpResponseMessage Put([FromBody]RepresentanteLegal representante)
        {
            LogHandlerCRM.Instance.Log("Representante / Put", string.Empty, TipoAuditoria.REQUEST, representante);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "Representante / Put";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(representante, Formatting.Indented);            

            try
            {
                if ((representante.REPLEGtipoid != null && representante.REPLEGtipoid != "" ) && (representante.REPLEGidentificacion != null && representante.REPLEGidentificacion != ""))
                {                    
                    DaoRepresentateLegal.Instance.ActualizarrRepresentante(representante);
                    LogHandlerCRM.Instance.Log("Representante / Put", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                    return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
                }
                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorClieRes2.ToString());

                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorClieRes2.ToString());
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
