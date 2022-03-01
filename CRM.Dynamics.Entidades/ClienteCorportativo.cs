using System;

namespace CRM.Dynamics.Entidades
{
    /// <summary>
    /// Descripcion: Objeto Cliente Corporativo
    /// </summary>
    public class ClienteCorportativo
    {
        /// <summary>
        /// Tipo de Documento
        /// </summary>
        public string tipoid { get; set; }

        /// <summary>
        /// Numero de Documento
        /// </summary>
        public string identificacion { get; set; }

        /// <summary>
        /// Codigo del Cliente
        /// </summary>
        public long? codigo { get; set; }

        /// <summary>
        /// Nombre del Cliente
        /// </summary>
        public string nombre { get; set; }

        /// <summary>
        /// Tipo de Documento
        /// </summary>
        public string razonsocial { get; set; }

        /// <summary>
        /// 1er Apellido
        /// </summary>
        public string apellido1 { get; set; }

        /// <summary>
        /// 2do Apellido
        /// </summary>
        public string apellido2 { get; set; }

        /// <summary>
        /// Direccion
        /// </summary>
        public string direccion { get; set; }

        /// <summary>
        /// Municipio
        /// </summary>
        public string municipio { get; set; }

        /// <summary>
        /// 1er telefono
        /// </summary>
        public string telefono1 { get; set; }

        /// <summary>
        /// 2do telefono
        /// </summary>
        public string telefono2 { get; set; }

        /// <summary>
        /// nro de FAX
        /// </summary>
        public string fax { get; set; }

        /// <summary>
        /// Contacto de contacto
        /// </summary>
        public string contacto { get; set; }

        /// <summary>
        /// Correo de Contacto
        /// </summary>
        public string contactomail { get; set; }

        /// <summary>
        /// Sexo del Cliente
        /// </summary>
        public string sexo { get; set; }

        /// <summary>
        /// Tipo de Cliente
        /// </summary>
        public string tipocliente { get; set; }

        /// <summary>
        /// Estado del Cliente
        /// </summary>
        public string estado { get; set; }

        /// <summary>
        /// Especificacion de Cliente
        /// </summary>
        public string catestado { get; set; }

        /// <summary>
        /// apartado
        /// </summary>
        public string apartado { get; set; }

        /// <summary>
        /// Correo
        /// </summary>
        public string mail { get; set; }

        /// <summary>
        /// Fecha de Registro del Cliente
        /// </summary>
        public DateTime? fechagrabacion { get; set; }

        /// <summary>
        /// Observaciones
        /// </summary>
        public string observaciones { get; set; }

        /// <summary>
        ///  Verificacion
        /// </summary>
        public string digitoverificacion { get; set; }

        /// <summary>
        /// Clase Contable
        /// </summary>
        public long? clascontable { get; set; }

        /// <summary>
        /// imptimbre
        /// </summary>
        public decimal? imptimbre { get; set; }

        /// <summary>
        /// ciudadIca
        /// </summary>
        public string ciudadIca { get; set; }

        /// <summary>
        /// Sector
        /// </summary>
        public string Sector { get; set; }

        /// <summary>
        /// Identificador Unico de Cliente
        /// </summary>
        public Guid? rowguid { get; set; }

        /// <summary>
        /// EstadoPeople
        /// </summary>
        public string EstadoPeople { get; set; }

        /// <summary>
        /// Fecha de estado activo
        /// </summary>
        public DateTime? FechaActEstadoPeople { get; set; }

        /// <summary>
        /// Fecha de actualizacion del Cliente
        /// </summary>
        public DateTime? fechaActualizacion { get; set; }

        /// <summary>
        /// Pagina Web del Cliente
        /// </summary>
        public string PaginaWeb { get; set; }

        /// <summary>
        /// Codigo de Region
        /// </summary>
        public string regional { get; set; }

        /// <summary>
        /// Codigo de Pais
        /// </summary>
        public string pais { get; set; }

        /// <summary>
        /// Clasificacion
        /// </summary>
        public string clasificacion { get; set; }

        /// <summary>
        /// Codigo regional Asignado
        /// </summary>
        public string regionalasignada { get; set; }

        /// <summary>
        /// Fecha de Activacion
        /// </summary>
        public DateTime? FechaActivacion { get; set; }

        /// <summary>
        /// Fecha de Inactivacion
        /// </summary>
        public DateTime? FechaInactivacion { get; set; }
    }
}
