using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.APIClient.Entities.EFC
{
    public class Municipio
    {
        /// <summary>
        /// Identificador GUID del municipio.
        /// </summary>
        public string efc_municipioid { get; set; }

        /// <summary>
        /// Código del municipio.
        /// </summary>
        public string efc_codigo { get; set; }

        /// <summary>
        /// Nombre del municipio.
        /// </summary>
        public string efc_nombre { get; set; }

        /// <summary>
        /// Departamento del municipio
        /// </summary>
        public Departamento efc_departamentoid { get; set; }

        /// <summary>
        /// País del municipio.
        /// </summary>
        public Pais efc_pais { get; set; }
    }

    public class Departamento
    {
        /// <summary>
        /// Identificador GUID del departamento.
        /// </summary>
        public string efc_departamentoid { get; set; }

        /// <summary>
        /// Código del departamento.
        /// </summary>
        public string efc_codigo { get; set; }

        /// <summary>
        /// Nombre del departamento.
        /// </summary>
        public string efc_nombre { get; set; }
    }

    public class Pais
    {
        /// <summary>
        /// Identificador GUID del país.
        /// </summary>
        public string efc_paisid { get; set; }

        /// <summary>
        /// Código del país.
        /// </summary>
        public string efc_codigo { get; set; }

        /// <summary>
        /// Nombre del país.
        /// </summary>
        public string efc_nombre { get; set; }
    }
}
