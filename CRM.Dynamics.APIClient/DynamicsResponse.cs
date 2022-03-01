using System.Net;

namespace CRM.Dynamics.APIClient
{
    public class DynamicsResponse
    {
        public DynamicsResponse(HttpStatusCode statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        public HttpStatusCode StatusCode { get; }

        public string Message { get; }
    }
}
