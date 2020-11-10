using Logic.Database;
using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
using Logic.Helpers;
using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace GUI.Errands
{
    /// <summary>
    /// Interaction logic for ErrandsPage.xaml
    /// </summary>
    public partial class ErrandsPage : Page
    {
        private readonly List<CarType> _carTypes = new List<CarType>() { CarType.Sedan, CarType.Herrgårdsvagn, CarType.Cabriolet, CarType.Halvkombi };
        private readonly List<Fuel> _fuelType = new List<Fuel>() { Fuel.Bensin, Fuel.Diesel, Fuel.Elektrisk };
        private readonly List<VehiclePart> _vehicleParts = new List<VehiclePart>() { VehiclePart.Kaross, VehiclePart.Bromsar, VehiclePart.Motor, VehiclePart.Hjul };

        private readonly MechanicService _mechanicService = new MechanicService();
        private readonly VehicleService _vehicleService = new VehicleService();
        private readonly ErrandService _errandService = new ErrandService();

        // Problem med serialisering av abstrakt klass (vehicles är en abstrakt klass...) 
        //private readonly List<Vehicle> _vehicles = JsonHelper.ReadFile<Vehicle>(@"DAL\Files\Vehicles.json");

        // Den här funkar, men den stör i funktionalitet när ovanstående problem kvarstår
        //private readonly List<Errand> _errands = JsonHelper.ReadFile<Errand>(@"DAL\Files\Errands.json");


        public ErrandsPage()
        {
            InitializeComponent();

            // DUMMY
            #region Dummyvärden
            var dummyMechanic = new Mechanic() { FirstName = "Lars", LastName = "Andersson", IsAvailable = true};
            dummyMechanic.Competences.Add(VehiclePart.Bromsar);
            db.CurrentMechanics.Add(dummyMechanic);

            var dummyVehicle = new Car()
            {
                ModelName = "Volvo",
                LicensePlate = "ABC123",
                CarType = CarType.Herrgårdsvagn,
                HasTowbar = true,
                FuelType = Fuel.Bensin,
                Odometer = 1233.4,
                RegistrationDate = DateTime.Now.ToString()
            };
            db.Vehicles.Add(dummyVehicle);

            var dummyErrand = new Errand()
            {
                Description = "Testärende: Fel på bromsar",
                Problem = VehiclePart.Bromsar,
                VehicleID = dummyVehicle.ID
            };
            db.Errands.Add(dummyErrand);

            #endregion
            cbFuelType.ItemsSource = _fuelType;
            cbProblem.ItemsSource = _vehicleParts;
            cbCarType.ItemsSource = _carTypes;
            // Här bör vi byta till _errands när all json fungerar som den ska.
            cbLiggande.ItemsSource = db.Errands.Where(errand => errand.ErrandStatus == ErrandStatus.Röd);
        }

        private void BtnCreateErrand_Click(object sender, RoutedEventArgs e)
        {
            if (cbFuelType.SelectedItem != null && cbProblem.SelectedItem != null)
            {
                var vehicleID = CreateVehicle();
                if (vehicleID == null)
                {
                    MessageBox.Show("Inkorrekt inmatning av uppgifter");
                }
                else
                {
                    var errandID = CreateErrand(vehicleID);
                    if (errandID == null)
                    {
                        MessageBox.Show("Inkorrekt inmatning av uppgifter");
                    }
                    else
                    {
                        if (cbAvailableMechanics.SelectedItem != null)
                        {
                            var mech = cbAvailableMechanics.SelectedItem as Mechanic;
                            _mechanicService.AddErrand(mech.ID, errandID); // I denna metoden händer lite för många hämtningar haha
                        }
                    }
                }
                MessageBox.Show("Ärende skapat.");
                UpdateErrandPage();
            }
        }





        private string CreateErrand(string vehicleID)
        {
            if (!string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                var description = tbDescription.Text;
                var problem = (VehiclePart)cbProblem.SelectedItem;
                return _errandService.CreateAndWriteErrand(description, problem, vehicleID);
            }
            return default;
        }

        /// <summary>
        /// Creates a vehicle if the needed textboxes has values in them
        /// <br>Returns the created vehicle's ID as <b>string</b></br>
        /// </summary>
        /// <returns></returns>
        private string CreateVehicle()
        {
            if (!string.IsNullOrWhiteSpace(tbModelName.Text)
                && !string.IsNullOrWhiteSpace(tbLicensePlate.Text)
                && !string.IsNullOrWhiteSpace(tbRegistrationDate.Text))
            {
                string modelName = tbModelName.Text;
                string licenseNumber = tbLicensePlate.Text;
                string registrationDate = tbRegistrationDate.Text;
                Fuel fuelType = (Fuel)cbFuelType.SelectedItem;
                double.TryParse(tbLengthDriven.Text, out double lengthDriven);

                if (rbCar.IsChecked == true && cbCarType.SelectedItem != null)
                {

                    CarType carType = (CarType)cbCarType.SelectedItem;

                    var hasTowbar = false;
                    if (rbYes.IsChecked == true)
                        hasTowbar = true;

                    else if (rbNo.IsChecked == true)
                        hasTowbar = false;

                    return _vehicleService.CreateAndWriteCar(modelName, licenseNumber, registrationDate, fuelType, hasTowbar, carType, lengthDriven);
                }

                else if (rbMotorcycle.IsChecked == true && int.TryParse(tbChangeable.Text, out int maxSpeed))
                {
                    return _vehicleService.CreateAndWriteMotorcycle(modelName, licenseNumber, registrationDate, fuelType, maxSpeed, lengthDriven);
                }


                else if (rbBus.IsChecked == true && int.TryParse(tbChangeable.Text, out int maxAmountofPassengers))
                {
                    return _vehicleService.CreateAndWriteBus(modelName, licenseNumber, registrationDate, fuelType, maxAmountofPassengers, lengthDriven);
                }

                else if (rbTruck.IsChecked == true && int.TryParse(tbChangeable.Text, out int maxLoad))
                {
                    return _vehicleService.CreateAndWriteTruck(modelName, licenseNumber, registrationDate, fuelType, maxLoad, lengthDriven);
                }
            }

            return default;
        }

        private void UpdateErrandPage()
        {
            tbModelName.Text = string.Empty;
            tbLicensePlate.Text = string.Empty;
            tbRegistrationDate.Text = string.Empty;
            tbLengthDriven.Text = string.Empty;
            tbChangeable.Text = string.Empty;

            tbDescription.Text = string.Empty;

            cbFuelType.SelectedItem = null;
            cbFuelType.Items.Refresh();

            cbCarType.SelectedItem = null;
            cbCarType.Items.Refresh();

            cbProblem.SelectedItem = null;
            cbProblem.Items.Refresh();

            cbAvailableMechanics.SelectedItem = null;
            cbAvailableMechanics.Items.Refresh();

            cbLiggande.ItemsSource = db.Errands.Where(errand => errand.ErrandStatus == ErrandStatus.Röd);
            cbLiggande.Items.Refresh();
        }

        private void ShowExtraVehiclePropertyField()
        {
            tbChangeable.Text = "";
            tbChangeable.Visibility = Visibility.Visible;
        }

        private void ChangeExtraVehicleProperty(string text)
        {
            lblChangeable.Content = text;
            lblChangeable.Visibility = Visibility.Visible;
            tbChangeable.Visibility = Visibility.Hidden;
        }

        private void HideCarSettings()
        {
            rbYes.Visibility = Visibility.Hidden;
            rbNo.Visibility = Visibility.Hidden;
            lblTypeOfCar.Visibility = Visibility.Hidden;
            cbCarType.Visibility = Visibility.Hidden;
        }

        private void ShowRadioButtons()
        {
            rbYes.Visibility = Visibility.Visible;
            rbNo.Visibility = Visibility.Visible;
        }

        // Uppdaterar listan med lediga mekaniker beroende på vad för problem det är på fordonet
        private void cbProblem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbProblem.SelectedItem != null)
            {
                var problem = (VehiclePart)cbProblem.SelectedItem;
                cbAvailableMechanics.ItemsSource = db.CurrentMechanics.Where(mech => mech.IsAvailable == true && mech.Competences.Contains(problem));
            }
        }

        private void rbCar_Checked(object sender, RoutedEventArgs e)
        {
            ChangeExtraVehicleProperty("Dragkrok:");
            lblTypeOfCar.Visibility = Visibility.Visible;
            cbCarType.Visibility = Visibility.Visible;
            tbChangeable.Visibility = Visibility.Hidden;
            ShowRadioButtons();
        }
        private void rbMotorcycle_Checked(object sender, RoutedEventArgs e)
        {
            HideCarSettings();
            ChangeExtraVehicleProperty("Maxhastighet i km/h");
            ShowExtraVehiclePropertyField();
        }
        private void rbBus_Checked(object sender, RoutedEventArgs e)
        {
            HideCarSettings();
            ChangeExtraVehicleProperty("Max antal passagerare:");
            ShowExtraVehiclePropertyField();
        }
        private void rbTruck_Checked(object sender, RoutedEventArgs e)
        {
            HideCarSettings();
            ChangeExtraVehicleProperty("Maxlast i KG:");
            ShowExtraVehiclePropertyField();
        }

        private void cbLiggande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLiggande.SelectedItem != null)
            {
                var errand = cbLiggande.SelectedItem as Errand;
                // Gör en metod härifrån
                GetErrandInfo(errand);
                cbAvailableMechanics.ItemsSource = db.CurrentMechanics.Where(mech => mech.IsAvailable == true && mech.Competences.Contains(errand.Problem));
            }
        }

        private void GetErrandInfo(Errand errand)
        {
            // Här bör vi använda listan som vi hämtat från json-filen
            var vehicle = db.Vehicles.FirstOrDefault(vehicle => errand.VehicleID == vehicle.ID);

            if (vehicle != default)
            {
                lblTilldelaMekaniker.Visibility = Visibility.Visible;
                cbErrandAvailableMechanics.Visibility = Visibility.Visible;
                lblErrandTypeOfCar.Visibility = Visibility.Visible;

                tbErrandModelName.Text = vehicle.ModelName;
                tbErrandLicensePlate.Text = vehicle.LicensePlate;
                tbErrandFuelType.Text = vehicle.FuelType.ToString();
                tbErrandRegistrationdate.Text = vehicle.RegistrationDate;
                tbErrandOdometer.Text = vehicle.Odometer.ToString();
                tbErrandDescription.Text = errand.Description;
                tbErrandProblem.Text = errand.Problem.ToString();

                if (vehicle is Car)
                {
                    var car = vehicle as Car;
                        
                    tbErrandVehicleType.Text = "Bil";
                    if (car.HasTowbar)
                        tbErrandChangeable.Text = "Ja";
                    else
                        tbErrandChangeable.Text = "Nej";

                    tbErrandChangeable.Visibility = Visibility.Visible;
                    lblErrandChangeable.Content = "Dragkrok: ";
                    lblErrandChangeable.Visibility = Visibility.Visible;
                    tbErrandTypeOfCar.Text = car.CarType.ToString();
                    tbErrandTypeOfCar.Visibility = Visibility.Visible;

                }

                else if (vehicle is Motorcycle)
                {
                    vehicle = vehicle as Motorcycle;

                }

                else if (vehicle is Bus)
                {
                    vehicle = vehicle as Bus;

                }

                else if (vehicle is Truck)
                {
                    vehicle = vehicle as Truck;

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Förändringar på ärendet sparades.");
        }
    }
}
