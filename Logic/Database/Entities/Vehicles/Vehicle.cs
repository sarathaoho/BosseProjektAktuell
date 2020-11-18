using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    // Klassen kan visst inte vara abstract pga fel i serialisering till/från json?
    public abstract class Vehicle : AEntity
    {
        public string ModelName { get; set; }
        public string LicensePlate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Fuel FuelType { get; set; }
        public int Wheels { get; set; }
        public double Odometer { get; set; }

        public Vehicle()
        {

        }
    }
}
