using CRM.Dynamics.Entidades;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CRM.Dynamics.AccesoDatos.Usuarios
{
    public class DaoUsuario : DaoObjectBase, IDaoUsuario
    {
        #region Singleton

        private static volatile DaoUsuario instancia;
        private static object syncRoot = new Object();
        private int max_retries = 3;

        public static DaoUsuario Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoUsuario();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Obtiene un usuario especialista de servicio
        /// </summary>
        /// <param name="tipo">Tipo de identificación</param>
        /// <param name="identificacion">Número de identificación</param>
        /// <returns>Objeto Usuario</returns>
        public Usuario ConsultarEspecialistaServicio(string tipo, string identificacion)
        {
            DbCommand comando = DB.GetStoredProcCommand("spUSUSelEspecialistaServicio");
            DB.AddInParameter(comando, "@USUtipoIdentificacion", DbType.String, tipo);
            DB.AddInParameter(comando, "@USUidentificacion", DbType.String, identificacion);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaPS = from row in dsCampos.Tables[0].AsEnumerable()
                              select new Usuario()
                              {
                                  USUcodigo = row.Field<string>("USUcodigo"),
                                  USUtipoIdentificacion = row.Field<string>("USUtipoIdentificacion"),
                                  USUidentificacion = row.Field<string>("USUidentificacion"),
                                  USUnombre = row.Field<string>("USUnombre"),
                                  USUapellidos = row.Field<string>("USUapellidos"),
                                  USUPS = row.Field<string>("USUPS"),
                                  USUestado = row.Field<string>("USUestado"),
                                  USUcatestado = row.Field<string>("USUcatestado"),
                                  USUarea = row.Field<string>("USUarea"),
                                  USUidcliente = row.Field<string>("USUidcliente"),
                                  USUcorreo = row.Field<string>("USUcorreo"),
                              };

                return ListaPS.ToList<Usuario>().FirstOrDefault();
            }

            return new Usuario();
        }
    }
}
