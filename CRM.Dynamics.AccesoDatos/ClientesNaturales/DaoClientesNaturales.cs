using CRM.Dynamics.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.AccesoDatos.ClientesNaturales
{
    public class DaoClientesNaturales : DaoObjectBase, IDaoClientesNaturales
    {
        #region Singleton

        private static volatile DaoClientesNaturales instancia;
        private static object syncRoot = new Object();

        public static DaoClientesNaturales Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoClientesNaturales();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Consultar Clientes Naturales
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido1"></param>
        /// <param name="apellido2"></param>
        /// <param name="telefono"></param>
        /// <param name="direccion"></param>
        /// <param name="municipio"></param>
        /// <param name="municipioExp"></param>
        /// <param name="correo"></param>
        /// <param name="ocupacion"></param>
        /// <param name="celular"></param>
        /// <param name="codigoPostal"></param>
        /// <param name="clientePEP"></param>
        /// <param name="sexo"></param>
        /// <param name="tipodocumento"></param>
        /// <param name="numeroIdentificacion"></param>B
        /// <param name="estado"></param>
        /// <returns>Listado de Clientes Naturales</returns>
        public List<ClienteNatural> ConsultarClienteNatural( string tipodocumento, Int64? numeroIdentificacion)
        {
            DbCommand comando = DBCli.GetStoredProcCommand("spIDNCLISelClienteNatural_CRM");            
            DBCli.AddInParameter(comando, "@IDECLITipodocumento", DbType.String, tipodocumento);
            DBCli.AddInParameter(comando, "@IDECLINumeroIdentificacion", DbType.Int64, numeroIdentificacion);

            DataSet dsCampos = this.DBCli.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaClienteNatual = from row in dsCampos.Tables[0].AsEnumerable()

                                               select new ClienteNatural()
                                               {
                                                   Nombre = row.Field<string>("CLIREGNombre"),
                                                   Apellido1 = row.Field<string>("CLIREGApellido1"),
                                                   Apellido2 = row.Field<string>("CLIREGApellido2"),
                                                   Telefono = row.Field<string>("CLIREGTelefono"),
                                                   Direccion = row.Field<string>("CLIREGDireccion"),
                                                   Municipio = row.Field<string>("CLIREGMunicipio"),
                                                   FechaCredb = row.Field<DateTime>("CLIREGFechaCredb"),                                                  
                                                   MunicipioExp = row.Field<string>("CLIREGMunicipioExp"),
                                                   Correo = row.Field<string>("CLIREGCorreo"),
                                                   Ocupacion = row.Field<string>("CLIREGOcupacion"),
                                                   Celular = row.Field<string>("CLIREGCelular"),                                                   
                                                   FechaExp = row.Field<DateTime>("CLIREGFechaExp"),
                                                   CodigoPostal = row.Field<string>("CLIREGCodigoPostal"),
                                                   ClientePEP = row.Field<string>("CLIREGClientePEP"),
                                                   FechaActualizacionDatos = row.Field<DateTime>("CLIREGFechaActualizacionDatos"),  
                                                   Sexo = row.Field<string>("CLIREGSexo"),
                                                   Estrato = row.Field<string>("CLIREGEstrato"),
                                                   tratamientoDatos = row.Field<bool>("CLIREGtratamientoDatos"),
                                                   Tipodocumento = row.Field<string>("IDECLITipodocumento"),
                                                   Vinculacion = row.Field<bool>("IDECLIVinculacion"),
                                                   NumeroIdentificacion = row.Field<Int64>("IDECLINumeroIdentificacion"),
                                                   FechaCredbId = row.Field<DateTime>("IDECLIFechaCredb"),
                                                   Estado = row.Field<string>("IDECLIEstado"),
                                               };
                return ListaClienteNatual.ToList<ClienteNatural>();
            }

            return new List<ClienteNatural>();
        }

        /// <summary>
        /// Inserta nuevo Cliente Natural en la Base de datos
        /// </summary>
        /// <param name="cliente"></param>
        public void InsertarClienteNatural(ClienteNatural cliente)
        {
            DbCommand comando = DBCli.GetStoredProcCommand("spIDNCLIinsClienteNatural_CRM");            

            DBCli.AddInParameter(comando, "@CLIREGNombre", DbType.String, cliente.Nombre);
            DBCli.AddInParameter(comando, "@CLIREGApellido1", DbType.String, cliente.Apellido1);
            DBCli.AddInParameter(comando, "@CLIREGApellido2", DbType.String, cliente.Apellido2);
            DBCli.AddInParameter(comando, "@CLIREGTelefono", DbType.String, cliente.Telefono);
            DBCli.AddInParameter(comando, "@CLIREGDireccion", DbType.String, cliente.Direccion);
            DBCli.AddInParameter(comando, "@CLIREGMunicipio", DbType.String, cliente.Municipio);
            DBCli.AddInParameter(comando, "@CLIREGMunicipioExp", DbType.String, cliente.MunicipioExp);
            DBCli.AddInParameter(comando, "@CLIREGCorreo", DbType.String, cliente.Correo);
            DBCli.AddInParameter(comando, "@CLIREGOcupacion", DbType.String, cliente.Ocupacion);
            DBCli.AddInParameter(comando, "@CLIREGCelular", DbType.String, cliente.Celular);
            DBCli.AddInParameter(comando, "@CLIREGCodigoPostal", DbType.String, cliente.CodigoPostal);
            DBCli.AddInParameter(comando, "@CLIREGClientePEP", DbType.String, cliente.ClientePEP);
            DBCli.AddInParameter(comando, "@CLIREGSexo", DbType.String, cliente.Sexo);
            DBCli.AddInParameter(comando, "@CLIREGEstrato", DbType.String, cliente.Estrato);
            DBCli.AddInParameter(comando, "@CLIREGtratamientoDatos", DbType.Boolean, cliente.tratamientoDatos);
            DBCli.AddInParameter(comando, "@IDECLITipodocumento", DbType.String, cliente.Tipodocumento);
            DBCli.AddInParameter(comando, "@IDECLIVinculacion", DbType.Boolean, cliente.Vinculacion);
            DBCli.AddInParameter(comando, "@IDECLINumeroIdentificacion", DbType.Int64, cliente.NumeroIdentificacion);
            DBCli.AddInParameter(comando, "@IDECLIEstado", DbType.String, cliente.Estado);

            ExecuteTransaction(DBCli, comando);
        }

        /// <summary>
        /// Actualiza Cliente Natural 
        /// </summary>
        /// <param name="cliente"></param>
        public void ActualizarClienteNatural(ClienteNatural cliente)
        {
            DbCommand comando = DBCli.GetStoredProcCommand("spIDNCLIUpdClienteNatural_CRM");            

            DBCli.AddInParameter(comando, "@CLIREGNombre", DbType.String, cliente.Nombre);
            DBCli.AddInParameter(comando, "@CLIREGApellido1", DbType.String, cliente.Apellido1);
            DBCli.AddInParameter(comando, "@CLIREGApellido2", DbType.String, cliente.Apellido2);
            DBCli.AddInParameter(comando, "@CLIREGTelefono", DbType.String, cliente.Telefono);
            DBCli.AddInParameter(comando, "@CLIREGDireccion", DbType.String, cliente.Direccion);
            DBCli.AddInParameter(comando, "@CLIREGMunicipio", DbType.String, cliente.Municipio);
            DBCli.AddInParameter(comando, "@CLIREGMunicipioExp", DbType.String, cliente.MunicipioExp);
            DBCli.AddInParameter(comando, "@CLIREGCorreo", DbType.String, cliente.Correo);
            DBCli.AddInParameter(comando, "@CLIREGOcupacion", DbType.String, cliente.Ocupacion);
            DBCli.AddInParameter(comando, "@CLIREGCelular", DbType.String, cliente.Celular);
            DBCli.AddInParameter(comando, "@CLIREGCodigoPostal", DbType.String, cliente.CodigoPostal);
            DBCli.AddInParameter(comando, "@CLIREGClientePEP", DbType.String, cliente.ClientePEP);
            DBCli.AddInParameter(comando, "@CLIREGSexo", DbType.String, cliente.Sexo);
            DBCli.AddInParameter(comando, "@CLIREGEstrato", DbType.String, cliente.Estrato);
            DBCli.AddInParameter(comando, "@CLIREGtratamientoDatos", DbType.Boolean, cliente.tratamientoDatos);
            DBCli.AddInParameter(comando, "@IDECLITipodocumento", DbType.String, cliente.Tipodocumento);
            DBCli.AddInParameter(comando, "@IDECLIVinculacion", DbType.Boolean, cliente.Vinculacion);
            DBCli.AddInParameter(comando, "@IDECLINumeroIdentificacion", DbType.Int64, cliente.NumeroIdentificacion);
            DBCli.AddInParameter(comando, "@IDECLIEstado", DbType.String, cliente.Estado);

            ExecuteTransaction(DBCli, comando);
        }
    }
}
