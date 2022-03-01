using CRM.Dynamics.Framework.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CRM.Dynamics.Framework
{
    public class Dynamics
    {
        #region Atributos

        /// <summary>
        /// Referencia interna de la instancia singleton del objeto CRMDynamics.
        /// </summary>
        private static Dynamics _instance;

        /// <summary>
        /// Objeto HttpClient que contiene las cabeceras necesarias para consuir el API de Dynamics 365.
        /// </summary>
        private HttpClient basicHttpClient;

        /// <summary>
        /// Token de autenticación para CRM Dynamics.
        /// </summary>
        private string token;

        /// <summary>
        /// Uri del dominio del servicio a consumir.
        /// </summary>
        private string uri;

        /// <summary>
        /// Nombre de aplicación.
        /// </summary>
        private string app;

        /// <summary>
        /// Usuario del API de CRM Dynamics.
        /// </summary>
        private string user;

        /// <summary>
        /// Contraseña de usuario del API de CRM Dynamics.
        /// </summary>
        private string pass;

        #endregion

        /// <summary>
        /// Obtiene una instancia de WebRequest utilizando el patrón singleton.
        /// </summary>        
        public static Dynamics GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Dynamics();
            }

            return _instance;
        }

        /// <summary>
        /// Constructor, obtiene las llaves de autorización para el consumo del servicio y obtiene el token de autorización.
        /// </summary>
        private Dynamics()
        {
            GetAppKeys();
            OpenConection();
        }

        #region Métodos privados

        /// <summary>
        /// Obtien las llaves de autorización del servicio.
        /// </summary>
        private void GetAppKeys()
        {
            try
            {
                this.uri = Config.GetString("CRM.Dynamics.Uri");
                this.user = Config.GetString("CRM.Dynamics.User");
                this.pass = Config.GetString("CRM.Dynamics.Password");
                this.app = Config.GetString("CRM.Dynamics.ApplicationName");                
            }
            catch
            {
                throw new Exception("No se pudieron obtener las llaves de autenticación para el servicio de CRM dynamics.");
            }
        }

        /// <summary>
        /// Obtiene el token de autenticación para comenzar a consumir el servicio.
        /// </summary>
        private void OpenConection()
        {
            try
            {
                // Omite verificación de errores de certificado SSL                
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                // Es necesario establecer el protocolo TLS 1.2 para poder ejecutar solicitudes HttpClient desde un componente web
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                // Obtiene parámetros de autenticación (Bearer challenge)
                HttpClient authRequest = new HttpClient();
                authRequest.BaseAddress = new Uri(uri);
                authRequest.Timeout = new TimeSpan(0, 0, 30);
                // Url para obtener el token
                string url = String.Format("{0}token/get?user={1}&pass={2}&app={3}", uri, user, pass, app);
                // Realiza solicitud asíncrona
                Task<string> authResource = authRequest.GetStringAsync(url);
                token = authResource.Result;

                // Libera los recursos del objeto http client en caso de reconexión
                if (basicHttpClient != null)
                {
                    basicHttpClient.Dispose();
                }
                // Crea cabecera de solicitud con token de autenticación
                basicHttpClient = new HttpClient();
                basicHttpClient.BaseAddress = new Uri(uri);
                basicHttpClient.Timeout = new TimeSpan(0, 0, 30);
                basicHttpClient.DefaultRequestHeaders.Accept.Clear();
                basicHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                basicHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            catch (Exception e)
            {
                throw new Exception("No fue posible establecer conexión con CRM Dynamics.", e);
            }
        }

        /// <summary>
        /// Realiza una solicitud HTTP al API CRM Dynamics
        /// </summary>
        /// <param name="request">Uri de la solicitud</param>
        /// <returns></returns>
        private DynamicsResponse DoRequest(string request)
        {
            Task<string> asyncTask;

            try
            {
                // Realiza la solicitud
                asyncTask = basicHttpClient.GetStringAsync(request);
                return new DynamicsResponse(HttpStatusCode.OK, asyncTask.Result);
            }
            catch (Exception e)
            {
                // Valida si se ha perdido la autorización
                if (IsUnauthorized(e))
                {
                    // Se renueva el token y se reintenta la operación
                    OpenConection();
                    // Realiza la solicitud
                    asyncTask = this.basicHttpClient.GetStringAsync(request);
                    return new DynamicsResponse(HttpStatusCode.OK, asyncTask.Result);
                }
                else
                {
                    return new DynamicsResponse(HttpStatusCode.BadRequest, e.Message);
                }
            }
        }

        /// <summary>
        /// Realiza una solicitud HTTP al API CRM Dynamics
        /// </summary>
        /// <param name="request">Objeto de solicitud</param>
        /// <returns></returns>
        private DynamicsResponse DoRequest(HttpRequestMessage request)
        {
            Task<HttpResponseMessage> asyncTask;
            HttpResponseMessage response;
            string responseMessage;

            try
            {
                // Realiza la solicitud
                asyncTask = this.basicHttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                response = asyncTask.Result;
                // Lee el contenido de la respuesta
                responseMessage = response.Content.ReadAsStringAsync().Result;

                return new DynamicsResponse(response.StatusCode, responseMessage);
            }
            catch (Exception e)
            {
                // Valida si se ha perdido la autorización
                if (IsUnauthorized(e))
                {
                    // Se renueva el token y se reintenta la operación
                    OpenConection();
                    // Realiza la solicitud
                    asyncTask = this.basicHttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                    response = asyncTask.Result;
                    // Lee el contenido de la respuesta
                    responseMessage = response.Content.ReadAsStringAsync().Result;

                    return new DynamicsResponse(response.StatusCode, responseMessage);
                }
                else
                {
                    return new DynamicsResponse(HttpStatusCode.BadRequest, e.Message);
                }
            }
        }

        /// <summary>
        /// Método que determina si la excepción se originó por perdida de autorización.
        /// </summary>
        /// <param name="e">Exceptión originada.</param>
        /// <returns></returns>
        private bool IsUnauthorized(Exception e)
        {
            if (e.InnerException != null)
            {
                if (e.InnerException.Message.Contains(" 401 "))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Obtiene una fecha tipeada como DateTime a partir de una cadena de texto en formato yyyy-MM-ddTHH:mm:ssZ
        /// </summary>
        /// <param name="date">Fecha en formato yyyy-MM-ddTHH:mm:ssZ</param>
        /// <returns>Fecha tipeada como DateTime</returns>
        public static DateTime? GetDate(string date)
        {
            try
            {
                return DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene un objeto tipeado a partir de un objeto Json.
        /// </summary>
        /// <typeparam name="T">Tipo génerico</typeparam>
        /// <param name="response">Respuesta del servicio</param>
        /// <returns>Objeto tipeado</returns>
        public static T GetEntity<T>(string response)
        {
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<T>(response);
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        ///Obtiene un listado de objetos tipeados a partir de un objeto Json
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="response">Respuesta del servicio de OData</param>
        /// <returns>Listado de objetos tipeados</returns>
        public static List<T> GetEntityList<T>(string response)
        {
            if (!string.IsNullOrEmpty(response))
            {
                return JsonConvert.DeserializeObject<List<T>>(response);
            }
            else
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Realiza una consulta por oData
        /// </summary>
        /// <param name="odataExpresion">Expresión oData de la consulta</param>
        /// <returns>Json response</returns>
        public DynamicsResponse Get(string url)
        {
            // Valida existencia de la conexión
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new Exception("Conexión no establecida con CRM Dynamics.");
            }

            return DoRequest(this.basicHttpClient.BaseAddress + url);
        }

        /// <summary>
        /// Realiza una inserción
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a insertar</typeparam>
        /// <param name="entity">Nombrea de la entidad a insertar</param>
        /// <param name="value">Objeto con los datos a insertar</param>
        /// <returns>Retorno objeto de respuesta con código y mensaje</returns>
        public DynamicsResponse Post<T>(string url, T value)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new Exception("Conexión no establecida con CRM Dynamics.");
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.basicHttpClient.BaseAddress + url);
            var jsonBody = JsonConvert.SerializeObject(value, Formatting.None);

            // Arma la cabecera de la solicitud
            request.Content = new StringContent(jsonBody);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            return DoRequest(request);
        }

        /// <summary>
        /// Realiza la actualización de una entidad
        /// </summary>        
        /// <param name="entity">Entidad a actualizar</param>
        /// <param name="field">Campo a actualizar</param>
        /// <param name="value">Valor de actualización</param>
        /// <returns>Retorno objeto de respuesta con código y mensaje</returns>
        public DynamicsResponse Put<T>(string url, T value)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new Exception("Conexión no establecida con CRM Dynamics.");
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, this.basicHttpClient.BaseAddress + url);
            var jsonBody = JsonConvert.SerializeObject(value, Formatting.None);

            // Arma la cabecera de la solicitud
            request.Content = new StringContent(jsonBody);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            return DoRequest(request);
        }

        /// <summary>
        /// Elimina un registro
        /// </summary>        
        /// <param name="entity">Entidad a eliminar (entityname(guid))</param>
        /// <returns>Retorno objeto de respuesta con código y mensaje</returns>
        public DynamicsResponse Delete(string url)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new Exception("Conexión no establecida con CRM Dynamics.");
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, this.basicHttpClient.BaseAddress + url);

            return DoRequest(request);
        }

        #endregion
    }
}
