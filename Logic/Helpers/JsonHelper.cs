using Logic.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    // Helper class för Json functions
    public static class JsonHelper
    {
        /// <summary>
        /// Reads file from the provided <b>filepath</b> and returns a list.
        ///<br>Returns an empty list if the file could not be found.</br>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<T> ReadFile<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                List<T> list = JsonSerializer.Deserialize<List<T>>(jsonString);
                return list;
            }
            return new List<T>();
        }

        public static Task<List<T>> ReadFileAsync<T>(string filePath)
        {
            return Task.Run(() =>
            {
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllTextAsync(filePath).Result;
                    List<T> list = JsonSerializer.Deserialize<List<T>>(jsonString);
                    return list;
                }
                return new List<T>();
            });
        }

        /// <summary>
        /// Writes a list to file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        public static void WriteFile<T>(List<T> list, string filePath)
        {
            string json = JsonSerializer.Serialize(list);
            File.WriteAllText(filePath, json);
        }
    }
}
