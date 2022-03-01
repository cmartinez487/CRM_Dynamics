using CRM.Dynamics.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.AccesoDatos.ClientesCorporativos
{
    public interface IDaoClientesCorporativos
    {
        /// <summary>
        /// Consultar Clientes Corporativos
        /// </summary>
        /// <param name="tipoid"></param>
        /// <param name="identificacion"></param>
        /// <returns>Listado de Clientes</returns>
        List<ClienteCorportativo> ConsultarClienteCorporativo(string tipoid, string identificacion);

        /// <summary>
        /// Inserta nuevo Cliente en la Base de datos
        /// </summary>
        /// <param name="cliente"></param>
        void InsertarClienteCorporativo(ClienteCorportativo cliente);

        /// <summary>
        /// Actualiza un Cliente en la Base de datos
        /// </summary>
        /// <param name="cliente"></param>
        void ActualizarClienteCorporativo(ClienteCorportativo cliente);
    }
}
