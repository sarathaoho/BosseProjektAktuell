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
        public Car()
        {
            Wheels = 4;
        }

    }
}
