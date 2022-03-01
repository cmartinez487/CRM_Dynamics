using CRM.Dynamics.Entidades.PAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.AccesoDatos.PAP
{
    public interface IDaoPuntoServicios
    {
        /// <summary>
        /// Consultar PAP
        /// </summary>
        /// <param name="codigoPS"></param>
        /// <param name="IdRed"></param>
        /// <returns>Listado de PAP</returns>
        List<PuntoServicio> ConsultarPAP(string codigoPS, string IdRed);

        /// <summary>
        /// Inserta nueva PS en la Base de daots
        /// </summary>
        /// <param name="ps"></param>
        bool InsertarPAP(PuntoServicio ps);

        /// <summary>
        /// Actualiza PS en la Base de daots
        /// </summary>
        /// <param name="ps"></param>
        bool ActualizarPAP(PuntoServicio ps);

        /// <summary>
        /// Consultar Proyectos_ps
        /// </summary>
        /// <param name="codigoPS"></param>
        /// <param name="PROcodigo"></param>
        /// <returns>Listado de Proyectos asociados a los pap</returns>
        List<Proyecto_PS> ConsultarPAP_Proyecto(string codigoPS, string PROcodigo);

        /// <summary>
        /// Asocia un proyecto al pap
        /// </summary>
        /// <param name="codigoPS"></param>
        /// <param name="PROcodigo"></param>
        object InsertarProyecto_PS(List<Proyecto_PS_Insert> proyectos);

        /// <summary>
        /// Borra el proyecto asociado al pap
        /// </summary>
        /// <param name="codigoPS"></param>
        /// <param name="PROcodigo"></param>
        bool BorrarProyecto_PS(string codigoPS, Int64 PROcodigo);
    }
}
