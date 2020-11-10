using Logic.Database.Entities.Vehicles;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    public class Bus : Vehicle
    {
        public int MaxAmountOfPassengers { get; set; }

        public Bus(string modelName, string registrationDate, Fuel fuelType, int maxAmountOfPassengers) : base(modelName, registrationDate, fuelType)
        {
            MaxAmountOfPassengers = maxAmountOfPassengers;
            Wheels = 6;
        }

        public Bus()
        {

        }
    }
}
