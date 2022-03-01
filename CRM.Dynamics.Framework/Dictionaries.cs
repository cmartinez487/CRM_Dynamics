using CRM.Dynamics.Entidades.Tipos;
using System;
using System.Collections.Generic;

namespace CRM.Dynamics.Framework
{
    public class Dictionaries
    {
        /// <summary>
        /// Diccionario de tipos de documento.
        /// </summary>
        private static Dictionary<string, string> _tipoDocumento;

        /// <summary>
        /// Fecha de caché del diccionario de tipos de documento.
        /// </summary>
        private static DateTime CacheDate_tipoDocumento;

        /// <summary>
        /// Obtiene el guid de un tipo de documento a partir de su código.
        /// </summary>
        /// <param name="tipo">Código del tipo de documento</param>
        /// <returns>guid del tipo de documento</returns>
        public static string GetTipoDocumento(string tipo)
        {
            try
            {
                if (_tipoDocumento == null || DateTime.Now.Subtract(CacheDate_tipoDocumento).Minutes > 60)
                {
                    var crm = Dynamics.GetInstance();

                    DynamicsResponse response = crm.Get("api/dynamics/tipodocumento");
                    List<TipoDocumento> list = Dynamics.GetEntityList<TipoDocumento>(response.Message);

                    // Crea diccionario
                    _tipoDocumento = new Dictionary<string, string>();
                    foreach (TipoDocumento i in list)
                    {
                        _tipoDocumento.Add(i.Codigo.ToUpper(), i.GUID);
                    }

                    CacheDate_tipoDocumento = DateTime.Now;
                }

                return _tipoDocumento[tipo.ToUpper()];
            }
            catch (KeyNotFoundException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }
    }
}
