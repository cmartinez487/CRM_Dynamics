using CRM.Dynamics.Entidades.Colaboradores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CRM.Dynamics.AccesoDatos.Colaboradores
{
    public class DaoContratistas : DaoObjectBase, IDaoContratistas
    {
        #region Singleton

        private static volatile DaoContratistas instancia;
        private static object syncRoot = new Object();
        private int max_retries = 3;

        public static DaoContratistas Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoContratistas();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Consultar Contratistas
        /// </summary>
        /// <param name="tipoid"></param>
        /// <param name="identificacion"></param>
        /// <returns>Listado de Contratistas</returns>
        public List<Contratistas> ConsultarContratistas(string tipoid, string identificacion)
        {
            DbCommand comando = DB.GetStoredProcCommand("spCONTSelCONTRATISTA_CRM");
            DB.AddInParameter(comando, "@CONtipoid", DbType.String, tipoid);
            DB.AddInParameter(comando, "@CONidentificacion", DbType.String, identificacion);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaContratistas = from row in dsCampos.Tables[0].AsEnumerable()

                                        select new Contratistas()
                                        {
                                            CONtipoid = row.Field<string>("CONtipoid"),
                                            CONidentificacion = row.Field<string>("CONidentificacion"),
                                            CONdigitoverificacion = row.Field<string>("CONdigitoverificacion"),
                                            CONRazonsocial = row.Field<string>("CONRazonsocial"),
                                            CONtipoidRepresentanteLegal = row.Field<string>("CONtipoidRepresentanteLegal"),
                                            CONidRepresentanteLegal = row.Field<string>("CONidRepresentanteLegal"),
                                            CONEstado = row.Field<string>("CONEstado"),
                                            CONCategoria = row.Field<string>("CONCategoria"),
                                            CONDireccion = row.Field<string>("CONDireccion"),
                                            CONMunicipio = row.Field<string>("CONMunicipio"),
                                            CONTelefono1 = row.Field<string>("CONTelefono1"),
                                            CONTelefono2 = row.Field<string>("CONTelefono2"),
                                            CONFax = row.Field<string>("CONFax"),
                                            CONMail = row.Field<string>("CONMail"),
                                            CONClasificacionCliente = row.Field<string>("CONClasificacionCliente"),
                                            CONClasificacionContable = row.Field<int?>("CONClasificacionContable"),
                                            CONFechaGrabacionBD = row.Field<DateTime>("CONFechaGrabacionBD"),
                                            CONUniExplot = row.Field<string>("CONUniExplot"),
                                            CONCCostoPeople = row.Field<string>("CONCCostoPeople"),
                                            CONtipoContrato = row.Field<string>("CONtipoContrato"),
                                            CONIngresosUVT = row.Field<decimal?>("CONIngresosUVT"),
                                            CONAccesoWiki = row.Field<bool>("CONAccesoWiki"),
                                            CONTipoContratista = row.Field<string>("CONTipoContratista"),
                                            CONFechaInactivacion = row.Field<DateTime?>("CONFechaInactivacion"),
                                            CONRegionalAsignada = row.Field<string>("CONRegionalAsignada"),
                                            CONZona = row.Field<string>("CONZona"),
                                            CONFacilitadorAsignado = row.Field<string>("CONFacilitadorAsignado"),
                                        };
                return ListaContratistas.ToList<Contratistas>();
            }

            return new List<Contratistas>();
        }

        /// <summary>
        /// Insertar contratista en la Base de datos
        /// </summary>
        /// <param name="contratista"></param>
        public bool InsertarContratista(Contratistas contratista)
        {
            DbCommand comando = DB.GetStoredProcCommand("spCONTInsCONTRATISTA_CRM");

            DB.AddInParameter(comando, "@CONtipoid", DbType.String, contratista.CONtipoid);
            DB.AddInParameter(comando, "@CONidentificacion", DbType.String, contratista.CONidentificacion);
            DB.AddInParameter(comando, "@CONdigitoverificacion", DbType.String, contratista.CONdigitoverificacion);
            DB.AddInParameter(comando, "@CONRazonsocial", DbType.String, contratista.CONRazonsocial);
            DB.AddInParameter(comando, "@CONtipoidRepresentanteLegal", DbType.String, contratista.CONtipoidRepresentanteLegal);
            DB.AddInParameter(comando, "@CONidRepresentanteLegal", DbType.String, contratista.CONidRepresentanteLegal);
            DB.AddInParameter(comando, "@CONEstado", DbType.String, contratista.CONEstado);
            DB.AddInParameter(comando, "@CONCategoria", DbType.String, contratista.CONCategoria);
            DB.AddInParameter(comando, "@CONDireccion", DbType.String, contratista.CONDireccion);
            DB.AddInParameter(comando, "@CONMunicipio", DbType.String, contratista.CONMunicipio);
            DB.AddInParameter(comando, "@CONTelefono1", DbType.String, contratista.CONTelefono1);
            DB.AddInParameter(comando, "@CONTelefono2", DbType.String, contratista.CONTelefono2);
            DB.AddInParameter(comando, "@CONFax", DbType.String, contratista.CONFax);
            DB.AddInParameter(comando, "@CONMail", DbType.String, contratista.CONMail);
            DB.AddInParameter(comando, "@CONClasificacionCliente", DbType.String, contratista.CONClasificacionCliente);
            DB.AddInParameter(comando, "@CONClasificacionContable", DbType.String, contratista.CONClasificacionContable);
            DB.AddInParameter(comando, "@CONUniExplot", DbType.String, contratista.CONUniExplot);
            DB.AddInParameter(comando, "@CONCCostoPeople", DbType.String, contratista.CONCCostoPeople);
            DB.AddInParameter(comando, "@CONtipoContrato", DbType.String, contratista.CONtipoContrato);
            DB.AddInParameter(comando, "@CONIngresosUVT", DbType.String, contratista.CONIngresosUVT);
            DB.AddInParameter(comando, "@CONAccesoWiki", DbType.String, contratista.CONAccesoWiki);
            DB.AddInParameter(comando, "@CONTipoContratista", DbType.String, contratista.CONTipoContratista);
            DB.AddInParameter(comando, "@CONFechaInactivacion", DbType.DateTime, contratista.CONFechaInactivacion);
            DB.AddInParameter(comando, "@CONRegionalAsignada", DbType.String, contratista.CONRegionalAsignada);
            DB.AddInParameter(comando, "@CONZona", DbType.String, contratista.CONZona);
            DB.AddInParameter(comando, "@CONFacilitadorAsignado", DbType.String, contratista.CONFacilitadorAsignado);
            DB.AddOutParameter(comando, "@Mensaje", DbType.Boolean, 3);

            ExecuteTransaction(DB, comando);

            return Convert.ToBoolean(DB.GetParameterValue(comando, "@Mensaje"));
        }

        /// <summary>
        /// actualizar contratista en la Base de datos
        /// </summary>
        /// <param name="contratista"></param>
        public bool ActualizarContratista(Contratistas contratista)
        {
            DbCommand comando = DB.GetStoredProcCommand("spCONTUpdCONTRATISTA_CRM");

            DB.AddInParameter(comando, "@CONtipoid", DbType.String, contratista.CONtipoid);
            DB.AddInParameter(comando, "@CONidentificacion", DbType.String, contratista.CONidentificacion);
            DB.AddInParameter(comando, "@CONdigitoverificacion", DbType.String, contratista.CONdigitoverificacion);
            DB.AddInParameter(comando, "@CONRazonsocial", DbType.String, contratista.CONRazonsocial);
            DB.AddInParameter(comando, "@CONtipoidRepresentanteLegal", DbType.String, contratista.CONtipoidRepresentanteLegal);
            DB.AddInParameter(comando, "@CONidRepresentanteLegal", DbType.String, contratista.CONidRepresentanteLegal);
            DB.AddInParameter(comando, "@CONEstado", DbType.String, contratista.CONEstado);
            DB.AddInParameter(comando, "@CONCategoria", DbType.String, contratista.CONCategoria);
            DB.AddInParameter(comando, "@CONDireccion", DbType.String, contratista.CONDireccion);
            DB.AddInParameter(comando, "@CONMunicipio", DbType.String, contratista.CONMunicipio);
            DB.AddInParameter(comando, "@CONTelefono1", DbType.String, contratista.CONTelefono1);
            DB.AddInParameter(comando, "@CONTelefono2", DbType.String, contratista.CONTelefono2);
            DB.AddInParameter(comando, "@CONFax", DbType.String, contratista.CONFax);
            DB.AddInParameter(comando, "@CONMail", DbType.String, contratista.CONMail);
            DB.AddInParameter(comando, "@CONClasificacionCliente", DbType.String, contratista.CONClasificacionCliente);
            DB.AddInParameter(comando, "@CONClasificacionContable", DbType.String, contratista.CONClasificacionContable);
            DB.AddInParameter(comando, "@CONUniExplot", DbType.String, contratista.CONUniExplot);
            DB.AddInParameter(comando, "@CONCCostoPeople", DbType.String, contratista.CONCCostoPeople);
            DB.AddInParameter(comando, "@CONtipoContrato", DbType.String, contratista.CONtipoContrato);
            DB.AddInParameter(comando, "@CONIngresosUVT", DbType.String, contratista.CONIngresosUVT);
            DB.AddInParameter(comando, "@CONAccesoWiki", DbType.String, contratista.CONAccesoWiki);
            DB.AddInParameter(comando, "@CONTipoContratista", DbType.String, contratista.CONTipoContratista);
            DB.AddInParameter(comando, "@CONFechaInactivacion", DbType.DateTime, contratista.CONFechaInactivacion);
            DB.AddInParameter(comando, "@CONRegionalAsignada", DbType.String, contratista.CONRegionalAsignada);
            DB.AddInParameter(comando, "@CONZona", DbType.String, contratista.CONZona);
            DB.AddInParameter(comando, "@CONFacilitadorAsignado", DbType.String, contratista.CONFacilitadorAsignado);
            DB.AddOutParameter(comando, "@Mensaje", DbType.Boolean, 3);

            ExecuteTransaction(DB, comando);

            return Convert.ToBoolean(DB.GetParameterValue(comando, "@Mensaje"));

        }
    }
}
