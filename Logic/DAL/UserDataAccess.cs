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
        private readonly string path = $@"{typeof(T).Name}.json";
        private readonly string currentMechanicsPath = @"CurrentMechanics.json";
        private readonly string oldMechanicsPath = @"OldMechanics.json";
        /// <summary>
        /// Loads list from file if it exists. Returns an empty list if file is not found
        /// </summary>
        /// <returns></returns>
        public List<T> LoadList()
        {
            List<T> list = JsonHelper.ReadFile<T>(path);
            return list;
        }

        // Detta är fult :/ TODO!!!
        public List<Mechanic> LoadCurrentMechanics()
        {
            List<Mechanic> list = JsonHelper.ReadFile<Mechanic>(currentMechanicsPath);
            return list;
        }
        // TODO!!!
        public List<Mechanic> LoadOldMechanics()
        {
            List<Mechanic> list = JsonHelper.ReadFile<Mechanic>(oldMechanicsPath);
            return list;
        }

        /// <summary>
        /// Saves a list to file. Filename = object type.json.
        /// </summary>
        /// <param name="list"></param>
        public void SaveList(List<T> list)
        {
            JsonHelper.WriteFile<T>(list, path);
        }

        public void SaveMechanicList(List<Mechanic> list, string path)
        {
            JsonHelper.WriteFile(list, path);
        }


    }
}
