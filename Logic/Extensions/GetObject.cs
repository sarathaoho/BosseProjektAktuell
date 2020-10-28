using System.Collections.Generic;

namespace Logic.Extensions
{
    public static class GetObject
    {
        /// <summary>
        /// Returnerar ett objekt baserat på om användarinmatningen överensstämmer med propertyns värde. 
        /// </summary>
        /// <typeparam name="Children"></typeparam>
        /// <typeparam name="Parent"></typeparam>
        /// <param name="objectlist"></param>
        /// <param name="userinput"></param>
        /// <param name="propertyid"></param>
        /// <returns></returns>

        public static object GetObjectFromList<T>(this List<T> objectlist, string userinput, string propertyid)
        {

            foreach (var obj in objectlist)
            {

                if (obj.GetType().GetProperty(propertyid).GetValue(obj).ToString().ToLower().Replace(" ", "") == userinput.ToLower().Replace(" ", ""))
                {
                    return obj;
                }

            }
            return null;
        }
    }
}
