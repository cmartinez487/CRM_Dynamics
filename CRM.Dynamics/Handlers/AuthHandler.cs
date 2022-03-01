using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CRM.Dynamics.Comun;
using CRM.Dynamics.AccesoDatos;

namespace CRM.Dynamics.Handlers
{
    public class AuthHandler : DelegatingHandler
    {
        #region Singleton

        private static volatile AuthHandler instancia;
        private static object syncRoot = new Object();
        private int max_retries = 3;

        public static AuthHandler Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new AuthHandler();
                    }
                }
                return instancia;
            }
        }

        #endregion

        #region Variables Globales
        private string UserName_Parametros;
        private string Password_Parametros;
        private string Aplicativo_Parametros;
        private string Key_Parametros;
        #endregion

        public AuthHandler()
        {
            this.UserName_Parametros = DaoComun.Instance.ConsultarParametroGeneral("CRM.Dynamics.APIUser");
            this.Password_Parametros = DaoComun.Instance.ConsultarParametroGeneral("CRM.Dynamics.APIPwd");
            this.Aplicativo_Parametros = DaoComun.Instance.ConsultarParametroGeneral("CRM.Dynamics.APIApp");
            this.Key_Parametros = DaoComun.Instance.ConsultarParametroGeneral("CRM.Dynamics.APIKey");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var headers = request.Headers;

            string UserName = this.UserName_Parametros; //comun.ConsultarParametroGeneral("CRM.Dynamics.APIUser");

            string Password = this.Password_Parametros;
            string Aplicativo = this.Aplicativo_Parametros;
            string Key = this.Key_Parametros;

            //si viene el valor de Authorization en el header http y ademas es Basic
            if (headers.Authorization != null && headers.Authorization.Scheme == "Bearer")
            {
                string TokenAuth = string.Empty;
                if (headers.Authorization.Parameter != null)
                {
                    TokenAuth = headers.Authorization.Parameter;
                    string ToKen = TokenJWT.Instance.DesencriptarLlave(TokenAuth, Key);

                    //string tokenprueba = TokenJWT.Instance.EncriptarLlave(ToKen);

                    //pico la cadena para obtener el nombre de usuario que envia la peticion, su contraseña de autenticacion y
                    //ademas la aplicacion
                    if (ToKen.IndexOf('|') > -1)
                    {
                        string usernameToken = ToKen.Substring(0, ToKen.IndexOf('|'));
                        var contrapli = ToKen.Substring(ToKen.IndexOf('|') + 1);
                        string[] separadores = { "#" };
                        if (ToKen.IndexOf(separadores[0]) > -1)
                        {
                            string[] partes = contrapli.Split(separadores, StringSplitOptions.RemoveEmptyEntries);
                            string passwordToken = partes[0];
                            string aplicativoToken = partes[1];

                            if (UserName == usernameToken && Password == passwordToken && Aplicativo == aplicativoToken)
                            {
                                var principal = new GenericPrincipal(new GenericIdentity(usernameToken), null);
                                PutPrincipal(principal);
                            }
                        }
                    }
                }
            }
            return base.SendAsync(request, cancellationToken);
        }

        private void PutPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}