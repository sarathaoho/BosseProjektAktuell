using Logic.DAL;
using Logic.Database;
using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
using Logic.Helpers;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Services
{
    // Klass som sköter all logik kring fordon
    public class VehicleService
    {
        // Fixa dessa
        private UserDataAccess<Car> _dbCar; //= new UserDataAccess<Car>();
        private UserDataAccess<Motorcycle> _dbMotorcycle; //= new UserDataAccess<Car>();
        private UserDataAccess<Bus> _dbBus; //= new UserDataAccess<Car>();
        private UserDataAccess<Truck> _dbTruck; //= new UserDataAccess<Car>();
        



        public VehicleService()
        {
            _dbCar = new UserDataAccess<Car>();
            _dbMotorcycle = new UserDataAccess<Motorcycle>();
            _dbBus = new UserDataAccess<Bus>();
            _dbTruck = new UserDataAccess<Truck>();
        }
      
        public string SetLicensePlate(string licenseNumber)
        {
            return licenseNumber.ToUpper().Replace(" ", "");
        }

        public Vehicle GetVehicleFromErrand(Errand errand)
        {

            var car = db.Cars.FirstOrDefault(car => errand.VehicleID.Contains(car.ID));
            if (car != null)
                return car;

            var motorcycle = db.Motorcycles.FirstOrDefault(motorcycle => errand.VehicleID.Contains(motorcycle.ID));
            if (motorcycle != null)
            {
                return motorcycle;
            }

            var bus = db.Buses.FirstOrDefault(bus => errand.VehicleID.Contains(bus.ID));
            if (bus != null)
            {
                return bus;
            }

            var truck = db.Trucks.FirstOrDefault(truck => errand.VehicleID.Contains(truck.ID));
            if (truck != null)
            {
                return bus;
            }

            return null;
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

            db.Cars.Add(car);
            _dbCar.WriteList(db.Cars);

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

            db.Motorcycles.Add(motorcycle);
            //JsonHelper.WriteFile(db.Motorcycles, _motorcyclesPath);
            _dbMotorcycle.WriteList(db.Motorcycles);

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

            db.Buses.Add(bus);
            //JsonHelper.WriteFile(db.Buses, _busesPath);
            _dbBus.WriteList(db.Buses);
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

            db.Trucks.Add(truck);
            //JsonHelper.WriteFile(db.Trucks, _trucksPath);
            _dbTruck.WriteList(db.Trucks);
            return truck.ID;
        }
    }
}
