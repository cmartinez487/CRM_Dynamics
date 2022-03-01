using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.Dynamics.Entidades
{
    public class Proyecto
    {
        /// <summary>
        /// Codigo de Proyecto
        /// </summary>
        [Required]
        public long Codigo { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        [Required]
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha de Creacion
        /// </summary>
        [Required]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required]
        public string Estado { get; set; }

        /// <summary>
        /// Estado de Categorizacion
        /// </summary>
        public string CatEstado { get; set; }

        /// <summary>
        /// Producto
        /// </summary>
        [Required]
        public int? Producto { get; set; }

        /// <summary>
        /// SubCosto
        /// </summary>
        public string SubCosto { get; set; }

        /// <summary>
        /// Aplica Descuento Automatico
        /// </summary>
        [Required]
        public bool? DtoAutomatico { get; set; }

        /// <summary>
        /// Forma de Pago
        /// </summary>
        [Required]
        public string FormaPago { get; set; }

        /// <summary>
        /// Tipo de comision
        /// </summary>
        public string TipoComision { get; set; }

        /// <summary>
        /// Tipo de Cliente
        /// </summary>
        [Required]
        public string TidCliente { get; set; }

        /// <summary>
        /// Id del Cliente
        /// </summary>
        [Required]
        public string IdCliente { get; set; }

        /// <summary>
        /// Aplica Liquidacion
        /// </summary>
        public bool? Liquida { get; set; }

        /// <summary>
        /// Aplica Acumulador de Puntos
        /// </summary>
        [Required]
        public bool? Acumpuntos { get; set; }

        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Tipo de Vendedor
        /// </summary>
        public string TidVendedor { get; set; }

        /// <summary>
        /// ID del vendedor
        /// </summary>
        public string IdVendedor { get; set; }

        /// <summary>
        /// Aplica RecibeVencido
        /// </summary>
        public bool? RecibeVencido { get; set; }

        /// <summary>
        /// Aplica DuenoProyecto
        /// </summary>
        public bool? DuenoProyecto { get; set; }

        /// <summary>
        /// Tipo de Liquidacion
        /// </summary>
        public string TipoLiquidacion { get; set; }

        /// <summary>
        /// Inicio del Contrato
        /// </summary>
        public DateTime? InicioContrato { get; set; }

        /// <summary>
        /// Finalizacion del Contrato
        /// </summary>
        public DateTime? FinContrato { get; set; }

        /// <summary>
        /// Vinculacion
        /// </summary>
        public decimal? Vinculacion { get; set; }

        /// <summary>
        /// Mantenimiento
        /// </summary>
        public decimal? Mantenimiento { get; set; }

        /// <summary>
        /// Aplica CapturaCheque
        /// </summary>
        public bool? CapturaCheque { get; set; }

        /// <summary>
        /// Basica
        /// </summary>
        [Required]
        public decimal? Basica { get; set; }

        /// <summary>
        /// Variable
        /// </summary>
        [Required]
        public decimal? Variable { get; set; }

        /// <summary>
        /// IvaIncluido
        /// </summary>
        [Required]
        public bool? IvaIncluido { get; set; }

        /// <summary>
        /// Cuenta de Banco
        /// </summary>
        public string CuentaBanco { get; set; }

        /// <summary>
        /// Modificador de Archivo
        /// </summary>
        public short? ModificadorArchivo { get; set; }

        /// <summary>
        /// Fecha Asobancaria 
        /// </summary>
        public DateTime? FechaAsobancaria { get; set; }

        /// <summary>
        /// Codigo Ean13 
        /// </summary>
        public string CodigoEan13 { get; set; }

        /// <summary>
        /// Entidad de Recaudadora
        /// </summary>
        public string EntidadRecaudadora { get; set; }

        /// <summary>
        /// Plazo para Dias de Pago
        /// </summary>
        public int? PlazoDiasPago { get; set; }

        /// <summary>
        /// Ambiente
        /// </summary>
        public string Ambiente { get; set; }

        /// <summary>
        /// Aplica Asobancaria
        /// </summary>
        public bool? Asobancaria { get; set; }

        /// <summary>
        /// aplica Cod de Barras
        /// </summary>
        public bool? CodBarras { get; set; }

        /// <summary>
        /// Aplica Asobancaria
        /// </summary>
        [Required]
        public bool? ServicioPublico { get; set; }

        /// <summary>
        /// Aplica Duplicados
        /// </summary>
        public bool? Duplicados { get; set; }

        /// <summary>
        /// Porcentaje Minimo Reportadas
        /// </summary>
        public double? PorcentajeMinimoReportadas { get; set; }

        /// <summary>
        /// Tiempo Minimo Para Reportar
        /// </summary>
        public long? TiempoMinimoParaReportar { get; set; }

        /// <summary>
        /// aplica NotasCobranComision
        /// </summary>
        [Required]
        public bool? NotasCobranComision { get; set; }

        /// <summary>
        /// aplica ReembolsaComision
        /// </summary>
        [Required]
        public bool? ReembolsaComision { get; set; }

        /// <summary>
        /// Minimo Garantizado
        /// </summary>
        public decimal? MinimoGarantizado { get; set; }

        /// <summary>
        /// Valor de Penalizacion
        /// </summary>
        public decimal? ValorPenalizacion { get; set; }

        /// <summary>
        /// Codigo de la tarifa
        /// </summary>
        [Required]
        public int? CodTipoCom { get; set; }

        /// <summary>
        /// Periodo de Liquidacion
        /// </summary>
        [Required]
        public string PeriodoLiq { get; set; }

        /// <summary>
        /// Costo Total del Proyecto
        /// </summary>
        public decimal? CostoTotalProyecto { get; set; }

        /// <summary>
        /// Tarifa de Prod Devuelto
        /// </summary>
        public decimal? TarifaProdDevuelto { get; set; }

        /// <summary>
        /// CcPeople
        /// </summary>
        public string CcPeople { get; set; }

        /// <summary>
        /// Dias de Facturacion
        /// </summary>
        [Required]
        public string DiasFacturacion { get; set; }

        /// <summary>
        /// Fecha de Actualizacion
        /// </summary>
        public DateTime? FechaActualizacion { get; set; }

        /// <summary>
        /// Aplica Recoger Fuera De Linea
        /// </summary>
        public bool? RecogerFueraDeLinea { get; set; }

        /// <summary>
        /// Aplica Recoger ValidarCampos
        /// </summary>
        public bool? ValidarCampos { get; set; }

        /// <summary>
        /// Aplica GrabarNoValidados
        /// </summary>
        public bool? GrabarNoValidados { get; set; }

        /// <summary>
        /// Nombre Procedimiento ValidaCampos
        /// </summary>
        public string NombreProcedimientoValidaCampos { get; set; }

        /// <summary>
        /// Aplica Internacionalizacion
        /// </summary>
        public bool? Internacionalizacion { get; set; }

        /// <summary>
        /// Aplica GrabarNoValidados
        /// </summary>
        public bool? ValidaBaseCliente { get; set; }

        /// <summary>
        /// Aplica SoloCapturaCodigoBarras
        /// </summary>
        public bool? SoloCapturaCodigoBarras { get; set; }

        /// <summary>
        /// Tipo de Proyecto
        /// </summary>
        public string TipoProyecto { get; set; }

        /// <summary>
        /// PLP
        /// </summary>
        public int? PLP { get; set; }

        /// <summary>
        /// Aplica Comision de Proyecto
        /// </summary>
        public bool? ComisionProyecto { get; set; }

        /// <summary>
        /// Aplica Prorroga AutoContrato
        /// </summary>
        public bool? ProrrogaAutoContrato { get; set; }

        /// <summary>
        /// Duracion del Contrato
        /// </summary>
        public int? DuracionContrato { get; set; }

        /// <summary>
        /// Comision del PS
        /// </summary>
        [Required]
        public decimal? ComisionPS { get; set; }

        /// <summary>
        /// ContraprestacionRecaudo
        /// </summary>
        public decimal? ContraprestacionRecaudo { get; set; }

        /// <summary>
        /// ContraprestacionPago
        /// </summary>
        public decimal? ContraprestacionPago { get; set; }

        /// <summary>
        /// Aplica Consulta de Informacion
        /// </summary>
        [Required]
        public bool? ConsultaInformacion { get; set; }

        /// <summary>
        /// Aplica Valor Modificable
        /// </summary>
        [Required]
        public bool? ValorModificable { get; set; }

        /// <summary>
        /// ProveedorValidacion
        /// </summary>
        public string ProveedorValidacion { get; set; }

        /// <summary>
        /// ProveedorConsulta
        /// </summary>
        public string ProveedorConsulta { get; set; }

        /// <summary>
        /// NombreLogoCDT
        /// </summary>
        public string NombreLogoCDT { get; set; }

        /// <summary>
        /// NombreLogoCDT
        /// </summary>
        public string ProcPag { get; set; }

        /// <summary>
        /// Aplica HaceReintento
        /// </summary>
        public bool? HaceReintento { get; set; }

        /// <summary>
        /// ProveedorProcesamiento
        /// </summary>
        public string ProveedorProcesamiento { get; set; }

        /// <summary>
        /// Aplica PagoCualquierPS
        /// </summary>
        public bool? PagoCualquierPS { get; set; }

        /// <summary>
        /// Consultor de Negocio
        /// </summary>
        public string ConsultorNegocio { get; set; }

        /// <summary>
        /// Dias Max de Vencimiento
        /// </summary>
        public int? DiasMaxVencimiento { get; set; }

        /// <summary>
        /// Aplica ImprimeCopia
        /// </summary>
        public bool? ImprimeCopia { get; set; }

        /// <summary>
        /// Aplica PagoPIN
        /// </summary>
        public bool? PagoPIN { get; set; }

        /// <summary>
        /// Aplica ControlaFondoOpe
        /// </summary>
        public bool? ControlaFondoOpe { get; set; }

        /// <summary>
        /// Fecha de Activacion
        /// </summary>
        public DateTime? FechaActivacion { get; set; }

        /// <summary>
        /// Fecha de Inactivacion
        /// </summary>
        public DateTime? FechaInactivacion { get; set; }

        /// <summary>
        /// Lista Generica de PuntosServicio
        /// </summary>
        public List<string> PuntosServicio { get; set; }

        /// <summary>
        /// Lista del objeto HorariosProyectos
        /// </summary>
        public List<HorariosProyectos> HorariosProyectos { get; set; }

        /// <summary>
        /// Lista del objeto Tarifa
        /// </summary>
        public List<Tarifa> Tarifas { get; set; }

        /// <summary>
        /// Lista del objeto CuentasCiudades
        /// </summary>
        public List<CuentasCiudades> CuentasCiudades { get; set; }

        /// <summary>
        /// Lista del objeto DiasLiquidacion
        /// </summary>
        public List<DiasLiquidacion> DiasLiquidacion { get; set; }
    }
}
