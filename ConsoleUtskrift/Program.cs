using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Logic.Database;
using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
using Logic.Services;
using GUI.Home;
using Logic.Models;
using Logic.Helpers;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ConsoleUtskrift
{
    class Program
    {
        public static void Main(string[] args)
        {
            string datum = "1991/11/28";
            var dtDatum = DateTime.Parse(datum);
            string strdatum = dtDatum.ToShortDateString();

            datum = "11/28/1991";
            dtDatum = DateTime.Parse(datum);
            strdatum = dtDatum.ToShortDateString();


            var mechanic = new Mechanic()
            {
                FirstName = "Peter",
                LastName = "Wallenäs",
                DateOfBirth = DateTime.Parse("1991-11-15"),
            };

            var _mechanicService = new MechanicService();

            int age = _mechanicService.GetAge(mechanic);

            if (_mechanicService.IsBirthday(mechanic))
            {
                Console.WriteLine($"Födelsedag! Woho! Du fyller {age} år!");
            }



        }
    }
}
