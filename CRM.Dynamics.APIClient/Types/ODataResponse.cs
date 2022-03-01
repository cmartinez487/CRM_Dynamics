using System.Runtime.Serialization;

namespace CRM.Dynamics.APIClient.Types
{
    /// <summary>
    /// Clase que modelo la respuesta OData del CRM Dynamics 365
    /// </summary>
    [DataContract]
    sealed class ODataResponse
    {
        /// <summary>
        /// Uri de contexto
        /// </summary>
        [DataMember]
        public string context { get; set; }

        /// <summary>
        /// Cantidad de registros
        /// </summary>
        [DataMember]
        public int count { get; set; }

        /// <summary>
        /// Cantidad de registros
        /// </summary>
        [DataMember]
        public object[] value { get; set; }
    }
}
