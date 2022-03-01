using CRM.Dynamics.Entidades.Colaboradores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CRM.Dynamics.AccesoDatos.Colaboradores
{
    public class DaoRepresentateLegal : DaoObjectBase, IDaoRepresentanteLegal
    {
        #region Singleton

        private static volatile DaoRepresentateLegal instancia;
        private static object syncRoot = new Object();
        private int max_retries = 3;

        public static DaoRepresentateLegal Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoRepresentateLegal();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Consultar Respresentantes Legales
        /// </summary>
        /// <param name="tipoid"></param>
        /// <param name="identificacion"></param>
        /// <returns>Listado de Representantes</returns>
        public List<RepresentanteLegal> ConsultarRepresentantes(string tipoid, string identificacion)
        {
            DbCommand comando = DB.GetStoredProcCommand("spREPLEGSelREPRESENTANTELEGAL_CRM");
            DB.AddInParameter(comando, "@REPLEGtipoid", DbType.String, tipoid);
            DB.AddInParameter(comando, "@REPLEGidentificacion", DbType.String, identificacion);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaRepresentante_Legal = from row in dsCampos.Tables[0].AsEnumerable()

                                               select new RepresentanteLegal()
                                               {
                                                   REPLEGtipoid = row.Field<string>("REPLEGtipoid"),
                                                   REPLEGidentificacion = row.Field<string>("REPLEGidentificacion"),
                                                   REPLEGNombre = row.Field<string>("REPLEGNombre"),
                                                   REPLEGApellido1 = row.Field<string>("REPLEGApellido1"),
                                                   REPLEGApellido2 = row.Field<string>("REPLEGApellido2"),
                                                   REPLEGDireccion = row.Field<string>("REPLEGDireccion"),
                                                   REPLEGTelefono = row.Field<string>("REPLEGTelefono"),
                                               };
                return ListaRepresentante_Legal.ToList<RepresentanteLegal>();
            }


            return new List<RepresentanteLegal>();
        }

        /// <summary>
        /// Inserta nuevo representante en la Base de datos
        /// </summary>
        /// <param name="representante"></param>
        public void InsertarRepresentante(RepresentanteLegal representante)
        {
            DbCommand comando = DB.GetStoredProcCommand("spREPLEGInsREPRESENTANTELEGAL_CRM");

            DB.AddInParameter(comando, "@REPLEGtipoid", DbType.String, representante.REPLEGtipoid);
            DB.AddInParameter(comando, "@REPLEGidentificacion", DbType.String, representante.REPLEGidentificacion);
            DB.AddInParameter(comando, "@REPLEGNombre", DbType.String, representante.REPLEGNombre);
            DB.AddInParameter(comando, "@REPLEGApellido1", DbType.String, representante.REPLEGApellido1);
            DB.AddInParameter(comando, "@REPLEGApellido2", DbType.String, representante.REPLEGApellido2);
            DB.AddInParameter(comando, "@REPLEGDireccion", DbType.String, representante.REPLEGDireccion);
            DB.AddInParameter(comando, "@REPLEGTelefono", DbType.String, representante.REPLEGTelefono);

            ExecuteTransaction(DB, comando);
        }

        /// <summary>
        /// actualizar representante en la Base de datos
        /// </summary>
        /// <param name="representante"></param>
        public void ActualizarrRepresentante(RepresentanteLegal representante)
        {
            DbCommand comando = DB.GetStoredProcCommand("spREPLEGUpdREPRESENTANTELEGAL_CRM");

            DB.AddInParameter(comando, "@REPLEGtipoid", DbType.String, representante.REPLEGtipoid);
            DB.AddInParameter(comando, "@REPLEGidentificacion", DbType.String, representante.REPLEGidentificacion);
            DB.AddInParameter(comando, "@REPLEGNombre", DbType.String, representante.REPLEGNombre);
            DB.AddInParameter(comando, "@REPLEGApellido1", DbType.String, representante.REPLEGApellido1);
            DB.AddInParameter(comando, "@REPLEGApellido2", DbType.String, representante.REPLEGApellido2);
            DB.AddInParameter(comando, "@REPLEGDireccion", DbType.String, representante.REPLEGDireccion);
            DB.AddInParameter(comando, "@REPLEGTelefono", DbType.String, representante.REPLEGTelefono);

            ExecuteTransaction(DB, comando);
        }
    }
}
