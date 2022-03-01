using System.Net;

namespace CRM.Dynamics.Framework
{
    public class DynamicsResponse
    {
        /// <summary>
        /// Constructor de la respuesta de dinamics
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public DynamicsResponse(HttpStatusCode statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        /// <summary>
        /// Código de respuesta
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Mensaje de respuesta
        /// </summary>
        public string Message { get; }
    }
}
