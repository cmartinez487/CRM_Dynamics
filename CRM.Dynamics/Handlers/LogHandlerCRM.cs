using CRM.Dynamics.AccesoDatos;
using CRM.Dynamics.Entidades;
using Newtonsoft.Json;
using System;

namespace CRM.Dynamics.WebApi.Handlers
{
    /// <summary>
    /// Clase para el manejo de la auditoria
    /// </summary>
    public class LogHandlerCRM
    {
        #region Singleton

        private static volatile LogHandlerCRM instancia;
        private static object syncRoot = new Object();

        public static LogHandlerCRM Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new LogHandlerCRM();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Método que registrar la auditoria
        /// </summary>
        /// <param name="api">Método / Acción</param>
        /// <param name="mensaje">Mensaje error</param>
        /// <param name="tipo">Tipo auditoria</param>
        /// <param name="parameter">Parametro enviado</param>
        public void Log(string api, string mensaje, TipoAuditoria tipo, string parameter = "")
        {
            AuditoriaMensajes auditoria = new AuditoriaMensajes
            {
                ErrorID = Guid.NewGuid().ToString(),
                Api = api,
                Mensaje = mensaje,
                Tipo = tipo,
                Parametros = parameter
            };

            DaoComun.Instance.InsertarAuditoria(auditoria.ErrorID, auditoria.Api, auditoria.Mensaje, auditoria.Parametros, auditoria.Tipo);
        }

        /// <summary>
        /// Método que registrar la auditoria
        /// </summary>
        /// <param name="api">Método / Acción</param>
        /// <param name="mensaje">Mensaje error</param>
        /// <param name="tipo">Tipo auditoria</param>
        /// <param name="parameters">Objeto de parametros enviado</param>
        public void Log(string api, string mensaje, TipoAuditoria tipo, object parameters)
        {
            AuditoriaMensajes auditoria = new AuditoriaMensajes
            {
                ErrorID = Guid.NewGuid().ToString(),
                Api = api,
                Mensaje = mensaje,
                Tipo = tipo,
                Parametros = JsonConvert.SerializeObject(parameters, Formatting.Indented)
            };

            DaoComun.Instance.InsertarAuditoria(auditoria.ErrorID, auditoria.Api, auditoria.Mensaje, auditoria.Parametros, auditoria.Tipo);
        }
    }
}
