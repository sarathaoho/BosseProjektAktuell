using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    public class Truck : Vehicle
    {
        public int MaxLoadInKG { get; set; }

        public Truck()
        {
            Wheels = 6;
            FuelType = "Diesel";
        }
    }
}
