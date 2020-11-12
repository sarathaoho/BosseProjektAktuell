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
using Logic.Extensions;
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
            string datum = "1991-11-28";

            var dtDatum = DateTime.Parse(datum);

            List<Vehicle> fordon = new List<Vehicle>();

            var car = new Car()
            {
                ModelName = "Bil1",
                RegistrationDate = "1234",
                CarType = CarType.Sedan,
                FuelType = Fuel.Bensin,
                HasTowbar = false,
                LicensePlate = "ABC123",
                Odometer = 1234.5,
                Wheels = 4
            };
            fordon.Add(car);

            var motorcycle = new Motorcycle()
            {
                ModelName = "Motorcykel1",
                RegistrationDate = "1234",
                FuelType = Fuel.Bensin,
                LicensePlate = "ABC123",
                Odometer = 1234.5,
                Wheels = 2,
                MaxSpeed = 250
            };
            fordon.Add(motorcycle);

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            string serialized = JsonConvert.SerializeObject(fordon, settings);

            List<Vehicle> deserializedList = JsonConvert.DeserializeObject<List<Vehicle>>(serialized, settings);

        }

        //private static void AdminMenu(User user)
        //{
        //    Console.Clear();
        //    Console.WriteLine("1. Skapa/ändra/ta bort ärende");
        //    Console.WriteLine("2. Lista alla ärenden och dess status");
        //    Console.WriteLine("3. Lägg till/ändra/ta bort användare");
        //    Console.WriteLine("4. Lägg till/ändra/ta bort mekaniker");
        //    Console.WriteLine("5. Avsluta");

        //    int.TryParse(Console.ReadLine(), out int menuInput);

        //    switch (menuInput)
        //    {


        //        case 1:
        //            #region Skapa/Ta bort ärende
        //            Console.Clear();
        //            Console.WriteLine("1. Skapa ärende");
        //            Console.WriteLine("2. Ändra ärende");


        //            int.TryParse(Console.ReadLine(), out menuInput);
        //            switch (menuInput)
        //            {
        //                #region Skapa ärende
        //                case 1:
        //                    Console.WriteLine("---- SKAPA ÄRENDE");
        //                    Console.WriteLine("Beskriv problemet: ");
        //                    var errandDescription = Console.ReadLine();

        //                    Console.WriteLine("Typ av ärende: " +
        //                        "\n1. Kaross" +
        //                        "\n2. Bromsar" +
        //                        "\n3. Motor" +
        //                        "\n4. Däck" +
        //                        "\n5. Vindruta");

        //                    var problem = ErrandChooser();

        //                    Console.WriteLine("Typ av fordon:" +
        //                        "\n1. Bil" +
        //                        "\n2. Motorcykel" +
        //                        "\n3. Buss" +
        //                        "\n4. Lastbil");

        //                    var vehicleChoice = VehicleChooser();
        //                    var vehicle = ConsoleHelper.CreateVehicle(vehicleChoice);

        //                    db.Vehicles.Add(vehicle);

        //                    var errand = new Errand() { Description = errandDescription, Problem = problem, Vehicle = vehicle };
        //                    //bool success = user.TryAddErrand(errand);

        //                    //if (success)
        //                    //{
        //                    //    Console.WriteLine($"Ärendet \"{errand.Description}\" togs emot av mekaniker: \"{user.UserMechanic.FirstName}\"");
        //                    //    errand.ErrandStatus = ErrandStatus.Gul;
        //                    //    Listor.Errands.Add(errand);
        //                    //}

        //                    //else
        //                    //{
        //                    //    Console.WriteLine("Hittade ingen ledig mekaniker med rätt kompetens. Ärendet lagras.");
        //                    //    Listor.Errands.Add(errand);
        //                    //}
        //                    #endregion
        //                    BackToMenu();
        //                    break;

        //                case 2:
        //                    Console.WriteLine("----ÄNDRA ÄRENDE----");
        //                    Console.WriteLine("Vilket ärende vill du ändra på?");

        //                    if (db.Errands.Count == 0)
        //                    {
        //                        Console.WriteLine("Det finns inga liggande eller pågående ärenden.");
        //                    }

        //                    else
        //                    {
        //                        PrintUnfinishedErrands();

        //                        Console.WriteLine("Vilket ärende vill du ändra på? Sök på registreringsnummer: ");
        //                        var licensePlateToMatch = Console.ReadLine().ToUpper().Replace(" ", "");

        //                        Errand errandToChange = null;
        //                        foreach (var err in db.Errands)
        //                        {
        //                            if (err.Vehicle.LicensePlate == licensePlateToMatch)
        //                            {
        //                                errandToChange = err;
        //                            }

        //                            else
        //                            {
        //                                Console.WriteLine("Hittade inte ett fordon med det reg.numret.");
        //                            }
        //                        }

        //                        if (errandToChange != null)
        //                        {
        //                            Console.WriteLine($"Nuvarande status på ärendet är \"{errandToChange.ErrandStatus}\"");
        //                            Console.WriteLine("Vad vill du ändra den till? " +
        //                                "\n1. Grön" +
        //                                "\n2. Gul");

        //                            int.TryParse(Console.ReadLine(), out int statusPicked);
        //                            ErrandStatus newStatus = (ErrandStatus)statusPicked;

        //                            //basic.UserMechanic.ChangeErrandStatus(errandToChange, newStatus);


        //                        }
        //                    }




        //                    BackToMenu();

        //                    break;

        //                default:
        //                    break;
        //            }

        //            break;
        //        #endregion

        //        case 2:
        //            PrintUnfinishedErrands();
        //            Console.ReadLine();
        //            break;


        //        case 3:
        //            // TODO: Här jobbade du senast
        //            #region Lägg till/ändra/ta bort anställd
        //            Console.WriteLine("1. Lägg till användare");
        //            var answer = Convert.ToInt32(Console.ReadLine());

        //            if (answer == 1)
        //            {
        //                Console.WriteLine("Mata in användarnamn: ");
        //                var userName = Console.ReadLine();
        //                Console.WriteLine("Mata in lösenord: ");
        //                var passWord = Console.ReadLine();

        //                User user2 = new User();
        //                foreach (var mechanic in db.OldMechanics)
        //                {
        //                    Console.WriteLine($"Namn: {mechanic.FirstName} {mechanic.LastName}");
        //                }
        //                Console.WriteLine("Mata in ID på den mekaniker som användaren är kopplad till: ");
        //                var userID = Convert.ToInt32(Console.ReadLine());

        //                //user2.UserMechanic = db.Mechanics.FirstOrDefault(obj => obj.ID == userID);

        //                Console.WriteLine("----ANVÄNDARE SKAPAD-----");
        //                Console.WriteLine($"Användarnamn: {userName}");
        //                Console.WriteLine($"Lösenord: {passWord}");
        //                //Console.WriteLine($"Mekaniker: {user2.UserMechanic.FirstName} {user2.UserMechanic.LastName}, " +
        //                //    $"ID: {user2.UserMechanic.ID}");
        //            }


        //            break;
        //        #endregion

        //        default:
        //            break;

        //    }
        //}

        //private static void PrintUnfinishedErrands()
        //{
        //    foreach (var err in db.Errands)
        //    {
        //        if (err.ErrandStatus != ErrandStatus.Grön)
        //        {
        //            Console.WriteLine("\t----------");
        //            Console.WriteLine($"Ärendeproblem: {err.Problem}" +
        //                $"\nÄrendebeskrivning: {err.Description}" +
        //                $"\nNamn: {err.Vehicle.ModelName}" +
        //                $"\nRegistreringsnummer: {err.Vehicle.LicensePlate}" +
        //                $"\nÄrendestatus: {err.ErrandStatus}");
        //        }
        //    }
        //}

        private static void BackToMenu()
        {
            Console.WriteLine("Tryck på enter för att fortsätta...");
            Console.ReadLine();
        }


        private static VehiclePart ErrandChooser()
        {

            int.TryParse(Console.ReadLine(), out int problem);
            VehiclePart chosenPart = (VehiclePart)problem;
            return chosenPart;
        }

        public static Vehicle VehicleChooser()
        {
            Vehicle vehicle = null;

            int.TryParse(Console.ReadLine(), out int choice);

            if (choice == 1)
            {
                vehicle = new Car();
            }

            else if (choice == 2)
            {
                vehicle = new Motorcycle();
            }
            else if (choice == 3)
            {
                vehicle = new Bus();
            }
            else if (choice == 5)
            {
                vehicle = new Truck();
            }

            return vehicle;


        }

        //bool Email = true;
        //while (Email)
        //{
        //    MessageBox.Show("Ange en email:");
        //    string Username = Console.ReadLine();
        //    if (Regex.IsMatch(Username, @"^([\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$"))
        //    {
        //        MessageBox.Show("du har angett en giltig email");
        //        Email = false;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Felaktig imat, försk igen!");
        //    }


        //}
    }
}
