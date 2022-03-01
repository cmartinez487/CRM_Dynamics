using CRM.Dynamics.AccesoDatos.ClientesNaturales;
using CRM.Dynamics.APIClient;
using CRM.Dynamics.APIClient.Dictionaries;
using CRM.Dynamics.APIClient.Entities;
using CRM.Dynamics.APIClient.Entities.EFC;
using CRM.Dynamics.Entidades;
using CRM.Dynamics.Entidades.Clientes;
using CRM.Dynamics.WebApi.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRM.Dynamics.Areas.Dynamics365.Controllers
{
    public class ContactoController : ApiController
    {
        /// <summary>
        /// Obtiene una cliente natural
        /// </summary>
        /// <param name="tipo">Tipo de identificación</param>
        /// <param name="identificacion">Identificación</param>
        /// <returns>Devuelve una persona natural</returns>
        [HttpGet]
        [Route("api/dynamics/contacto/{tipo}/{identificacion}")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Get(string tipo, string identificacion)
        {
            string query = string.Empty;

            try
            {
                // Obtiene instancia del api de CRM Dynamics 365
                var api = DynamicsClient.GetInstance();

                query = string.Format("contacts?$select=efc_numerodocumento,_efc_tipodocumentoid_value,firstname,lastname,efc_direccion,_efc_municipioid_value,efc_numerotelefono,mobilephone,emailaddress1&$filter=contains(efc_numerodocumento,'{1}')&$expand=efc_tipodocumentoid($filter=efc_tipodocumentoid eq {0}),efc_municipioid($select=efc_codigo,efc_nombre)&$count=true",
                    tipo, identificacion);

                // Realiza solicitud al API de CRM Dynamics 365
                DynamicsResponse response = api.Get(query);

                // Obtiene la cantidad de registros
                int count = DynamicsClient.GetCount(response.Message);

                List<Contacto> responseList = new List<Contacto>();

                if (count > 0)
                {
                    // Obtiene el contacto
                    ContactQuery contact = DynamicsClient.GetEntity<ContactQuery>(response.Message);
                    Municipio municipio = DynamicsClient.GetEntityFromProperty<Municipio>(contact.efc_municipioid);

                    // Agrega el contacto a la lista de respuesta
                    responseList.Add(new Contacto
                    {
                        TipoDocumentoGUID = contact._efc_tipodocumentoid_value,
                        TipoDocumento = Dictionaries.GetTipoDocumento(contact._efc_tipodocumentoid_value),
                        NumeroDocumento = contact.efc_numerodocumento,
                        Nombres = contact.firstname,
                        Apellidos = contact.lastname,
                        Direccion = contact.efc_direccion,
                        CorreoElectronico = contact.emailaddress1,
                        TelefonoFijo = contact.efc_numerotelefono,
                        TelefonoCelular = contact.mobilephone,
                        MunicipioGUID = municipio.efc_municipioid,
                        Municipio = municipio.efc_nombre
                    });
                }
                else {

                    List<ClienteNatural> clientes = DaoClientesNaturales.Instance.ConsultarClienteNatural(Dictionaries.GetTipoDocumento(tipo), long.Parse(identificacion));
                    if (clientes.Count > 0)
                    {
                        // Agrega el cliente a la lista de respuesta
                        responseList.Add(new Contacto
                        {
                            TipoDocumentoGUID = Dictionaries.GetTipoDocumentoGUID(clientes[0].Tipodocumento),
                            TipoDocumento = clientes[0].Tipodocumento.Trim(),
                            NumeroDocumento = clientes[0].NumeroIdentificacion.ToString(),
                            Nombres = clientes[0].Nombre,
                            Apellidos = string.Format("{0} {1}", clientes[0].Apellido1, clientes[0].Apellido2),
                            Direccion = clientes[0].Direccion,
                            CorreoElectronico = clientes[0].Correo,
                            TelefonoFijo = clientes[0].Telefono,
                            TelefonoCelular = clientes[0].Celular,
                            Municipio = clientes[0].Municipio
                        });
                    }
                }

                // Devuelve resultado de la solicitud
                return Request.CreateResponse(response.StatusCode, responseList);
            }
            catch (DynamicsAPIException e)
            {
                LogHandlerCRM.Instance.Log("Contacto / GET", e.Message, TipoAuditoria.ERROR, query);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, JsonConvert.DeserializeObject(e.Message));
            }
            catch (Exception ex)
            {
                object exception = DynamicsClient.BuildJsonError(ex);
                LogHandlerCRM.Instance.Log("Contacto / GET", exception.ToString(), TipoAuditoria.ERROR, query);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
