using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Dynamics.Comun
{
    public class Utilidades
    {
        #region Singleton

        private static volatile Utilidades instancia;
        private static object syncRoot = new Object();

        public static Utilidades Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new Utilidades();
                    }
                }
                return instancia;
            }
        }

        #endregion Singleton

        /// <summary>
        /// Decencriptación de cadena
        /// </summary>
        /// <param name="cadena">Cadena a decencriptar</param>
        /// <returns>Cadena decencriptada</returns>
        public string Decencriptar(string cadena)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(cadena));
        }

        /// <summary>
        /// Encriptar en base 64 una cadena de string
        /// </summary>
        /// <param name="cadena">Cadena a encriptar</param>
        /// <returns>Cadena Encriptada</returns>
        public static string EncriptarBase64(string cadena)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(cadena);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decencriptar una cadena de string en base 64
        /// </summary>
        /// <param name="cadena">Cadena a decencriptar</param>
        /// <returns>Cadena decencriptada</returns>
        public static string DecencriptarBase64(string cadena)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(cadena);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Encriptación de cadena
        /// </summary>
        /// <param name="cadena">Cadena de encriptar</param>
        /// <returns>Cadena encriptrada</returns>
        public static string Encriptar(string cadena)
        {
            string hash = string.Empty;

            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, cadena);
            }

            return hash;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        static bool CompararMD5(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Genera el codigo de pin
        /// </summary>
        /// <param name="longitud">0 por defecto no controla longitud</param>
        /// <returns>Codigo de pin</returns>
        public string GenerarPin(int longitud = 0)
        {
            string pin = string.Empty;

            pin = Guid.NewGuid().ToString().Replace("-", "");
            try
            {
                //Se debe realizar el Reemplazo de Cero (0) y ó por aleatorio del 1 al 9
                Random rnd = new Random();
                int Valor = rnd.Next(1, 9);
                pin = pin.Replace("0", Valor.ToString());
                Valor = rnd.Next(1, 9);
                pin = pin.Replace("O", Valor.ToString());
                Valor = rnd.Next(1, 9);
                pin = pin.Replace("o", Valor.ToString());

            }
            catch
            {
                pin = Guid.NewGuid().ToString().Replace("-", "");
            }

            if (longitud > 0)
                pin = pin.Substring(0, longitud);

            return pin.ToUpper();
        }

        /// <summary>
        /// Funcion para remover espacios en blanco y caracteres especiales
        /// </summary>
        /// <param name="Cadena">Cadena a limpiar</param>
        /// <returns>cadena con la revision</returns>
        public string RemoverCaractreresEspeciales(string Cadena)
        {
            string result = Cadena;
            try
            {
                result = result.TrimStart().TrimEnd();
                result = result.Replace("*", "");
                result = result.Replace("#", "");
                result = result.Replace(".", "");
                result = result.Replace(",", "");
                result = result.Replace(":", "");
                result = result.Replace(";", "");
                result = result.Replace("{", "");
                result = result.Replace("}", "");
                result = result.Replace("[", "");
                result = result.Replace("]", "");
            }
            catch
            {
                result = Cadena;
            }
            return result;
        }
    }
}
