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

        public string VehicleID { get; set; }

        public VehiclePart Problem { get; set; }

        public ErrandStatus ErrandStatus { get; set; }

        public string MechanicID { get; set; }

        public Errand()
        {
            ErrandStatus = ErrandStatus.Röd;
        }
    }
}
