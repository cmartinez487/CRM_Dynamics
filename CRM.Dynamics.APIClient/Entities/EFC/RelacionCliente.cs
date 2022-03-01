namespace CRM.Dynamics.APIClient.Entities.EFC
{
    public class RelacionCliente
    {

        /// <summary>
        /// Tipo de relación. Se utiliza para indicar el tipo de contacto que se relaciona al incidente, por ejemplo un cajero.
        /// </summary>
        public string efc_tiporelacionid_ODATABIND { get; set; }

        /// <summary>
        /// Relación al punto de atención
        /// </summary>
        public string efc_puntoatencionid_ODATABIND { get; set; }

        /// <summary>
        /// Relación con el contacto
        /// </summary>
        public string efc_contactoid_ODATABIND { get; set; }
    }
}
