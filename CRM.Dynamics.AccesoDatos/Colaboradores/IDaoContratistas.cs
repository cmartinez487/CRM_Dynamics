using CRM.Dynamics.Entidades.Colaboradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.AccesoDatos.Colaboradores
{
    public interface IDaoContratistas
    {
        /// <summary>
        /// Consultar Contratistas
        /// </summary>
        /// <param name="tipoid"></param>
        /// <param name="identificacion"></param>
        /// <returns>Listado de Contratistas</returns>
        List<Contratistas> ConsultarContratistas(string tipoid, string identificacion);

        /// <summary>
        /// Insertar contratista en la Base de datos
        /// </summary>
        /// <param name="contratista"></param>
        bool InsertarContratista(Contratistas contratista);

        /// <summary>
        /// actualizar contratista en la Base de datos
        /// </summary>
        /// <param name="contratista"></param>
        bool ActualizarContratista(Contratistas contratista);


    }
}
