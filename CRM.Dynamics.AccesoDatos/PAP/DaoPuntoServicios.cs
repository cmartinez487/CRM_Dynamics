using CRM.Dynamics.Entidades.PAP;
using CRM.Dynamics.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace CRM.Dynamics.AccesoDatos.PAP
{
    public class DaoPuntoServicios : DaoObjectBase, IDaoPuntoServicios
    {
        #region Singleton

        private static volatile DaoPuntoServicios instancia;
        private static object syncRoot = new Object();
        private readonly int bulkCopyTimeOut = Config.GetInt("bulkCopyTimeOut");


        public static DaoPuntoServicios Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoPuntoServicios();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Consultar PAP
        /// </summary>
        /// <param name="codigoPS"></param>
        /// <param name="IdRed"></param>
        /// <returns>Listado de PAP</returns>
        public List<PuntoServicio> ConsultarPAP(string codigoPS, string IdRed)
        {
            DbCommand comando = DB.GetStoredProcCommand("spPSVSelPuntoServicio_CRM");
            DB.AddInParameter(comando, "@PUNSERcodigo", DbType.String, codigoPS);
            DB.AddInParameter(comando, "@PUNSERIdRed", DbType.String, IdRed);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaPS = from row in dsCampos.Tables[0].AsEnumerable()

                              select new PuntoServicio()
                              {
                                  PUNSERcodigo = row.Field<string>("PUNSERcodigo"),
                                  PUNSERestado = row.Field<string>("PUNSERestado"),
                                  PUNSERmunicipio = row.Field<string>("PUNSERmunicipio"),
                                  PUNSERdescripcion = row.Field<string>("PUNSERdescripcion"),
                                  PUNSERdireccion = row.Field<string>("PUNSERdireccion"),
                                  PUNSERtelefono1 = row.Field<string>("PUNSERtelefono1"),
                                  PUNSERtelefono2 = row.Field<string>("PUNSERtelefono2"),
                                  PUNSERmail = row.Field<string>("PUNSERmail"),
                                  PUNSERfondofijo = row.Field<decimal>("PUNSERfondofijo"),
                                  PUNSERFondoCalculoMDC = row.Field<bool>("PUNSERFondoCalculoMDC"),
                                  PUNSERtipo = row.Field<string>("PUNSERtipo"),
                                  PUNSERsistema = row.Field<int>("PUNSERsistema"),
                                  PUNSERpagado = row.Field<Double>("PUNSERpagado"),
                                  PUNSERnatural = row.Field<Double>("PUNSERnatural"),
                                  PUNSERempresarial = row.Field<Double>("PUNSERempresarial"),
                                  PUNSERgrupoCE = row.Field<string>("PUNSERgrupoCE"),
                                  PUNSERFechaInactivacion = row.Field<DateTime?>("PUNSERFechaInactivacion"),
                                  PUNSERRegional = row.Field<string>("PUNSERRegional"),
                                  PUNSERFechaCierre = row.Field<DateTime?>("PUNSERFechaCierre"),
                                  PUNSERSectorRegional = row.Field<string>("PUNSERSectorRegional"),
                                  PUNSERClasificacion = row.Field<string>("PUNSERClasificacion"),
                                  PUNSERAConsultorCanales = row.Field<string>("PUNSERAConsultorCanales"),
                                  PUNSERIdRed = row.Field<string>("PUNSERIdRed"),
                              };
                return ListaPS.ToList<PuntoServicio>();
            }

            return new List<PuntoServicio>();
        }

        /// <summary>
        /// Inserta nueva PS en la Base de daots
        /// </summary>
        /// <param name="ps"></param>
        public bool InsertarPAP(PuntoServicio ps)
        {
            DbCommand comando = DB.GetStoredProcCommand("spPSVInsPuntoServicio_CRM");

            DB.AddInParameter(comando, "@PUNSERcodigo", DbType.String, ps.PUNSERcodigo);
            DB.AddInParameter(comando, "@PUNSERIdRed", DbType.String, ps.PUNSERIdRed);
            DB.AddInParameter(comando, "@PUNSERestado", DbType.String, ps.PUNSERestado);
            DB.AddInParameter(comando, "@PUNSERmunicipio", DbType.String, ps.PUNSERmunicipio);
            DB.AddInParameter(comando, "@PUNSERdescripcion", DbType.String, ps.PUNSERdescripcion);
            DB.AddInParameter(comando, "@PUNSERdireccion", DbType.String, ps.PUNSERdireccion);
            DB.AddInParameter(comando, "@PUNSERtelefono1", DbType.String, ps.PUNSERtelefono1);
            DB.AddInParameter(comando, "@PUNSERtelefono2", DbType.String, ps.PUNSERtelefono2);
            DB.AddInParameter(comando, "@PUNSERmail", DbType.String, ps.PUNSERmail);
            DB.AddInParameter(comando, "@PUNSERfondofijo", DbType.Decimal, ps.PUNSERfondofijo);
            DB.AddInParameter(comando, "@PUNSERFondoCalculoMDC", DbType.Boolean, ps.PUNSERFondoCalculoMDC);
            DB.AddInParameter(comando, "@PUNSERtipo", DbType.String, ps.PUNSERtipo);
            DB.AddInParameter(comando, "@PUNSERsistema", DbType.Int16, ps.PUNSERsistema);
            DB.AddInParameter(comando, "@PUNSERpagado", DbType.Double, ps.PUNSERpagado);
            DB.AddInParameter(comando, "@PUNSERnatural", DbType.Double, ps.PUNSERnatural);
            DB.AddInParameter(comando, "@PUNSERempresarial", DbType.Double, ps.PUNSERempresarial);
            DB.AddInParameter(comando, "@PUNSERgrupoCE", DbType.String, ps.PUNSERgrupoCE);
            DB.AddInParameter(comando, "@PUNSERFechaInactivacion", DbType.DateTime, ps.PUNSERFechaInactivacion);
            DB.AddInParameter(comando, "@PUNSERRegional", DbType.String, ps.PUNSERRegional);
            DB.AddInParameter(comando, "@PUNSERFechaCierre", DbType.DateTime, ps.PUNSERFechaCierre);
            DB.AddInParameter(comando, "@PUNSERSectorRegional", DbType.String, ps.PUNSERSectorRegional);
            DB.AddInParameter(comando, "@PUNSERClasificacion", DbType.String, ps.PUNSERClasificacion);
            DB.AddInParameter(comando, "@PUNSERAConsultorCanales", DbType.String, ps.PUNSERAConsultorCanales);
            DB.AddInParameter(comando, "@PUNSEREXTlinea", DbType.String, ps.PUNSEREXTlinea);
            DB.AddOutParameter(comando, "@Mensaje", DbType.Boolean, 3);

            ExecuteTransaction(DB, comando);

            return Convert.ToBoolean(DB.GetParameterValue(comando, "@Mensaje"));
        }

        /// <summary>
        /// Actualiza PS en la Base de daots
        /// </summary>
        /// <param name="ps"></param>
        public bool ActualizarPAP(PuntoServicio ps)
        {
            DbCommand comando = DB.GetStoredProcCommand("spPSVUpdPuntoServicio_CRM");

            DB.AddInParameter(comando, "@PUNSERcodigo", DbType.String, ps.PUNSERcodigo);
            DB.AddInParameter(comando, "@PUNSERIdRed", DbType.String, ps.PUNSERIdRed);
            DB.AddInParameter(comando, "@PUNSERestado", DbType.String, ps.PUNSERestado);
            DB.AddInParameter(comando, "@PUNSERmunicipio", DbType.String, ps.PUNSERmunicipio);
            DB.AddInParameter(comando, "@PUNSERdescripcion", DbType.String, ps.PUNSERdescripcion);
            DB.AddInParameter(comando, "@PUNSERdireccion", DbType.String, ps.PUNSERdireccion);
            DB.AddInParameter(comando, "@PUNSERtelefono1", DbType.String, ps.PUNSERtelefono1);
            DB.AddInParameter(comando, "@PUNSERtelefono2", DbType.String, ps.PUNSERtelefono2);
            DB.AddInParameter(comando, "@PUNSERmail", DbType.String, ps.PUNSERmail);
            DB.AddInParameter(comando, "@PUNSERfondofijo", DbType.Decimal, ps.PUNSERfondofijo);
            DB.AddInParameter(comando, "@PUNSERFondoCalculoMDC", DbType.Boolean, ps.PUNSERFondoCalculoMDC);
            DB.AddInParameter(comando, "@PUNSERtipo", DbType.String, ps.PUNSERtipo);
            DB.AddInParameter(comando, "@PUNSERsistema", DbType.Int16, ps.PUNSERsistema);
            DB.AddInParameter(comando, "@PUNSERpagado", DbType.Double, ps.PUNSERpagado);
            DB.AddInParameter(comando, "@PUNSERnatural", DbType.Double, ps.PUNSERnatural);
            DB.AddInParameter(comando, "@PUNSERempresarial", DbType.Double, ps.PUNSERempresarial);
            DB.AddInParameter(comando, "@PUNSERgrupoCE", DbType.String, ps.PUNSERgrupoCE);
            DB.AddInParameter(comando, "@PUNSERFechaInactivacion", DbType.DateTime, ps.PUNSERFechaInactivacion);
            DB.AddInParameter(comando, "@PUNSERRegional", DbType.String, ps.PUNSERRegional);
            DB.AddInParameter(comando, "@PUNSERFechaCierre", DbType.DateTime, ps.PUNSERFechaCierre);
            DB.AddInParameter(comando, "@PUNSERSectorRegional", DbType.String, ps.PUNSERSectorRegional);
            DB.AddInParameter(comando, "@PUNSERClasificacion", DbType.String, ps.PUNSERClasificacion);
            DB.AddInParameter(comando, "@PUNSERAConsultorCanales", DbType.String, ps.PUNSERAConsultorCanales);
            DB.AddInParameter(comando, "@PUNSEREXTlinea", DbType.String, ps.PUNSEREXTlinea);
            DB.AddOutParameter(comando, "@Mensaje", DbType.Boolean, 3);

            ExecuteTransaction(DB, comando);

            return Convert.ToBoolean(DB.GetParameterValue(comando, "@Mensaje"));

        }

        /// <summary>
        /// Consultar PAP
        /// </summary>
        /// <param name="codigoPS"></param>
        /// <param name="PROcodigo"></param>
        /// <returns>Listado de PAP</returns>
        public List<Proyecto_PS> ConsultarPAP_Proyecto(string codigoPS, string PROcodigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spPROPSSelProyectoPS_CRM");
            DB.AddInParameter(comando, "@CodigoPS", DbType.String, codigoPS);
            DB.AddInParameter(comando, "@PROcodigo", DbType.String, PROcodigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaPS = from row in dsCampos.Tables[0].AsEnumerable()

                              select new Proyecto_PS()
                              {
                                  PROPSproyecto = row.Field<Int64>("PROPSproyecto"),
                                  PROPSps = row.Field<string>("PROPSps"),
                                  PROPSfechagrabacion = row.Field<DateTime>("PROPSfechagrabacion"),
                                  PROPSestado = row.Field<string>("PROPSestado"),
                                  PROPScatestado = row.Field<string>("PROPScatestado"),
                                  PROPSobservaciones = row.Field<string>("PROPSobservaciones"),
                                  PROPSfechaActualizacion = row.Field<DateTime>("PROPSfechaActualizacion"),
                              };
                return ListaPS.ToList<Proyecto_PS>();
            }

            return new List<Proyecto_PS>();
        }

        /// <summary>
        /// Asocia un proyecto al pap
        /// </summary>
        /// <param name="codigoPS"></param>
        /// <param name="PROcodigo"></param>
        public object InsertarProyecto_PS(List<Proyecto_PS_Insert> proyectos)
        {
            //Mapping List to DataTable
            DataTable dtproyectos = ConvertToDataTable(proyectos);
            object result = null;
            //Bulk proccess with Datatable to a temp table
            using (SqlConnection connection = new SqlConnection(DB.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "CREATE TABLE #PROYECTOS_PS_TEMP(PROPSproyecto bigint,PROPSps varchar(9))";
                    command.ExecuteNonQuery();

                    using (SqlBulkCopy copy = new SqlBulkCopy(connection))
                    {
                        copy.BulkCopyTimeout = bulkCopyTimeOut;
                        copy.DestinationTableName = "#PROYECTOS_PS_TEMP";
                        copy.WriteToServer(dtproyectos);
                        copy.Close();
                    }

                    command.CommandText = "DECLARE @SummaryOfChanges TABLE(Change VARCHAR(20)); MERGE PROYECTOS_PS AS target USING " +
                        "( SELECT pps.*, ps.PUNSERcodigo FROM #PROYECTOS_PS_TEMP pps INNER JOIN PROYECTOS p ON p.PROcodigo = PROPSproyecto " +
                        "INNER JOIN PUNTOSERVICIO ps ON ps.PUNSERcodigo = PROPSps ) AS source ON (target.PROPSproyecto = source.PROPSproyecto AND target.PROPSps = source.PROPSps) " +
                        "WHEN NOT MATCHED THEN INSERT (PROPSproyecto,PROPSps,PROPSfechagrabacion,PROPSestado) VALUES (source.PROPSproyecto,source.PROPSps,GETDATE(),'ACT') " +
                        "OUTPUT $action INTO @SummaryOfChanges; " +
                        "SELECT Change, COUNT(Change) as Quantity FROM @SummaryOfChanges GROUP BY Change " +
                        "SELECT pps.* FROM #PROYECTOS_PS_TEMP pps LEFT JOIN PROYECTOS p ON p.PROcodigo = PROPSproyecto " +
                        "LEFT JOIN PUNTOSERVICIO ps ON ps.PUNSERcodigo = PROPSps WHERE ps.PUNSERcodigo IS NULL OR p.PROcodigo IS NULL";

                    using (var readerMerged = command.ExecuteReader())
                    {
                        List<object> listSuccess = new List<object>();
                        while (readerMerged.Read())
                        {
                            listSuccess.Add(new
                            {
                                Operacion = readerMerged.GetString(0),
                                NumeroRegistros = readerMerged.GetInt32(1)
                            });
                        }

                        if (readerMerged.NextResult())
                        {
                            List<Proyecto_PS_Insert> resProyectos = new List<Proyecto_PS_Insert>();
                            while (readerMerged.Read())
                            {
                                resProyectos.Add(new Proyecto_PS_Insert
                                {
                                    PROPSproyecto = readerMerged.GetInt64(0),
                                    PROPSps = readerMerged.GetString(1)
                                });
                            }

                            result = new
                            {
                                ConteoProcesadosExitosamente = listSuccess,
                                RegistrosFallidos = resProyectos
                            };

                        }
                    }

                    command.CommandText = "DROP TABLE #PROYECTOS_PS_TEMP";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Borra el proyecto asociado al pap
        /// </summary>
        /// <param name="codigoPS"></param>
        /// <param name="PROcodigo"></param>
        public bool BorrarProyecto_PS(string codigoPS, Int64 PROcodigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spPROPSDelProyectoPS_CRM");

            DB.AddInParameter(comando, "@CodigoPS", DbType.String, codigoPS);
            DB.AddInParameter(comando, "@PROcodigo", DbType.Int64, PROcodigo);
            DB.AddOutParameter(comando, "@Mensaje", DbType.Boolean, 3);

            ExecuteTransaction(DB, comando);

            return Convert.ToBoolean(DB.GetParameterValue(comando, "@Mensaje"));
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
    }
}
