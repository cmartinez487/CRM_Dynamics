using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.Entidades.Sms
{
    public class SmsResponse
    {
        /// <summary>
        /// Numero de celular para el envio
        /// </summary>
        public long Celular { get; set; }

        /// <summary>
        /// fecha de envio del mensaje
        /// </summary>
        public DateTime? FechaEnvio { get; set; }

        /// <summary>
        /// Identificador de la aplicacion
        /// </summary>
        public int Idaplicacion { get; set; }

        /// <summary>
        /// Estado del mensaje
        /// </summary>
        public string IdEstadoEfecty { get; set; }

        /// <summary>
        /// Fecha actualizacion del estado del mensaje
        /// </summary>
        public DateTime? FechaActualización { get; set; }

        /// <summary>
        /// Fecha de entrega del mensaje
        /// </summary>
        public DateTime? FechaEntrega { get; set; }

        /// <summary>
        /// Fecha de creacion del mensaje
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Referencia del mensaje se almacena la cedula para los mensajes de doble via
        /// </summary>
        public string Refencia { get; set; }

        


        

        
    }
}
