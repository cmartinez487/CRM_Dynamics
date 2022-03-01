using CRM.Dynamics.AccesoDatos;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Dynamics.Comun
{
    public class TokenJWT
    {
        #region Singleton
        private static volatile TokenJWT instancia;
        private static object syncRoot = new Object();


        public static TokenJWT Instance
        {
            get
            {
                if (instancia == null)
                {
                    lock (syncRoot)
                    {
                        if (instancia == null)
                            instancia = new TokenJWT();
                    }
                }
                return instancia;
            }
        }

        #endregion Singleton

        /// <summary>
        /// Funcion para serializar mediante JWT
        /// </summary>
        /// <param name="parametros">Diccionario de datos a serializar</param>
        /// <returns>la cadena con el token generado</returns>
        public string Serialize(Dictionary<string, object> parametros, string Key)
        {

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(parametros, Key);

            return token;
        }

        /// <summary>
        /// Funcion para deserializar un token mediante JWT
        /// </summary>
        /// <param name="token">cadena de token a deserializar</param>
        /// <returns></returns>
        public string Deserialize(string token, string Key)
        {
            string result = string.Empty;
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                result = decoder.Decode(token, Key, verify: true);
            }
            catch (TokenExpiredException)
            {
                result = "Token has expired";
            }
            catch (SignatureVerificationException)
            {
                result = "Token has invalid signature";
            }
            return result;
        }

        /// <summary>
        /// Funcion para deserializar un token mediante JWT
        /// </summary>
        /// <param name="token">cadena de token a deserializar</param>
        /// <returns></returns>
        public T DeserializeObject<T>(string token, string Key) where T : new()
        {
            string result = string.Empty;
            var returnobj = new T();
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                result = decoder.Decode(token, Key, verify: true);
                returnobj = JsonConvert.DeserializeObject<T>(result);
            }
            catch (TokenExpiredException)
            {
                result = null;
            }
            catch (SignatureVerificationException)
            {
                result = null;
            }

            return returnobj;
        }

        /// <summary>
        /// Funcion para serializar el toque del objeto enviado como parametros
        /// </summary>
        /// <param name="parametros">Objeto con parametros a cifrar</param>
        /// <returns></returns>
        public string SerializeJWT(Dictionary<string, object> parametros, string Key)
        {
            string key = Key;
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.Default.GetBytes(key));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(signingCredentials);
            var payload = new JwtPayload();
            foreach (KeyValuePair<string, object> item in parametros)
            {
                payload.Add(item.Key, item.Value);
            }

            var secToken = new JwtSecurityToken(header, payload);

            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);
            return tokenString;
        }

        /// <summary>
        /// Funcion para deserealizar el token de sesion
        /// </summary>
        /// <param name="token">Cadena Token cifrado</param>
        /// <returns></returns>
        public Dictionary<string, object> DeserializeJWT(string token)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            var tokenDecode = new JwtSecurityToken(jwtEncodedString: token);
            List<Claim> lista = tokenDecode.Claims.ToList();

            if (lista.Count() > 0)
            {
                foreach (Claim item in lista)
                {
                    parametros.Add(item.Type, item.Value);
                }
            }
            return parametros;
        }


        /// <summary>
        /// Funcion para encripta en MD5
        /// </summary>
        /// <param name="cadena">Cadena string a encriptar</param>
        /// <returns></returns>
        public string EncriptarLlave(string cadena, string key)
        {
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto que vamos a encriptar            
            byte[] CadenaByte = UTF8Encoding.UTF8.GetBytes(cadena);

            //se utilizan las clases de encriptación            
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado = cTransform.TransformFinalBlock(CadenaByte, 0, CadenaByte.Length);
            tdes.Clear();
            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }

        /// <summary>
        /// Funcion para desencriptar la cadena con MD5
        /// </summary>
        /// <param name="clave"></param>
        /// <returns></returns>
        public string DesencriptarLlave(string clave, string key)
        {
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] ClaveByte = Convert.FromBase64String(clave);

            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray =
            cTransform.TransformFinalBlock(ClaveByte, 0, ClaveByte.Length);

            tdes.Clear();
            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
