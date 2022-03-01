using CRM.Dynamics.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CRM.Dynamics.AccesoDatos.ClientesCorporativos
{
    public class DaoClientesCorporativos : DaoObjectBase, IDaoClientesCorporativos
    {
        #region Singleton

        private static volatile DaoClientesCorporativos instancia;
        private static object syncRoot = new Object();
        private int max_retries = 3;

        public static DaoClientesCorporativos Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoClientesCorporativos();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Consultar Clientes Corporativos
        /// </summary>
        /// <param name="tipoid"></param>
        /// <param name="identificacion"></param>
        /// <returns>Listado de Clientes</returns>
        public List<ClienteCorportativo> ConsultarClienteCorporativo(string tipoid, string identificacion)
        {
            DbCommand comando = DB.GetStoredProcCommand("spCliSelClienteCorporativo_CRM");
            DB.AddInParameter(comando, "@CLItipoid", DbType.String, tipoid);
            DB.AddInParameter(comando, "@CLIidentificacion", DbType.String, identificacion);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaClienteCorportativo = from row in dsCampos.Tables[0].AsEnumerable()

                                               select new ClienteCorportativo()
                                               {
                                                   tipoid = row.Field<string>("CLItipoid"),
                                                   identificacion = row.Field<string>("CLIidentificacion"),
                                                   codigo = row.Field<long?>("CLIcodigo"),
                                                   nombre = row.Field<string>("CLInombre"),
                                                   razonsocial = row.Field<string>("CLIrazonsocial"),
                                                   apellido1 = row.Field<string>("CLIapellido1"),
                                                   apellido2 = row.Field<string>("CLIapellido2"),
                                                   direccion = row.Field<string>("CLIdireccion"),
                                                   municipio = row.Field<string>("CLImunicipio"),
                                                   telefono1 = row.Field<string>("CLItelefono1"),
                                                   telefono2 = row.Field<string>("CLItelefono2"),
                                                   fax = row.Field<string>("CLIfax"),
                                                   contacto = row.Field<string>("CLIcontacto"),
                                                   contactomail = row.Field<string>("CLIcontactomail"),
                                                   sexo = row.Field<string>("CLIsexo"),
                                                   tipocliente = row.Field<string>("CLItipocliente"),
                                                   estado = row.Field<string>("CLIestado"),
                                                   catestado = row.Field<string>("CLIcatestado"),
                                                   apartado = row.Field<string>("CLIapartado"),
                                                   mail = row.Field<string>("CLImail"),
                                                   fechagrabacion = row.Field<DateTime>("CLIfechagrabacion"),
                                                   observaciones = row.Field<string>("CLIobservaciones"),
                                                   digitoverificacion = row.Field<string>("CLIdigitoverificacion"),
                                                   clascontable = row.Field<long?>("CLIclascontable"),
                                                   imptimbre = row.Field<decimal?>("CLIimptimbre"),
                                                   ciudadIca = row.Field<string>("CLIciudadIca"),
                                                   Sector = row.Field<string>("CLISector"),
                                                   rowguid = row.Field<Guid>("rowguid"),
                                                   EstadoPeople = row.Field<string>("CLIEstadoPeople"),
                                                   FechaActEstadoPeople = row.Field<DateTime?>("CLIFechaActEstadoPeople"),
                                                   fechaActualizacion = row.Field<DateTime?>("CLIfechaActualizacion"),
                                                   PaginaWeb = row.Field<string>("CLIPaginaWeb"),
                                                   regional = row.Field<string>("CLIregional"),
                                                   pais = row.Field<string>("CLIpais"),
                                                   clasificacion = row.Field<string>("CLIclasificacion"),
                                                   regionalasignada = row.Field<string>("CLIregionalasignada"),
                                                   FechaActivacion = row.Field<DateTime?>("CLIFechaActivacion"),
                                                   FechaInactivacion = row.Field<DateTime?>("CLIFechaInactivacion"),
                                               };
                return ListaClienteCorportativo.ToList<ClienteCorportativo>();
            }


            return new List<ClienteCorportativo>();
        }

        /// <summary>
        /// Inserta nuevo Cliente en la Base de datos
        /// </summary>
        /// <param name="cliente"></param>
        public void InsertarClienteCorporativo(ClienteCorportativo cliente)
        {
            DbCommand comando = DB.GetStoredProcCommand("spCliInsClienteCorporativo_CRM");

            DB.AddInParameter(comando, "@tipoid", DbType.String, cliente.tipoid);
            DB.AddInParameter(comando, "@identificacion", DbType.String, cliente.identificacion);
            DB.AddInParameter(comando, "@codigo", DbType.Int64, cliente.codigo);
            DB.AddInParameter(comando, "@nombre", DbType.String, cliente.nombre);
            DB.AddInParameter(comando, "@razonsocial", DbType.String, cliente.razonsocial);
            DB.AddInParameter(comando, "@apellido1", DbType.String, cliente.apellido1);
            DB.AddInParameter(comando, "@apellido2", DbType.String, cliente.apellido2);
            DB.AddInParameter(comando, "@direccion", DbType.String, cliente.direccion);
            DB.AddInParameter(comando, "@municipio", DbType.String, cliente.municipio);
            DB.AddInParameter(comando, "@telefono1", DbType.String, cliente.telefono1);
            DB.AddInParameter(comando, "@telefono2", DbType.String, cliente.telefono2);
            DB.AddInParameter(comando, "@fax", DbType.String, cliente.fax);
            DB.AddInParameter(comando, "@contacto", DbType.String, cliente.contacto);
            DB.AddInParameter(comando, "@contactomail", DbType.String, cliente.contactomail);
            DB.AddInParameter(comando, "@sexo", DbType.String, cliente.sexo);
            DB.AddInParameter(comando, "@tipocliente", DbType.String, cliente.tipocliente);
            DB.AddInParameter(comando, "@estado", DbType.String, cliente.estado);
            DB.AddInParameter(comando, "@catestado", DbType.String, cliente.catestado);
            DB.AddInParameter(comando, "@apartado", DbType.String, cliente.apartado);
            DB.AddInParameter(comando, "@mail", DbType.String, cliente.mail);
            DB.AddInParameter(comando, "@observaciones", DbType.String, cliente.observaciones);
            DB.AddInParameter(comando, "@digitoverificacion", DbType.String, cliente.digitoverificacion);
            DB.AddInParameter(comando, "@clascontable", DbType.Int16, cliente.clascontable);
            DB.AddInParameter(comando, "@imptimbre", DbType.Decimal, cliente.imptimbre);
            DB.AddInParameter(comando, "@ciudadIca", DbType.String, cliente.ciudadIca);
            DB.AddInParameter(comando, "@Sector", DbType.String, cliente.Sector);
            DB.AddInParameter(comando, "@EstadoPeople", DbType.String, cliente.EstadoPeople);
            DB.AddInParameter(comando, "@FechaActEstadoPeople", DbType.DateTime, cliente.FechaActEstadoPeople);
            DB.AddInParameter(comando, "@fechaActualizacion", DbType.DateTime, cliente.fechaActualizacion);
            DB.AddInParameter(comando, "@PaginaWeb", DbType.String, cliente.PaginaWeb);
            DB.AddInParameter(comando, "@pais", DbType.String, cliente.pais);
            DB.AddInParameter(comando, "@clasificacion", DbType.String, cliente.clasificacion);
            DB.AddInParameter(comando, "@regionalasignada", DbType.String, cliente.regionalasignada);
            DB.AddInParameter(comando, "@FechaActivacion", DbType.DateTime, cliente.FechaActivacion);
            DB.AddInParameter(comando, "@FechaInactivacion", DbType.DateTime, cliente.FechaInactivacion);

            ExecuteTransaction(DB, comando);
        }

        /// <summary>
        /// Actualiza un Cliente en la Base de datos
        /// </summary>
        /// <param name="cliente"></param>
        public void ActualizarClienteCorporativo(ClienteCorportativo cliente)
        {
            DbCommand comando = DB.GetStoredProcCommand("spCliUpdClienteCorporativo_CRM");

            DB.AddInParameter(comando, "@tipoid", DbType.String, cliente.tipoid);
            DB.AddInParameter(comando, "@identificacion", DbType.String, cliente.identificacion);
            DB.AddInParameter(comando, "@codigo", DbType.Int64, cliente.codigo);
            DB.AddInParameter(comando, "@nombre", DbType.String, cliente.nombre);
            DB.AddInParameter(comando, "@razonsocial", DbType.String, cliente.razonsocial);
            DB.AddInParameter(comando, "@apellido1", DbType.String, cliente.apellido1);
            DB.AddInParameter(comando, "@apellido2", DbType.String, cliente.apellido2);
            DB.AddInParameter(comando, "@direccion", DbType.String, cliente.direccion);
            DB.AddInParameter(comando, "@municipio", DbType.String, cliente.municipio);
            DB.AddInParameter(comando, "@telefono1", DbType.String, cliente.telefono1);
            DB.AddInParameter(comando, "@telefono2", DbType.String, cliente.telefono2);
            DB.AddInParameter(comando, "@fax", DbType.String, cliente.fax);
            DB.AddInParameter(comando, "@contacto", DbType.String, cliente.contacto);
            DB.AddInParameter(comando, "@contactomail", DbType.String, cliente.contactomail);
            DB.AddInParameter(comando, "@sexo", DbType.String, cliente.sexo);
            DB.AddInParameter(comando, "@tipocliente", DbType.String, cliente.tipocliente);
            DB.AddInParameter(comando, "@estado", DbType.String, cliente.estado);
            DB.AddInParameter(comando, "@catestado", DbType.String, cliente.catestado);
            DB.AddInParameter(comando, "@apartado", DbType.String, cliente.apartado);
            DB.AddInParameter(comando, "@mail", DbType.String, cliente.mail);
            DB.AddInParameter(comando, "@observaciones", DbType.String, cliente.observaciones);
            DB.AddInParameter(comando, "@digitoverificacion", DbType.String, cliente.digitoverificacion);
            DB.AddInParameter(comando, "@clascontable", DbType.Int16, cliente.clascontable);
            DB.AddInParameter(comando, "@imptimbre", DbType.Decimal, cliente.imptimbre);
            DB.AddInParameter(comando, "@ciudadIca", DbType.String, cliente.ciudadIca);
            DB.AddInParameter(comando, "@Sector", DbType.String, cliente.Sector);
            DB.AddInParameter(comando, "@EstadoPeople", DbType.String, cliente.EstadoPeople);
            DB.AddInParameter(comando, "@FechaActEstadoPeople", DbType.DateTime, cliente.FechaActEstadoPeople);
            DB.AddInParameter(comando, "@PaginaWeb", DbType.String, cliente.PaginaWeb);
            DB.AddInParameter(comando, "@pais", DbType.String, cliente.pais);
            DB.AddInParameter(comando, "@clasificacion", DbType.String, cliente.clasificacion);
            DB.AddInParameter(comando, "@regionalasignada", DbType.String, cliente.regionalasignada);
            DB.AddInParameter(comando, "@FechaActivacion", DbType.DateTime, cliente.FechaActivacion);
            DB.AddInParameter(comando, "@FechaInactivacion", DbType.DateTime, cliente.FechaInactivacion);

            ExecuteTransaction(DB, comando);            
        }
    }
}
