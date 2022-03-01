namespace CRM.Dynamics.APIClient.Types
{
    /// <summary>
    /// Clase que procesa los valores deacuerdo a su tipo
    /// </summary>
    public static class EntityValue
    {
        /// <summary>
        /// Devuelve el valor tipeado
        /// </summary>
        /// <param name="value">Valor expresado como cadena de texto</param>
        /// <returns></returns>
        public static IEntityValue GetTypedValue(string value)
        {
            IEntityValue obValue;
            
            if (int.TryParse(value, out int i))
            {
                obValue = new IntValue { value = i };
            }
            else
            {                
                if (bool.TryParse(value, out bool b))
                {
                    obValue = new BoolValue { value = b };
                }
                else
                {
                    obValue = new StringValue { value = value };
                }
            }

            return obValue;
        }
    }

    /// <summary>
    /// Interface que representa un valor tipeado
    /// </summary>
    public interface IEntityValue { }

    /// <summary>
    /// Clase que representa un valor string
    /// </summary>
    sealed class StringValue : IEntityValue
    {
        public string value { get; set; }
    }

    /// <summary>
    /// Clase que representa un valor entero
    /// </summary>
    sealed class IntValue : IEntityValue
    {
        public int value { get; set; }
    }

    /// <summary>
    /// Clase que representa un valor booleano
    /// </summary>
    sealed class BoolValue : IEntityValue
    {
        public bool value { get; set; }
    }
}
