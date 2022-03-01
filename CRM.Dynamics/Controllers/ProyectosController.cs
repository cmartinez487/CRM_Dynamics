using CRM.Dynamics.AccesoDatos;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.WebApi.Handlers;
using CRM.Dynamics.WebApi.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace CRM.Dynamics.WebApi.Controllers
{
    public class ProyectosController : ApiController
    {
        // GET: api/Proyectos
        /// <summary>
        /// Obtiene todos los proyectos
        /// </summary>
        [Authorize]
        public dynamic Get()
        {
            LogHandlerCRM.Instance.Log("Proyectos / Get", string.Empty, TipoAuditoria.REQUEST);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();

            try
            {
                List<Proyecto> list = DaoProyectos.Instance.ConsultaProyectos();
                LogHandlerCRM.Instance.Log("Proyectos / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, list);

                return list;
            }
            catch (Exception e)
            {
                Auditoria.Api = "Proyectos / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, null);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

        // GET: api/Proyectos/5
        /// <summary>
        /// Obtiene un proyecto de acuerdo al codigo enviado
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        public dynamic Get(long id)
        {
            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Proyecto proyecto = new Proyecto();
            proyecto.IdCliente = Convert.ToString(id);

            LogHandlerCRM.Instance.Log("Proyectos / Get", string.Empty, TipoAuditoria.REQUEST, proyecto);

            try
            {                
                Proyecto pro = DaoProyectos.Instance.ConsultaProyectosXCodigo(id);
                LogHandlerCRM.Instance.Log("Proyectos / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, pro);
                
                return pro;
            }
            catch (Exception e)
            {
                Auditoria.Api = "Proyectos / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Parametros = JsonConvert.SerializeObject(proyecto, Formatting.Indented);

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

        // POST: api/Proyectos
        /// <summary>
        /// Crea un nuevo proyecto
        /// </summary>
        /// <param name="p"></param>
        [Authorize]
        public HttpResponseMessage Post([FromBody]Proyecto p)
        {
            LogHandlerCRM.Instance.Log("Proyectos / Post", string.Empty, TipoAuditoria.REQUEST, p);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "Proyectos / Post";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(p, Formatting.Indented);

            try
            {                
                string mensaje = DaoProyectos.Instance.CrearProyecto(p);
                LogHandlerCRM.Instance.Log("Proyectos / Post", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);

                return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
            }
            catch (Exception e)
            {
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

        // PUT: api/Proyectos/5
        /// <summary>
        /// Actualiza el proyecto que se indique en el id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        [Authorize]
        public HttpResponseMessage Put([FromBody]Proyecto p)
        {
            LogHandlerCRM.Instance.Log("Proyectos / Put", string.Empty, TipoAuditoria.REQUEST, p);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "Proyectos / Put";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(p, Formatting.Indented);

            try
            {             
                string mensaje = DaoProyectos.Instance.ActualizarProyecto(p);
                LogHandlerCRM.Instance.Log("Proyectos / Put", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);

                return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
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
