using Logic.DAL;
using Logic.Database;
using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
using Logic.Extensions;
using Logic.Helpers;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    //public interface IErrandService
    //{
    //    public void AddErrand();
    //    public void RemoveErrand();
    //}

    // Klass för att sköta all logik med ärenden
    public class ErrandService
    {
        private readonly UserDataAccess<Errand> _dbErrand;

        public ErrandService()
        {
            _dbErrand = new UserDataAccess<Errand>();
        }
      public string CreateAndWriteErrand(string description, VehiclePart problem, string vehicleID)
        {
            var errand = new Errand() { Description = description, Problem = problem, VehicleID = vehicleID };
            db.Errands.Add(errand);
            //JsonHelper.WriteFile(db.Errands, _errandsPath);
            _dbErrand.WriteList(db.Errands);
            return errand.ID;
        }
    }
}
