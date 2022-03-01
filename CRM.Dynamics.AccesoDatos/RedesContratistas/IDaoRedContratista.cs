using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Dynamics.Entidades.RedContratista;

namespace CRM.Dynamics.AccesoDatos.RedesContratistas
{
    public interface IDaoRedContratista
    {
        /// <summary>
        /// Consultar Redes de Contratistas
        /// </summary>
        /// <param name="REDCONId"></param>
        /// <returns>Listado de Redes</returns>

        List<RedContratista> ConsultarRedesContratistas(string REDCONId);

        /// <summary>
        /// Inserta nueva Red de Contratista en la Base de daots
        /// </summary>
        /// <param name="red"></param>

        bool InsertarRedContratista(RedContratista red);

        /// <summary>
        /// Actualizwr Red de Contratista en la Base de daots
        /// </summary>
        /// <param name="red"></param>

        bool ActualizarRedContratista(RedContratista red);
    }
}
