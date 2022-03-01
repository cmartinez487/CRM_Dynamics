using CRM.Dynamics.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CRM.Dynamics.AccesoDatos
{
    public class DaoProyectos : DaoObjectBase, IDaoProyectos
    {
        #region Singleton

        private static volatile DaoProyectos instancia;
        private static object syncRoot = new Object();

        public static DaoProyectos Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new DaoProyectos();
                    }
                }
                return instancia;
            }
        }

        #endregion

        /// <summary>
        /// Consulta un proyecto por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public Proyecto ConsultaProyectosXCodigo(long codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelProyectosXCodigo_CRM");
            DB.AddInParameter(comando, "@PROcodigo", DbType.Int64, codigo);
            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                #region PROYECTO
                var ListaProyectos = from row in dsCampos.Tables[0].AsEnumerable()
                                     select new Proyecto()
                                     {
                                         Codigo = row.Field<long>("PROcodigo"),
                                         Descripcion = row.Field<string>("PROdescripcion"),
                                         FechaCreacion = row.Field<DateTime>("PROfechacreacion"),
                                         Estado = row.Field<string>("PROestado"),
                                         CatEstado = row.Field<string>("PROcatestado"),
                                         Producto = row.Field<int>("PROproducto"),
                                         SubCosto = row.Field<string>("PROsubcosto"),
                                         DtoAutomatico = row.Field<bool>("PROdtoautomatico"),
                                         FormaPago = row.Field<string>("PROformapago"),
                                         TipoComision = row.Field<string>("PROtipocomision"),
                                         TidCliente = row.Field<string>("PROtidcliente"),
                                         IdCliente = row.Field<string>("PROidcliente"),
                                         Liquida = row.Field<bool?>("PROliquida"),
                                         Acumpuntos = row.Field<bool>("PROacumpuntos"),
                                         Observaciones = row.Field<string>("PROobservaciones"),
                                         TidVendedor = row.Field<string>("PROtidvendedor"),
                                         IdVendedor = row.Field<string>("PROidvendedor"),
                                         RecibeVencido = row.Field<bool?>("PROrecibevencido"),
                                         DuenoProyecto = row.Field<bool?>("PROduenoProyecto"),
                                         TipoLiquidacion = row.Field<string>("PROtipoliquidacion"),
                                         InicioContrato = row.Field<DateTime>("PROinicioContrato"),
                                         FinContrato = row.Field<DateTime>("PROfinContrato"),
                                         Vinculacion = row.Field<decimal?>("PROVinculacion"),
                                         Mantenimiento = row.Field<decimal?>("PROMantenimiento"),
                                         CapturaCheque = row.Field<bool>("PROCapturaCheque"),
                                         Basica = row.Field<decimal?>("PRObasica"),
                                         Variable = row.Field<decimal?>("PROvariable"),
                                         IvaIncluido = row.Field<bool>("PROIvaIncluido"),
                                         CuentaBanco = row.Field<string>("PROcuentabanco"),
                                         ModificadorArchivo = row.Field<short?>("PROmodificadorarchivo"),//
                                         FechaAsobancaria = row.Field<DateTime?>("PROfechaasobancaria"),
                                         CodigoEan13 = row.Field<string>("PROcodigoean13"),//
                                         EntidadRecaudadora = row.Field<string>("PROentidadrecaudadora"),//
                                         PlazoDiasPago = row.Field<byte?>("PROPlazoDiasPago"),//
                                         Ambiente = row.Field<string>("PROambiente"),
                                         Asobancaria = row.Field<bool?>("PROasobancaria"),
                                         CodBarras = row.Field<bool?>("PROcodbarras"),
                                         ServicioPublico = row.Field<bool?>("PROServicioPublico"),
                                         Duplicados = row.Field<bool?>("PRODuplicados"),
                                         PorcentajeMinimoReportadas = row.Field<double?>("PROPorcentajeMinimoReportadas"),//
                                         TiempoMinimoParaReportar = row.Field<long?>("PROTiempoMinimoParaReportar"),
                                         NotasCobranComision = row.Field<bool?>("PRONotasCobranComision"),
                                         ReembolsaComision = row.Field<bool?>("PROReembolsaComision"),
                                         MinimoGarantizado = row.Field<decimal?>("PROMinimoGarantizado"),//
                                         ValorPenalizacion = row.Field<decimal?>("PROValorPenalizacion"),//
                                         CodTipoCom = row.Field<int?>("PROCodTipoCom"),
                                         PeriodoLiq = row.Field<string>("PROPeriodoLiq"),//
                                         CostoTotalProyecto = row.Field<decimal?>("PROCostoTotalProyecto"),//
                                         TarifaProdDevuelto = row.Field<decimal?>("PROTarifaProdDevuelto"),//
                                         CcPeople = row.Field<string>("PROCcPeople"),
                                         DiasFacturacion = row.Field<string>("PRODiasFacturacion"),
                                         FechaActualizacion = row.Field<DateTime?>("PROfechaActualizacion"),
                                         RecogerFueraDeLinea = row.Field<bool?>("PRORecogerFueraDeLinea"),
                                         ValidarCampos = row.Field<bool?>("PROValidarCampos"),
                                         GrabarNoValidados = row.Field<bool?>("PROGrabarNoValidados"),
                                         NombreProcedimientoValidaCampos = row.Field<string>("PRONombreProcedimientoValidaCampos"),
                                         Internacionalizacion = row.Field<bool?>("PROYInternacionalizacion"),
                                         ValidaBaseCliente = row.Field<bool>("PROYValidaBaseCliente"),
                                         SoloCapturaCodigoBarras = row.Field<bool>("PROYSoloCapturaCodigoBarras"),
                                         TipoProyecto = row.Field<string>("PROtipoProyecto"),//
                                         PLP = row.Field<int?>("PROPLP"),
                                         ComisionProyecto = row.Field<bool?>("PROComisionProyecto"),
                                         ProrrogaAutoContrato = row.Field<bool?>("PROProrrogaAutoContrato"),
                                         DuracionContrato = row.Field<int?>("PRODuracionContrato"),
                                         ComisionPS = row.Field<decimal>("PROComisionPS"),//
                                         ContraprestacionRecaudo = row.Field<decimal?>("PROContraprestacionRecaudo"),
                                         ContraprestacionPago = row.Field<decimal?>("PROContraprestacionPago"),
                                         ConsultaInformacion = row.Field<bool?>("PROConsultaInformacion"),
                                         ValorModificable = row.Field<bool?>("PROValorModificable"),
                                         ProveedorValidacion = row.Field<string>("PROProveedorValidacion"),
                                         ProveedorConsulta = row.Field<string>("PROProveedorConsulta"),
                                         NombreLogoCDT = row.Field<string>("PRONombreLogoCDT"),
                                         ProcPag = row.Field<string>("PROprocPag"),
                                         HaceReintento = row.Field<bool?>("PROHaceReintento"),
                                         ProveedorProcesamiento = row.Field<string>("PROProveedorProcesamiento"),
                                         PagoCualquierPS = row.Field<bool?>("PROPagoCualquierPS"),
                                         ConsultorNegocio = row.Field<string>("PROConsultorNegocio"),
                                         DiasMaxVencimiento = row.Field<int?>("PROdiasMaxVencimiento"),
                                         ImprimeCopia = row.Field<bool>("PROImprimeCopia"),
                                         PagoPIN = row.Field<bool?>("PROPagoPIN"),
                                         ControlaFondoOpe = row.Field<bool?>("PROcontrolaFondoOpe"),
                                         DiasLiquidacion = ConsultaDiasLiquidacionXCodigo(row.Field<long>("PROcodigo")),
                                         CuentasCiudades = ConsultaCuentasCiudadesXCodigo(row.Field<long>("PROcodigo")),
                                         PuntosServicio = ConsultaProyectoPuntoServicio(row.Field<long>("PROcodigo")),
                                         HorariosProyectos = ConsultaHorariosProyectosXCodigo(row.Field<long>("PROcodigo")),
                                         Tarifas = ConsultaTarifas(row.Field<int?>("PROCodTipoCom").Value, row.Field<long>("PROcodigo"))
                                     };
                return ListaProyectos.ToList()[0];
                #endregion
            }
            return new Proyecto();
        }

        /// <summary>
        /// Consulta todos los proyectos
        /// </summary>
        /// <returns></returns>
        public List<Proyecto> ConsultaProyectos()
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelProyectosXCodigo_CRM");

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                #region PROYECTO
                var ListaProyectos = from row in dsCampos.Tables[0].AsEnumerable()
                                     select new Proyecto()
                                     {
                                         Codigo = row.Field<long>("PROcodigo"),
                                         Descripcion = row.Field<string>("PROdescripcion"),
                                         FechaCreacion = row.Field<DateTime>("PROfechacreacion"),
                                         Estado = row.Field<string>("PROestado"),
                                         CatEstado = row.Field<string>("PROcatestado"),
                                         Producto = row.Field<int>("PROproducto"),
                                         SubCosto = row.Field<string>("PROsubcosto"),
                                         DtoAutomatico = row.Field<bool>("PROdtoautomatico"),
                                         FormaPago = row.Field<string>("PROformapago"),
                                         TipoComision = row.Field<string>("PROtipocomision"),
                                         TidCliente = row.Field<string>("PROtidcliente"),
                                         IdCliente = row.Field<string>("PROidcliente"),
                                         Liquida = row.Field<bool?>("PROliquida"),
                                         Acumpuntos = row.Field<bool>("PROacumpuntos"),
                                         Observaciones = row.Field<string>("PROobservaciones"),
                                         TidVendedor = row.Field<string>("PROtidvendedor"),
                                         IdVendedor = row.Field<string>("PROidvendedor"),
                                         RecibeVencido = row.Field<bool?>("PROrecibevencido"),
                                         DuenoProyecto = row.Field<bool?>("PROduenoProyecto"),
                                         TipoLiquidacion = row.Field<string>("PROtipoliquidacion"),
                                         InicioContrato = row.Field<DateTime>("PROinicioContrato"),
                                         FinContrato = row.Field<DateTime>("PROfinContrato"),
                                         Vinculacion = row.Field<decimal?>("PROVinculacion"),
                                         Mantenimiento = row.Field<decimal?>("PROMantenimiento"),
                                         CapturaCheque = row.Field<bool>("PROCapturaCheque"),
                                         Basica = row.Field<decimal?>("PRObasica"),
                                         Variable = row.Field<decimal?>("PROvariable"),
                                         IvaIncluido = row.Field<bool>("PROIvaIncluido"),
                                         CuentaBanco = row.Field<string>("PROcuentabanco"),
                                         ModificadorArchivo = row.Field<short?>("PROmodificadorarchivo"),//
                                         FechaAsobancaria = row.Field<DateTime?>("PROfechaasobancaria"),
                                         CodigoEan13 = row.Field<string>("PROcodigoean13"),//
                                         EntidadRecaudadora = row.Field<string>("PROentidadrecaudadora"),//
                                         PlazoDiasPago = row.Field<byte?>("PROPlazoDiasPago"),//
                                         Ambiente = row.Field<string>("PROambiente"),
                                         Asobancaria = row.Field<bool?>("PROasobancaria"),
                                         CodBarras = row.Field<bool?>("PROcodbarras"),
                                         ServicioPublico = row.Field<bool?>("PROServicioPublico"),
                                         Duplicados = row.Field<bool?>("PRODuplicados"),
                                         PorcentajeMinimoReportadas = row.Field<double?>("PROPorcentajeMinimoReportadas"),//
                                         TiempoMinimoParaReportar = row.Field<long?>("PROTiempoMinimoParaReportar"),
                                         NotasCobranComision = row.Field<bool?>("PRONotasCobranComision"),
                                         ReembolsaComision = row.Field<bool?>("PROReembolsaComision"),
                                         MinimoGarantizado = row.Field<decimal?>("PROMinimoGarantizado"),//
                                         ValorPenalizacion = row.Field<decimal?>("PROValorPenalizacion"),//
                                         CodTipoCom = row.Field<int?>("PROCodTipoCom"),
                                         PeriodoLiq = row.Field<string>("PROPeriodoLiq"),//
                                         CostoTotalProyecto = row.Field<decimal?>("PROCostoTotalProyecto"),//
                                         TarifaProdDevuelto = row.Field<decimal?>("PROTarifaProdDevuelto"),//
                                         CcPeople = row.Field<string>("PROCcPeople"),
                                         DiasFacturacion = row.Field<string>("PRODiasFacturacion"),
                                         FechaActualizacion = row.Field<DateTime?>("PROfechaActualizacion"),
                                         RecogerFueraDeLinea = row.Field<bool?>("PRORecogerFueraDeLinea"),
                                         ValidarCampos = row.Field<bool?>("PROValidarCampos"),
                                         GrabarNoValidados = row.Field<bool?>("PROGrabarNoValidados"),
                                         NombreProcedimientoValidaCampos = row.Field<string>("PRONombreProcedimientoValidaCampos"),
                                         Internacionalizacion = row.Field<bool?>("PROYInternacionalizacion"),
                                         ValidaBaseCliente = row.Field<bool>("PROYValidaBaseCliente"),
                                         SoloCapturaCodigoBarras = row.Field<bool>("PROYSoloCapturaCodigoBarras"),
                                         TipoProyecto = row.Field<string>("PROtipoProyecto"),//
                                         PLP = row.Field<int?>("PROPLP"),
                                         ComisionProyecto = row.Field<bool?>("PROComisionProyecto"),
                                         ProrrogaAutoContrato = row.Field<bool?>("PROProrrogaAutoContrato"),
                                         DuracionContrato = row.Field<int?>("PRODuracionContrato"),
                                         ComisionPS = row.Field<decimal>("PROComisionPS"),//
                                         ContraprestacionRecaudo = row.Field<decimal?>("PROContraprestacionRecaudo"),
                                         ContraprestacionPago = row.Field<decimal?>("PROContraprestacionPago"),
                                         ConsultaInformacion = row.Field<bool?>("PROConsultaInformacion"),
                                         ValorModificable = row.Field<bool?>("PROValorModificable"),
                                         ProveedorValidacion = row.Field<string>("PROProveedorValidacion"),
                                         ProveedorConsulta = row.Field<string>("PROProveedorConsulta"),
                                         NombreLogoCDT = row.Field<string>("PRONombreLogoCDT"),
                                         ProcPag = row.Field<string>("PROprocPag"),
                                         HaceReintento = row.Field<bool?>("PROHaceReintento"),
                                         ProveedorProcesamiento = row.Field<string>("PROProveedorProcesamiento"),
                                         PagoCualquierPS = row.Field<bool?>("PROPagoCualquierPS"),
                                         ConsultorNegocio = row.Field<string>("PROConsultorNegocio"),
                                         DiasMaxVencimiento = row.Field<int?>("PROdiasMaxVencimiento"),
                                         ImprimeCopia = row.Field<bool>("PROImprimeCopia"),
                                         PagoPIN = row.Field<bool?>("PROPagoPIN"),
                                         ControlaFondoOpe = row.Field<bool?>("PROcontrolaFondoOpe")
                                     };
                return ListaProyectos.ToList<Proyecto>();
                #endregion
            }
            return new List<Proyecto>();
        }

        /// <summary>
        /// Crea un proyecto
        /// </summary>
        /// <param name="p"></param>
        public string CrearProyecto(Proyecto p)
        {
            string mensaje = "";

            ValidaCampos(p);

            #region CREA PROYECTO

            using (DbConnection connection = DB.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    DbCommand comando = DB.GetStoredProcCommand("spInsProyectos_CRM");
                    DB.AddInParameter(comando, "@PROcodigo", DbType.Int64, p.Codigo);
                    DB.AddInParameter(comando, "@PROdescripcion", DbType.String, p.Descripcion);
                    DB.AddInParameter(comando, "@PROfechacreacion", DbType.DateTime, p.FechaCreacion);
                    DB.AddInParameter(comando, "@PROestado", DbType.String, p.Estado);
                    DB.AddInParameter(comando, "@PROcatestado", DbType.String, p.CatEstado);
                    DB.AddInParameter(comando, "@PROproducto", DbType.Int32, p.Producto);
                    DB.AddInParameter(comando, "@PROsubcosto", DbType.String, p.SubCosto);
                    DB.AddInParameter(comando, "@PROdtoautomatico", DbType.Boolean, p.DtoAutomatico);
                    DB.AddInParameter(comando, "@PROformapago", DbType.String, p.FormaPago);
                    DB.AddInParameter(comando, "@PROtipocomision", DbType.String, p.TipoComision);
                    DB.AddInParameter(comando, "@PROtidcliente", DbType.String, p.TidCliente);
                    DB.AddInParameter(comando, "@PROidcliente", DbType.String, p.IdCliente);
                    DB.AddInParameter(comando, "@PROliquida", DbType.Boolean, p.Liquida);
                    DB.AddInParameter(comando, "@PROacumpuntos", DbType.Boolean, p.Acumpuntos);
                    DB.AddInParameter(comando, "@PROobservaciones", DbType.String, p.Observaciones);
                    DB.AddInParameter(comando, "@PROtidvendedor", DbType.String, p.TidVendedor);
                    DB.AddInParameter(comando, "@PROidvendedor", DbType.String, p.IdVendedor);
                    DB.AddInParameter(comando, "@PROrecibevencido", DbType.Boolean, p.RecibeVencido);
                    DB.AddInParameter(comando, "@PROduenoProyecto", DbType.Boolean, p.DuenoProyecto);
                    DB.AddInParameter(comando, "@PROtipoliquidacion", DbType.String, p.TipoLiquidacion);
                    DB.AddInParameter(comando, "@PROinicioContrato", DbType.DateTime, p.InicioContrato);
                    DB.AddInParameter(comando, "@PROfinContrato", DbType.DateTime, p.FinContrato);
                    DB.AddInParameter(comando, "@PROVinculacion", DbType.Decimal, p.Vinculacion);
                    DB.AddInParameter(comando, "@PROMantenimiento", DbType.Decimal, p.Mantenimiento);
                    DB.AddInParameter(comando, "@PROCapturaCheque", DbType.Boolean, p.CapturaCheque);
                    DB.AddInParameter(comando, "@PRObasica", DbType.Decimal, p.Basica);
                    DB.AddInParameter(comando, "@PROvariable", DbType.Decimal, p.Variable);
                    DB.AddInParameter(comando, "@PROIvaIncluido", DbType.Boolean, p.IvaIncluido);
                    DB.AddInParameter(comando, "@PROcuentabanco", DbType.String, p.CuentaBanco);
                    DB.AddInParameter(comando, "@PROmodificadorarchivo", DbType.Int32, p.ModificadorArchivo);
                    DB.AddInParameter(comando, "@PROfechaasobancaria", DbType.DateTime, p.FechaAsobancaria);
                    DB.AddInParameter(comando, "@PROcodigoean13", DbType.String, p.CodigoEan13);
                    DB.AddInParameter(comando, "@PROentidadrecaudadora", DbType.String, p.EntidadRecaudadora);
                    DB.AddInParameter(comando, "@PROPlazoDiasPago", DbType.Int32, p.PlazoDiasPago);
                    DB.AddInParameter(comando, "@PROambiente", DbType.String, p.Ambiente);
                    DB.AddInParameter(comando, "@PROasobancaria", DbType.Boolean, p.Asobancaria);
                    DB.AddInParameter(comando, "@PROcodbarras", DbType.Boolean, p.CodBarras);
                    DB.AddInParameter(comando, "@PROServicioPublico", DbType.Boolean, p.ServicioPublico);
                    DB.AddInParameter(comando, "@PRODuplicados", DbType.Boolean, p.Duplicados);
                    DB.AddInParameter(comando, "@PROPorcentajeMinimoReportadas", DbType.Decimal, p.PorcentajeMinimoReportadas);
                    DB.AddInParameter(comando, "@PROTiempoMinimoParaReportar", DbType.Int64, p.TiempoMinimoParaReportar);
                    DB.AddInParameter(comando, "@PRONotasCobranComision", DbType.Boolean, p.NotasCobranComision);
                    DB.AddInParameter(comando, "@PROReembolsaComision", DbType.Boolean, p.ReembolsaComision);
                    DB.AddInParameter(comando, "@PROMinimoGarantizado", DbType.Decimal, p.MinimoGarantizado);
                    DB.AddInParameter(comando, "@PROValorPenalizacion", DbType.Decimal, p.ValorPenalizacion);
                    DB.AddInParameter(comando, "@PROCodTipoCom", DbType.Int32, p.CodTipoCom);
                    DB.AddInParameter(comando, "@PROPeriodoLiq", DbType.String, p.PeriodoLiq);
                    DB.AddInParameter(comando, "@PROCostoTotalProyecto", DbType.Decimal, p.CostoTotalProyecto);
                    DB.AddInParameter(comando, "@PROTarifaProdDevuelto", DbType.Decimal, p.TarifaProdDevuelto);
                    DB.AddInParameter(comando, "@PROCcPeople", DbType.String, p.CcPeople);
                    DB.AddInParameter(comando, "@PRODiasFacturacion", DbType.String, p.DiasFacturacion);
                    DB.AddInParameter(comando, "@PROfechaActualizacion", DbType.DateTime, p.FechaActualizacion);
                    DB.AddInParameter(comando, "@PRORecogerFueraDeLinea", DbType.Boolean, p.RecogerFueraDeLinea);
                    DB.AddInParameter(comando, "@PROValidarCampos", DbType.Boolean, p.ValidarCampos);
                    DB.AddInParameter(comando, "@PROGrabarNoValidados", DbType.Boolean, p.GrabarNoValidados);
                    DB.AddInParameter(comando, "@PRONombreProcedimientoValidaCampos", DbType.String, p.NombreProcedimientoValidaCampos);
                    DB.AddInParameter(comando, "@PROYInternacionalizacion", DbType.Boolean, p.Internacionalizacion);
                    DB.AddInParameter(comando, "@PROYValidaBaseCliente", DbType.Boolean, p.ValidaBaseCliente);
                    DB.AddInParameter(comando, "@PROYSoloCapturaCodigoBarras", DbType.Boolean, p.SoloCapturaCodigoBarras);
                    DB.AddInParameter(comando, "@PROtipoProyecto", DbType.String, p.TipoProyecto);
                    DB.AddInParameter(comando, "@PROPLP", DbType.Int32, p.PLP);
                    DB.AddInParameter(comando, "@PROComisionProyecto", DbType.Boolean, p.ComisionProyecto);
                    DB.AddInParameter(comando, "@PROProrrogaAutoContrato", DbType.Boolean, p.ProrrogaAutoContrato);
                    DB.AddInParameter(comando, "@PRODuracionContrato", DbType.Int32, p.DuracionContrato);
                    DB.AddInParameter(comando, "@PROComisionPS", DbType.Decimal, p.ComisionPS);
                    DB.AddInParameter(comando, "@PROContraprestacionRecaudo", DbType.Decimal, p.ContraprestacionRecaudo);
                    DB.AddInParameter(comando, "@PROContraprestacionPago", DbType.Decimal, p.ContraprestacionPago);
                    DB.AddInParameter(comando, "@PROConsultaInformacion", DbType.Boolean, p.ConsultaInformacion);
                    DB.AddInParameter(comando, "@PROValorModificable", DbType.Boolean, p.ValorModificable);
                    DB.AddInParameter(comando, "@PROProveedorValidacion", DbType.String, p.ProveedorValidacion);
                    DB.AddInParameter(comando, "@PROProveedorConsulta", DbType.String, p.ProveedorConsulta);
                    DB.AddInParameter(comando, "@PRONombreLogoCDT", DbType.String, p.NombreLogoCDT);
                    DB.AddInParameter(comando, "@PROProcPag", DbType.String, p.ProcPag);
                    DB.AddInParameter(comando, "@PROHaceReintento", DbType.Boolean, p.HaceReintento);
                    DB.AddInParameter(comando, "@PROProveedorProcesamiento", DbType.String, p.ProveedorProcesamiento);
                    DB.AddInParameter(comando, "@PROPagoCualquierPS", DbType.Boolean, p.PagoCualquierPS);
                    DB.AddInParameter(comando, "@PROConsultorNegocio", DbType.String, p.ConsultorNegocio);
                    DB.AddInParameter(comando, "@PROdiasMaxVencimiento", DbType.Int32, p.DiasMaxVencimiento);
                    DB.AddInParameter(comando, "@PROImprimeCopia", DbType.Boolean, p.ImprimeCopia);
                    DB.AddInParameter(comando, "@PROPagoPIN", DbType.Boolean, p.PagoPIN);
                    DB.AddInParameter(comando, "@PROcontrolaFondoOpe", DbType.Boolean, p.ControlaFondoOpe);

                    this.DB.ExecuteDataSet(comando, transaction);

                    mensaje = "proyecto creado";

                    #endregion

                    if (p.DiasLiquidacion != null && !string.IsNullOrEmpty(Convert.ToString(p.DiasLiquidacion.Count())))
                    {
                        foreach (var item in p.DiasLiquidacion)
                        {
                            CrearDiasLiquidacion(item, transaction);
                            mensaje = mensaje + ", Dias Liquidacion Creado";
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ", Dias Liquidacion No Creado, Verificar";
                    }

                    if (p.CuentasCiudades != null && !string.IsNullOrEmpty(Convert.ToString(p.CuentasCiudades.Count())))
                    {
                        foreach (var item in p.CuentasCiudades)
                        {
                            CrearCuentasCiudades(item, transaction);
                            mensaje = mensaje + ", Cuenta Ciudades Creado";
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ", Cuenta Ciudades No Creado, Verificar";
                    }

                    if (p.PuntosServicio != null && !string.IsNullOrEmpty(Convert.ToString(p.PuntosServicio.Count())))
                    {
                        foreach (var item in p.PuntosServicio)
                        {
                            CrearProyectoPuntoServicio(item, p.Codigo, transaction);
                            mensaje = mensaje + ", asociacion Proyecto - Punto de servicio Creada";
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ",  asociacion Proyecto - Punto de servicio No Creada, Verificar";
                    }

                    if (p.HorariosProyectos != null && !string.IsNullOrEmpty(Convert.ToString(p.HorariosProyectos.Count())))
                    {
                        foreach (var item in p.HorariosProyectos)
                        {
                            CrearHorariosProyecto(item, transaction);
                            mensaje = mensaje + ", Horario Proyecto Creado";
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ",  Horario Proyecto No Creado, Verificar";
                    }

                    if (p.Tarifas != null && !string.IsNullOrEmpty(Convert.ToString(p.Tarifas.Count())))
                    {
                        foreach (var item in p.Tarifas)
                        {
                            CrearTarifa(item, p.CodTipoCom.Value, transaction);
                            mensaje = mensaje + ", Tarifa Creada";
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ", Tarifa No Creada, Verificar";
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    mensaje = e.Message;
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    transaction = null;
                    connection.Close();
                }
            }

            return mensaje;
        }

        /// <summary>
        /// Actualiza un proyecto
        /// </summary>
        /// <param name="p"></param>
        public string ActualizarProyecto(Proyecto p)
        {
            string mensaje = "";

            #region ACTUALIZA PROYECTO
            //ValidaCampos(p);

            using (DbConnection connection = DB.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    DbCommand comando = DB.GetStoredProcCommand("spUpdProyectos_CRM");
                    DB.AddInParameter(comando, "@PROcodigo", DbType.Int64, p.Codigo);
                    DB.AddInParameter(comando, "@PROdescripcion", DbType.String, p.Descripcion);
                    DB.AddInParameter(comando, "@PROfechacreacion", DbType.DateTime, p.FechaCreacion);
                    DB.AddInParameter(comando, "@PROestado", DbType.String, p.Estado);
                    DB.AddInParameter(comando, "@PROcatestado", DbType.String, p.CatEstado);
                    DB.AddInParameter(comando, "@PROproducto", DbType.Int32, p.Producto);
                    DB.AddInParameter(comando, "@PROsubcosto", DbType.String, p.SubCosto);
                    DB.AddInParameter(comando, "@PROdtoautomatico", DbType.Boolean, p.DtoAutomatico);
                    DB.AddInParameter(comando, "@PROformapago", DbType.String, p.FormaPago);
                    DB.AddInParameter(comando, "@PROtipocomision", DbType.String, p.TipoComision);
                    DB.AddInParameter(comando, "@PROtidcliente", DbType.String, p.TidCliente);
                    DB.AddInParameter(comando, "@PROidcliente", DbType.String, p.IdCliente);
                    DB.AddInParameter(comando, "@PROliquida", DbType.Boolean, p.Liquida);
                    DB.AddInParameter(comando, "@PROacumpuntos", DbType.Boolean, p.Acumpuntos);
                    DB.AddInParameter(comando, "@PROobservaciones", DbType.String, p.Observaciones);
                    DB.AddInParameter(comando, "@PROtidvendedor", DbType.String, p.TidVendedor);
                    DB.AddInParameter(comando, "@PROidvendedor", DbType.String, p.IdVendedor);
                    DB.AddInParameter(comando, "@PROrecibevencido", DbType.Boolean, p.RecibeVencido);
                    DB.AddInParameter(comando, "@PROduenoProyecto", DbType.Boolean, p.DuenoProyecto);
                    DB.AddInParameter(comando, "@PROtipoliquidacion", DbType.String, p.TipoLiquidacion);
                    DB.AddInParameter(comando, "@PROinicioContrato", DbType.DateTime, p.InicioContrato);
                    DB.AddInParameter(comando, "@PROfinContrato", DbType.DateTime, p.FinContrato);
                    DB.AddInParameter(comando, "@PROVinculacion", DbType.Decimal, p.Vinculacion);
                    DB.AddInParameter(comando, "@PROMantenimiento", DbType.Decimal, p.Mantenimiento);
                    DB.AddInParameter(comando, "@PROCapturaCheque", DbType.Boolean, p.CapturaCheque);
                    DB.AddInParameter(comando, "@PRObasica", DbType.Decimal, p.Basica);
                    DB.AddInParameter(comando, "@PROvariable", DbType.Decimal, p.Variable);
                    DB.AddInParameter(comando, "@PROIvaIncluido", DbType.Boolean, p.IvaIncluido);
                    DB.AddInParameter(comando, "@PROcuentabanco", DbType.String, p.CuentaBanco);
                    DB.AddInParameter(comando, "@PROmodificadorarchivo", DbType.Int32, p.ModificadorArchivo);
                    DB.AddInParameter(comando, "@PROfechaasobancaria", DbType.DateTime, p.FechaAsobancaria);
                    DB.AddInParameter(comando, "@PROcodigoean13", DbType.String, p.CodigoEan13);
                    DB.AddInParameter(comando, "@PROentidadrecaudadora", DbType.String, p.EntidadRecaudadora);
                    DB.AddInParameter(comando, "@PROPlazoDiasPago", DbType.Int32, p.PlazoDiasPago);
                    DB.AddInParameter(comando, "@PROambiente", DbType.String, p.Ambiente);
                    DB.AddInParameter(comando, "@PROasobancaria", DbType.Boolean, p.Asobancaria);
                    DB.AddInParameter(comando, "@PROcodbarras", DbType.Boolean, p.CodBarras);
                    DB.AddInParameter(comando, "@PROServicioPublico", DbType.Boolean, p.ServicioPublico);
                    DB.AddInParameter(comando, "@PRODuplicados", DbType.Boolean, p.Duplicados);
                    DB.AddInParameter(comando, "@PROPorcentajeMinimoReportadas", DbType.Decimal, p.PorcentajeMinimoReportadas);
                    DB.AddInParameter(comando, "@PROTiempoMinimoParaReportar", DbType.Int64, p.TiempoMinimoParaReportar);
                    DB.AddInParameter(comando, "@PRONotasCobranComision", DbType.Boolean, p.NotasCobranComision);
                    DB.AddInParameter(comando, "@PROReembolsaComision", DbType.Boolean, p.ReembolsaComision);
                    DB.AddInParameter(comando, "@PROMinimoGarantizado", DbType.Decimal, p.MinimoGarantizado);
                    DB.AddInParameter(comando, "@PROValorPenalizacion", DbType.Decimal, p.ValorPenalizacion);
                    DB.AddInParameter(comando, "@PROCodTipoCom", DbType.Int32, p.CodTipoCom);
                    DB.AddInParameter(comando, "@PROPeriodoLiq", DbType.String, p.PeriodoLiq);
                    DB.AddInParameter(comando, "@PROCostoTotalProyecto", DbType.Decimal, p.CostoTotalProyecto);
                    DB.AddInParameter(comando, "@PROTarifaProdDevuelto", DbType.Decimal, p.TarifaProdDevuelto);
                    DB.AddInParameter(comando, "@PROCcPeople", DbType.String, p.CcPeople);
                    DB.AddInParameter(comando, "@PRODiasFacturacion", DbType.String, p.DiasFacturacion);
                    DB.AddInParameter(comando, "@PROfechaActualizacion", DbType.DateTime, p.FechaActualizacion);
                    DB.AddInParameter(comando, "@PRORecogerFueraDeLinea", DbType.Boolean, p.RecogerFueraDeLinea);
                    DB.AddInParameter(comando, "@PROValidarCampos", DbType.Boolean, p.ValidarCampos);
                    DB.AddInParameter(comando, "@PROGrabarNoValidados", DbType.Boolean, p.GrabarNoValidados);
                    DB.AddInParameter(comando, "@PRONombreProcedimientoValidaCampos", DbType.String, p.NombreProcedimientoValidaCampos);
                    DB.AddInParameter(comando, "@PROYInternacionalizacion", DbType.Boolean, p.Internacionalizacion);
                    DB.AddInParameter(comando, "@PROYValidaBaseCliente", DbType.Boolean, p.ValidaBaseCliente);
                    DB.AddInParameter(comando, "@PROYSoloCapturaCodigoBarras", DbType.Boolean, p.SoloCapturaCodigoBarras);
                    DB.AddInParameter(comando, "@PROtipoProyecto", DbType.String, p.TipoProyecto);
                    DB.AddInParameter(comando, "@PROPLP", DbType.Int32, p.PLP);
                    DB.AddInParameter(comando, "@PROComisionProyecto", DbType.Boolean, p.ComisionProyecto);
                    DB.AddInParameter(comando, "@PROProrrogaAutoContrato", DbType.Boolean, p.ProrrogaAutoContrato);
                    DB.AddInParameter(comando, "@PRODuracionContrato", DbType.Int32, p.DuracionContrato);
                    DB.AddInParameter(comando, "@PROComisionPS", DbType.Decimal, p.ComisionPS);
                    DB.AddInParameter(comando, "@PROContraprestacionRecaudo", DbType.Decimal, p.ContraprestacionRecaudo);
                    DB.AddInParameter(comando, "@PROContraprestacionPago", DbType.Decimal, p.ContraprestacionPago);
                    DB.AddInParameter(comando, "@PROConsultaInformacion", DbType.Boolean, p.ConsultaInformacion);
                    DB.AddInParameter(comando, "@PROValorModificable", DbType.Boolean, p.ValorModificable);
                    DB.AddInParameter(comando, "@PROProveedorValidacion", DbType.String, p.ProveedorValidacion);
                    DB.AddInParameter(comando, "@PROProveedorConsulta", DbType.String, p.ProveedorConsulta);
                    DB.AddInParameter(comando, "@PRONombreLogoCDT", DbType.String, p.NombreLogoCDT);
                    DB.AddInParameter(comando, "@PROProcPag", DbType.String, p.ProcPag);
                    DB.AddInParameter(comando, "@PROHaceReintento", DbType.Boolean, p.HaceReintento);
                    DB.AddInParameter(comando, "@PROProveedorProcesamiento", DbType.String, p.ProveedorProcesamiento);
                    DB.AddInParameter(comando, "@PROPagoCualquierPS", DbType.Boolean, p.PagoCualquierPS);
                    DB.AddInParameter(comando, "@PROConsultorNegocio", DbType.String, p.ConsultorNegocio);
                    DB.AddInParameter(comando, "@PROdiasMaxVencimiento", DbType.Int32, p.DiasMaxVencimiento);
                    DB.AddInParameter(comando, "@PROImprimeCopia", DbType.Boolean, p.ImprimeCopia);
                    DB.AddInParameter(comando, "@PROPagoPIN", DbType.Boolean, p.PagoPIN);
                    DB.AddInParameter(comando, "@PROcontrolaFondoOpe", DbType.Boolean, p.ControlaFondoOpe);

                    this.DB.ExecuteDataSet(comando, transaction);

                    mensaje = "proyecto actualizado";
                    #endregion

                    if (p.DiasLiquidacion != null && !string.IsNullOrEmpty(Convert.ToString(p.DiasLiquidacion.Count())))
                    {
                        foreach (var item in p.DiasLiquidacion)
                        {
                            ValidaCampos(item);
                            if (ConsultaDiasLiquidacionXCodigo(p.Codigo).Exists(x => x.Combinacion == item.Combinacion && x.Liquidacion == item.Liquidacion))
                            {
                                ActualizarDiasLiquidacion(item, transaction);
                                mensaje = mensaje + ", Dias Liquidacion Actualizado";
                            }
                            else
                            {
                                CrearDiasLiquidacion(item, transaction);
                                mensaje = mensaje + ", Dias Liquidacion Creado";
                            }
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ", Dias Liquidacion No Actualizado, Verificar";
                    }


                    if (p.CuentasCiudades != null && !string.IsNullOrEmpty(Convert.ToString(p.CuentasCiudades.Count())))
                    {
                        foreach (var item in p.CuentasCiudades)
                        {
                            ValidaCampos(item);
                            if (ConsultaCuentasCiudadesXCodigo(p.Codigo).Count > 0)
                            {
                                ActualizarCuentasCiudades(item, transaction);
                                mensaje = mensaje + ", Cuenta Ciudades Actualizada";
                            }
                            else
                            {
                                CrearCuentasCiudades(item, transaction);
                                mensaje = mensaje + ", Cuenta Ciudades Creada";
                            }
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ", Cuenta Ciudades No Actualizado, Verificar";
                    }

                    if (p.PuntosServicio != null && !string.IsNullOrEmpty(Convert.ToString(p.PuntosServicio.Count())))
                    {
                        foreach (var item in p.PuntosServicio)
                        {
                            if (!ConsultaProyectoPuntoServicio(p.Codigo).Exists(x => x == item))
                            {
                                CrearProyectoPuntoServicio(item, p.Codigo, transaction);
                                mensaje = mensaje + ", asociacion Proyecto - Punto de servicio Creada";
                            }
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ",  asociacion Proyecto - Punto de servicio No Actualizada, Verificar";
                    }

                    if (p.HorariosProyectos != null && !string.IsNullOrEmpty(Convert.ToString(p.HorariosProyectos.Count())))
                    {
                        foreach (var item in p.HorariosProyectos)
                        {
                            ValidaCampos(item);
                            if (ConsultaHorariosProyectosXCodigo(p.Codigo).Exists(x => x.Dia == item.Dia))
                            {
                                ActualizarHorariosProyecto(item, transaction);
                                mensaje = mensaje + ", Horario Proyecto Actualizado";
                            }
                            else
                            {
                                CrearHorariosProyecto(item, transaction);
                                mensaje = mensaje + ", Horario Proyecto Creado";
                            }
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ",  Horario Proyecto No Actualizado, Verificar";
                    }

                    if (p.Tarifas != null && !string.IsNullOrEmpty(Convert.ToString(p.Tarifas.Count())))
                    {
                        foreach (var item in p.Tarifas)
                        {
                            ActualizarTarifa(item, p.CodTipoCom.Value, transaction);
                            mensaje = mensaje + ", Tarifa Actualizada";
                        }
                    }
                    else
                    {
                        mensaje = mensaje + ", Tarifa No Actualizada, Verificar";
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    mensaje = e.Message;
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    transaction = null;
                    connection.Close();
                }
            }

            return mensaje;
        }

        /// <summary>
        /// Consulta los dias de liquidacion por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<DiasLiquidacion> ConsultaDiasLiquidacionXCodigo(long codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelDiasLiquidacion_CRM");
            DB.AddInParameter(comando, "@DLIQproyecto", DbType.Int64, codigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaDias = from row in dsCampos.Tables[0].AsEnumerable()
                                select new DiasLiquidacion()
                                {
                                    CodProyecto = row.Field<long>("DLIQproyecto"),
                                    Combinacion = row.Field<string>("DLIQcombinacion"),
                                    Liquidacion = row.Field<string>("DLIQliquidacion"),
                                    DiaPago = row.Field<string>("DLIQdiapago"),
                                    Cuenta = row.Field<string>("DLIQCuenta")
                                };
                return ListaDias.ToList<DiasLiquidacion>();
            }
            return new List<DiasLiquidacion>();
        }

        /// <summary>
        /// Crea un dia de liquidacion
        /// </summary>
        /// <param name="d"></param>
        public void CrearDiasLiquidacion(DiasLiquidacion d, DbTransaction transaction)
        {
            ValidaCampos(d);
            DbCommand comando = DB.GetStoredProcCommand("spInsDiasLiquidacion_CRM");
            DB.AddInParameter(comando, "@DLIQproyecto", DbType.Int64, d.CodProyecto);
            DB.AddInParameter(comando, "@DLIQcombinacion", DbType.String, d.Combinacion);
            DB.AddInParameter(comando, "@DLIQliquidacion", DbType.String, d.Liquidacion);
            DB.AddInParameter(comando, "@DLIQdiapago", DbType.String, d.DiaPago);
            DB.AddInParameter(comando, "@DLIQCuenta", DbType.String, d.Cuenta);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Actualiza un dia de liquidacion
        /// </summary>
        /// <param name="d"></param>
        public void ActualizarDiasLiquidacion(DiasLiquidacion d, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spUpdDiasLiquidacion_CRM");
            DB.AddInParameter(comando, "@DLIQproyecto", DbType.Int64, d.CodProyecto);
            DB.AddInParameter(comando, "@DLIQcombinacion", DbType.String, d.Combinacion);
            DB.AddInParameter(comando, "@DLIQliquidacion", DbType.String, d.Liquidacion);
            DB.AddInParameter(comando, "@DLIQdiapago", DbType.String, d.DiaPago);
            DB.AddInParameter(comando, "@DLIQCuenta", DbType.String, d.Cuenta);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Consulta todas las cuentas de ciudades por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<CuentasCiudades> ConsultaCuentasCiudadesXCodigo(long codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelCuentasCiudades_CRM");
            DB.AddInParameter(comando, "@CTACIUproyecto", DbType.Int64, codigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaCuentas = from row in dsCampos.Tables[0].AsEnumerable()
                                   select new CuentasCiudades()
                                   {
                                       CodProyecto = row.Field<long>("CTACIUproyecto"),
                                       Banco = row.Field<string>("CTACIUbanco").Trim(),
                                       Cuenta = row.Field<string>("CTACIUcuenta").Trim(),
                                       Municipio = row.Field<string>("CTACIUmunicipio").Trim(),
                                       Tipo = row.Field<string>("CTACIUTipo").Trim(),
                                       Estado = row.Field<string>("CTACIUestado").Trim(),
                                   };
                return ListaCuentas.ToList();
            }
            return new List<CuentasCiudades>();
        }

        /// <summary>
        /// Crea cuenta de ciudad
        /// </summary>
        /// <param name="c"></param>
        public void CrearCuentasCiudades(CuentasCiudades c, DbTransaction transaction)
        {
            ValidaCampos(c);
            DbCommand comando = DB.GetStoredProcCommand("spInsCuentasCiudades_CRM");
            DB.AddInParameter(comando, "@CTACIUproyecto", DbType.Int64, c.CodProyecto);
            DB.AddInParameter(comando, "@CTACIUbanco", DbType.String, c.Banco);
            DB.AddInParameter(comando, "@CTACIUcuenta", DbType.String, c.Cuenta);
            DB.AddInParameter(comando, "@CTACIUmunicipio", DbType.String, c.Municipio);
            DB.AddInParameter(comando, "@CTACIUTipo", DbType.String, c.Tipo);
            DB.AddInParameter(comando, "@CTACIUestado", DbType.String, c.Estado);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Actualiza cuenta de ciudad
        /// </summary>
        /// <param name="c"></param>
        public void ActualizarCuentasCiudades(CuentasCiudades c, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spUpdCuentasCiudades_CRM");
            DB.AddInParameter(comando, "@CTACIUproyecto", DbType.Int64, c.CodProyecto);
            DB.AddInParameter(comando, "@CTACIUbanco", DbType.String, c.Banco);
            DB.AddInParameter(comando, "@CTACIUcuenta", DbType.String, c.Cuenta);
            DB.AddInParameter(comando, "@CTACIUmunicipio", DbType.String, c.Municipio);
            DB.AddInParameter(comando, "@CTACIUTipo", DbType.String, c.Tipo);
            DB.AddInParameter(comando, "@CTACIUestado", DbType.String, c.Estado);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Consulta los puntos de servicio por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<string> ConsultaProyectoPuntoServicio(long codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelProyectosPSXCodigo_CRM");
            DB.AddInParameter(comando, "@PROPSproyecto", DbType.Int64, codigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaPS = from row in dsCampos.Tables[0].AsEnumerable()
                              select row.Field<string>("PROPSps");
                return ListaPS.ToList();
            }
            return new List<string>();
        }

        /// <summary>
        /// Crea un punto de servicio asociado a proyecto
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="codigo"></param>
        public void CrearProyectoPuntoServicio(string ps, long codigo, DbTransaction transaction)
        {
            if (ps != "" && codigo != 0)
            {
                //throw new ValidationException("Punto servicio: PS y codigo requeridos");
            }
            else
            {
                DbCommand comando = DB.GetStoredProcCommand("spInsProyectosPS_CRM");
                DB.AddInParameter(comando, "@PROPSproyecto", DbType.Int64, codigo);
                DB.AddInParameter(comando, "@PROPSps", DbType.String, ps);

                this.DB.ExecuteDataSet(comando, transaction);
            }
        }

        /// <summary>
        /// Consulta los horarios por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<HorariosProyectos> ConsultaHorariosProyectosXCodigo(long codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelHorariosProyectos_CRM");
            DB.AddInParameter(comando, "@HORPROCodProy", DbType.Int64, codigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaHorarios = from row in dsCampos.Tables[0].AsEnumerable()
                                    select new HorariosProyectos()
                                    {
                                        CodProyecto = row.Field<long>("HORPROCodProy"),
                                        Dia = row.Field<int>("HORPRODia"),
                                        HoraInicial = row.Field<int>("HORPROHoraInicial"),
                                        HoraFinal = row.Field<int>("HORPROHoraFinal")
                                    };
                return ListaHorarios.ToList();
            }
            return new List<HorariosProyectos>();
        }

        /// <summary>
        /// Crea horario de proyecto
        /// </summary>
        /// <param name="h"></param>
        public void CrearHorariosProyecto(HorariosProyectos h, DbTransaction transaction)
        {
            ValidaCampos(h);
            DbCommand comando = DB.GetStoredProcCommand("spInsHorariosProyectos_CRM");
            DB.AddInParameter(comando, "@HORPROCodProy", DbType.Int64, h.CodProyecto);
            DB.AddInParameter(comando, "@HORPRODia", DbType.Int32, h.Dia);
            DB.AddInParameter(comando, "@HORPROHoraInicial", DbType.Int32, h.HoraInicial);
            DB.AddInParameter(comando, "@HORPROHoraFinal", DbType.Int32, h.HoraFinal);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Actualiza horario de proyecto
        /// </summary>
        /// <param name="h"></param>
        public void ActualizarHorariosProyecto(HorariosProyectos h, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spUpdHorariosProyectos_CRM");
            DB.AddInParameter(comando, "@HORPROCodProy", DbType.Int64, h.CodProyecto);
            DB.AddInParameter(comando, "@HORPRODia", DbType.Int32, h.Dia);
            DB.AddInParameter(comando, "@HORPROHoraInicial", DbType.Int32, h.HoraInicial);
            DB.AddInParameter(comando, "@HORPROHoraFinal", DbType.Int32, h.HoraFinal);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Consulta los rangos de movilizado
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Tarifa> ConsultaRangosMovilizado(long codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelRangosMovilizadoLiquidacion_CRM");
            DB.AddInParameter(comando, "@RANMOVLIQproyecto", DbType.Int64, codigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaTarifas = from row in dsCampos.Tables[0].AsEnumerable()
                                   select new Tarifa()
                                   {
                                       CodProyecto = row.Field<long>("RANMOVLIQproyecto"),
                                       Valor = row.Field<decimal>("RANMOVLIQvalor"),
                                       Grupo = row.Field<string>("RANMOVLIQgrupo"),
                                       ValorInicial = row.Field<decimal>("RANMOVLIQvalinicial"),
                                       ValorFinal = row.Field<decimal>("RANMOVLIQvalfinal")
                                   };
                return ListaTarifas.ToList();
            }
            return new List<Tarifa>();
        }

        /// <summary>
        /// Crea rango de movilizado
        /// </summary>
        /// <param name="t"></param>
        public void CrearRangosMovilizado(Tarifa t, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spInsRangosMovilizadoLiquidacion_CRM");
            DB.AddInParameter(comando, "@RANMOVLIQproyecto", DbType.Int64, t.CodProyecto);
            DB.AddInParameter(comando, "@RANMOVLIQvalor", DbType.Decimal, t.Valor);
            DB.AddInParameter(comando, "@RANMOVLIQgrupo", DbType.String, t.Grupo);
            DB.AddInParameter(comando, "@RANMOVLIQvalinicial", DbType.Decimal, t.ValorInicial);
            DB.AddInParameter(comando, "@RANMOVLIQvalfinal", DbType.Decimal, t.ValorFinal);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Actualiza rango de movilizado
        /// </summary>
        /// <param name="t"></param>
        public void ActualizarRangosMovilizado(Tarifa t, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spUpdRangosMovilizadoLiquidacion_CRM");
            DB.AddInParameter(comando, "@RANMOVLIQproyecto", DbType.Int64, t.CodProyecto);
            DB.AddInParameter(comando, "@RANMOVLIQvalor", DbType.Decimal, t.Valor);
            DB.AddInParameter(comando, "@RANMOVLIQgrupo", DbType.String, t.Grupo);
            DB.AddInParameter(comando, "@RANMOVLIQvalinicial", DbType.Decimal, t.ValorInicial);
            DB.AddInParameter(comando, "@RANMOVLIQvalfinal", DbType.Decimal, t.ValorFinal);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Consulta rango de operacion
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Tarifa> ConsultaRangosOperacion(long codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelRangosOperacionesLiquidacion_CRM");
            DB.AddInParameter(comando, "@RANOPELIQproyecto", DbType.Int64, codigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaTarifas = from row in dsCampos.Tables[0].AsEnumerable()
                                   select new Tarifa()
                                   {
                                       CodProyecto = row.Field<long>("RANOPELIQproyecto"),
                                       Valor = row.Field<decimal>("RANOPELIQvalor"),
                                       Grupo = row.Field<string>("RANOPELIQgrupo"),
                                       ValorInicial = row.Field<decimal>("RANOPELIQvalinicial"),
                                       ValorFinal = row.Field<decimal>("RANOPELIQvalfinal")
                                   };
                return ListaTarifas.ToList();
            }
            return new List<Tarifa>();
        }

        /// <summary>
        /// Crea rango de operacion
        /// </summary>
        /// <param name="t"></param>
        public void CrearRangosOperacion(Tarifa t, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spInsRangosOperacionesLiquidacion_CRM");
            DB.AddInParameter(comando, "@RANOPELIQproyecto", DbType.Int64, t.CodProyecto);
            DB.AddInParameter(comando, "@RANOPELIQvalor", DbType.Decimal, t.Valor);
            DB.AddInParameter(comando, "@RANOPELIQgrupo", DbType.String, t.Grupo);
            DB.AddInParameter(comando, "@RANOPELIQvalinicial", DbType.Decimal, t.ValorInicial);
            DB.AddInParameter(comando, "@RANOPELIQvalfinal", DbType.Decimal, t.ValorFinal);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Actualiza rango de operacion
        /// </summary>
        /// <param name="t"></param>
        public void ActualizarRangosOperacion(Tarifa t, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spUpdRangosOperacionesLiquidacion_CRM");
            DB.AddInParameter(comando, "@RANOPELIQproyecto", DbType.Int64, t.CodProyecto);
            DB.AddInParameter(comando, "@RANOPELIQvalor", DbType.Decimal, t.Valor);
            DB.AddInParameter(comando, "@RANOPELIQgrupo", DbType.String, t.Grupo);
            DB.AddInParameter(comando, "@RANOPELIQvalinicial", DbType.Decimal, t.ValorInicial);
            DB.AddInParameter(comando, "@RANOPELIQvalfinal", DbType.Decimal, t.ValorFinal);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Consulta comisiones de proyecto por operaciones por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Tarifa> ConsultaComisionesProyectoXOperaciones(long codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelComisionesProyectosXOperaciones_CRM");
            DB.AddInParameter(comando, "@COMPROOPEproyecto", DbType.Int64, codigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaTarifas = from row in dsCampos.Tables[0].AsEnumerable()
                                   select new Tarifa()
                                   {
                                       CodProyecto = row.Field<long>("COMPROOPEproyecto"),
                                       Valor = row.Field<decimal>("COMPROOPEvalor"),
                                       Grupo = row.Field<string>("COMPROOPEgrupo"),
                                       Observaciones = row.Field<string>("COMPROOPEobservaciones")
                                   };
                return ListaTarifas.ToList();
            }
            return new List<Tarifa>();
        }

        /// <summary>
        /// Crea comision de proyecto por operacion
        /// </summary>
        /// <param name="t"></param>
        public void CrearComisionesProyectoXOperaciones(Tarifa t, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spInsComisionesProyectosXOperaciones_CRM");
            DB.AddInParameter(comando, "@COMPROOPEproyecto", DbType.Int64, t.CodProyecto);
            DB.AddInParameter(comando, "@COMPROOPEvalor", DbType.Decimal, t.Valor);
            DB.AddInParameter(comando, "@COMPROOPEgrupo", DbType.String, t.Grupo);
            DB.AddInParameter(comando, "@COMPROOPEobservaciones", DbType.String, t.Observaciones);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Actualiza comision de proyecto por operacion
        /// </summary>
        /// <param name="t"></param>
        public void ActualizarComisionesProyectoXOperaciones(Tarifa t, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spUpdComisionesProyectosXOperaciones_CRM");
            DB.AddInParameter(comando, "@COMPROOPEproyecto", DbType.Int64, t.CodProyecto);
            DB.AddInParameter(comando, "@COMPROOPEvalor", DbType.Decimal, t.Valor);
            DB.AddInParameter(comando, "@COMPROOPEgrupo", DbType.String, t.Grupo);
            DB.AddInParameter(comando, "@COMPROOPEobservaciones", DbType.String, t.Observaciones);
            DB.AddInParameter(comando, "@COMPROOPEfechagrabacion", DbType.DateTime, t.FechaGrabacion);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Consulta comisiones de proyecto por movilizado por codigo de proyecto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Tarifa> ConsultaComisionesProyectoXMovilizado(long codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelComisionesProyectosXMovilizado_CRM");
            DB.AddInParameter(comando, "@COMPROMOVproyecto", DbType.Int64, codigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var ListaTarifas = from row in dsCampos.Tables[0].AsEnumerable()
                                   select new Tarifa()
                                   {
                                       CodProyecto = row.Field<long>("COMPROMOVproyecto"),
                                       Valor = row.Field<decimal>("COMPROMOVvalor"),
                                       Grupo = row.Field<string>("COMPROMOVgrupo"),
                                       Observaciones = row.Field<string>("COMPROMOVobservaciones")
                                   };
                return ListaTarifas.ToList();
            }
            return new List<Tarifa>();
        }

        /// <summary>
        /// Crea comision de proyecto por movilizado
        /// </summary>
        /// <param name="t"></param>
        public void CrearComisionesProyectoXMovilizado(Tarifa t, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spInsComisionesProyectosXMovilizado_CRM");
            DB.AddInParameter(comando, "@COMPROMOVproyecto", DbType.Int64, t.CodProyecto);
            DB.AddInParameter(comando, "@COMPROMOVvalor", DbType.Decimal, t.Valor);
            DB.AddInParameter(comando, "@COMPROMOVgrupo", DbType.String, t.Grupo);
            DB.AddInParameter(comando, "@COMPROMOVobservaciones", DbType.String, t.Observaciones);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Actualiza comision de proyecto por movilizado
        /// </summary>
        /// <param name="t"></param>
        public void ActualizarComisionesProyectoXMovilizado(Tarifa t, DbTransaction transaction)
        {
            DbCommand comando = DB.GetStoredProcCommand("spUpdComisionesProyectosXMovilizado_CRM");
            DB.AddInParameter(comando, "@COMPROMOVproyecto", DbType.Int64, t.CodProyecto);
            DB.AddInParameter(comando, "@COMPROMOVvalor", DbType.Decimal, t.Valor);
            DB.AddInParameter(comando, "@COMPROMOVgrupo", DbType.String, t.Grupo);
            DB.AddInParameter(comando, "@COMPROMOVobservaciones", DbType.String, t.Observaciones);
            DB.AddInParameter(comando, "@COMPROMOVfechagrabacion", DbType.DateTime, t.FechaGrabacion);

            this.DB.ExecuteDataSet(comando, transaction);
        }

        /// <summary>
        /// Consulta la tabla en la que se debe guardar la tarifa
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string ConsultaTablaTarifa(int codigo)
        {
            DbCommand comando = DB.GetStoredProcCommand("spSelTablaTarifa_CRM");
            DB.AddInParameter(comando, "@codigo", DbType.Int32, codigo);

            DataSet dsCampos = this.DB.ExecuteDataSet(comando);

            if (dsCampos.Tables.Count > 0 && dsCampos.Tables[0].Rows.Count > 0)
            {
                var list = from row in dsCampos.Tables[0].AsEnumerable()
                           select row.Field<string>("CONTIPTARTabla");
                return list.ToList()[0];
            }
            return "";
        }

        /// <summary>
        /// Consulta las tarifas de acuerdo al tipo y al codigo del proyecto
        /// </summary>
        /// <param name="tipoTarifa"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Tarifa> ConsultaTarifas(int tipoTarifa, long codigo)
        {
            string tabla = ConsultaTablaTarifa(tipoTarifa);
            List<Tarifa> tarifas = tabla == "COMISIONES_PROYECTOSXMOVILIZADO" ? ConsultaComisionesProyectoXMovilizado(codigo) :
                      tabla == "COMISIONES_PROYECTOSXOPERACIONES" ? ConsultaComisionesProyectoXOperaciones(codigo) :
                      tabla == "RANGOS_MOVILIZADO_LIQUIDACION" ? ConsultaRangosMovilizado(codigo) :
                      tabla == "RANGOS_OPERACIONES_LIQUIDACION" ? ConsultaRangosOperacion(codigo) : new List<Tarifa>();
            return tarifas;
        }

        /// <summary>
        /// Crea la tarifa de acuerdo al tipo de tarifa
        /// </summary>
        /// <param name="t"></param>
        /// <param name="tipoTarifa"></param>
        public void CrearTarifa(Tarifa t, int tipoTarifa, DbTransaction transaction)
        {
            ValidaCampos(t);
            string tabla = ConsultaTablaTarifa(tipoTarifa);
            if (tabla == "COMISIONES_PROYECTOSXMOVILIZADO")
                CrearComisionesProyectoXMovilizado(t, transaction);
            else if (tabla == "COMISIONES_PROYECTOSXOPERACIONES")
                CrearComisionesProyectoXOperaciones(t, transaction);
            else if (tabla == "RANGOS_MOVILIZADO_LIQUIDACION")
                CrearRangosMovilizado(t, transaction);
            else if (tabla == "RANGOS_OPERACIONES_LIQUIDACION")
                CrearRangosOperacion(t, transaction);
        }

        /// <summary>
        /// Actualiza la tarifa de acuerdo al tipo de tarifa
        /// </summary>
        /// <param name="t"></param>
        /// <param name="tipoTarifa"></param>
        public void ActualizarTarifa(Tarifa t, int tipoTarifa, DbTransaction transaction)
        {
            ValidaCampos(t);
            string tabla = ConsultaTablaTarifa(tipoTarifa);

            if (tabla == "COMISIONES_PROYECTOSXMOVILIZADO")
                ActualizarComisionesProyectoXMovilizado(t, transaction);
            else if (tabla == "COMISIONES_PROYECTOSXOPERACIONES")
                ActualizarComisionesProyectoXOperaciones(t, transaction);
            else if (tabla == "RANGOS_MOVILIZADO_LIQUIDACION")
                ActualizarRangosMovilizado(t, transaction);
            else if (tabla == "RANGOS_OPERACIONES_LIQUIDACION")
                ActualizarRangosOperacion(t, transaction);
        }

        /// <summary>
        /// Valida los campos de una clase T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        private void ValidaCampos<T>(T obj)
        {
            ValidationContext context = new ValidationContext(obj, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(obj, context, results, true);

            if (!valid)
            {
                string error = "";
                foreach (ValidationResult vr in results)
                {
                    error += obj.GetType().ToString() + ":" + vr.MemberNames.First() + " " + vr.ErrorMessage + Environment.NewLine;
                }
                throw new ValidationException(error);
            }
        }
    }
}
