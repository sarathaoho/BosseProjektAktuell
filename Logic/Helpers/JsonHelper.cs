using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Logic.Helpers
{
    public static class JsonHelper
    {
        // Generisk metod som läser in valfri typ av lista
        public static List<T> ReadFile<T>(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            List<T> list = JsonSerializer.Deserialize<List<T>>(jsonString);
            return list;
        }

    }
}
