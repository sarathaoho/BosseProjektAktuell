using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database
{
    public class BossesBilverkstad
    {
        public List<Vehicle> Vehicles { get; set; }
        public Dictionary<string, Vehicle> RegNrDictionary { get; set; }
        public List<Mechanic> Mechanics { get; set; }
        public List<Errand> Errands { get; set; }


        public VehiclePart Components { get; set; }

        public BossesBilverkstad()
        {
            Mechanics = new List<Mechanic>();
            RegNrDictionary = new Dictionary<string, Vehicle>();
            Vehicles = new List<Vehicle>();
            Errands = new List<Errand>();
        }
    }
}
