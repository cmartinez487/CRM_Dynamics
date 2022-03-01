using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.AccesoDatos.ClientesNaturales;
using CRM.Dynamics.WebApi.Resource;
using CRM.Dynamics.WebApi.Handlers;
using Newtonsoft.Json;

namespace CRM.Dynamics.WebApi.Controllers
{
    public class ClientesNaturalesController : ApiController
    {
        /// <summary>
        /// Metodo GET para consulta de Clientes Naturales
        /// </summary>
        /// <returns>devuelve un listado de clientes</returns>
        // GET: api/ConsultarClienteNatural
        [Authorize]
        public dynamic Get(string tipodocumento, Int64? numeroIdentificacion)
        {
            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            ClienteNatural cliente = new ClienteNatural();
            cliente.Tipodocumento = tipodocumento;
            cliente.NumeroIdentificacion = numeroIdentificacion;

            LogHandlerCRM.Instance.Log("ConsultarClienteNatural / Get", string.Empty, TipoAuditoria.REQUEST, cliente);

            try
            {                
                List<ClienteNatural> Clientes = DaoClientesNaturales.Instance.ConsultarClienteNatural(tipodocumento, numeroIdentificacion);
                LogHandlerCRM.Instance.Log("ConsultarClienteNatural / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, Clientes);

                return Clientes;
            }
            catch (Exception e)
            {
                Auditoria.Api = "ClienteNatural / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Parametros = JsonConvert.SerializeObject(cliente, Formatting.Indented);

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }

        }

        /// <summary>
        /// Metodo POST para la insercion de nuevos Clientes Naturales
        /// </summary>
        /// <param name="cliente">Modelo</param>
        // POST: api/ClientesNaturales
        [Authorize]
        public HttpResponseMessage Post([FromBody]ClienteNatural cliente)
        {
            LogHandlerCRM.Instance.Log("ClienteNatural / Post", string.Empty, TipoAuditoria.REQUEST, cliente);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "ClienteNatural / Post";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(cliente, Formatting.Indented);

            try
            {
                if (cliente.Tipodocumento != "" && cliente.NumeroIdentificacion != 0)
                {                    
                    DaoClientesNaturales.Instance.InsertarClienteNatural(cliente);
                    LogHandlerCRM.Instance.Log("ClienteNatural / Post", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);

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

        /// <summary>
        /// Metodo PUT para la actualizacion de Clientes Naturales
        /// </summary>
        /// <param name="cliente">Modelo</param>
        // PUT: api/ClientesNaturales
        [Authorize]
        public HttpResponseMessage Put([FromBody]ClienteNatural cliente)
        {
            LogHandlerCRM.Instance.Log("ClienteNatural / Put", string.Empty, TipoAuditoria.REQUEST, cliente);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "ClienteNatural / Put";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(cliente, Formatting.Indented);
            try
            {
                if (cliente.Tipodocumento != "" && cliente.NumeroIdentificacion != 0)
                {                    
                    DaoClientesNaturales.Instance.ActualizarClienteNatural(cliente);
                    LogHandlerCRM.Instance.Log("ClienteNatural / Put", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);

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

                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message);
            }

        }
    }
}