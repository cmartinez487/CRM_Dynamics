using System;

namespace CRM.Dynamics.Entidades
{
    /// <summary>
    /// Descripcion: Objeto Cliente Natural
    /// Autor: Ruben Wilches
    /// Fecha: 27/02/2019
    /// </summary>
    public class ClienteNatural
    {
        /// <summary>
        /// Nombres del cliente
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Primer apellido del cliente
        /// </summary>
        public string Apellido1 { get; set; }

        /// <summary>
        /// Segundo apellido del cliente
        /// </summary>
        public string Apellido2 { get; set; }

        /// <summary>
        /// Numero de teléfono del cliente
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Dirección del cliente
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Ciudad donde se registro el cliente.
        /// </summary>
        public string Municipio { get; set; }

        /// <summary>
        /// Fecha de creacion Cliente
        /// </summary>
        public DateTime? FechaCredb { get; set; }


        /// <summary>
        /// Ciudad de expedicion de Documento.
        /// </summary>
        public string MunicipioExp { get; set; }

        /// <summary>
        /// Correo electronico del Cliente.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Ocupacion del Cliente.
        /// </summary>
        public string Ocupacion { get; set; }

        /// <summary>
        /// Telefono celular del Cliente.
        /// </summary>
        public string Celular { get; set; }


        /// <summary>
        /// Fecha de expedicion de la cedula de ciudadania del cliente
        /// </summary>
        public DateTime? FechaExp { get; set; }

        /// <summary>
        /// Codigo postal del Cliente
        /// </summary>
        public string CodigoPostal { get; set; }

        /// <summary>
        /// Columna que registra el motivo por el cual la persona es expuesta publicamente
        /// </summary>
        public string ClientePEP { get; set; }

        /// <summary>
        /// Fecha actualizacion datos
        /// </summary>
        public DateTime? FechaActualizacionDatos { get; set; }

        /// <summary>
        /// Sexo de Cliente
        /// </summary>
        public string Sexo { get; set; }

        /// <summary>
        /// Estrato Socio económico de Cliente
        /// </summary>
        public string Estrato { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? tratamientoDatos { get; set; }


        /*IDENTIFICACION_CLIENTE*/
        /// <summary>
        /// Tipo de documento de identidad del cliente
        /// </summary>
        public string Tipodocumento { get; set; }

        /// <summary>
        /// Indica si es el documento principal del cliente (con el único que puede realizar operaciones)
        /// </summary>
        public bool? Vinculacion { get; set; }

        /// <summary>
        /// Número de identificación del cliente
        /// </summary>
        public long? NumeroIdentificacion { get; set; }

        /// <summary>
        /// Fecha de creacion Identificacio Cliente
        /// </summary>
        public DateTime? FechaCredbId { get; set; }

        /// <summary>
        /// Determina si el cliente requiere modificaciones (ACT=NO, INA=Modificacion Total, HUE=Edicion de Huella, DAT=Modificar todos los datos)
        /// </summary>
        public string Estado { get; set; }
    }
}
