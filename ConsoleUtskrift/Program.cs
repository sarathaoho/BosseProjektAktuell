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

namespace ConsoleUtskrift
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test av inloggningsservice
            LoginService loginService = new LoginService();
            Console.Write("Användarnamn: ");
            string username = Console.ReadLine();
            Console.Write("Lösenord: ");
            string password = Console.ReadLine();

            bool successful = loginService.Login(username, password);


            #region Skapar upp mekaniker och lägger in i Listor.Mechanics
            var mechanic1 = new Mechanic()
            {
                FirstName = "Bob",
                LastName = "Builder",
                DateOfBirth = "1961-01-01",
                DateOfEmployment = "2005-05-03",
                MechanicID = "1"
            };
            Database.AllMechanics.Add(mechanic1);

            mechanic1 = new Mechanic()
            {
                FirstName = "Peter",
                LastName = "Wallenäs",
                DateOfBirth = "1991-11-28",
                DateOfEmployment = "2018-05-03",
                MechanicID = "2"
            };

            Database.AllMechanics.Add(mechanic1);
            #endregion

            #region Skapar upp två ärenden och läggen in de i Listor.Errands
            Errand errand1 = new Errand()
            {
                Description = "Testdata",
                ErrandStatus = ErrandStatus.Röd,
                Problem = VehiclePart.Motor,
                Vehicle = new Motorcycle()
                {
                    ModelName = "Yamaha",
                    LicensePlate = "QWE345"
                }
            };
            Database.Errands.Add(errand1);

            errand1 = new Errand(
                "Kund klagar på att motorn låter",
                new Car()
                {
                    CarType = CarType.Cabriolet,
                    ModelName = "Mazda",
                    HasTowbar = false,
                    RegistrationDate = "2019-07-08",
                    FuelType = Fuel.Bensin
                },
                VehiclePart.Motor);

            Database.Errands.Add(errand1);
            #endregion

            var competence = VehiclePart.Bromsar;

            MechanicService mechanicService = new MechanicService();
            

        }

        private static void AdminMenu(User user)
        {
            Console.Clear();
            Console.WriteLine("1. Skapa/ändra/ta bort ärende");
            Console.WriteLine("2. Lista alla ärenden och dess status");
            Console.WriteLine("3. Lägg till/ändra/ta bort användare");
            Console.WriteLine("4. Lägg till/ändra/ta bort mekaniker");
            Console.WriteLine("5. Avsluta");

            int.TryParse(Console.ReadLine(), out int menuInput);

            switch (menuInput)
            {


                case 1:
                    #region Skapa/Ta bort ärende
                    Console.Clear();
                    Console.WriteLine("1. Skapa ärende");
                    Console.WriteLine("2. Ändra ärende");


                    int.TryParse(Console.ReadLine(), out menuInput);
                    switch (menuInput)
                    {
                        #region Skapa ärende
                        case 1:
                            Console.WriteLine("---- SKAPA ÄRENDE");
                            Console.WriteLine("Beskriv problemet: ");
                            var errandDescription = Console.ReadLine();

                            Console.WriteLine("Typ av ärende: " +
                                "\n1. Kaross" +
                                "\n2. Bromsar" +
                                "\n3. Motor" +
                                "\n4. Däck" +
                                "\n5. Vindruta");

                            var problem = ErrandChooser();

                            Console.WriteLine("Typ av fordon:" +
                                "\n1. Bil" +
                                "\n2. Motorcykel" +
                                "\n3. Buss" +
                                "\n4. Lastbil");

                            var vehicleChoice = VehicleChooser();
                            var vehicle = ConsoleHelper.CreateVehicle(vehicleChoice);

                            Database.Vehicles.Add(vehicle);

                            var errand = new Errand() { Description = errandDescription, Problem = problem, Vehicle = vehicle };
                            //bool success = user.TryAddErrand(errand);

                            //if (success)
                            //{
                            //    Console.WriteLine($"Ärendet \"{errand.Description}\" togs emot av mekaniker: \"{user.UserMechanic.FirstName}\"");
                            //    errand.ErrandStatus = ErrandStatus.Gul;
                            //    Listor.Errands.Add(errand);
                            //}

                            //else
                            //{
                            //    Console.WriteLine("Hittade ingen ledig mekaniker med rätt kompetens. Ärendet lagras.");
                            //    Listor.Errands.Add(errand);
                            //}
                            #endregion
                            BackToMenu();
                            break;

                        case 2:
                            Console.WriteLine("----ÄNDRA ÄRENDE----");
                            Console.WriteLine("Vilket ärende vill du ändra på?");

                            if (Database.Errands.Count == 0)
                            {
                                Console.WriteLine("Det finns inga liggande eller pågående ärenden.");
                            }

                            else
                            {
                                PrintUnfinishedErrands();

                                Console.WriteLine("Vilket ärende vill du ändra på? Sök på registreringsnummer: ");
                                var licensePlateToMatch = Console.ReadLine().ToUpper().Replace(" ", "");

                                Errand errandToChange = null;
                                foreach (var err in Database.Errands)
                                {
                                    if (err.Vehicle.LicensePlate == licensePlateToMatch)
                                    {
                                        errandToChange = err;
                                    }

                                    else
                                    {
                                        Console.WriteLine("Hittade inte ett fordon med det reg.numret.");
                                    }
                                }

                                if (errandToChange != null)
                                {
                                    Console.WriteLine($"Nuvarande status på ärendet är \"{errandToChange.ErrandStatus}\"");
                                    Console.WriteLine("Vad vill du ändra den till? " +
                                        "\n1. Grön" +
                                        "\n2. Gul");

                                    int.TryParse(Console.ReadLine(), out int statusPicked);
                                    ErrandStatus newStatus = (ErrandStatus)statusPicked;

                                    //basic.UserMechanic.ChangeErrandStatus(errandToChange, newStatus);


                                }
                            }




                            BackToMenu();

                            break;

                        default:
                            break;
                    }

                    break;
                #endregion

                case 2:
                    PrintUnfinishedErrands();
                    Console.ReadLine();
                    break;


                case 3:
                    // TODO: Här jobbade du senast
                    #region Lägg till/ändra/ta bort anställd
                    Console.WriteLine("1. Lägg till användare");
                    var answer = Convert.ToInt32(Console.ReadLine());

                    if (answer == 1)
                    {
                        Console.WriteLine("Mata in användarnamn: ");
                        var userName = Console.ReadLine();
                        Console.WriteLine("Mata in lösenord: ");
                        var passWord = Console.ReadLine();

                        User user2 = new BasicUser();
                        foreach (var mechanic in Database.AllMechanics)
                        {
                            Console.WriteLine($"Namn: {mechanic.FirstName} {mechanic.LastName}");
                            Console.WriteLine($"ID: {mechanic.MechanicID}");
                        }
                        Console.WriteLine("Mata in ID på den mekaniker som användaren är kopplad till: ");
                        var userID = Convert.ToInt32(Console.ReadLine());

                        //user2.UserMechanic = db.Mechanics.FirstOrDefault(obj => obj.ID == userID);

                        Console.WriteLine("----ANVÄNDARE SKAPAD-----");
                        Console.WriteLine($"Användarnamn: {userName}");
                        Console.WriteLine($"Lösenord: {passWord}");
                        //Console.WriteLine($"Mekaniker: {user2.UserMechanic.FirstName} {user2.UserMechanic.LastName}, " +
                        //    $"ID: {user2.UserMechanic.ID}");
                    }


                    break;
                #endregion

                default:
                    break;

            }
        }

        private static void PrintUnfinishedErrands()
        {
            foreach (var err in Database.Errands)
            {
                if (err.ErrandStatus != ErrandStatus.Grön)
                {
                    Console.WriteLine("\t----------");
                    Console.WriteLine($"Ärendeproblem: {err.Problem}" +
                        $"\nÄrendebeskrivning: {err.Description}" +
                        $"\nNamn: {err.Vehicle.ModelName}" +
                        $"\nRegistreringsnummer: {err.Vehicle.LicensePlate}" +
                        $"\nÄrendestatus: {err.ErrandStatus}");
                }
            }
        }

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

    }
}
