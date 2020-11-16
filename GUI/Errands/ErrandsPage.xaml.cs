using GUI.Home;
using Logic.DAL;
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
        private readonly List<VehiclePart> _vehicleParts = new List<VehiclePart>() { VehiclePart.Kaross, VehiclePart.Bromsar, VehiclePart.Motor, VehiclePart.Hjul, VehiclePart.Vindruta };

        private readonly MechanicService _mechanicService;
        private readonly VehicleService _vehicleService;
        private readonly ErrandService _errandService;

        private readonly UserDataAccess<Errand> _dbErrands;
        private readonly UserDataAccess<Car> _dbCars;
        private readonly UserDataAccess<Motorcycle> _dbMotorCycles;
        private readonly UserDataAccess<Bus> _dbBuses;
        private readonly UserDataAccess<Truck> _dbTrucks;
        private readonly UserDataAccess<Mechanic> _dbCurrentMechanics;

        //private readonly UserDataAccess<Errand> _dbErrands;

        public ErrandsPage()
        {
            InitializeComponent();

            _dbErrands = new UserDataAccess<Errand>();
            _dbCars = new UserDataAccess<Car>();
            _dbMotorCycles = new UserDataAccess<Motorcycle>();
            _dbBuses = new UserDataAccess<Bus>();
            _dbTrucks = new UserDataAccess<Truck>();
            _dbCurrentMechanics = new UserDataAccess<Mechanic>();

            _mechanicService = new MechanicService();
            _vehicleService = new VehicleService();
            _vehicleService = new VehicleService();
            _errandService = new ErrandService();

            cbFuelType.ItemsSource = _fuelType;
            cbProblem.ItemsSource = _vehicleParts;
            cbCarType.ItemsSource = _carTypes;

            UpdateErrandPage();
        }

        private void RefreshLists()
        {
            db.Errands = _dbErrands.LoadList();
            db.CurrentMechanics = _dbCurrentMechanics.LoadCurrentMechanics();

            db.Cars = _dbCars.LoadList();
            db.Motorcycles = _dbMotorCycles.LoadList();
            db.Buses = _dbBuses.LoadList();
            db.Trucks = _dbTrucks.LoadList();
        }
        private string CreateErrand(string vehicleID)
        {
            if (!string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                var description = tbDescription.Text;
                var problem = (VehiclePart)cbProblem.SelectedItem;
                return _errandService.CreateAndSaveErrand(description, problem, vehicleID);
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
                && dpRegistrationDate.SelectedDate != null)
            {
                string modelName = tbModelName.Text;
                string licenseNumber = tbLicensePlate.Text;
                DateTime registrationDate = (DateTime)dpRegistrationDate.SelectedDate;
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

                else if (rbMotorcycle.IsChecked == true && int.TryParse(tbChangeable.Text.Replace(" ", ""), out int maxSpeed))
                {
                    return _vehicleService.CreateAndWriteMotorcycle(modelName, licenseNumber, registrationDate, fuelType, maxSpeed, lengthDriven);
                }


                else if (rbBus.IsChecked == true && int.TryParse(tbChangeable.Text.Replace(" ", ""), out int maxAmountofPassengers))
                {
                    return _vehicleService.CreateAndWriteBus(modelName, licenseNumber, registrationDate, fuelType, maxAmountofPassengers, lengthDriven);
                }

                else if (rbTruck.IsChecked == true && int.TryParse(tbChangeable.Text.Replace(" ", ""), out int maxLoad))
                {
                    return _vehicleService.CreateAndWriteTruck(modelName, licenseNumber, registrationDate, fuelType, maxLoad, lengthDriven);
                }
            }

            return default;
        }

        // Uppdaterar listan med lediga mekaniker beroende på vad för problem det är på fordonet
        private void cbProblem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbProblem.SelectedItem != null)
            {
                var problem = (VehiclePart)cbProblem.SelectedItem;
                cbAvailableMechanics.ItemsSource = _mechanicService.GetAvailableMechanic(problem);
            }
        }
        private void cbLiggande_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLiggande.SelectedItem != null)
            {
                cbPågående.SelectedItem = null;
                cbPågående.Items.Refresh();
                cbKlara.SelectedItem = null;
                cbKlara.Items.Refresh();

                var errand = cbLiggande.SelectedItem as Errand;
                GetErrandInfo(errand);

                cbErrandAvailableMechanics.ItemsSource = _mechanicService.GetAvailableMechanic(errand.Problem);

                cbErrandAvailableMechanics.Visibility = Visibility.Visible;
                btnSaveChanges.Visibility = Visibility.Visible;

                lblErrandTilldeladMekaniker.Content = "Tilldela mekaniker:";
            }
        }
        private void cbPågående_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPågående.SelectedItem != null)
            {

                cbLiggande.SelectedItem = null;
                cbLiggande.Items.Refresh();
                cbKlara.SelectedItem = null;
                cbKlara.Items.Refresh();
                var errand = cbPågående.SelectedItem as Errand;
                var mechanic = _mechanicService.GetMechanicFromErrand(errand.ID);
                GetErrandInfo(errand);

                btnÄrendeKlart.Visibility = Visibility.Visible;
                btnAvbryt.Visibility = Visibility.Visible;
                lblErrandTilldeladMekaniker.Content = "Tilldelad mekaniker:";
                tbErrandWorkingMechanic.Text = $"{mechanic.FirstName} {mechanic.LastName}";
                tbErrandWorkingMechanic.Visibility = Visibility.Visible;
            }
        }
        private void cbKlara_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbKlara.SelectedItem != null)
            {
                cbPågående.SelectedItem = null;
                cbPågående.Items.Refresh();
                cbLiggande.SelectedItem = null;
                cbLiggande.Items.Refresh();

                var errand = cbKlara.SelectedItem as Errand;
                GetErrandInfo(errand);
                lblErrandTilldeladMekaniker.Content = "Tilldelad mekaniker:";

                Mechanic mechanic = _mechanicService.GetMechanicWhoFinishedErrand(errand);

                // Behöver eventuellt fånga upp om mekanikern blir null
                if (mechanic != null)
                {
                    tbErrandWorkingMechanic.Text = $"{mechanic.FirstName} {mechanic.LastName}";
                }
                tbErrandWorkingMechanic.Visibility = Visibility.Visible;

            }
        }

        private void rbCar_Checked(object sender, RoutedEventArgs e)
        {
            ClearErrandComboboxes();
            UpdateErrandPage();
            ChangeExtraVehicleProperty("Dragkrok:");
            lblTypeOfCar.Visibility = Visibility.Visible;
            cbCarType.Visibility = Visibility.Visible;
            tbChangeable.Visibility = Visibility.Hidden;
            ShowRadioButtons();
        }
        private void rbMotorcycle_Checked(object sender, RoutedEventArgs e)
        {
            ClearErrandComboboxes();
            UpdateErrandPage();
            HideCarSettings();
            ChangeExtraVehicleProperty("Maxhastighet i km/h");
            ShowExtraVehiclePropertyField();
        }
        private void rbBus_Checked(object sender, RoutedEventArgs e)
        {
            ClearErrandComboboxes();
            UpdateErrandPage();
            HideCarSettings();
            ChangeExtraVehicleProperty("Max antal passagerare:");
            ShowExtraVehiclePropertyField();
        }
        private void rbTruck_Checked(object sender, RoutedEventArgs e)
        {
            ClearErrandComboboxes();
            UpdateErrandPage();
            HideCarSettings();
            ChangeExtraVehicleProperty("Maxlast i KG:");
            ShowExtraVehiclePropertyField();
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
                            _mechanicService.AddCurrentErrand(mech.ID, errandID); // I denna metoden händer eventuellt lite för många hämtningar?
                            _errandService.SetMechanicIdToErrand(errandID, mech.ID);
                            MessageBox.Show("Ärende skapat.");
                        }
                    }
                }
                UpdateErrandPage();
            }
        }
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (cbLiggande.SelectedItem != null)
            {
                if (cbErrandAvailableMechanics.SelectedItem != null)
                {
                    var errand = cbLiggande.SelectedItem as Errand;
                    var mechanic = cbErrandAvailableMechanics.SelectedItem as Mechanic;

                    _mechanicService.AddCurrentErrand(mechanic.ID, errand.ID);
                    _errandService.SetMechanicIdToErrand(errand.ID, mechanic.ID);

                    MessageBox.Show("Förändringar på ärendet sparades.\nÄrendestatus: Pågående", "Ärende sparat");
                    UpdateErrandPage();
                }

            }
        }
        private void btnÄrendeKlart_Click(object sender, RoutedEventArgs e)
        {
            if (cbPågående.SelectedItem != null)
            {
                var errand = cbPågående.SelectedItem as Errand;
                var mechanic = _mechanicService.GetMechanicFromErrand(errand.ID);

                _mechanicService.RemoveCurrentErrand(mechanic, errand);

                MessageBox.Show("Ärende avslutat.");
                UpdateErrandPage();
            }
        }
        private void btnAvbryt_Click(object sender, RoutedEventArgs e)
        {
            cbPågående.SelectedItem = null;
            cbPågående.Items.Refresh();

            UpdateErrandPage();
        }


        private void GetErrandInfo(Errand errand)
        {
            var vehicle = _vehicleService.GetVehicleFromErrand(errand);

            if (vehicle != null)
            {
                GetAndSetVehicleInfo(errand, vehicle);

                if (vehicle is Car)
                {
                    var car = vehicle as Car;

                    if (car.HasTowbar)
                        tbErrandChangeable.Text = "Ja";
                    else
                        tbErrandChangeable.Text = "Nej";

                    tbErrandVehicleType.Text = "Bil";
                    tbErrandChangeable.Visibility = Visibility.Visible;
                    lblErrandChangeable.Content = "Dragkrok:";
                    lblErrandChangeable.Visibility = Visibility.Visible;
                    lblErrandTypeOfCar.Visibility = Visibility.Visible;
                    tbErrandTypeOfCar.Text = car.CarType.ToString();
                    tbErrandTypeOfCar.Visibility = Visibility.Visible;

                }

                else if (vehicle is Motorcycle)
                {
                    var motorcycle = vehicle as Motorcycle;
                    tbErrandVehicleType.Text = "Motorcykel";
                    tbErrandChangeable.Text = motorcycle.MaxSpeed.ToString();
                    tbErrandChangeable.Visibility = Visibility.Visible;
                    lblErrandChangeable.Content = "Maxhastighet km/h:";
                    lblErrandChangeable.Visibility = Visibility.Visible;
                }

                else if (vehicle is Bus)
                {
                    var bus = vehicle as Bus;
                    tbErrandVehicleType.Text = "Buss";
                    tbErrandChangeable.Text = bus.MaxAmountOfPassengers.ToString();
                    tbErrandChangeable.Visibility = Visibility.Visible;
                    lblErrandChangeable.Content = "Max antal passagerare:";
                    lblErrandChangeable.Visibility = Visibility.Visible;
                }

                else if (vehicle is Truck)
                {
                    var truck = vehicle as Truck;
                    tbErrandVehicleType.Text = "Lastbil";
                    tbErrandChangeable.Text = truck.MaxLoadInKG.ToString();
                    tbErrandChangeable.Visibility = Visibility.Visible;
                    lblErrandChangeable.Content = "Maxlast kg:";
                    lblErrandChangeable.Visibility = Visibility.Visible;
                }
            }
        }
        private void GetAndSetVehicleInfo(Errand errand, Vehicle vehicle)
        {
            lblErrandTilldeladMekaniker.Visibility = Visibility.Visible;
            tbErrandModelName.Text = vehicle.ModelName;
            tbErrandLicensePlate.Text = vehicle.LicensePlate;
            tbErrandFuelType.Text = vehicle.FuelType.ToString();
            tbErrandRegistrationdate.Text = vehicle.RegistrationDate.ToShortDateString();
            tbErrandOdometer.Text = vehicle.Odometer.ToString();
            tbErrandDescription.Text = errand.Description;
            tbErrandProblem.Text = errand.Problem.ToString();
        }

        private void ClearErrandComboboxes()
        {
            cbLiggande.SelectedItem = null;
            cbPågående.SelectedItem = null;
            cbKlara.SelectedItem = null;
        }
        private void UpdateErrandPage()
        {
            RefreshLists();

            #region Uppdaterar "Skapa ärende"
            tbModelName.Text = string.Empty;
            tbLicensePlate.Text = string.Empty;
            dpRegistrationDate.SelectedDate = null;
            tbLengthDriven.Text = string.Empty;
            tbChangeable.Text = string.Empty;

            tbDescription.Text = string.Empty;

            cbFuelType.SelectedItem = null;
            cbFuelType.Items.Refresh();

            cbCarType.SelectedItem = null;
            cbCarType.Items.Refresh();

            cbProblem.SelectedItem = null;
            cbProblem.Items.Refresh();

            cbAvailableMechanics.Visibility = Visibility.Visible;
            cbAvailableMechanics.SelectedItem = null;
            cbAvailableMechanics.Items.Refresh();

            #endregion

            #region Uppdaterar "Ändra ärende"
            cbLiggande.ItemsSource = db.Errands.Where(errand => errand.ErrandStatus == ErrandStatus.Röd);
            cbPågående.ItemsSource = db.Errands.Where(errand => errand.ErrandStatus == ErrandStatus.Gul);
            cbKlara.ItemsSource = db.Errands.Where(errand => errand.ErrandStatus == ErrandStatus.Grön);


            cbErrandAvailableMechanics.ItemsSource = null;
            cbErrandAvailableMechanics.Items.Refresh();
            cbErrandAvailableMechanics.Visibility = Visibility.Hidden;

            lblErrandChangeable.Visibility = Visibility.Hidden;
            lblErrandTypeOfCar.Visibility = Visibility.Hidden;

            tbErrandChangeable.Text = string.Empty;
            tbErrandChangeable.Visibility = Visibility.Hidden;
            tbErrandDescription.Text = string.Empty;
            tbErrandFuelType.Text = string.Empty;
            tbErrandLicensePlate.Text = string.Empty;
            tbErrandModelName.Text = string.Empty;
            tbErrandOdometer.Text = string.Empty;
            tbErrandProblem.Text = string.Empty;
            tbErrandRegistrationdate.Text = string.Empty;
            tbErrandTypeOfCar.Text = string.Empty;
            tbErrandTypeOfCar.Visibility = Visibility.Hidden;
            tbErrandVehicleType.Text = string.Empty;

            btnSaveChanges.Visibility = Visibility.Hidden;
            lblErrandTilldeladMekaniker.Visibility = Visibility.Hidden;
            tbErrandWorkingMechanic.Text = string.Empty;
            tbErrandWorkingMechanic.Visibility = Visibility.Hidden;

            btnÄrendeKlart.Visibility = Visibility.Hidden;
            btnAvbryt.Visibility = Visibility.Hidden;
            #endregion


        }
        private void ShowExtraVehiclePropertyField()
        {
            tbChangeable.Text = "";
            tbChangeable.Visibility = Visibility.Visible;
        }
        private void ShowRadioButtons()
        {
            rbYes.Visibility = Visibility.Visible;
            rbNo.Visibility = Visibility.Visible;
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

        private void btnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            var homePage = new HomePage();
            this.NavigationService.Navigate(homePage);
        }
    }
}
