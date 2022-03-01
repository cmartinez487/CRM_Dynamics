using System;

namespace CRM.Dynamics.Entidades.PAP
{
    public class PuntoServicio
    {
        /// <summary>
        /// Codigo dep PS
        /// </summary>
        public string PUNSERcodigo { get; set; }
        /// <summary>
        /// Estado del PS
        /// </summary>
        public string PUNSERestado { get; set; }
        /// <summary>
        /// Municipio donde esta registrado el PS
        /// </summary>
        public string PUNSERmunicipio { get; set; }
        /// <summary>
        /// Nombre del PS
        /// </summary>
        public string PUNSERdescripcion { get; set; }
        /// <summary>
        /// Direccion del PS
        /// </summary>
        public string PUNSERdireccion { get; set; }
        /// <summary>
        /// Telefono del PS
        /// </summary>
        public string PUNSERtelefono1 { get; set; }
        /// <summary>
        /// Telefono del PS (opcional)
        /// </summary>
        public string PUNSERtelefono2 { get; set; }
        /// <summary>
        /// Mail del PS
        /// </summary>
        public string PUNSERmail { get; set; }
        /// <summary>
        /// Fondo Fijo asociado al PS
        /// </summary>
        public decimal? PUNSERfondofijo { get; set; }
        /// <summary>
        /// Indicador de calculo del Saldo Final del PS 
        /// </summary>
        public bool? PUNSERFondoCalculoMDC { get; set; }
        /// <summary>
        /// Tipo de PS
        /// </summary>
        public string PUNSERtipo { get; set; }
        /// <summary>
        /// Indicado de existencia de sistema para el PS
        /// </summary>
        public int? PUNSERsistema { get; set; }
        /// <summary>
        /// porcentaje ganado por pagar traslados naturales
        /// </summary>
        public Double? PUNSERpagado { get; set; }
        /// <summary>
        /// porcentaje ganado por recibir traslados de personas naturales
        /// </summary>
        public Double? PUNSERnatural { get; set; }
        /// <summary>
        /// porcentaje ganado por recibir pagos de empresariales
        /// </summary>
        public Double? PUNSERempresarial { get; set; }
        /// <summary>
        /// Grupo que se toma como base para la liquidación del cliente empresarial
        /// </summary>
        public string PUNSERgrupoCE { get; set; }
        /// <summary>
        /// Fecha de inactivación del PS
        /// </summary>
        public DateTime? PUNSERFechaInactivacion { get; set; }
        /// <summary>
        /// Código regional al que pertenece el PS
        /// </summary>
        public string PUNSERRegional { get; set; }
        /// <summary>
        /// Fecha de cierre del PS
        /// </summary>
        public DateTime? PUNSERFechaCierre { get; set; }
        /// <summary>
        /// Corresponde al código del sector de la regional a la que está asociado el PS
        /// </summary>
        public string PUNSERSectorRegional { get; set; }
        /// <summary>
        /// Clasificación de PS 
        /// </summary>
        public string PUNSERClasificacion { get; set; }
        /// <summary>
        /// Consultor de Canales asociado al PAP
        /// </summary>
        public string PUNSERAConsultorCanales { get; set; }
        /// <summary>
        /// Relaciona el punto de servicio con la tabla RED_CONTRATISTA
        /// </summary>
        public string PUNSERIdRed { get; set; }

        /// <summary>
        /// Línea del punto de servicio
        /// </summary>
        public string PUNSEREXTlinea { get; set; }
    }
}
