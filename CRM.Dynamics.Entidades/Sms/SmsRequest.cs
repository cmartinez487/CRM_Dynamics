using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.Entidades.Sms
{
    public class SmsRequest
    {
        /// <summary>
        /// Numero de celular para el envio
        /// </summary>
        public long Celular { get; set; }

        /// <summary>
        /// Tipo de identificacion
        /// </summary>
        public string TipoIdentificacion { get; set; }

        /// <summary>
        /// Identificacion
        /// </summary>
        public int Identificacion { get; set; }

        /// <summary>
        /// Referencia del mensaje se almacena la cedula para los mensajes de doble via
        /// </summary>
        public string Refencia { get; set; }
    }
}
