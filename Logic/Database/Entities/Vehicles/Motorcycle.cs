using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public int MaxSpeed { get; set; }


        public Motorcycle(string modelName, string registrationDate, Fuel fuelType, int maxSpeed) : base(modelName, registrationDate, fuelType)
        {
            MaxSpeed = maxSpeed;
            Wheels = 2;
        }

        public Motorcycle()
        {

        }
    }
}
