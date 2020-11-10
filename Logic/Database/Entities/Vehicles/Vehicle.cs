using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    public abstract class Vehicle : AEntity
    {
       


        public string ModelName { get; set; }
        public string LicensePlate { get; set; }
        public string RegistrationDate { get; set; }
        public Fuel FuelType { get; set; }
        public int Wheels { get; set; }

    }
}
