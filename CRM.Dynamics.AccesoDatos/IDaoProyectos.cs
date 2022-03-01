using CRM.Dynamics.Entidades;
using System.Collections.Generic;
using System.Data.Common;

namespace CRM.Dynamics.AccesoDatos
{
    public interface IDaoProyectos
    {
        /// <summary>
        /// Consulta un proyecto por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        Proyecto ConsultaProyectosXCodigo(long codigo);
        /// <summary>
        /// Consulta todos los proyectos
        /// </summary>
        /// <returns></returns>
        List<Proyecto> ConsultaProyectos();
        /// <summary>
        /// Crea un proyecto
        /// </summary>
        /// <param name="p"></param>
        string CrearProyecto(Proyecto p);
        /// <summary>
        /// Actualiza un proyecto
        /// </summary>
        /// <param name="p"></param>
        string ActualizarProyecto(Proyecto p);
        /// <summary>
        /// Consulta los dias de liquidacion por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        List<DiasLiquidacion> ConsultaDiasLiquidacionXCodigo(long codigo);
        /// <summary>
        /// Crea un dia de liquidacion
        /// </summary>
        /// <param name="d"></param>
        void CrearDiasLiquidacion(DiasLiquidacion d, DbTransaction transaction);
        /// <summary>
        /// Actualiza un dia de liquidacion
        /// </summary>
        /// <param name="d"></param>
        void ActualizarDiasLiquidacion(DiasLiquidacion d, DbTransaction transaction);
        /// <summary>
        /// Consulta todas las cuentas de ciudades por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        List<CuentasCiudades> ConsultaCuentasCiudadesXCodigo(long codigo);
        /// <summary>
        /// Crea cuenta de ciudad
        /// </summary>
        /// <param name="c"></param>
        void CrearCuentasCiudades(CuentasCiudades c, DbTransaction transaction);
        /// <summary>
        /// Actualiza cuenta de ciudad
        /// </summary>
        /// <param name="c"></param>
        void ActualizarCuentasCiudades(CuentasCiudades c, DbTransaction transaction);
        /// <summary>
        /// Consulta los puntos de servicio por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        List<string> ConsultaProyectoPuntoServicio(long codigo);
        /// <summary>
        /// Crea un punto de servicio asociado a proyecto
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="codigo"></param>
        void CrearProyectoPuntoServicio(string ps, long codigo, DbTransaction transaction);
        /// <summary>
        /// Consulta los horarios por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        List<HorariosProyectos> ConsultaHorariosProyectosXCodigo(long codigo);
        /// <summary>
        /// Crea horario de proyecto
        /// </summary>
        /// <param name="h"></param>
        void CrearHorariosProyecto(HorariosProyectos h, DbTransaction transaction);
        /// <summary>
        /// Actualiza horario de proyecto
        /// </summary>
        /// <param name="h"></param>
        void ActualizarHorariosProyecto(HorariosProyectos h, DbTransaction transaction);
        /// <summary>
        /// Consulta los rangos de movilizado
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        List<Tarifa> ConsultaRangosMovilizado(long codigo);
        /// <summary>
        /// Crea rango de movilizado
        /// </summary>
        /// <param name="t"></param>
        void CrearRangosMovilizado(Tarifa t, DbTransaction transaction);
        /// <summary>
        /// Actualiza rango de movilizado
        /// </summary>
        /// <param name="t"></param>
        void ActualizarRangosMovilizado(Tarifa t, DbTransaction transaction);
        /// <summary>
        /// Consulta rango de operacion
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        List<Tarifa> ConsultaRangosOperacion(long codigo);
        /// <summary>
        /// Crea rango de operacion
        /// </summary>
        /// <param name="t"></param>
        void CrearRangosOperacion(Tarifa t, DbTransaction transaction);
        /// <summary>
        /// Actualiza rango de operacion
        /// </summary>
        /// <param name="t"></param>
        void ActualizarRangosOperacion(Tarifa t, DbTransaction transaction);
        /// <summary>
        /// Consulta comisiones de proyecto por operaciones por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        List<Tarifa> ConsultaComisionesProyectoXOperaciones(long codigo);
        /// <summary>
        /// Crea comision de proyecto por operacion
        /// </summary>
        /// <param name="t"></param>
        void CrearComisionesProyectoXOperaciones(Tarifa t, DbTransaction transaction);
        /// <summary>
        /// Actualiza comision de proyecto por operacion
        /// </summary>
        /// <param name="t"></param>
        void ActualizarComisionesProyectoXOperaciones(Tarifa t, DbTransaction transaction);
        /// <summary>
        /// Consulta comisiones de proyecto por movilizado por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        List<Tarifa> ConsultaComisionesProyectoXMovilizado(long codigo);
        /// <summary>
        /// Crea comision de proyecto por movilizado
        /// </summary>
        /// <param name="t"></param>
        void CrearComisionesProyectoXMovilizado(Tarifa t, DbTransaction transaction);
        /// <summary>
        /// Actualiza comision de proyecto por movilizado
        /// </summary>
        /// <param name="t"></param>
        void ActualizarComisionesProyectoXMovilizado(Tarifa t, DbTransaction transaction);
        /// <summary>
        /// Consulta la tabla en la que se debe guardar la tarifa
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        string ConsultaTablaTarifa(int codigo);

    }
}
