using System.ComponentModel.DataAnnotations;

namespace CRM.Dynamics.Entidades
{
    public class CuentasCiudades
    {
        /// <summary>
        /// Codigo de Proyecto
        /// </summary>
        [Required]
        public long CodProyecto { get; set; }

        /// <summary>
        /// Codigo de Banco
        /// </summary>
        [Required]
        public string Banco { get; set; }

        /// <summary>
        /// Codigo de Cuenta
        /// </summary>
        [Required]
        public string Cuenta { get; set; }

        /// <summary>
        /// Codigo de Municipio
        /// </summary>
        [Required]
        public string Municipio { get; set; }

        /// <summary>
        /// Tipo de Proyecto
        /// </summary>
        [Required]
        public string Tipo { get; set; }

        /// <summary>
        /// Esatado de Cuenta
        /// </summary>
        [Required]
        public string Estado { get; set; }
    }
}
