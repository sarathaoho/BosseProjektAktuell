using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    public class Truck : Vehicle
    {
        public int MaxLoadInKG { get; set; }

        public Truck(string modelName, string registrationDate, Fuel fuelType, int maxLoadInKG) : base(modelName, registrationDate, fuelType)
        {
            MaxLoadInKG = maxLoadInKG;
            Wheels = 6;
        }

        public Truck()
        {

        }
    }
}
