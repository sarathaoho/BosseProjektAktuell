using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Logic.Extensions
{
    public static class Parsing
    {
        /// <summary>
        /// Konverterar en sträng till vald datatyp. Skickar ut felmeddelande och försöker igen tills inmatningen är korrekt.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userinput"></param>
        /// <returns></returns>
        public static T Parse<T>(this string userinput)
        {
            T result = default(T);
            while (EqualityComparer<T>.Default.Equals(result, default(T)))
            {
                if (!String.IsNullOrEmpty(userinput))
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));

                    try
                    {
                        result = (T)tc.ConvertFrom(userinput);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Felinmatning försök igen.");
                        userinput = Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Felinmatning försök igen.");
                        userinput = Console.ReadLine();
                    }
                }
            }
            return result;
        }
    }
}
