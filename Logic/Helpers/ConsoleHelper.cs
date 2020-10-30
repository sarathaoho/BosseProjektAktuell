
using Logic.Database;
using Logic.Database.Entities.Vehicles;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Logic.Helpers
{
    public static class ConsoleHelper
    {

        #region Metoder för inmatning
        /// <summary>
        /// Skriver ut en fråga i konsolen, användaren matar in svaret som returneras i string.
        /// </summary>
        /// <param name="whatToWrite">Vad konsolen ska skriva ut för fråga</param>
        /// <returns>String</returns>
        private static string ReadName(string whatToWrite)
        {
            string name;
            var isSettingName = true;
            do
            {
                Console.Write(whatToWrite);
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                    Console.WriteLine("Du måste mata in något.");

                else
                    isSettingName = false;

            } while (isSettingName);

            return name;
        }

        /// <summary>
        /// Skriver ut en fråga i konsolen, användaren matar in svaret som returneras i string i <b>versaler</b>
        /// </summary>
        /// <param name="whatToWrite"></param>
        /// <returns></returns>
        private static string ReadLicensePlate(string whatToWrite)
        {
            string licensePlate;
            var isSettingLicensePlate = true;
            do
            {
                Console.Write(whatToWrite);
                licensePlate = Console.ReadLine().ToUpper();
                licensePlate = licensePlate.Replace(" ", "");
                if (string.IsNullOrEmpty(licensePlate))
                    Console.WriteLine("Du måste mata in något.");

                else
                    isSettingLicensePlate = false;

            } while (isSettingLicensePlate);

            return licensePlate;
        }

        /// <summary>
        /// Skriver ut en fråga i konsolen, användaren matar in svaret som returneras i bool.
        /// </summary>
        /// <param name="whatToWrite">Vad konsolen ska skriva ut för fråga</param>
        /// <returns>True/false</returns>
        private static bool ReadBool(string whatToWriteWithJorNquestion)
        {

            bool hasSomething = false;
            var isSettingBool = true;
            do
            {
                Console.Write(whatToWriteWithJorNquestion);
                var answer = Console.ReadLine().ToLower();
                if (string.IsNullOrEmpty(answer))
                    Console.WriteLine("Du måste svara på frågan.");

                else
                {
                    hasSomething = (answer == "j");
                    isSettingBool = false;
                }

            } while (isSettingBool);

            return hasSomething;
        }

        /// <summary>
        /// Skriver ut en fråga i konsolen, användaren matar in svaret som returneras i integer.
        /// </summary>
        /// <param name="whatToWrite">Vad konsolen ska skriva ut för fråga</param>
        /// <returns>Integer</returns>
        private static int ReadInteger(string whatToWrite)
        {
            int valueToSet = 0;
            var isSettingInt = true;
            do
            {
                Console.Write(whatToWrite);
                if (!int.TryParse(Console.ReadLine(), out valueToSet))
                    Console.WriteLine("Du måste mata in ett heltal.");

                else if (valueToSet < 0)
                    Console.WriteLine("Värdet kan inte vara minus.");

                else
                    isSettingInt = false;

            } while (isSettingInt);

            return valueToSet;
        }

        /// <summary>
        /// Skriver ut en fråga i konsolen, användaren matar in svaret som returneras i decimal.
        /// </summary>
        /// <param name="whatToWrite">Vad konsolen ska skriva ut för fråga</param>
        /// <returns>Decimaltal</returns>
        private static decimal ReadDecimal(string whatToWrite)
        {
            decimal valueToSet = 0;
            var IsSettingDecimal = true;
            do
            {
                Console.Write(whatToWrite);
                if (!Decimal.TryParse(Console.ReadLine(), out valueToSet))
                    Console.WriteLine("Du måste mata in ett tal.");

                else if (valueToSet < 0)
                    Console.WriteLine("Värdet kan inte vara minus.");

                else
                    IsSettingDecimal = false;

            } while (IsSettingDecimal);

            return valueToSet;
        }

        private static CarType ReadCarType()
        {
            var isInputting = true;
            while (isInputting)
            {
                Console.WriteLine("Vad är det för typ av bil?" +
                    "\n1. Sedan" +
                    "\n2. Herrgårdsvagn" +
                    "\n3. Cabriolet" +
                    "\n4. Halvkombi");

                int.TryParse(Console.ReadLine(), out int carChooser);
                if (carChooser > 4 || carChooser < 1)
                {
                    Console.WriteLine("Felinmatning, försök igen.");
                }

                else
                {
                    CarType carType = (CarType)carChooser;
                    isInputting = false;
                    return carType;
                }
            }


            return 0;
        }
        #endregion


        #region Metoder för att skapa fordon

        public static Vehicle CreateVehicle(Vehicle vehicle)
        {
            if (vehicle is Car)
            {
                var car = ConsoleHelper.CreateCar();
                return car;

            }

            else if (vehicle is Motorcycle)
            {
                var motorcycle = ConsoleHelper.CreateMotorcycle();
                return motorcycle;
            }

            else if (vehicle is Bus)
            {
                var bus = ConsoleHelper.CreateBus();
                return bus;
            }

            else if (vehicle is Truck)
            {
                var truck = ConsoleHelper.CreateTruck();
                return truck;
            }
            return null;
        }

        public static Vehicle CreateCar()
        {
            var car = new Car();
            var isAdding = true;
            while (isAdding)
            {
                Console.Clear();

                car.ModelName = ReadName("Mata in modellnamn: ");
                car.CarType = ReadCarType();
                car.LicensePlate = ReadLicensePlate("Mata in registreringsnummer: ");
                car.RegistrationDate = DateTime.Now.ToString("yyyy/MM/dd");
                car.HasTowbar = ReadBool("Har bilen en dragkrok? j/n: ");

                var lengthDriven = ReadDecimal("Mata in hur många mil fordonet har gått: ");

                PrintInfo(car);
                var isAnswering = true;
                do
                {
                    Console.Write("\nStämmer dessa uppgifter? j/n: ");
                    var answer = Console.ReadLine().ToLower();
                    if (answer == "j")
                    {
                        isAdding = false;
                        isAnswering = false;
                        break;
                    }

                    else if (string.IsNullOrEmpty(answer))
                        Console.WriteLine("Du måste svara.");

                    else
                        isAnswering = false;

                } while (isAnswering);

            }
            return car;


        }

        public static Vehicle CreateMotorcycle()
        {
            var motorcycle = new Motorcycle();
            var isAdding = true;
            while (isAdding)
            {
                Console.Clear();

                motorcycle.ModelName = ReadName("Mata in modellnamn: ");
                motorcycle.LicensePlate = ReadLicensePlate("Mata in registreringsnummer: ");
                motorcycle.RegistrationDate = DateTime.Now.ToString("yyyy/MM/dd");
                motorcycle.MaxSpeed = ReadInteger("Mata in maxhastighet i km/h: ");

                var lengthDriven = ReadDecimal("Mata in hur många mil fordonet har gått: ");

                PrintInfo(motorcycle);
                var isAnswering = true;
                do
                {
                    Console.Write("\nStämmer dessa uppgifter? j/n: ");
                    var answer = Console.ReadLine().ToLower();
                    if (answer == "j")
                    {
                        isAdding = false;
                        isAnswering = false;
                        break;
                    }

                    else if (string.IsNullOrEmpty(answer))
                        Console.WriteLine("Du måste svara.");

                    else
                        isAnswering = false;

                } while (isAnswering);
            }

            return motorcycle;
        }

        public static Vehicle CreateTruck()
        {
            var truck = new Truck();

            var isAdding = true;
            while (isAdding)
            {
                Console.Clear();

                truck.ModelName = ReadName("Mata in modellnamn: ");
                truck.LicensePlate = ReadLicensePlate("Mata in registreringsnummer: ");
                truck.RegistrationDate = DateTime.Now.ToString("yyyy/MM/dd");
                truck.MaxLoadInKG = ReadInteger("Mata in maxlast i kg: ");

                var lengthDriven = ReadDecimal("Mata in hur många mil fordonet har gått: ");

                PrintInfo(truck);
                var isAnswering = true;
                do
                {
                    Console.Write("\nStämmer dessa uppgifter? j/n: ");
                    var answer = Console.ReadLine().ToLower();
                    if (answer == "j")
                    {
                        isAdding = false;
                        isAnswering = false;
                        break;
                    }

                    else if (string.IsNullOrEmpty(answer))
                        Console.WriteLine("Du måste svara.");

                    else
                        isAnswering = false;

                } while (isAnswering);
            }

            return truck;
        }

        public static Vehicle CreateBus()
        {
            var bus = new Bus();

            var isAdding = true;
            while (isAdding)
            {
                Console.Clear();

                bus.ModelName = ReadName("Mata in modellnamn: ");
                bus.LicensePlate = ReadLicensePlate("Mata in registreringsnummer: ");
                bus.RegistrationDate = DateTime.Now.ToString("yyyy/MM/dd");
                bus.MaxAmountOfPassengers = ReadInteger("Mata in max antal passagerare: ");

                var lengthDriven = ReadDecimal("Mata in hur många mil fordonet har gått: ");

                PrintInfo(bus);
                var isAnswering = true;
                do
                {
                    Console.Write("\nStämmer dessa uppgifter? j/n: ");
                    var answer = Console.ReadLine().ToLower();
                    if (answer == "j")
                    {
                        isAdding = false;
                        isAnswering = false;
                        break;
                    }

                    else if (string.IsNullOrEmpty(answer))
                        Console.WriteLine("Du måste svara.");

                    else
                        isAnswering = false;

                } while (isAnswering);
            }

            return bus;

        }
        #endregion

        //public static Vehicle FindVehicleToRemove(BossesBilverkstad verkstad, string regNrToMatch)
        //{
        //    if (!verkstad.GetRegNrDictionary().ContainsKey(regNrToMatch))
        //    {
        //        var vehicle = verkstad.GetRegNrDictionary()[regNrToMatch];
        //        return vehicle;
        //    }

        //    return null;
        //}

        public static void PrintInfo(Vehicle vehicle)
        {
            Console.WriteLine("\t----------");
            Console.WriteLine($"\nNamn: {vehicle.ModelName}" +
                $"\nRegistreringsnummer: {vehicle.LicensePlate}" +
                $"\nRegisterades: {vehicle.RegistrationDate}");

            if (vehicle is Car)
            {
                var car = vehicle as Car;
                if (car.HasTowbar)
                    Console.WriteLine("Dragkrok: Ja");

                else
                    Console.WriteLine("Dragkrok: Nej");
            }

            else if (vehicle is Motorcycle)
            {
                var motorcycle = vehicle as Motorcycle;
                Console.WriteLine($"Maxhastighet: {motorcycle.MaxSpeed}");
            }

            else if (vehicle is Truck)
            {
                var truck = vehicle as Truck;
                Console.WriteLine($"Maxlast i kg: {truck.MaxLoadInKG}");
            }

            else
            {
                var bus = vehicle as Bus;
                Console.WriteLine($"Max antal passagerare: {bus.MaxAmountOfPassengers}");
            }

        }

        ///// <summary>
        ///// Söker efter ett fordon med hjälp av <b>registreringsnummer</b>.
        ///// </summary>
        ///// <param name="verkstad">Listan som sökningen sker i.</param>
        ///// <returns><b>Om matching lyckas:</b> kopia av fordon som ska tas bort.
        /////  <br><b>Om matching misslyckas:</b> null</br>
        ///// </returns>
        //public static Vehicle FindVehicleToRemove(IVerkstad verkstad)
        //{
        //    Console.WriteLine("Är du säker på att du vill ta bort ett fordon? j/n: ");
        //    var answer = Console.ReadLine().ToLower();

        //    Vehicle vehicleToRemove = null;

        //    if (answer == "j")
        //    {



        //        foreach (var vehicle in verkstad.GetListOfVehicles())
        //            PrintInfo(vehicle);

        //        Console.WriteLine("\nSkriv in registreringsnummer på det fordon du vill ta bort: ");
        //        string licensePlateToMatch = Console.ReadLine().ToUpper();
        //        licensePlateToMatch = licensePlateToMatch.Replace(" ", ""); // Tar bort whitespace i inmatningen.

        //        var hasFoundVehicle = false;

        //        foreach (var vehicle in verkstad.GetListOfVehicles())
        //        {
        //            if (licensePlateToMatch == vehicle.LicensePlate)
        //            {
        //                hasFoundVehicle = true;
        //                vehicleToRemove = vehicle;
        //                return vehicleToRemove;
        //            }
        //        }

        //        if (!hasFoundVehicle)
        //        {
        //            Console.WriteLine("Hittade inte ett fordon som matchade registreringsnumret.");
        //        }
        //    }

        //    return vehicleToRemove;
        //}




    }
}
