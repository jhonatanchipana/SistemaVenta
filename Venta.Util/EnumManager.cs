using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LaTinka.Common
{
    /// <summary>
    /// Clase que gestiona la interacción con las enumeraciones personalizadas
    /// </summary>
    public sealed class EnumManager
    {
        private const string SIN_VALOR = "-1";

        private static readonly Lazy<EnumManager> lazy =
           new Lazy<EnumManager>(() => new EnumManager());

        /// <summary>
        /// Instancia de la clase
        /// </summary>
        public static EnumManager Instance { get { return lazy.Value; } }
        
        private EnumManager()
        {
        }

        /// <summary>
        /// Retorna la descripción de una enumeración
        /// </summary>
        /// <param name="value">Enumeración requerida</param>
        /// <returns>Descripción de la enumeración</returns>
        public static string GetEnumDescription(Enum value)
        {
            if (value != null)
            {
                if (value.ToString() == SIN_VALOR)
                    return string.Empty;

                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Retorna una lista de valores de una enumeración
        /// </summary>
        /// <typeparam name="T">Enumeración</typeparam>
        /// <returns>Lista con los valores de la enumeración</returns>
        /// <exception cref="ArgumentException">Genera una excepción si el tipo no es una enumeración</exception>
        public static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);
            
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

        public static bool IsValid<T>(int value, out T enumeration)
        {
            enumeration = default(T);

            if (Enum.IsDefined(typeof(T), value))
            {
                enumeration = (T)Convert.ChangeType(value, typeof(T));
                return true;
            }

            return false;
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description.ToUpper() == description.ToUpper())
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description.ToUpper())
                        return (T)field.GetValue(null);
                }
            }
            //throw new ArgumentException("Not found.", "description");
            return default(T);
        }
    }
}
