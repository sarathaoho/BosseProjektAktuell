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
    public static class Database
    {
        private static List<User> _users;
        private static List<Vehicle> _vehicles;
        private static List<Mechanic> _allMechanics;
        private static List<Mechanic> _currentMechanics;
        private static List<Errand> _errands;
        private static List<VehiclePart> _components;

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
        public static List<Mechanic> AllMechanics
        {
            get
            {
                if (_allMechanics == null)
                {
                    _allMechanics = new List<Mechanic>();
                }
                return _allMechanics;
            }
            set { _allMechanics = value; }
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
