namespace CRM.Dynamics.Entidades.NotaAdjunta
{
    public class NotaAdjunta
    {
        /// <summary>
        /// Título de la nota
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Contenido de la nota
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Contenido del archivo adjunto (base64)
        /// </summary>
        public string ContenidoArchivo { get; set; }

        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string NombreArchivo { get; set; }

        /// <summary>
        /// Tipo del archivo adjunto
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Tamaño del archivo adjunto en bytes
        /// </summary>
        public int TamanioArchivo { get; set; }

        /// <summary>
        /// Fecha de creación.
        /// </summary>
        public string FechaCreacion { get; set; }
    }
}
