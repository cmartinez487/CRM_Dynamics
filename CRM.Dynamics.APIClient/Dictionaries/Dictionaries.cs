using CRM.Dynamics.APIClient.Entities.EFC;
using System;
using System.Collections.Generic;

namespace CRM.Dynamics.APIClient.Dictionaries
{
    public class Dictionaries
    {
        /// <summary>
        /// Diccionario de códigos de estado.
        /// </summary>
        private static Dictionary<int, string> _statusCode;

        /// <summary>
        /// Diccionario de códigos de categoría de estado.
        /// </summary>
        private static Dictionary<int, string> _stateCode;

        /// <summary>
        /// Diccionario de tipos de documento.
        /// </summary>
        private static Dictionary<string, string> _tipoDocumento;

        /// <summary>
        /// Diccionario de tipos de documento con dóigos GUID.
        /// </summary>
        private static Dictionary<string, string> _tipoDocumentoGUID;

        /// <summary>
        /// Diccionario de estados CUN.
        /// </summary>
        private static Dictionary<string, string> _estadoCUN;

        /// <summary>
        /// Diccionario de tipos de solicitud.
        /// </summary>
        private static Dictionary<string, string> _tipoSolicitud;

        /// <summary>
        /// Diccionario de sub tipos de solicitud.
        /// </summary>
        private static Dictionary<string, string> _subTipoSolicitud;

        /// <summary>
        /// Fecha de caché del diccionario de tipos de documento.
        /// </summary>
        private static DateTime CacheDate_tipoDocumento;

        /// <summary>
        /// Fecha de caché del diccionario de tipos de documento con códigos GUID.
        /// </summary>
        private static DateTime CacheDate_tipoDocumentoGUID;

        /// <summary>
        /// Fecha de caché del diccionario de estados CUN.
        /// </summary>
        private static DateTime CacheDate_estadoCUN;

        /// <summary>
        /// Fecha de caché del diccionario de tipos de solicitud.
        /// </summary>
        private static DateTime CacheDate_tipoSolicitud;

        /// <summary>
        /// Fecha de caché del diccionario de sub tipos de solicitud.
        /// </summary>
        private static DateTime CacheDate_subTipoSolicitud;

        /// <summary>
        /// Obtiene el nombre del estado
        /// </summary>
        /// <param name="code">Código del estado</param>
        /// <returns></returns>
        public static string GetStatusCode(int code)
        {
            try
            {
                if (_statusCode == null)
                {
                    _statusCode = new Dictionary<int, string>
                    {
                        { 1, "En Curso" }, // In Progress
                        { 2, "Detenido" }, // On Hold
                        { 3, "Esperando detalles" }, // Waiting for Details
                        { 4, "Investigando" }, // Researching
                        { 5, "Resuelto" }, // Problem Solved
                        { 6, "Cancelado"}, // Cancelled
                        { 1000, "Información entregada"}, // Information Provided
                        { 2000, "Combinado"}, //Merged
                    };
                }
                return _statusCode[code];
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene nombre de la categoria de estado
        /// </summary>
        /// <param name="code">Código de categoría de estado</param>
        /// <returns></returns>
        public static string GetStateCode(int code)
        {
            try
            {
                if (_stateCode == null)
                {
                    _stateCode = new Dictionary<int, string>
                    {
                        { 0, "Activo" }, // Active
                        { 1, "Resulto" }, // Resolved
                        { 2, "Cancelado" }, // Cancelled
                    };
                }
                return _stateCode[code];
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Homologa el tipo de documento de Dynamics 365 a SieWebLive
        /// </summary>
        /// <param name="guid">Guid del tipo de documento</param>
        /// <returns></returns>
        public static string GetTipoDocumento(string guid)
        {
            try
            {
                if (_tipoDocumento == null || DateTime.Now.Subtract(CacheDate_tipoDocumento).Minutes > 30)
                {
                    // Obtiene instancia del api de CRM Dynamics 365
                    var api = DynamicsClient.GetInstance();
                    // Realiza solicitud al API de CRM Dynamics 365
                    DynamicsResponse response = api.Get("efc_tipodocumentos?$select=efc_codigo,efc_nombre");

                    List<TipoDocumento> list = new List<TipoDocumento>();
                    list = DynamicsClient.GetEntityList<TipoDocumento>(response.Message);

                    // Crea diccionario
                    _tipoDocumento = new Dictionary<string, string>();
                    foreach (TipoDocumento i in list)
                    {
                        _tipoDocumento.Add(i.efc_tipodocumentoid, i.efc_codigo);
                    }

                    CacheDate_tipoDocumento = DateTime.Now;
                }

                return _tipoDocumento[guid];
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Homologa el tipo de documento de SieWebLive a Dynamics 365
        /// </summary>
        /// <param name="tipo">Tipo de documento</param>
        /// <returns></returns>
        public static string GetTipoDocumentoGUID(string tipo)
        {
            try
            {
                if (_tipoDocumentoGUID == null || DateTime.Now.Subtract(CacheDate_tipoDocumentoGUID).Minutes > 30)
                {
                    // Obtiene instancia del api de CRM Dynamics 365
                    var api = DynamicsClient.GetInstance();
                    // Realiza solicitud al API de CRM Dynamics 365
                    DynamicsResponse response = api.Get("efc_tipodocumentos?$select=efc_codigo,efc_nombre");

                    List<TipoDocumento> list = new List<TipoDocumento>();
                    list = DynamicsClient.GetEntityList<TipoDocumento>(response.Message);

                    // Crea diccionario
                    _tipoDocumentoGUID = new Dictionary<string, string>();
                    foreach (TipoDocumento i in list)
                    {
                        _tipoDocumentoGUID.Add(i.efc_codigo, i.efc_tipodocumentoid);
                    }

                    CacheDate_tipoDocumentoGUID = DateTime.Now;
                }

                return _tipoDocumentoGUID[tipo.Trim()];
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene descripción de propiedad estadocun
        /// </summary>
        /// <param name="guid">Guid del estado CUN</param>
        /// <returns></returns>
        public static string GetEstadoCUN(string guid)
        {
            try
            {
                if (_estadoCUN == null || DateTime.Now.Subtract(CacheDate_estadoCUN).Minutes > 30)
                {
                    // Obtiene instancia del api de CRM Dynamics 365
                    var api = DynamicsClient.GetInstance();
                    // Realiza solicitud al API de CRM Dynamics 365
                    DynamicsResponse response = api.Get("efc_estadocuns?$select=efc_codigo,efc_nombre");

                    List<EstadoCUN> list = new List<EstadoCUN>();
                    list = DynamicsClient.GetEntityList<EstadoCUN>(response.Message);

                    // Crea diccionario
                    _estadoCUN = new Dictionary<string, string>();
                    foreach (EstadoCUN i in list)
                    {
                        _estadoCUN.Add(i.efc_estadocunid, i.efc_nombre);
                    }

                    CacheDate_estadoCUN = DateTime.Now;
                }

                return _estadoCUN[guid];
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene nombre del tipo de la solicitud
        /// </summary>
        /// <param name="guid">Guis del tipo de solicitud</param>
        /// <returns></returns>
        public static string GetTipoSolicitud(string guid)
        {
            try
            {
                if (_tipoSolicitud == null || DateTime.Now.Subtract(CacheDate_tipoSolicitud).Minutes > 30)
                {
                    // Obtiene instancia del api de CRM Dynamics 365
                    var api = DynamicsClient.GetInstance();
                    // Realiza solicitud al API de CRM Dynamics 365
                    DynamicsResponse response = api.Get("efc_tiposolicituds?$select=efc_codigo,efc_nombre");

                    List<TipoSolicitud> list = new List<TipoSolicitud>();
                    list = DynamicsClient.GetEntityList<TipoSolicitud>(response.Message);

                    // Crea diccionario
                    _tipoSolicitud = new Dictionary<string, string>();
                    foreach (TipoSolicitud i in list)
                    {
                        _tipoSolicitud.Add(i.efc_tiposolicitudid, i.efc_nombre);
                    }

                    CacheDate_tipoSolicitud = DateTime.Now;
                }

                return _tipoSolicitud[guid];
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene descripción de propiedad subtiposolicitud
        /// </summary>
        /// <param name="guid">Guid del subtiposolicitud</param>
        /// <returns></returns>
        public static string GetSubTipoSolicitud(string guid)
        {
            try
            {
                if (_subTipoSolicitud == null || DateTime.Now.Subtract(CacheDate_subTipoSolicitud).Minutes > 30)
                {
                    // Obtiene instancia del api de CRM Dynamics 365
                    var api = DynamicsClient.GetInstance();
                    // Realiza solicitud al API de CRM Dynamics 365
                    DynamicsResponse response = api.Get("efc_subtiposolicituds?$select=efc_codigo,efc_nombre");

                    List<SubTipoSolicitud> list = new List<SubTipoSolicitud>();
                    list = DynamicsClient.GetEntityList<SubTipoSolicitud>(response.Message);

                    // Crea diccionario
                    _subTipoSolicitud = new Dictionary<string, string>();
                    foreach (SubTipoSolicitud i in list)
                    {
                        _subTipoSolicitud.Add(i.efc_subtiposolicitudid, i.efc_nombre);
                    }

                    CacheDate_subTipoSolicitud = DateTime.Now;
                }

                return _subTipoSolicitud[guid];
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

    }
}
