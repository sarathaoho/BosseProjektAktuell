using Logic.Database.Entities.Vehicles;
using Logic.Extensions;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public interface IErrand
    {
        public void AddErrand();
        public void RemoveErrand();
    }

    public class Errand : IErrand
    {
        public string Description { get; set; }

        public Vehicle Vehicle { get; set; }

        public VehiclePart Problem { get; set; }

        public ErrandStatus ErrandStatus { get; set; }

        public int ID { get; set; }

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

        public void AddErrand()
        {

        }
        public void RemoveErrand()
        {

        }
    }
}
