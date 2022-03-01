namespace CRM.Dynamics.Entidades.RedContratista
{
    public class RedContratista
    {
        /// <summary>
        /// ID de la RED
        /// </summary>
        public string REDCONId { get; set; }

        /// <summary>
        /// Tipo identificacion de la Red Contratista
        /// </summary>
        public string REDCONTipoIdRed { get; set; }

        /// <summary>
        /// Identificacion de la Red Contratista
        /// </summary>
        public string REDCONIdRed { get; set; }

        /// <summary>
        /// Tipo identificacion del Subcontratista
        /// </summary>
        public string REDCONTipoIdContratista { get; set; }

        /// <summary>
        /// Identificacion del Subcontratista
        /// </summary>
        public string REDCONIdContratista { get; set; }

        /// <summary>
        /// Porcetaje de Contraprestacion por Marca
        /// </summary>
        public decimal? REDPorcentajeMarca { get; set; }
    }
}
