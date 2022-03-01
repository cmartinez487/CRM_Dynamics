using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Dynamics.Entidades.PAP;
using CRM.Dynamics.AccesoDatos.PAP;
using CRM.Dynamics.WebApi.Resource;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.WebApi.Handlers;
using Newtonsoft.Json;

namespace CRM.Dynamics.WebApi.Controllers.PAP
{
    public class PuntoServiciosController : ApiController
    {
        /// <summary>
        /// Metodo GET para consulta listado de PAP
        /// </summary>
        /// <returns>devuelve un listado de PAP</returns>
        // GET: api/PuntoServicios
        [Authorize]
        public dynamic Get(string PUNSERcodigo, string PUNSERIdRed)
        {
            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            PuntoServicio ps = new PuntoServicio();
            ps.PUNSERcodigo = PUNSERcodigo;
            ps.PUNSERIdRed = PUNSERIdRed;

            LogHandlerCRM.Instance.Log("PuntoServicios / Get", string.Empty, TipoAuditoria.REQUEST, ps);

            try
            {                
                List<PuntoServicio> ListadoPS = DaoPuntoServicios.Instance.ConsultarPAP(PUNSERcodigo, PUNSERIdRed);
                LogHandlerCRM.Instance.Log("PuntoServicios / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, ListadoPS);

                return ListadoPS;
            }
            catch (Exception e)
            {
                Auditoria.Api = "PuntoServicios / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Parametros = JsonConvert.SerializeObject(ps, Formatting.Indented);

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Metodo POST para la insercion de nuevo PAP
        /// </summary>
        /// <param name="ps">Modelo</param>
        // POST: api/PuntoServicios
        [Authorize]
        public HttpResponseMessage Post([FromBody]PuntoServicio ps)
        {
            LogHandlerCRM.Instance.Log("PuntoServicios / Post", string.Empty, TipoAuditoria.REQUEST, ps);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "PuntoServicios / Post";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(ps, Formatting.Indented);

            bool respuesta;
            try
            {
                if ((ps.PUNSERcodigo != null && ps.PUNSERcodigo != "") && (ps.PUNSERIdRed != null && ps.PUNSERIdRed !=""))
                {                    
                    respuesta = DaoPuntoServicios.Instance.InsertarPAP(ps);                    

                    if (respuesta == true)
                    {
                        LogHandlerCRM.Instance.Log("PuntoServicios / Post", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                        return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
                    }
                    else
                    {                        
                        Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorPsNR.ToString());

                        ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                        return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorPsNR.ToString());
                    }
                }
                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorPsIN.ToString());

                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorPsIN.ToString());
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
        /// Metodo PUT para la actualizacion de PAP
        /// </summary>
        /// <param name="ps">Modelo</param>
        // PUT: api/PuntoServicios/5
        //[Authorize]
        public HttpResponseMessage Put([FromBody]PuntoServicio ps)
        {
            LogHandlerCRM.Instance.Log("PuntoServicios / Put", string.Empty, TipoAuditoria.REQUEST, ps);

            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Auditoria.Api = "PuntoServicios / Put";
            Auditoria.ErrorID = Guid.NewGuid().ToString();
            Auditoria.Parametros = JsonConvert.SerializeObject(ps, Formatting.Indented);

            bool respuesta;
            try
            {
                if ((ps.PUNSERcodigo != null && ps.PUNSERcodigo != ""))
                {                    
                    respuesta = DaoPuntoServicios.Instance.ActualizarPAP(ps);

                    if (respuesta == true)
                    {
                        LogHandlerCRM.Instance.Log("PuntoServicios / Put", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);
                        return Request.CreateResponse(HttpStatusCode.OK, (int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
                    }
                    else
                    {
                        Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorPsNR.ToString());

                        ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                        return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorPsNR.ToString());
                    }
                }
                else
                {
                    Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorPsIN.ToString());

                    ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                    return Request.CreateResponse(HttpStatusCode.Forbidden, (int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorPsIN.ToString());
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
