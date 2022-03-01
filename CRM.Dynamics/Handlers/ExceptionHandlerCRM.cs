using CRM.Dynamics.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Dynamics.WebApi.Handlers
{
    public class ExceptionHandlerCRM : HandleErrorAttribute
    {
        #region Singleton

        private static volatile ExceptionHandlerCRM instancia;
        private static object syncRoot = new Object();
        private int max_retries = 3;

        public static ExceptionHandlerCRM Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new ExceptionHandlerCRM();
                    }
                }
                return instancia;
            }
        }

        #endregion

        public void ExceptionLog(string ErrorID, string mensaje, string api, string parametros)
        {
            string myLogName = "CRMExceptionLog";
            if (!EventLog.SourceExists("CRM_WebAPI"))
            {
                EventLog.CreateEventSource("CRM_WebAPI", myLogName);
                return;
            }
            else
            {
                myLogName = EventLog.LogNameFromSourceName("CRM_WebAPI", ".");
               EventLog myEventLog = new EventLog();
               myEventLog.Source = api;
               myEventLog.Log = myLogName;
               myEventLog.WriteEntry(mensaje);
            }

            DaoComun.Instance.InsertarAuditoria(ErrorID, api, mensaje, parametros);
        }
    }
}