using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Dynamics.Entidades.Sms;

namespace CRM.Dynamics.AccesoDatos.SMS
{
    public interface IDaoSMS
    {
        /// <summary>
        /// Consulta de mensajes por numero de ceuluar
        /// </summary>
        /// <param name="Celular"></param>
        /// <returns></returns>
        List<SmsResponse> ConsultarSms(Int64 Celular);

        /// <summary>
        /// Inserta nuevo Sms en la Base de datos
        /// </summary>
        /// <param name="Sms"></param>
        void InsertarSms(SmsRequest Sms);
    }
}
