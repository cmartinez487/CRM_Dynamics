using System;
using System.Data;
using System.Data.Common;
using CRM.Dynamics.Entidades;

namespace CRM.Dynamics.AccesoDatos
{
    public class DaoComun : DaoObjectBase
    {
        #region Singleton

        private static volatile DaoComun instancia;
        private static object syncRoot = new Object();
        private int max_retries = 3;

        public static DaoComun Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoComun();
                    }
                }
                return instancia;
            }
        }

        #endregion

        public string ConsultarParametroGeneral(string idParametro)
        {
            DbCommand comando = this.DB.GetStoredProcCommand("SpLnCOMSelParametriosGenerales");            
            DB.AddInParameter(comando, "@Llave", DbType.String, idParametro);

            object resultado = this.DB.ExecuteScalar(comando);

            if (resultado != null)
                return resultado.ToString();

            return null;
        }

        /// <summary>
        /// Método para insertar el registro de la auditoria
        /// </summary>
        /// <param name="ID">Identificador del registro</param>
        /// <param name="api">Método / Acción</param>
        /// <param name="mensaje">Mensaje error</param>
        /// <param name="parametros">Parametro enviado</param>
        /// <param name="tipo">Tipo auditoria</param>
        public void InsertarAuditoria(string ID, string api, string mensaje, string parametros, TipoAuditoria tipo = TipoAuditoria.ERROR)
        {
            DbCommand comando = this.DBAU.GetStoredProcCommand("spAUAInsAuditoriaAPI_CRM");            

            DBAU.AddInParameter(comando, "@AudiErrorID", DbType.String, ID);
            DBAU.AddInParameter(comando, "@AudiAPI", DbType.String, api);
            DBAU.AddInParameter(comando, "@AudiMensaje", DbType.String, mensaje);
            DBAU.AddInParameter(comando, "@AudiParametros", DbType.String, parametros);
            DBAU.AddInParameter(comando, "@AudiTipo", DbType.String, tipo.ToString());

            this.DBAU.ExecuteDataSet(comando);
        }
    }
}
