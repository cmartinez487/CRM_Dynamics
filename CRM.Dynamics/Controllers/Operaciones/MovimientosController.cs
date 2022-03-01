using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRM.Dynamics.Entidades.Operaciones;
using CRM.Dynamics.AccesoDatos.Operaciones;
using CRM.Dynamics.WebApi.Resource;

namespace CRM.Dynamics.WebApi.Controllers.Operaciones
{
    public class MovimientosController : ApiController
    {
        /// <summary>
        /// Consultar Clientes Corporativos
        /// </summary>
        /// <param name="MOVdocumento"></param>
        /// <param name="MOVtipo"></param>
        /// <returns>Listado de Movimientos/Operaciones</returns>
        // GET: api/Movimientos
        public dynamic Get(string MOVdocumento, string MOVtipo)
        {
            try
            {
                List<Movimiento> ops = DaoMovimientos.Instance.ConsultarOperaciones(MOVdocumento, MOVtipo);

                return ops;
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, (int)HttpStatusCode.InternalServerError + " - " + e.Message.ToString());
            }
        }
    }
}
