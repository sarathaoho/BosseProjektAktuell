using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    public class Motorcycle : Vehicle
    {
        public int MaxSpeed { get; set; }


        public Motorcycle()
        {
            Wheels = 2;
        }
    }
}
