using CRM.Dynamics.AccesoDatos;
using CRM.Dynamics.APIClient.Helpers;
using CRM.Dynamics.APIClient.Types;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CRM.Dynamics.APIClient
{
    public class DynamicsClient
    {
        /// <summary>
        /// Referencia interna de la instancia singleton del objeto DynamicsClient.
        /// </summary>
        private static DynamicsClient instance;

        /// <summary>
        /// Objeto HttpClient que contiene las cabeceras necesarias para consuir el API de Dynamics 365.
        /// </summary>
        private HttpClient basicHttpClient;

        /// <summary>
        /// Token de autenticación para CRM Dynamics.
        /// </summary>
        private string token;

        /// <summary>
        /// Id de CRM Dynamics en Azure.
        /// </summary>
        private string appId;

        /// <summary>
        /// Api web de la instancia hasta /data/.
        /// </summary>
        private string baseUrl;

        /// <summary>
        /// Api web de la instancia completa.
        /// </summary>
        private string fullUrl;

        /// <summary>
        /// Usuario del API de Microsoft dynamics CRM 365.
        /// </summary>
        private string user;

        /// <summary>
        /// Contraseña de usuario del API de Microsoft dynamics CRM 365.
        /// </summary>
        private string pass;

        /// <summary>
        /// Constructor, obtiene las llaves de autorización para el consumo del servicio y obtiene el token de autorización.
        /// </summary>
        private DynamicsClient()
        {
            GetAppKeys();
            OpenConection();
        }

        /// <summary>
        /// Obtiene una instancia de APIClient utilizando el patrón singleton.
        /// </summary>
        /// <returns></returns>
        public static DynamicsClient GetInstance()
        {
            if (instance == null)
            {
                instance = new DynamicsClient();
            }

            return instance;
        }

        #region Métodos privados
        /// <summary>
        /// Obtien las llaves de autorización del servicio.
        /// </summary>
        private void GetAppKeys()
        {
            try
            {
                DaoComun db = DaoComun.Instance;
                this.appId = db.ConsultarParametroGeneral("CRM.Dynamics.365.AppId");
                this.baseUrl = db.ConsultarParametroGeneral("CRM.Dynamics.365.BaseUrl");
                this.fullUrl = db.ConsultarParametroGeneral("CRM.Dynamics.365.FullUrl");
                this.user = db.ConsultarParametroGeneral("CRM.Dynamics.365.User");
                this.pass = Crypto.Decrypt(db.ConsultarParametroGeneral("CRM.Dynamics.365.Password"));
            }
            catch
            {
                throw new DynamicsAPIException("No se pudieron obtener las llaves de autenticación para el servicio de CRM dynamics.");
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
                authRequest.BaseAddress = new Uri(fullUrl);
                authRequest.Timeout = new TimeSpan(0, 0, 30);
                authRequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", string.Empty);
                // Realiza solicitud asíncrona
                Task<HttpResponseMessage> authResource = authRequest.GetAsync(baseUrl);
                // Obtiene URIs de autenticación
                string wwwAuthenticate = authResource.Result.Headers.WwwAuthenticate.ToString();
                authRequest.Dispose();
                AuthenticationContext authContext = new AuthenticationContext(GetAuthorityUrl(wwwAuthenticate), false);
                // Obtiene token de autorización
                UserCredential credentials = new UserCredential(user, pass);
                AuthenticationResult result = authContext.AcquireToken(GetResourceUrl(wwwAuthenticate), appId, credentials);
                token = result.AccessToken;

                // Libera los recursos del objeto http client en caso de reconexión
                if (basicHttpClient != null)
                {
                    basicHttpClient.Dispose();
                }
                // Crea cabecera de solicitud con token de autenticación
                basicHttpClient = new HttpClient();
                basicHttpClient.BaseAddress = new Uri(fullUrl);
                basicHttpClient.Timeout = new TimeSpan(0, 0, 30);
                basicHttpClient.DefaultRequestHeaders.Accept.Clear();
                basicHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                basicHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                basicHttpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
                basicHttpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
                basicHttpClient.DefaultRequestHeaders.Add("MSCRM.SuppressDuplicateDetection", "false");
            }
            catch
            {
                throw new DynamicsAPIException("No fue posible establecer conexión con CRM Dynamics.");
            }
        }

        /// <summary>
        /// Extrae la Url de solicitud de autorización.
        /// </summary>
        /// <param name="wwwAuthenticate">wwwAuthenticate</param>
        /// <returns>Url de solicitud de autorización</returns>
        private string GetAuthorityUrl(string wwwAuthenticate)
        {
            wwwAuthenticate = wwwAuthenticate.Split(',')[0];
            wwwAuthenticate = wwwAuthenticate.Replace("Bearer authorization_uri=", string.Empty);
            return wwwAuthenticate;
        }

        /// <summary>
        /// Extrae la Url del recurso.
        /// </summary>
        /// <param name="wwwAuthenticate">wwwAuthenticate</param>
        /// <returns>Url del recurso</returns>
        private string GetResourceUrl(string wwwAuthenticate)
        {
            wwwAuthenticate = wwwAuthenticate.Split(',')[1];
            wwwAuthenticate = wwwAuthenticate.Replace(" resource_id=", string.Empty);
            return wwwAuthenticate;
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
                    throw e;
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
                    throw e;
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
        /// Realiza una consulta por oData
        /// </summary>
        /// <param name="odataExpresion">Expresión oData de la consulta</param>
        /// <returns>Json response</returns>
        public DynamicsResponse Get(string odataExpresion)
        {
            // Valida existencia de la conexión
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new DynamicsAPIException("Conexión no establecida con CRM Dynamics.");
            }

            return DoRequest(this.basicHttpClient.BaseAddress + odataExpresion);
        }

        /// <summary>
        /// Realiza una consulta por oData con tamaño de página
        /// </summary>
        /// <param name="odataExpresion">Expresión oData de la consulta</param>
        /// <param name="pageSize">Tamaño de la página (Máximo 5000)</param>
        /// <returns>Json response</returns>
        public DynamicsResponse Get(string odataExpresion, int pageSize)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new DynamicsAPIException("Conexión no establecida con CRM Dynamics.");
            }

            HttpClient httpClient = new HttpClient();
            httpClient = this.basicHttpClient;
            httpClient.DefaultRequestHeaders.Add("Prefer", "odata.maxpagesize=" + pageSize);
            Task<string> task = httpClient.GetStringAsync(this.fullUrl + odataExpresion);
            string response = task.Result;
            httpClient.Dispose();

            return new DynamicsResponse(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Realiza una inserción
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a insertar</typeparam>
        /// <param name="entity">Nombrea de la entidad a insertar</param>
        /// <param name="value">Objeto con los datos a insertar</param>
        /// <returns>Retorno objeto de respuesta con código y mensaje</returns>
        public DynamicsResponse Post<T>(string entity, T value)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new DynamicsAPIException("Conexión no establecida con CRM Dynamics.");
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.basicHttpClient.BaseAddress + entity);
            var jsonBody = JsonConvert.SerializeObject(value, Formatting.None);
            // Aplica la sintáxis correcta para el enlace para los campos relacionales
            jsonBody = jsonBody.Replace("_ODATABIND", "@odata.bind");
            // Arma la cabecera de la solicitud
            request.Content = new StringContent(jsonBody);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            return DoRequest(request);
        }

        /// <summary>
        /// Realiza una inserción con devolución de datos
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a insertar</typeparam>
        /// <param name="entity">Nombrea de la entidad a insertar</param>
        /// <param name="value">Objeto con los datos a insertar</param>
        /// <param name="select">Lista de campos para devolver</param>
        /// <returns>Retorno objeto de respuesta con código y mensaje</returns>
        public DynamicsResponse Post<T>(string entity, T value, string select)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new DynamicsAPIException("Conexión no establecida con CRM Dynamics.");
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.basicHttpClient.BaseAddress + entity + "?$select=" + select);
            var jsonBody = JsonConvert.SerializeObject(value, Formatting.None);
            // Aplica la sintáxis correcta para el enlace para los campos relacionales
            jsonBody = jsonBody.Replace("_ODATABIND", "@odata.bind");
            // Arma la cabecera de la solicitud
            request.Content = new StringContent(jsonBody);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            request.Headers.Add("Prefer", "return=representation");

            return DoRequest(request);
        }

        /// <summary>
        /// Realiza la actualización de un campo determinado
        /// </summary>        
        /// <param name="entity">Entidad a actualizar</param>
        /// <param name="guid">Identificción de la entidad a actualizar</param>
        /// <param name="field">Campo a actualizar</param>
        /// <param name="value">Valor de actualización</param>
        /// <returns>Retorno objeto de respuesta con código y mensaje</returns>
        public DynamicsResponse Put(string entity, string guid, string field, string value)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new DynamicsAPIException("Conexión no establecida con CRM Dynamics.");
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, this.basicHttpClient.BaseAddress + entity + "(" + guid + ")/" + field);
            var jsonBody = JsonConvert.SerializeObject(EntityValue.GetTypedValue(value), Formatting.None);
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
        public DynamicsResponse Put<T>(string entity, string guid, T value)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new DynamicsAPIException("Conexión no establecida con CRM Dynamics.");
            }

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), this.basicHttpClient.BaseAddress + entity + "(" + guid + ")");
            var jsonBody = JsonConvert.SerializeObject(value, Formatting.None);
            // Aplica la sintáxis correcta para el enlace para los campos relacionales
            jsonBody = jsonBody.Replace("_ODATABIND", "@odata.bind");
            request.Content = new StringContent(jsonBody);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            return DoRequest(request);
        }

        /// <summary>
        /// Elimina un registro
        /// </summary>        
        /// <param name="entity">Entidad a eliminar (entityname(guid))</param>
        /// <returns>Retorno objeto de respuesta con código y mensaje</returns>
        public DynamicsResponse Delete(string entity, string guid)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new DynamicsAPIException("Conexión no establecida con CRM Dynamics.");
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, this.basicHttpClient.BaseAddress + entity + "(" + guid + ")");

            return DoRequest(request);
        }

        /// <summary>
        /// Ejecuta una operación por lotes
        /// </summary>
        /// <typeparam name="T">Tipo del objeto</typeparam>
        /// <param name="entity">Nombrea de la entidad a insertar</param>
        /// <param name="list">Lista de objetos a ser afectados</param>
        /// <returns>Retorno objeto de respuesta con código y mensaje</returns>
        public DynamicsResponse Batch<T>(string entity, HttpMethod method, List<T> list)
        {
            if (basicHttpClient == null || basicHttpClient.DefaultRequestHeaders.Authorization == null)
            {
                throw new DynamicsAPIException("Conexión no establecida con CRM Dynamics.");
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.basicHttpClient.BaseAddress + "$batch");

            // Itera sobre la lista para armar cada operación
            int index = 0;
            string changesetGUID = Guid.NewGuid().ToString();
            string changeset = string.Empty;
            foreach (System.Collections.IEnumerable ob in list)
            {
                var jsonBody = JsonConvert.SerializeObject(ob, Formatting.None);
                // Aplica la sintáxis correcta para el enlace de los campos relacionales
                jsonBody = jsonBody.Replace("_ODATABIND", "@odata.bind");

                index++;
                changeset += string.Format(
                @"--changeset_{0} \n
                Content-Type: application/http \n
                Content-Transfer-Encoding: binary \n
                Content-ID: {1} \n
                \n
                {2} {3}{4} HTTP/1.1 \n
                Content-Type: application/json \n
                {5} \n
                \n"
                , changesetGUID
                , index
                , method.Method.ToUpper()
                , this.basicHttpClient.BaseAddress
                , entity
                , jsonBody);
            }

            // Estructura el batch
            string batchGUID = Guid.NewGuid().ToString();
            string batch = string.Format(
            @"--batch_{0} \n
            Content-Type: multipart/mixed;boundary=changeset_{1} \n
            \n
            {2}
            \n
            --changeset_{1}-- \n
            --batch_{0}--"
            , batchGUID
            , changesetGUID
            , changeset);

            // Arma la cabecera de la solicitud
            request.Content = new StringContent(batch);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/mixed;boundary=batch_" + batchGUID);

            return DoRequest(request);
        }

        /// <summary>
        /// Método que obtiene la cantidad de registros de una consulta
        /// </summary>
        /// <param name="response">Respuesta del servicio de OData</param>
        /// <returns></returns>
        public static int GetCount(string response)
        {
            // Elimina las expresiones especiales
            response = response.Replace("@odata.", string.Empty);
            // Deserializa el objeto de respuesta de OData
            ODataResponse odata = JsonConvert.DeserializeObject<ODataResponse>(response);
            // Retorna la cantidad de elemento de la respuesta
            return odata.count;
        }

        /// <summary>
        /// Método que extrae una entidad oData
        /// </summary>
        /// <typeparam name="T">Tipo génerico</typeparam>
        /// <param name="response">Respuesta del servicio de OData</param>
        /// <returns>Objeto OData</returns>
        public static T GetEntity<T>(string response)
        {
            // Elimina las expresiones especiales
            response = response.Replace("@odata.", string.Empty);
            // Deserializa el objeto de respuesta de OData
            ODataResponse odata = JsonConvert.DeserializeObject<ODataResponse>(response);
            // Obtiene el primer elemento de la lista de resultados
            if (odata.value != null && odata.value.Length > 0)
            {
                string value = odata.value[0].ToString();
                // Deserializa el elemento y lo convierte al tipo que corresponde de acuerdo al llamado
                return JsonConvert.DeserializeObject<T>(value);
            }
            else
            {
                // Deserializa el elemento y lo convierte al tipo que corresponde de acuerdo al llamado
                return JsonConvert.DeserializeObject<T>(response);
            }
        }

        /// <summary>
        /// Método que obtiene un listado de objetos tipeados
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="response">Respuesta del servicio de OData</param>
        /// <returns>Listado de objetos tipeados</returns>
        public static List<T> GetEntityList<T>(string response)
        {
            // Elimina las expresiones especiales
            response = response.Replace("@odata.", string.Empty);
            // Deserializa el objeto de respuesta de OData
            ODataResponse odata = JsonConvert.DeserializeObject<ODataResponse>(response);

            if (odata.value != null)
            {
                List<T> list = new List<T>();
                // Recorre los elementos de la colección
                foreach (object o in odata.value)
                {
                    string value = o.ToString();
                    // Deserializa el elemento, lo convierte al tipo T y lo agrega a la lista
                    list.Add(JsonConvert.DeserializeObject<T>(value));
                }
                return list;
            }
            else
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Método que obtiene un objeto de una propiedad expandida
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="jsonObject">Objeto json</param>
        /// <returns>Objeto tipeado</returns>
        public static T GetEntityFromProperty<T>(object jsonObject)
        {
            if (jsonObject != null)
            {
                // Obtiene primer elemento de la lista
                string value = jsonObject.ToString();
                // Deserializa el elemento y lo convierte al tipo T
                return JsonConvert.DeserializeObject<T>(value);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Método que obtiene un listado de objetos tipeados
        /// </summary>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <param name="jsonlist">Lista de objetos json</param>
        /// <returns>Listado de objetos tipeados</returns>
        public static List<T> GetEntityListFromProperty<T>(object[] jsonlist)
        {
            if (jsonlist != null)
            {
                List<T> list = new List<T>();
                // Recorre los elementos de la colección
                foreach (object o in jsonlist)
                {
                    string value = o.ToString();
                    // Deserializa el elemento, lo convierte al tipo T y lo agrega a la lista
                    list.Add(JsonConvert.DeserializeObject<T>(value));
                }
                return list;
            }
            else
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Obtiene un objeto Json con el mensaje de error
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        /// <returns>Json object</returns>
        public static object BuildJsonError(string message)
        {
            return JsonConvert.DeserializeObject("{error:'" + message.Replace("'", "\"") + "'}");
        }

        /// <summary>
        /// Obtiene un objeto Json desde la excepción
        /// </summary>
        /// <param name="e">Excepción</param>
        /// <returns>Json object</returns>
        public static object BuildJsonError(Exception e)
        {
            string message = (e.InnerException == null) ? e.Message : string.Format("{0} Inner: {1}", e.Message, e.InnerException.Message);            
            return JsonConvert.DeserializeObject("{error:'" + message.Replace("'", "\"") + "'}");
        }

        /// <summary>
        /// Obtiene un objeto Json con el mensaje de proceso correcto
        /// </summary>
        /// <param name="message">Mensaje de proceso correcto</param>
        /// <returns>Json object</returns>
        public static object BuildJsonOK(string message)
        {
            return JsonConvert.DeserializeObject("{success:'" + message + "'}");
        }

        #endregion
    }
}
