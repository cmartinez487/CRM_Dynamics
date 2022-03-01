using System;

namespace CRM.Dynamics.Entidades.Operaciones
{
    public class Movimiento
    {
        /// <summary>
        /// Codigo de Movimiento / Operacion
        /// </summary>
        public string MOVdocumento { get; set; }
        /// <summary>
        /// Tipo de Movimiento / Operacion
        /// </summary>
        public string MOVtipo { get; set; }
        /// <summary>
        /// Fecha de creacion de Movimiento / Operacion
        /// </summary>
        public DateTime? MOVfechacredb { get; set; }
        /// <summary>
        /// Fecha de pago de Movimiento / Operacion
        /// </summary>
        public DateTime? MOVfechapago { get; set; }
        /// <summary>
        /// Valor del Movimiento / Operacion
        /// </summary>
        public decimal MOVvalor { get; set; }
        /// <summary>
        /// Punto de Servicio de Origen del Movimiento / Operacion
        /// </summary>
        public string MOVpsorigen { get; set; }
        /// <summary>
        /// Codigo de Regional de Origen del Movimiento / Operacion
        /// </summary>
        public string PUNSERRegionalOrigen { get; set; }
        /// <summary>
        /// Punto de Servicio de Destino del Movimiento / Operacion
        /// </summary>
        public string MOVpsdestino { get; set; }
        /// <summary>
        /// Codigo de Regional de Destino del Movimiento / Operacion
        /// </summary>
        public string PUNSERRegionalDestino { get; set; }
        /// <summary>
        /// Codigo de proyecto del Movimiento / Operacion
        /// </summary>
        public Int64? MOVcodigoproyecto { get; set; }
    }
}
