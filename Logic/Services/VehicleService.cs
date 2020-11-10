using Logic.Database;
using Logic.Database.Entities.Vehicles;
using Logic.Exceptions;
using Logic.Helpers;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    // Klass som sköter all logik kring fordon
    public class VehicleService
    {
        private readonly string _vehiclesPath = @"DAL\Files\Vehicles.json";

      
        public string SetLicensePlate(string licenseNumber)
        {
            return licenseNumber.ToUpper().Replace(" ", "");
        }



        /// <summary>
        /// Creates a car with the provided parameters.
        /// <br>Returns: The created cars ID as <b>string</b></br>
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="licensePlate"></param>
        /// <param name="registrationDate"></param>
        /// <param name="fuelType"></param>
        /// <param name="hasTowbar"></param>
        /// <param name="carType"></param>
        /// <param name="lengthDriven"></param>
        /// <returns></returns>
        public string CreateAndWriteCar(string modelName, string licensePlate, string registrationDate, Fuel fuelType, bool hasTowbar, CarType carType, double lengthDriven)
        {
            var car = new Car()
            {
                ModelName = modelName,
                LicensePlate = SetLicensePlate(licensePlate),
                RegistrationDate = registrationDate,
                FuelType = fuelType,
                HasTowbar = hasTowbar,
                CarType = carType,
                Odometer = lengthDriven
            };

            db.Vehicles.Add(car);
            JsonHelper.WriteFile(db.Vehicles, _vehiclesPath);

            return car.ID;
        }

        public string CreateAndWriteMotorcycle(string modelName, string licensePlate, string registrationDate, Fuel fuelType, int maxSpeed, double lengthDriven)
        {
            var motorcycle = new Motorcycle()
            {
                ModelName = modelName,
                LicensePlate = SetLicensePlate(licensePlate),
                RegistrationDate = registrationDate,
                FuelType = fuelType,
                MaxSpeed = maxSpeed,
                Odometer = lengthDriven
            };

            db.Vehicles.Add(motorcycle);
            JsonHelper.WriteFile(db.Vehicles, _vehiclesPath);

            return motorcycle.ID;
        }

        public string CreateAndWriteBus(string modelName, string licensePlate, string registrationDate, Fuel fuelType, int maxAmountOfPassengers, double lengthDriven)
        {
            var bus = new Bus()
            {
                ModelName = modelName,
                LicensePlate = SetLicensePlate(licensePlate),
                RegistrationDate = registrationDate,
                FuelType = fuelType,
                MaxAmountOfPassengers = maxAmountOfPassengers,
                Odometer = lengthDriven
            };

            db.Vehicles.Add(bus);
            JsonHelper.WriteFile(db.Vehicles, _vehiclesPath);

            return bus.ID;
        }

        public string CreateAndWriteTruck(string modelName, string licensePlate, string registrationDate, Fuel fuelType, int maxLoad, double lengthDriven)
        {
            var truck = new Truck()
            {
                ModelName = modelName,
                LicensePlate = SetLicensePlate(licensePlate),
                RegistrationDate = registrationDate,
                FuelType = fuelType,
                MaxLoadInKG = maxLoad,
                Odometer = lengthDriven
            };

            db.Vehicles.Add(truck);
            JsonHelper.WriteFile(db.Vehicles, _vehiclesPath);

            return truck.ID;
        }
    }
}
