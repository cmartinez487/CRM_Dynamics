using System;

namespace CRM.Dynamics.Entidades.Colaboradores
{
    public class Contratistas
    {
        /// <summary>
        /// Tipo de identificacion del CONTRATISTA
        /// </summary>
        public string CONtipoid { get; set; }

        /// <summary>
        /// Identificacion del CONTRATISTA
        /// </summary>
        public string CONidentificacion { get; set; }

        /// <summary>
        /// Digito de verificacion del documento del CONTRATISTA
        /// </summary>
        public string CONdigitoverificacion { get; set; }

        /// <summary>
        /// Razon solicial del CONTRATISTA
        /// </summary>
        public string CONRazonsocial { get; set; }

        /// <summary>
        /// Tipo de identificacion del representante Legal
        /// </summary>
        public string CONtipoidRepresentanteLegal { get; set; }

        /// <summary>
        /// Identificacion del representante Legal
        /// </summary>
        public string CONidRepresentanteLegal { get; set; }

        /// <summary>
        /// Estado del CONTRATISTA
        /// </summary>
        public string CONEstado { get; set; }

        /// <summary>
        /// Categoria del Estado del CONTRATISTA
        /// </summary>
        public string CONCategoria { get; set; }

        /// <summary>
        /// Direccion CONTRATISTA
        /// </summary>
        public string CONDireccion { get; set; }

        /// <summary>
        /// Municipio CONTRATISTA
        /// </summary>
        public string CONMunicipio { get; set; }

        /// <summary>
        /// Telefono 1 del CONTRATISTA
        /// </summary>
        public string CONTelefono1 { get; set; }

        /// <summary>
        /// Telefono 2 del CONTRATISTA
        /// </summary>
        public string CONTelefono2 { get; set; }

        /// <summary>
        /// Fax del CONTRATISTA
        /// </summary>
        public string CONFax { get; set; }

        /// <summary>
        /// Correo electronico del CONTRATISTA
        /// </summary>
        public string CONMail { get; set; }

        /// <summary>
        /// Clasificacion Cliente
        /// </summary>
        public string CONClasificacionCliente { get; set; }

        /// <summary>
        /// Clasificacion contable
        /// </summary>
        public int? CONClasificacionContable { get; set; }

        /// <summary>
        /// Fecha de creación del registro en BD
        /// </summary>
        public DateTime? CONFechaGrabacionBD { get; set; }

        /// <summary>
        /// unidad de explotación al cual va a aplicar la liquidación de contraprestación para el contratista seleccionado
        /// </summary>
        public string CONUniExplot { get; set; }

        /// <summary>
        /// Centro de Costo al cual va a aplicar la liquidación de contraprestación
        /// </summary>
        public string CONCCostoPeople { get; set; }

        /// <summary>
        /// Determina el tipo de contrato de cada contratista (CX,KX)
        /// </summary>
        public string CONtipoContrato { get; set; }

        /// <summary>
        /// Registro con la sumatoria de las contraprestaciones Efectivo y Circulante que correspondan al PAP o mas PAP
        /// </summary>
        public decimal? CONIngresosUVT { get; set; }

        /// <summary>
        /// Campo para validar si permite el acceso al portal WIKI a los PAP asociados
        /// </summary>
        public bool? CONAccesoWiki { get; set; }

        /// <summary>
        /// Tipo de Contratista (Retail,Canal Corporativo, Sombrilla)
        /// </summary>
        public string CONTipoContratista { get; set; }

        /// <summary>
        /// Fecha de inactivación del CONTRATISTA
        /// </summary>
        public DateTime? CONFechaInactivacion { get; set; }

        /// <summary>
        /// Código Regional del CONTRATISTA
        /// </summary>
        public string CONRegionalAsignada { get; set; }

        /// <summary>
        /// Zona del CONTRATISTA
        /// </summary>
        public string CONZona { get; set; }

        /// <summary>
        /// Usuario del Facilitador asignado al CONTRATISTA
        /// </summary>
        public string CONFacilitadorAsignado { get; set; }
    }
}
