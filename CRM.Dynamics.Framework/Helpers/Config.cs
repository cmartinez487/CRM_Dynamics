﻿using System;
using System.Configuration;

namespace CRM.Dynamics.Framework.Helpers
{
    public class Config
    {
        /// <summary>
        /// Obtiene el valor entero de una llave de configuración
        /// </summary>
        /// <param name="key">Nombre de la llave de configuración</param>
        /// <returns>Valor entero de la llave de configuración</returns>
        public static int GetInt(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                throw new Exception("No existe la llave '" + key + "'.");
            }
            else
            {
                if (int.TryParse(ConfigurationManager.AppSettings[key].ToString(), out int i))
                {
                    return i;
                }
                else
                {
                    throw new Exception("Se esperaba un valor entero en la lavve '" + key + "'.");
                }
            }
        }

        /// <summary>
        /// Obtiene el valor de una llave de configuración
        /// </summary>
        /// <param name="key">Nombre de la llave de configuración</param>
        /// <returns>Texto de la llave de configuración</returns>
        public static string GetString(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                throw new Exception("No existe la llave '" + key + "'.");
            }
            else
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
        }
    }
}
