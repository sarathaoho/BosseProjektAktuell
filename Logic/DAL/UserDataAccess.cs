using Logic.Database;
using Logic.Database.Entities;
using Logic.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Logic.DAL
{
    public class UserDataAccess<T> where T : AEntity
    {
        // Behöver skapa mappen om den inte finns när programmet startas
        // TODO: Fixa också path till att inte bara hämta User
        private const string path = @"DAL\Files\User.json"; 


        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            string jsonString = File.ReadAllText(path);
            List<T> list = JsonHelper.ReadFile<T>(path);
            return list;
        }

    }
}
