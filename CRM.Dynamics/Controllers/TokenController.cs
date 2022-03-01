using CRM.Dynamics.AccesoDatos;
using CRM.Dynamics.Comun;
using CRM.Dynamics.WebApi.Models;
using System.Web.Mvc;

namespace CRM.Dynamics.WebApi.Controllers
{
    public class TokenController : Controller
    {
        #region Variables Globales
        private string UserName_Parametros;
        private string Password_Parametros;
        private string Aplicativo_Parametros;
        private string Key_Parametros;
        #endregion

        public TokenController()
        {
            this.UserName_Parametros = DaoComun.Instance.ConsultarParametroGeneral("CRM.Dynamics.APIUser");
            this.Password_Parametros = DaoComun.Instance.ConsultarParametroGeneral("CRM.Dynamics.APIPwd");
            this.Aplicativo_Parametros = DaoComun.Instance.ConsultarParametroGeneral("CRM.Dynamics.APIApp");
            this.Key_Parametros = DaoComun.Instance.ConsultarParametroGeneral("CRM.Dynamics.APIKey");
        }

        public ActionResult GeneradorToken()
        {
            Token model = new Token();
            return View(model);
        }

        [HttpPost]
        public ActionResult GeneradorToken(string user, string pass, string app)
        {
            Token model = new Token();

            if (user == UserName_Parametros && pass == Password_Parametros && app == Aplicativo_Parametros)
            {
                string cadena = string.Concat(user, '|', pass, '#', app);
                string Key = this.Key_Parametros;
                model.token = TokenJWT.Instance.EncriptarLlave(cadena, Key);

                return View(model);
            }
            else
            {
                model.token = "error";
                return View(model);
            }
        }

        /// <summary>
        /// Método para obtener el token
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <param name="pass">Contraseña</param>
        /// <param name="app">Guid de la aplicación</param>
        /// <returns>Token de atutenticación</returns>        
        public dynamic Get(string user, string pass, string app)
        {
            if (user == UserName_Parametros && pass == Password_Parametros && app == Aplicativo_Parametros)
            {
                string cadena = string.Concat(user, '|', pass, '#', app);
                string Key = this.Key_Parametros;
                return TokenJWT.Instance.EncriptarLlave(cadena, Key);                
            }
            else
            {
                Response.StatusCode = 401;
                return string.Empty;
            }
        }
    }
}
