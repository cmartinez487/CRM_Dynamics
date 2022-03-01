using System;

namespace CRM.Dynamics.Entidades
{
    public class Usuario
    {
        /// <summary>
        /// Código
        /// </summary>
        public string USUcodigo { get; set; }
 
        /// <summary>
        /// Contraseña principal
        /// </summary>
        public string USUcontrasena { get; set; }

        /// <summary>
        /// Contraseña secundaria
        /// </summary>
        public string USUcontrasena2 { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string USUnombre { get; set; }

        /// <summary>
        /// Apellidos
        /// </summary>
        public string USUapellidos { get; set; }

        /// <summary>
        /// Fecha caducidad
        /// </summary>
        public DateTime USUcaducidad { get; set; }

        /// <summary>
        /// Cambio contraseña
        /// </summary>
        public bool USUcambiocontrasena { get; set; }

        /// <summary>
        /// Bloqueado
        /// </summary>
        public bool USUbloqueado { get; set; }

        /// <summary>
        /// Cambio días
        /// </summary>
        public string USUcambiodias { get; set; }

        /// <summary>
        /// Fecha ingreso
        /// </summary>
        public DateTime USUfechaing { get; set; }

        /// <summary>
        /// Punto de servicio
        /// </summary>
        public string USUPS { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string USUestado { get; set; }

        /// <summary>
        /// Categoría estado
        /// </summary>
        public string USUcatestado { get; set; }

        /// <summary>
        /// Área
        /// </summary>
        public string USUarea { get; set; }

        /// <summary>
        /// ID Cliente
        /// </summary>
        public string USUidcliente { get; set; }

        /// <summary>
        /// Fecha modificación
        /// </summary>
        public DateTime USUfechamod { get; set; }

        /// <summary>
        /// Correo
        /// </summary>
        public string USUcorreo { get; set; }

        /// <summary>
        /// Contraseña SHA
        /// </summary>
        public string USUContrasenaSha { get; set; }

        /// <summary>
        /// COntraseña SHA secundaria
        /// </summary>
        public string USUContasenaSha2 { get; set; }

        /// <summary>
        /// Fecha actualización
        /// </summary>
        public DateTime USUfechaActualizacion { get; set; }

        /// <summary>
        /// Desactivar
        /// </summary>
        public bool USUdesactivar { get; set; }

        /// <summary>
        /// Fecha inicio desactivar
        /// </summary>
        public DateTime USUfechaInicioDesactivar { get; set; }

        /// <summary>
        /// Fecha final desactivar
        /// </summary>
        public DateTime USUfechaFinalDesactivar { get; set; }

        /// <summary>
        /// Identificación
        /// </summary>
        public string USUidentificacion { get; set; }

        /// <summary>
        /// Huella
        /// </summary>
        public string USUhuePlantilla { get; set; }

        /// <summary>
        /// Enrolador por
        /// </summary>
        public string USUenroladoPor { get; set; }

        /// <summary>
        /// Tipo identificación
        /// </summary>
        public string USUtipoIdentificacion { get; set; }

        /// <summary>
        /// Confronta
        /// </summary>
        public bool USUConfronta { get; set; }

        /// <summary>
        /// Confronta fecha
        /// </summary>
        public DateTime USUConfrontaFecha { get; set; }

        /// <summary>
        /// Confronta reintentos
        /// </summary>
        public int USUconfrontaReintentos { get; set; }

        /// <summary>
        /// Número teléfono
        /// </summary>
        public string USUNumTelefono { get; set; }

    }
}
