using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data.Common;

namespace CRM.Dynamics.AccesoDatos
{
    public class DaoObjectBase : MarshalByRefObject
    {
        #region Dependencias

        /// <summary>
        /// Conexión a base de datos
        /// </summary>
        public Database DB;

        /// <summary>
        /// Conexión a base de datos
        /// </summary>
        public Database DBTx;

        /// <summary>
        /// Conexión a base de datos
        /// </summary>
        public Database DBCli;

        /// <summary>
        /// Conexión a base de datos
        /// </summary>
        public Database DBAU;

        #endregion

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public DaoObjectBase()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            DB = factory.Create("Efecty.CadenaConexion");
            DBCli = factory.Create("Efecty.CadenaConexion.Clientes");
            DBAU = factory.Create("Efecty.CadenaConexion.Auditoria");
        }

        /// <summary>
        /// Ejecuta un comando sin devolución de datos dentro de una transacción. Hace rollback ante cualquier error interno del comando o sp.
        /// </summary>
        /// <param name="command">Comando SQL</param>
        public void ExecuteTransaction(Database db, DbCommand command)
        {            
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(command, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
