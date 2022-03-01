using CRM.Dynamics.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.AccesoDatos.ClientesNaturales
{
    public interface IDaoClientesNaturales
    {

        /// <summary>
        /// Consultar Clientes Naturales
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido1"></param>
        /// <param name="apellido2"></param>
        /// <param name="telefono"></param>
        /// <param name="direccion"></param>
        /// <param name="municipio"></param>
        /// <param name="municipioExp"></param>
        /// <param name="correo"></param>
        /// <param name="ocupacion"></param>
        /// <param name="celular"></param>
        /// <param name="codigoPostal"></param>
        /// <param name="clientePEP"></param>
        /// <param name="sexo"></param>
        /// <param name="tipodocumento"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="estado"></param>
        /// <returns>Listado de Clientes Naturales</returns>
        List<ClienteNatural> ConsultarClienteNatural(string tipodocumento, Int64? numeroIdentificacion);

        /// <summary>
        /// Inserta nuevo Cliente Natural
        /// </summary>
        /// <param name="cliente"></param>
        void InsertarClienteNatural(ClienteNatural cliente);

        /// <summary>
        /// Actualiza un Cliente Natural
        /// </summary>
        /// <param name="cliente"></param>
        void ActualizarClienteNatural(ClienteNatural cliente);
    }
}
