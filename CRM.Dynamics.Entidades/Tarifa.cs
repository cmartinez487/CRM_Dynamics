using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.Dynamics.Entidades
{
    public class Tarifa
    {
        /// <summary>
        /// Codigo de Proyecto
        /// </summary>
        [Required]
        public long CodProyecto { get; set; }

        /// <summary>
        /// valor de la tarifa
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// grupo al que pertenece la tarifa
        /// </summary>
        [Required]
        public string Grupo { get; set; }

        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Valor Inicial
        /// </summary>
        public decimal? ValorInicial { get; set; }

        /// <summary>
        /// Valor Final
        /// </summary>
        public decimal? ValorFinal { get; set; }

        /// <summary>
        /// Fecha de grabación
        /// </summary>
        public DateTime? FechaGrabacion { get; set; }
    }
}
