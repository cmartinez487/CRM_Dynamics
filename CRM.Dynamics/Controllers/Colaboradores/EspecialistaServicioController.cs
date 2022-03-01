using CRM.Dynamics.AccesoDatos.Usuarios;
using CRM.Dynamics.APIClient;
using CRM.Dynamics.APIClient.Dictionaries;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.WebApi.Handlers;
using CRM.Dynamics.WebApi.Resource;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRM.Dynamics.WebApi.Controllers.Colaboradores
{
    public class EspecialistaServicioController : ApiController
    {
        /// <summary>
        /// Obtiene especialista de servicio
        /// </summary>
        /// <param name="tipo">Tipo de identificación</param>
        /// <param name="identificacion">Número de identificación</param>
        /// <returns></returns>        
        [Route("api/EspecialistaServicio/{tipo}/{identificacion}")]
        [Authorize]
        public dynamic Get(string tipo, string identificacion)
        {
            Usuario usuario = new Usuario()
            {
                USUtipoIdentificacion = tipo,
                USUidentificacion = identificacion
            };

            LogHandlerCRM.Instance.Log("EspecialistaServicio / GET", string.Empty, TipoAuditoria.REQUEST, usuario);

            try
            {
                // Consulta especialista de servicio
                usuario = DaoUsuario.Instance.ConsultarEspecialistaServicio(Dictionaries.GetTipoDocumento(tipo), identificacion);

                // Valida existencia de especialista de servicio
                if (usuario.USUcodigo == null)
                {
                    throw new DynamicsAPIException("El especialista de servicio no existe.");
                }

                LogHandlerCRM.Instance.Log("EspecialistaServicio / GET", ResourceMensaje.SuccessMessage, TipoAuditoria.RESPONSE, usuario);

                return usuario;
            }
            catch (Exception e)
            {
                LogHandlerCRM.Instance.Log("EspecialistaServicio / GET", e.Message, TipoAuditoria.ERROR, usuario);
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonConvert.DeserializeObject(e.Message));
            }
        }
    }
}
