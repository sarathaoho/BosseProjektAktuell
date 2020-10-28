using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities.Vehicles
{
    public abstract class Vehicle
    {
        protected decimal _odometer;
        
        public string ModelName { get; set; }
        public string LicensePlate { get; set; }
        public string RegistrationDate { get; set; }
        public string FuelType { get; set; }
        public int Wheels { get; set; }


        public void SetOdometer(decimal lengthDriven)
        {
            if (lengthDriven > 0)
            {
                _odometer += lengthDriven;
            }
        }

        public decimal GetOdometer()
        {
            return _odometer;
        }

    }
}
