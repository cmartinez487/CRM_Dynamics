
using CRM.Dynamics.Entidades.Sms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CRM.Dynamics.AccesoDatos.SMS
{
    public class DaoSMS : DaoObjectBase, IDaoSMS
    {

        #region Singleton

        private static volatile DaoSMS instancia;
        private static object syncRoot = new Object();

        public static DaoSMS Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoSMS();
                    }
                }
                return instancia;
            }
        }

        #endregion

        int idAplicacion;
        public DaoSMS()
        {
            idAplicacion = Convert.ToInt32(DaoComun.Instance.ConsultarParametroGeneral("CRM.Dynamics.IdAplicacion"));
        }

        /// <summary>
        /// Consulta de mensajes por numero de ceuluar
        /// </summary>
        /// <param name="Celular"></param>
        /// <returns></returns>
        public List<SmsResponse> ConsultarSms(Int64 Celular)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelSMSREGConsultarMensajes_CRM");
            DB.AddInParameter(comando, "@SMSREGCelular", DbType.String, Celular);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaSms = from row in dsCampos.Tables[0].AsEnumerable()

                               select new SmsResponse()
                               {
                                   Celular = row.Field<Int64>("SMSREGCelular"),
                                   FechaEnvio = row.Field<DateTime>("SMSREGFechaEnvio"),
                                   Idaplicacion = row.Field<Int32>("SMSREGidaplicacion"),
                                   IdEstadoEfecty = row.Field<string>("SMSREGIdEstadoEfecty"),
                                   FechaActualización = row.Field<DateTime>("SMSREGFechaActualización"),
                                   FechaEntrega = row.Field<DateTime>("SMSREGFechaEntrega"),
                                   FechaCreacion = row.Field<DateTime>("SMSREGFechaCreacion"),
                                   Refencia = row.Field<string>("SMSREGRefencia")
                               };
                return ListaSms.ToList<SmsResponse>();
            }
            return new List<SmsResponse>();
        }

        /// <summary>
        /// Inserta nuevo Sms en la Base de datos
        /// </summary>
        /// <param name="Sms"></param>
        public void InsertarSms(SmsRequest Sms)
        {
            DbCommand comando = DB.GetStoredProcCommand("spInsSMSREGInsertarMensajeSMSGeneral");

            DB.AddInParameter(comando, "@NumeroCel", DbType.String, Sms.Celular);
            DB.AddInParameter(comando, "@IdAplicacion", DbType.Int32, idAplicacion);
            DB.AddInParameter(comando, "@Referencia", DbType.String, Sms.Refencia);

            ExecuteTransaction(DB, comando);
        }
    }
}
