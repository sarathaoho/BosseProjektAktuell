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

    // Helper class för Json
    public static class JsonHelper
    {

        //Generisk metod som läser in valfri typ av lista
        public static List<T> ReadFile<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);

                //File.ReadAllTextAsync;
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

                    //File.ReadAllTextAsync;
                    List<T> list = JsonSerializer.Deserialize<List<T>>(jsonString);
                    return list;
                }
                return new List<T>();
            });
        }

        public static void WriteFile<T>(List<T> list, string filePath)
        {
            string json = JsonSerializer.Serialize(list);

            File.WriteAllText(filePath, json);

            //File.WriteAllTextAsync;
        }

    }
}
