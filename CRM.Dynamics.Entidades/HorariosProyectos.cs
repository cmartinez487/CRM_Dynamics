using System.ComponentModel.DataAnnotations;

namespace CRM.Dynamics.Entidades
{
    public class HorariosProyectos
    {
        /// <summary>
        /// Codigo de Proyecto
        /// </summary>
        [Required]
        public long CodProyecto { get; set; }

        /// <summary>
        /// Dia programado para Proyecto
        /// </summary>
        [Required]
        public int Dia { get; set; }

        /// <summary>
        /// Hora programado para inicio de Proyecto
        /// </summary>
        [Required]
        public int HoraInicial { get; set; }

        /// <summary>
        /// Hora programado para finalizacion de Proyecto
        /// </summary>
        [Required]
        public int HoraFinal { get; set; }

    }
}
