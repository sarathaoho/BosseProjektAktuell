using Logic.Database;
using System;
using System.Collections.Generic;
using System.IO;
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
            string jsonString = File.ReadAllText(filePath);
            List<T> list = JsonSerializer.Deserialize<List<T>>(jsonString);
            return list;

        }

        // Skriv ut valfri lista
        public static void WriteFile<T>(List<T> list, string filePath)
        {
            string json = JsonSerializer.Serialize(list);

            FileStream fs = File.OpenWrite(filePath);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(json);
            sw.Close();
        }

    }
}
