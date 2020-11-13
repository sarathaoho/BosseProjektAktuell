using Logic.DAL;
using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Logic.Database
{
    // Statisk klass som tar hand om alla listor som ska skrivas till fil
    public static class db
    {
        private static List<User> _users;
        private static List<Vehicle> _vehicles;
        private static List<Mechanic> _oldMechanics; // Lista för de som inte jobbar kvar på verkstaden
        private static List<Mechanic> _currentMechanics; // Lista för de som jobbar på verkstaden nu
        private static List<Errand> _errands;
        private static List<VehiclePart> _components;
        private static List<Car> _cars;
        private static List<Motorcycle> _motorcycles;
        private static List<Bus> _buses;
        private static List<Truck> _trucks;

        //public class MinaVechiacleListor
        //{
        //    public List<Car> Cars { get; set; }
        //    public List<Motorcycle> Bikes { get; set; }
        //}

        public static List<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new List<User>();
                }
                return _users;
            }
            set { _users = value; }
        }
        public static List<Vehicle> Vehicles
        {
            get
            {
                if (_vehicles == null)
                {
                    _vehicles = new List<Vehicle>();
                }
                return _vehicles;
            }
            set { _vehicles = value; }
        }

        public static List<Car> Cars
        {
            get
            {
                if (_cars == null)
                {
                    _cars = new List<Car>();
                }
                return _cars;
            }
            set
            {
                _cars = value;
            }
        }
        public static List<Motorcycle> Motorcycles
        {
            get
            {
                if (_motorcycles == null)
                {
                    _motorcycles = new List<Motorcycle>();
                }
                return _motorcycles;

            }
            set
            {
                _motorcycles = value;
            }
        }
        public static List<Bus> Buses
        {
            get
            {
                if (_buses == null)
                {
                    _buses = new List<Bus>();
                }
                return _buses;

            }
            set
            {
                _buses = value;
            }
        }
        public static List<Truck> Trucks
        {
            get
            {
                if (_trucks == null)
                {
                    _trucks = new List<Truck>();
                }
                return _trucks;

            }
            set
            {
                _trucks = value;
            }
        }

        public static List<Mechanic> OldMechanics
        {
            get
            {
                if (_oldMechanics == null)
                {
                    _oldMechanics = new List<Mechanic>();
                }
                return _oldMechanics;
            }
            set { _oldMechanics = value; }
        }
        public static List<Mechanic> CurrentMechanics
        {
            get
            {
                if (_currentMechanics == null)
                {
                    _currentMechanics = new List<Mechanic>();
                }
                return _currentMechanics;
            }
            set { _currentMechanics = value; }
        }
        public static List<Errand> Errands
        {
            get
            {
                if (_errands == null)
                {
                    _errands = new List<Errand>();
                }
                return _errands;
            }
            set { _errands = value; }
        }
        public static List<VehiclePart> Competences
        {
            get
            {
                if (_components == null)
                {
                    _components = new List<VehiclePart>()
                    {
                        VehiclePart.Kaross,
                        VehiclePart.Bromsar,
                        VehiclePart.Motor,
                        VehiclePart.Hjul,
                        VehiclePart.Vindruta

                     };
                }
                return _components;
            }
        }

        

        // Skiter i denna?
        //public Dictionary<string, Vehicle> RegNrDictionary { get; set; }
        //public BossesBilverkstad()
        //{
        //    Mechanics = new List<Mechanic>();
        //    RegNrDictionary = new Dictionary<string, Vehicle>();
        //    Vehicles = new List<Vehicle>();
        //    Errands = new List<Errand>();
        //}
    }
}
