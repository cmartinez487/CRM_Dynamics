using CRM.Dynamics.Entidades.Colaboradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.AccesoDatos.Colaboradores
{
    public interface IDaoRepresentanteLegal
    {
        /// <summary>
        /// Consultar Respresentantes Legales
        /// </summary>
        /// <param name="tipoid"></param>
        /// <param name="identificacion"></param>
        /// <returns>Listado de Representantes</returns>
        List<RepresentanteLegal> ConsultarRepresentantes(string tipoid, string identificacion);

        /// <summary>
        /// Inserta nuevo representante en la Base de datos
        /// </summary>
        /// <param name="representante"></param>
        void InsertarRepresentante(RepresentanteLegal representante);

        /// <summary>
        /// actualizar representante en la Base de datos
        /// </summary>
        /// <param name="representante"></param>
        void ActualizarrRepresentante(RepresentanteLegal representante);
    }
}
