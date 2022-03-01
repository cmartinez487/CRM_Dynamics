using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Dynamics.Entidades.RedContratista;
using CRM.Dynamics.AccesoDatos.RedesContratistas;
using CRM.Dynamics.WebApi.Resource;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.WebApi.Handlers;
using Newtonsoft.Json;

namespace CRM.Dynamics.WebApi.Controllers.RedesContratistas
{
    public class RedesContratistasController : ApiController
    {
        /// <summary>
        /// Metodo GET para consulta de Redes de Contratistas
        /// </summary>
        /// <returns>devuelve un listado de redes</returns>
        // GET: api/RedContratista
        [Authorize]
		public dynamic Get(string REDCONId)
        {
            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            RedContratista red = new RedContratista();
            red.REDCONId = REDCONId;
            LogHandlerCRM.Instance.Log("RedContratista / Get", string.Empty, TipoAuditoria.REQUEST, red);

            try
            {                
                List<RedContratista> redes = DaoRedContratista.Instance.ConsultarRedesContratistas(REDCONId);
                LogHandlerCRM.Instance.Log("RedContratista / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, redes);

                return redes;
            }
            catch (Exception e)
            {
                Auditoria.Api = "RedContratista / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Parametros = JsonConvert.SerializeObject(red, Formatting.Indented);

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

		/// <summary>
		/// Metodo POST para la insercion de nuevas Redes
		/// </summary>
		/// <param name="red">Modelo</param>
		// POST: api/RedContratista
		[Authorize]
		public HttpResponseMessage Post([FromBody]RedContratista red)
        {
            LogHandlerCRM.Instance.Log("RedContratista / Post", string.Empty, TipoAuditoria.REQUEST, red);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "RedContratista / Post";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(red, Formatting.Indented);

            bool respuesta;
			try
            {
                if ((red.REDCONId != null && red.REDCONId != "")
					&& (red.REDCONIdContratista != null && red.REDCONIdContratista != "") && (red.REDCONTipoIdContratista != null && red.REDCONTipoIdContratista != "")
					&& (red.REDCONIdRed != null && red.REDCONIdRed != "") && (red.REDCONTipoIdRed != null && red.REDCONTipoIdRed != ""))
                {                    
                    respuesta = DaoRedContratista.Instance.InsertarRedContratista(red);

					if (respuesta == true)
					{
                        LogHandlerCRM.Instance.Log("RedContratista / Post", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                        return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
                    }
					else
					{
                        Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorRedContNR.ToString());
                        ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                        return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorRedContNR.ToString());
					}
				}
                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorRedContIN.ToString());
                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorRedContIN.ToString());
                }
            }
            catch (Exception e)
            {
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

		/// <summary>
		/// Metodo POST para la actualizacion de nuevas Redes
		/// </summary>
		/// <param name="redes">Modelo</param>
		// PUT: api/RedContratista/5
		[Authorize]
		public HttpResponseMessage Put([FromBody]RedContratista red)
        {
            LogHandlerCRM.Instance.Log("RedContratista / Put", string.Empty, TipoAuditoria.REQUEST, red);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "RedContratista / Put";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(red, Formatting.Indented);

            bool respuesta;
			try
			{
				if ((red.REDCONId != null && red.REDCONId != "")
					&& (red.REDCONIdContratista != null && red.REDCONIdContratista != "") 
                    && (red.REDCONTipoIdContratista != null && red.REDCONTipoIdContratista != "")
					&& (red.REDCONIdRed != null && red.REDCONIdRed != "") 
                    && (red.REDCONTipoIdRed != null && red.REDCONTipoIdRed != ""))
				{                    
                    respuesta = DaoRedContratista.Instance.ActualizarRedContratista(red);                    

                    if (respuesta == true)
					{
                        LogHandlerCRM.Instance.Log("RedContratista / Put", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                        return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
                    }
                    else
                    {
                        Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorRedContNR.ToString());
                        ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                        return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorRedContNR.ToString());
                    }
                }
                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorRedContIN.ToString());
                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorRedContIN.ToString());
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
