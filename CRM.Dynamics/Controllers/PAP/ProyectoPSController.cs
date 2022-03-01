using CRM.Dynamics.AccesoDatos.PAP;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.Entidades.PAP;
using CRM.Dynamics.WebApi.Handlers;
using CRM.Dynamics.WebApi.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRM.Dynamics.WebApi.Controllers.PAP
{
    public class ProyectoPSController : ApiController
    {
        /// <summary>
        /// Consultar Lista de Proyectos asociados a un PAP
        /// </summary>
        /// <param name="MOVdocumento"></param>
        /// <param name="MOVtipo"></param>
        /// <returns>Listado de Proyectos_PS</returns>
        /// <returns>Listado de Proyectos_PS</returns>
        // GET: api/ProyectoPS
        [Authorize]
        public dynamic Get(string PROPSps, string PROPSproyecto)
        {
            AuditoriaMensajes Auditoria = new AuditoriaMensajes();
            Proyecto_PS proyecto_PS = new Proyecto_PS();
            proyecto_PS.PROPSps = PROPSps;
            proyecto_PS.PROPSproyecto = Convert.ToInt64(PROPSproyecto);

            LogHandlerCRM.Instance.Log("ProyectoPSController / Get", string.Empty, TipoAuditoria.REQUEST, proyecto_PS);

            try
            {
                List<Proyecto_PS> ps = DaoPuntoServicios.Instance.ConsultarPAP_Proyecto(PROPSps, PROPSproyecto);
                LogHandlerCRM.Instance.Log("ProyectoPSController / Get", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, ps);

                return ps;
            }
            catch (Exception e)
            {
                Auditoria.Api = "ProyectoPSController / Get";
                Auditoria.ErrorID = Guid.NewGuid().ToString();
                Auditoria.Mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Parametros = JsonConvert.SerializeObject(proyecto_PS, Formatting.Indented);

                ExceptionHandlerCRM.Instance.ExceptionLog(Auditoria.ErrorID, Auditoria.Mensaje, Auditoria.Api, Auditoria.Parametros);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }

        /// <summary>
        /// Metodo POST para la insercion de la asociacion de Proyecto con PAP
        /// </summary>
        /// <param name="Pro_PS">Modelo</param>
        [Authorize]
        public dynamic Post(List<Proyecto_PS_Insert> Pro_PS)
        {
            LogHandlerCRM.Instance.Log("ProyectoPSController / Post", string.Empty, TipoAuditoria.REQUEST, Pro_PS);

            List<AuditoriaMensajes> Auditoria = new List<AuditoriaMensajes>();
            string ErrorID = Guid.NewGuid().ToString();
            string mensaje;

            try
            {
                object respuesta = DaoPuntoServicios.Instance.InsertarProyecto_PS(Pro_PS);
                if (respuesta != null)
                {
                    mensaje = Convert.ToString((int)HttpStatusCode.OK);
                    Auditoria.Add(new AuditoriaMensajes() { ErrorID = ErrorID, Api = "Proyecto_PS / Post", Mensaje = mensaje, Parametros = JsonConvert.SerializeObject(respuesta, Formatting.Indented)});
                }
                else
                {
                    mensaje = Convert.ToString((int)HttpStatusCode.NotFound);
                    Auditoria.Add(new AuditoriaMensajes() { ErrorID = ErrorID, Api = "Proyecto_PS / Post", Mensaje = mensaje, Parametros = JsonConvert.SerializeObject(null, Formatting.Indented) });
                }
            }
            catch (Exception e)
            {
                mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                Auditoria.Add(new AuditoriaMensajes() { ErrorID = ErrorID, Api = "Proyecto_PS / Post", Mensaje = mensaje, Parametros = JsonConvert.SerializeObject(null, Formatting.Indented) });
            }

            foreach (var Auditor in Auditoria)
            {
                ExceptionHandlerCRM.Instance.ExceptionLog(Auditor.ErrorID, Auditor.Mensaje, Auditor.Api, Auditor.Parametros);
            }

            return Auditoria;
        }

        /// <summary>
        /// Metodo DELETE para la eliminacion de la asociacion de Proyecto con PAP
        /// </summary>
        /// <param name="Pro_PS">Modelo</param>
        // DELETE api/values/5
        [Authorize]
        public dynamic Delete(List<Proyecto_PS> Pro_PS)
        {
            LogHandlerCRM.Instance.Log("ProyectoPSController / Delete", string.Empty, TipoAuditoria.REQUEST, Pro_PS);

            List<AuditoriaMensajes> Auditoria = new List<AuditoriaMensajes>();
            string ErrorID = Guid.NewGuid().ToString();
            string mensaje;
            bool respuesta;

            foreach (var item in Pro_PS)
            {
                try
                {
                    if ((item.PROPSps != null && item.PROPSps != "") && (item.PROPSproyecto != 0))
                    {
                        respuesta = DaoPuntoServicios.Instance.BorrarProyecto_PS(item.PROPSps, item.PROPSproyecto);
                        LogHandlerCRM.Instance.Log("ProyectoPSController / Delete", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE);

                        if (respuesta == true)
                        {
                            mensaje = Convert.ToString((int)HttpStatusCode.OK + ResourceMensaje.SuccessMessage.ToString());
                            Auditoria.Add(new AuditoriaMensajes() { Api = "Proyecto_PS / Delete", Mensaje = mensaje });
                        }
                        else
                        {
                            mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorProyectoPSNR.ToString());
                            Auditoria.Add(new AuditoriaMensajes() { ErrorID = ErrorID, Api = "Proyecto_PS / Delete", Mensaje = mensaje, Parametros = JsonConvert.SerializeObject(item, Formatting.Indented) });
                        }
                    }
                    else
                    {
                        mensaje = Convert.ToString((int)HttpStatusCode.Forbidden + ResourceMensaje.ErrorProyectoPSIN.ToString());
                        Auditoria.Add(new AuditoriaMensajes() { ErrorID = ErrorID, Api = "Proyecto_PS / Delete", Mensaje = mensaje, Parametros = JsonConvert.SerializeObject(item, Formatting.Indented) });
                    }
                }
                catch (Exception e)
                {
                    mensaje = Convert.ToString((int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
                    Auditoria.Add(new AuditoriaMensajes() { ErrorID = ErrorID, Api = "Proyecto_PS / Delete", Mensaje = mensaje, Parametros = JsonConvert.SerializeObject(item, Formatting.Indented) });
                }
            }

            foreach (var Auditor in Auditoria)
            {
                ExceptionHandlerCRM.Instance.ExceptionLog(Auditor.ErrorID, Auditor.Mensaje, Auditor.Api, Auditor.Parametros);
            }

            return Auditoria;
        }
    }
}
