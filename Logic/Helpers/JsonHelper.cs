using Logic.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Logic.Helpers
{

    // Helper class för Json
    public static class JsonHelper
    {

        // Generisk metod som läser in valfri typ av lista
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

        // Skriv ut valfri lista
        public static void WriteFile<T>(List<T> list, string filePath)
        {
            string json = JsonSerializer.Serialize(list);

            File.WriteAllText(filePath, json);

            //// Fixa så att den skriver över filen snarare än att lägga till
            //FileStream fs = File.OpenWrite(filePath);

            //StreamWriter sw = new StreamWriter(fs);

            //sw.Write(json);
            //sw.Close();
        }
    }
}
