using Logic.Database.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    public class Bus : Vehicle
    {
        public int MaxAmountOfPassengers { get; set; }

        public Bus()
        {
            Wheels = 6;
            FuelType = "Diesel";
        }
    }
}
