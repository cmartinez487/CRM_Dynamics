using CRM.Dynamics.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.AccesoDatos.Operaciones
{
    public interface IDaoMovimientos
    {
        /// <summary>
        /// Consultar Clientes Corporativos
        /// </summary>
        /// <param name="MOVdocumento"></param>
        /// <param name="MOVtipo"></param>
        /// <returns>Listado de Clientes</returns>
        List<Movimiento> ConsultarOperaciones(string MOVdocumento, string MOVtipo);

    }
}
