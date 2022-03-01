using CRM.Dynamics.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CRM.Dynamics.AccesoDatos.Operaciones
{
    public class DaoMovimientos : DaoObjectBase, IDaoMovimientos
    {
        #region Singleton

        private static volatile DaoMovimientos instancia;
        private static object syncRoot = new Object();
        private int max_retries = 3;

        public static DaoMovimientos Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoMovimientos();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Consultar Clientes Corporativos
        /// </summary>
        /// <param name="MOVdocumento"></param>
        /// <param name="MOVtipo"></param>
        /// <returns>Listado de Clientes</returns>
        public List<Movimiento> ConsultarOperaciones(string MOVdocumento, string MOVtipo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spMOVSelConsultarOperaciones_CRM");            
            DB.AddInParameter(comando, "@MOVtipo", DbType.String, MOVtipo);
            DB.AddInParameter(comando, "@MOVdocumento", DbType.String, MOVdocumento);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaOperaciones = from row in dsCampos.Tables[0].AsEnumerable()

                                       select new Movimiento()
                                       {
                                           MOVdocumento = row.Field<string>("MOVdocumento"),
                                           MOVtipo = row.Field<string>("MOVtipo"),
                                           MOVfechacredb = row.Field<DateTime?>("MOVfechacredb"),
                                           MOVfechapago = row.Field<DateTime?>("MOVfechapago"),
                                           MOVvalor = row.Field<decimal>("MOVvalor"),
                                           MOVpsorigen = row.Field<string>("MOVpsorigen"),
                                           PUNSERRegionalOrigen = row.Field<string>("PUNSERRegionalOrigen"),
                                           MOVpsdestino = row.Field<string>("MOVpsdestino"),
                                           PUNSERRegionalDestino = row.Field<string>("PUNSERRegionalDestino"),
                                           MOVcodigoproyecto = row.Field<Int64?>("MOVcodigoproyecto"),
                                       };
                return ListaOperaciones.ToList<Movimiento>();
            }

            return new List<Movimiento>();
        }
    }
}
