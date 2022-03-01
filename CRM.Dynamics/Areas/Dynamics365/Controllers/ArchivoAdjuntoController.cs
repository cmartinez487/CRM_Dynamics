using CRM.Dynamics.APIClient;
using CRM.Dynamics.APIClient.Entities;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.Entidades.NotaAdjunta;
using CRM.Dynamics.WebApi.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRM.Dynamics.Areas.Dynamics365.Controllers
{
    public class ArchivoAdjuntoController : ApiController
    {
        /// <summary>
        /// Obtiene el archivo adjunto de un caso por nombre
        /// </summary>
        /// <param name="guid">Identificador GUID del caso</param>
        /// <param name="nombre">Nombre o parte de nombre del archivo</param>
        /// <returns>Devuelve la ruta de descarga del archivo</returns>
        [HttpGet]
        [Route("api/dynamics/archivoadjunto/{guid}/{nombre}")]
        [System.Web.Http.Authorize]
        public dynamic Get(string guid, string nombre)
        {
            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();

                string query = string.Format("annotations?$filter=_objectid_value eq '{0}' and (contains(filename,'{1}') or contains(subject,'respuesta'))",
                    guid, nombre);

                DynamicsResponse response = api.Get(query);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Annotation annotation = new Annotation();

                    // Obtiene datos del incidente
                    annotation = DynamicsClient.GetEntity<Annotation>(response.Message);

                    if (annotation.filesize > 0)
                    {
                        // Devuelve la primer nota adjunta
                        return new NotaAdjunta
                        {
                            NombreArchivo = annotation.filename,
                            ContenidoArchivo = annotation.documentbody,
                            MimeType = annotation.mimetype,
                            TamanioArchivo = annotation.filesize,
                            Titulo = annotation.subject,
                            Texto = annotation.notetext,
                            FechaCreacion = annotation.createdon
                        };
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject("[]"));
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("ArchivoAdjunto / GET", e.Message, TipoAuditoria.ERROR, guid);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("ArchivoAdjunto / GET", exception.ToString(), TipoAuditoria.ERROR, nombre);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
