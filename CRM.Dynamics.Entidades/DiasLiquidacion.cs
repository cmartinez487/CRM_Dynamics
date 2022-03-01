using System.ComponentModel.DataAnnotations;

namespace CRM.Dynamics.Entidades
{
    public class DiasLiquidacion
    {
        /// <summary>
        /// Codigo de Proyecto
        /// </summary>
        [Required]
        public long CodProyecto { get; set; }

        /// <summary>
        /// Combinacion
        /// </summary>
        [Required]
        public string Combinacion { get; set; }

        /// <summary>
        /// Codigo de Liquidacion
        /// </summary>
        [Required]
        public string Liquidacion { get; set; }

        /// <summary>
        /// DiaPago
        /// </summary>
        [Required]
        public string DiaPago { get; set; }

        /// <summary>
        /// Codigo de Cuenta
        /// </summary>
        [Required]
        public string Cuenta { get; set; }

    }
}
