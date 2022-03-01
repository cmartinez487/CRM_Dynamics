using CRM.Dynamics.Entidades;

namespace CRM.Dynamics.AccesoDatos.Usuarios
{
    interface IDaoUsuario
    {
        /// <summary>
        /// Obtiene un usuario especialista de servicio
        /// </summary>
        /// <param name="tipo">Tipo de identificación</param>
        /// <param name="identificacion">Número de identificación</param>
        /// <returns>Objeto Usuario</returns>
        Usuario ConsultarEspecialistaServicio(string tipo, string identificacion);
    }
}
