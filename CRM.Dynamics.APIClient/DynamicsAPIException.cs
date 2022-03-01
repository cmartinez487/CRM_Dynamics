using System;

namespace CRM.Dynamics.APIClient
{
    /// <summary>
    /// Clase que encapsula los errores del API del CRM
    /// </summary>
    public class DynamicsAPIException : Exception
    {
        /// <summary>
        /// Mensaje de la exceptión
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// Constructor de la clase DynamicsAPIException
        /// </summary>
        /// <param name="message">Mensaje de excepción</param>
        public DynamicsAPIException(string message)
        {
            string msg = message;
            if (this.InnerException != null)
            {
                msg = string.Format("{0} InnerException: {1}", msg, this.InnerException.Message);
            }

            this.Message = DynamicsClient.BuildJsonError(msg).ToString();
        }

        /// <summary>
        /// Constructor de la clase DynamicsAPIException
        /// </summary>
        /// <param name="e">Exception original</param>
        public DynamicsAPIException(Exception e)
        {
            string msg = e.Message;

            if (e.InnerException != null)
            {
                msg = string.Format("{0} InnerException: {1}", msg, this.InnerException.Message);
                if (e.InnerException.InnerException != null)
                {
                    msg = string.Format("{0} InnerException2: {1}", msg, e.InnerException.InnerException.Message);
                }
            }

            this.Message = DynamicsClient.BuildJsonError(msg).ToString();
        }

        /// <summary>
        /// Constructor de la clase DynamicsAPIException
        /// </summary>
        /// <param name="message">Mensaje de excepción</param>
        /// <param name="e">Exception original</param>
        public DynamicsAPIException(string message, Exception e)
        {
            string msg = string.Format("{0} {1}", message, e.Message);

            if (e.InnerException != null)
            {
                msg = string.Format("{0} InnerException: {1}", msg, this.InnerException.Message);
                if (e.InnerException.InnerException != null)
                {
                    msg = string.Format("{0} InnerException2: {1}", msg, e.InnerException.InnerException.Message);
                }
            }

            this.Message = DynamicsClient.BuildJsonError(msg).ToString();
        }
    }
}
