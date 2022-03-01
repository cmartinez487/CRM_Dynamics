using CRM.Dynamics.Entidades.RedContratista;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;


namespace CRM.Dynamics.AccesoDatos.RedesContratistas
{
    public class DaoRedContratista : DaoObjectBase, IDaoRedContratista
    {
        #region Singleton

        private static volatile DaoRedContratista instancia;
        private static object syncRoot = new Object();
        private int max_retries = 3;

        public static DaoRedContratista Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoRedContratista();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Consultar Redes de Contratistas
        /// </summary>
        /// <param name="REDCONId"></param>
        /// <returns>Listado de Redes</returns>
        public List<RedContratista> ConsultarRedesContratistas(string REDCONId)
        {
            DbCommand comando = DB.GetStoredProcCommand("spREDCONSelRedCONTRATISTA_CRM");
            DB.AddInParameter(comando, "@REDCONId", DbType.String, REDCONId);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaRedContratista = from row in dsCampos.Tables[0].AsEnumerable()

                                          select new RedContratista()
                                          {
                                              REDCONId = row.Field<string>("REDCONId"),
                                              REDCONTipoIdRed = row.Field<string>("REDCONTipoIdRed"),
                                              REDCONIdRed = row.Field<string>("REDCONIdRed"),
                                              REDCONTipoIdContratista = row.Field<string>("REDCONTipoIdContratista"),
                                              REDCONIdContratista = row.Field<string>("REDCONIdContratista"),
                                              REDPorcentajeMarca = row.Field<decimal>("REDPorcentajeMarca"),
                                          };
                return ListaRedContratista.ToList<RedContratista>();
            }

            return new List<RedContratista>();
        }

        /// <summary>
        /// Inserta nueva Red de Contratista en la Base de daots
        /// </summary>
        /// <param name="red"></param>
        public bool InsertarRedContratista(RedContratista red)
        {
            DbCommand comando = DB.GetStoredProcCommand("spREDCONInsRedCONTRATISTA_CRM");

            DB.AddInParameter(comando, "@REDCONId", DbType.String, red.REDCONId);
            DB.AddInParameter(comando, "@REDCONTipoIdRed", DbType.String, red.REDCONTipoIdRed);
            DB.AddInParameter(comando, "@REDCONIdRed", DbType.String, red.REDCONIdRed);
            DB.AddInParameter(comando, "@REDCONTipoIdContratista", DbType.String, red.REDCONTipoIdContratista);
            DB.AddInParameter(comando, "@REDCONIdContratista", DbType.String, red.REDCONIdContratista);
            DB.AddInParameter(comando, "@REDPorcentajeMarca", DbType.Decimal, red.REDPorcentajeMarca);
            DB.AddOutParameter(comando, "@Mensaje", DbType.Boolean, 3);

            ExecuteTransaction(DB, comando);

            return Convert.ToBoolean(DB.GetParameterValue(comando, "@Mensaje"));
        }

        /// <summary>
        /// Actualizwr Red de Contratista en la Base de daots
        /// </summary>
        /// <param name="red"></param>
        public bool ActualizarRedContratista(RedContratista red)
        {
            DbCommand comando = DB.GetStoredProcCommand("spREDCONUpdRedCONTRATISTA_CRM");

            DB.AddInParameter(comando, "@REDCONId", DbType.String, red.REDCONId);
            DB.AddInParameter(comando, "@REDCONTipoIdRed", DbType.String, red.REDCONTipoIdRed);
            DB.AddInParameter(comando, "@REDCONIdRed", DbType.String, red.REDCONIdRed);
            DB.AddInParameter(comando, "@REDCONTipoIdContratista", DbType.String, red.REDCONTipoIdContratista);
            DB.AddInParameter(comando, "@REDCONIdContratista", DbType.String, red.REDCONIdContratista);
            DB.AddInParameter(comando, "@REDPorcentajeMarca", DbType.Decimal, red.REDPorcentajeMarca);
            DB.AddOutParameter(comando, "@Mensaje", DbType.Boolean, 3);

            ExecuteTransaction(DB, comando);

            return Convert.ToBoolean(DB.GetParameterValue(comando, "@Mensaje"));
        }
    }
}
