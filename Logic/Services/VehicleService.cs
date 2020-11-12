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
    public class VehicleService
    {
        private UserDataAccess<Car> _dbCar; 
        private UserDataAccess<Motorcycle> _dbMotorcycle;
        private UserDataAccess<Bus> _dbBus; 
        private UserDataAccess<Truck> _dbTruck;
        

        public VehicleService()
        {
            _dbCar = new UserDataAccess<Car>();
            _dbMotorcycle = new UserDataAccess<Motorcycle>();
            _dbBus = new UserDataAccess<Bus>();
            _dbTruck = new UserDataAccess<Truck>();
        }
      
        /// <summary>
        /// Sets the licenseplate after forcing it to upper case and removing any spaces.
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <returns></returns>
        public string SetLicensePlate(string licenseNumber)
        {
            return licenseNumber.ToUpper().Replace(" ", "");
        }
        /// <summary>
        /// Gets the vehicle that's connected to an errand.
        /// </summary>
        /// <param name="errand"></param>
        /// <returns></returns>
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
        /// <br>Returns: The created car's ID as <b>string</b></br>
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
            _dbCar.SaveList(db.Cars);

            return car.ID;
        }
        /// <summary>
        /// Creates a motorcycle with the provided parameters.
        /// <br>Returns: The created motorcycle's ID as <b>string</b></br>
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="licensePlate"></param>
        /// <param name="registrationDate"></param>
        /// <param name="fuelType"></param>
        /// <param name="maxSpeed"></param>
        /// <param name="lengthDriven"></param>
        /// <returns></returns>
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
            _dbMotorcycle.SaveList(db.Motorcycles);

            return motorcycle.ID;
        }
        /// <summary>
        /// Creates a bus with the provided parameters.
        /// <br>Returns: The created bus' ID as <b>string</b></br>
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="licensePlate"></param>
        /// <param name="registrationDate"></param>
        /// <param name="fuelType"></param>
        /// <param name="maxAmountOfPassengers"></param>
        /// <param name="lengthDriven"></param>
        /// <returns></returns>
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
            _dbBus.SaveList(db.Buses);
            return bus.ID;
        }
        /// <summary>
        /// Creates a truck with the provided parameters.
        /// <br>Returns: The created truck's ID as <b>string</b></br>
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="licensePlate"></param>
        /// <param name="registrationDate"></param>
        /// <param name="fuelType"></param>
        /// <param name="maxLoad"></param>
        /// <param name="lengthDriven"></param>
        /// <returns></returns>
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
            _dbTruck.SaveList(db.Trucks);
            return truck.ID;
        }
    }
}
