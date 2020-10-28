using Logic.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Extensions
{
    public static class ObjPropsPrint
    {
        /// <summary>
        /// Listar ett objekts alla properties i konsolen, kräver att propertyn har ett attribut För att kunna skriva ut korrekta namnen på resultatet.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectProps(this object obj)
        {
            var properties = obj.GetType().GetProperties();
            var sb = new StringBuilder();

            foreach (var property in properties.Reverse())
            {
                Translate word = (Translate)Attribute.GetCustomAttribute(property, typeof(Translate));

                if (word == null)
                {
                    //Innebär att Egenskapen inte har något attribute med ett alternativnamn
                    Console.WriteLine("Okänd egenskap");
                }
                
                else
                {
                    sb.AppendLine($"{word.Name}: {property.GetValue(obj, null)} {word.Extra}");
                }
            }
            return sb.ToString();
        }
    }
}
