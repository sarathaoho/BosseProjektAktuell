using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    public class Car : Vehicle
    {
        public bool HasTowbar { get; set; }
        public CarType CarType { get; set; }

        public Car(string modelName, string registrationDate, Fuel fuelType, bool hasTowbar, CarType carType) : base(modelName, registrationDate, fuelType)
        {
            HasTowbar = hasTowbar;
            CarType = carType;
            Wheels = 4;
        }

        public Car()
        {
            
        }
    }
}
