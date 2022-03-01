namespace CRM.Dynamics.Entidades.Tipos
{
    public class Municipio
    {
        /// <summary>
        /// Identificador GUID del municipio.
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Código del municipio.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre del municipio.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Departamento del municipio
        /// </summary>
        public Departamento Departamento { get; set; }

        /// <summary>
        /// País del municipio.
        /// </summary>
        public Pais Pais { get; set; }
    }

    public class Departamento
    {
        /// <summary>
        /// Identificador GUID del departamento.
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Código del departamento.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre del departamento.
        /// </summary>
        public string Nombre { get; set; }
    }

    public class Pais
    {
        /// <summary>
        /// Identificador GUID del país.
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Código del país.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre del país.
        /// </summary>
        public string Nombre { get; set; }
    }
}
