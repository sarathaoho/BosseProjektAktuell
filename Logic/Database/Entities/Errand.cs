using Logic.Attributes;
using Logic.Database.Entities.Vehicles;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities
{
    public class Errand : AEntity
    {
        public string Description { get; set; }

        public Vehicle Vehicle { get; set; }

        public VehiclePart Problem { get; set; }

        public ErrandStatus ErrandStatus { get; set; }

        public Errand(string description, Vehicle vehicle, VehiclePart problem)
        {
            Description = description;
            Vehicle = vehicle;
            Problem = problem;

            ErrandStatus = ErrandStatus.Röd;
        }

        public Errand()
        {

        }
    }
}
