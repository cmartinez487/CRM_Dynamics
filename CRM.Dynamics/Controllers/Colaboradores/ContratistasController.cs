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
using Newtonsoft.Json;

namespace CRM.Dynamics.WebApi.Controllers.Colaboradores
{
    public class ContratistasController : ApiController
    {
        /// <summary>
        /// Metodo GET para consulta de Contratistas
        /// </summary>
        /// <returns>devuelve un listado de Contratistas</returns>
        // GET: api/ClientesCorporativos
        [Authorize]
		public dynamic Get(string tipoid, string identificacion)
        {
            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Contratistas contratista = new Contratistas();
            contratista.CONtipoid = tipoid;
            contratista.CONidentificacion = identificacion;

            LogHandlerCRM.Instance.Log("Contratista / Get", string.Empty, TipoAuditoria.REQUEST, contratista);

            try
            {
                List<Contratistas> Contratistas = DaoContratistas.Instance.ConsultarContratistas(tipoid, identificacion);
                LogHandlerCRM.Instance.Log("Contratista / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, Contratistas);

                return Contratistas;
            }
            catch (Exception e)
            {
                Auditoria.Api = "Contratista / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Parametros = JsonConvert.SerializeObject(contratista, Formatting.Indented);

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

		/// <summary>
		/// Metodo POST para la insercion de nuevos Contratistas
		/// </summary>
		/// <param name="contratista">Modelo</param>
		// POST: api/Contratistas
		[Authorize]
		public HttpResponseMessage Post([FromBody]Contratistas contratista)
        {
            LogHandlerCRM.Instance.Log("Contratista / Post", string.Empty, TipoAuditoria.REQUEST, contratista);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "Contratista / Post";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(contratista, Formatting.Indented);

            bool respuesta;
			try
            {
				if ((contratista.CONtipoid != null && contratista.CONtipoid !="") 
					&& (contratista.CONidentificacion != null && contratista.CONidentificacion != "") 
					&& (contratista.CONidRepresentanteLegal != null && contratista.CONidRepresentanteLegal != "")
					&& (contratista.CONtipoidRepresentanteLegal != null && contratista.CONtipoidRepresentanteLegal != ""))
				{                    
                    respuesta = DaoContratistas.Instance.InsertarContratista(contratista);

					if (respuesta == true)
					{
                        LogHandlerCRM.Instance.Log("Contratista / Post", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);                        
                        return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
					}
					else
					{
                        Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorContratistaNR.ToString());
                        ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                        return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorContratistaNR.ToString());
					}
                }
                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorContratistaIN.ToString());
                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorContratistaIN.ToString());
                }
               
            }
            catch (Exception e)
            {
                
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message);
            }
        }

		/// <summary>
		/// Metodo PUT para actualizacion de Contratistas
		/// </summary>
		/// <param name="contratista">Modelo</param>
		// PUT: api/Contratistas/5
		[Authorize]
		public HttpResponseMessage Put([FromBody]Contratistas contratista)
        {
            LogHandlerCRM.Instance.Log("Contratista / Put", string.Empty, TipoAuditoria.REQUEST, contratista);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "Contratista / Put";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(contratista, Formatting.Indented);

            bool respuesta;
			try
            {
				if ((contratista.CONtipoid != null && contratista.CONtipoid != "")
					   && (contratista.CONidentificacion != null && contratista.CONidentificacion != "")
					   && (contratista.CONidRepresentanteLegal != null && contratista.CONidRepresentanteLegal != "")
					   && (contratista.CONtipoidRepresentanteLegal != null && contratista.CONtipoidRepresentanteLegal != ""))
				{                    
                    respuesta = DaoContratistas.Instance.ActualizarContratista(contratista);

					if (respuesta == true)
					{
                        LogHandlerCRM.Instance.Log("Contratista / Put", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                        return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
					}
                    else
                    {
                        Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorContratistaNR.ToString());
                        ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                        return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorContratistaNR.ToString());
                    }
                }
                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorContratistaIN.ToString());
                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorContratistaIN.ToString());
                }
            }
            catch (Exception e)
            {
                
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message);
            }
        }
    }
}
