using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.AccesoDatos.ClientesCorporativos;
using CRM.Dynamics.WebApi.Resource;
using CRM.Dynamics.WebApi.Handlers;
using Newtonsoft.Json;

namespace CRM.Dynamics.WebApi.Controllers
{
    public class ClientesCorporativosController : ApiController
    {
        /// <summary>
        /// Metodo GET para consulta de Clientes Corporativos
        /// </summary>
        /// <returns>devuelve un listado de clientes</returns>
        // GET: api/ClientesCorporativos
        [Authorize]
        public dynamic Get(string tipoid, string identificacion)
        {
            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            ClienteCorportativo cliente = new ClienteCorportativo();
            cliente.tipoid = tipoid;
            cliente.identificacion = identificacion;

            LogHandlerCRM.Instance.Log("ClientesCorporativos / Get", string.Empty, TipoAuditoria.REQUEST, cliente);

            try
            {                
                List<ClienteCorportativo> Clientes = DaoClientesCorporativos.Instance.ConsultarClienteCorporativo(tipoid, identificacion);
                LogHandlerCRM.Instance.Log("ClientesCorporativos / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, Clientes);

                return Clientes;
            }
            catch (Exception e)
            {
                Auditoria.Api = "ClienteCorportativo / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Parametros = JsonConvert.SerializeObject(cliente, Formatting.Indented);

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }

        }

		/// <summary>
		/// Metodo POST para la insercion de nuevos Clientes
		/// </summary>
		/// <param name="cliente">Modelo</param>
		// POST: api/ClientesCorporativos
		[Authorize]
		public HttpResponseMessage Post([FromBody]ClienteCorportativo cliente)
        {
            LogHandlerCRM.Instance.Log("ClienteCorportativo / Post", string.Empty, TipoAuditoria.REQUEST, cliente);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "ClienteCorportativo / Post";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(cliente, Formatting.Indented);

            try
            {
				if ((cliente.tipoid != null && cliente.tipoid != "") && (cliente.identificacion != null && cliente.identificacion != ""))
				{                    
                    DaoClientesCorporativos.Instance.InsertarClienteCorporativo(cliente);
                    LogHandlerCRM.Instance.Log("ClienteCorportativo / Post", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);

                    return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
				}
                
                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorClieRes1.ToString());
                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorClieRes1.ToString());
                }
            }
            catch(Exception e)
            {
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - "+e.Message.ToString());
            }

        }

		/// <summary>
		/// Metodo PUT para la actualizacion de Clientes
		/// </summary>
		/// <param name="cliente">Modelo</param>
		// PUT: api/ClientesCorporativos/5
		[Authorize]
		public HttpResponseMessage Put([FromBody]ClienteCorportativo cliente)
        {
            LogHandlerCRM.Instance.Log("ClienteCorportativo / Put", string.Empty, TipoAuditoria.REQUEST, cliente);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "ClienteCorportativo / Put";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(cliente, Formatting.Indented);

            try
            {
				if ((cliente.tipoid != null && cliente.tipoid != "") && (cliente.identificacion != null && cliente.identificacion != ""))
				{                    
                    DaoClientesCorporativos.Instance.ActualizarClienteCorporativo(cliente);
                    LogHandlerCRM.Instance.Log("ClienteCorportativo / Put", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);

                    return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
                }
                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorClieRes2.ToString());
                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorClieRes2.ToString());
                }
            }
            catch(Exception e)
            {
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message);
            }

        }
    }
}
