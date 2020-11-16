using Logic.DAL;
using Logic.Database;
using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
using Logic.Helpers;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Services
{
    public class ErrandService
    {
        private readonly UserDataAccess<Errand> _dbErrand;

        public ErrandService()
        {
            _dbErrand = new UserDataAccess<Errand>();
        }

        /// <summary>
        /// Creates and saves an errand with the provided parameters.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="problem"></param>
        /// <param name="vehicleID"></param>
        /// <returns></returns>
        public string CreateAndSaveErrand(string description, VehiclePart problem, string vehicleID)
        {
            var errand = new Errand() { Description = description, Problem = problem, VehicleID = vehicleID };
            db.Errands.Add(errand);

            _dbErrand.SaveList(db.Errands);
            return errand.ID;
        }

        /// <summary>
        /// Sets the mechanic ID to an errand.
        /// </summary>
        /// <param name="errandID"></param>
        /// <param name="mechanicID"></param>
        public void SetMechanicIdToErrand(string errandID, string mechanicID)
        {
            var errand = db.Errands.FirstOrDefault(errand => errand.ID == errandID);
            errand.MechanicID = mechanicID;

            _dbErrand.SaveList(db.Errands);

        }

        public IEnumerable<Errand> GetMechanicErrands (Mechanic mechanic)
        {
            return db.Errands.Where(x => x.MechanicID == mechanic.ID);
        }
    }
}
