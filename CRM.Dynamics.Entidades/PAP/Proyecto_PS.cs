using System;

namespace CRM.Dynamics.Entidades.PAP
{
    public class Proyecto_PS
    {
        /// <summary>
        /// Codigo dep Proyecto
        /// </summary>
        public Int64 PROPSproyecto { get; set; }

        /// <summary>
        /// Codigo dep PS
        /// </summary>
        public string PROPSps { get; set; }
        /// <summary>
        /// Fecha de Grabacion
        /// </summary>
        public DateTime PROPSfechagrabacion { get; set; }
        /// <summary>
        /// Estado Actual de Proyecto
        /// </summary>
        public string PROPSestado { get; set; }
        /// <summary>
        /// Estado / Categoria del proyecto
        /// </summary>
        public string PROPScatestado { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string PROPSobservaciones { get; set; }
        /// <summary>
        /// Fecha de Actualizacion
        /// </summary>
        public DateTime PROPSfechaActualizacion { get; set; }

    }
}
