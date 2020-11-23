using GUI.Login;
using Logic.DAL;
using Logic.Database;
using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
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

namespace GUI.UserHome
{
    /// <summary>
    /// Interaction logic for UserHomePage.xaml
    /// </summary>
    public partial class UserHomePage : Page
    {
        private UserDataAccess<Mechanic> _dbMechanics;
        private UserDataAccess<Errand> _dbErrands;
        private UserDataAccess<Car> _dbCars;
        private UserDataAccess<Motorcycle> _dbMotorCycles;
        private UserDataAccess<Bus> _dbBuses;
        private UserDataAccess<Truck> _dbTrucks;


        private readonly MechanicService _mechanicService;
        private readonly VehicleService _vehicleService;
        private readonly ErrandService _errandService;
        public UserHomePage()
        {
            InitializeComponent();

            _mechanicService = new MechanicService();
            _vehicleService = new VehicleService();
            _errandService = new ErrandService();
            RefreshLists();



            lbMechanicCompetences.ItemsSource = LoggedInUserService.Mechanic.Competences;
            lbCompetences.ItemsSource = _mechanicService.GetRemainingCompetences(LoggedInUserService.Mechanic);

            lblHeader.Content = $"Välkommen {LoggedInUserService.Mechanic.FirstName}!";
            lblToday.Content = DateTime.Now.ToShortDateString();

        }

        private void RefreshLists()
        {
            _dbErrands = new UserDataAccess<Errand>();
            _dbMechanics = new UserDataAccess<Mechanic>();
            _dbCars = new UserDataAccess<Car>();
            _dbMotorCycles = new UserDataAccess<Motorcycle>();
            _dbBuses = new UserDataAccess<Bus>();
            _dbTrucks = new UserDataAccess<Truck>();

            db.Cars = _dbCars.LoadList();
            db.Motorcycles = _dbMotorCycles.LoadList();
            db.Buses = _dbBuses.LoadList();
            db.Trucks = _dbTrucks.LoadList();


            db.CurrentMechanics = _dbMechanics.LoadCurrentMechanics();
            db.Errands = _dbErrands.LoadList();
            LoggedInUserService.Mechanic = db.CurrentMechanics.FirstOrDefault(x => x.UserID == LoggedInUserService.User.ID);
            
            var errands = _errandService.GetMechanicErrands(LoggedInUserService.Mechanic);
            cbPågående.ItemsSource = errands.Where(x => x.ErrandStatus == ErrandStatus.Gul);
            cbKlara.ItemsSource = errands.Where(x => x.ErrandStatus == ErrandStatus.Grön);


        }


        private void btnLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if (lbCompetences.SelectedItem != null)
            {
                VehiclePart competence = (VehiclePart)lbCompetences.SelectedItem;
                _mechanicService.AddCompetence(LoggedInUserService.Mechanic, competence);

                lbMechanicCompetences.Items.Refresh();
                lbCompetences.ItemsSource = _mechanicService.GetRemainingCompetences(LoggedInUserService.Mechanic);
                _dbMechanics.SaveMechanicList(db.CurrentMechanics, "CurrentMechanics.json");
            }
        }
        private void btnRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (lbMechanicCompetences.SelectedItem != null)
            {
                VehiclePart competence = (VehiclePart)lbMechanicCompetences.SelectedItem;
                _mechanicService.RemoveCompetence(LoggedInUserService.Mechanic, competence);

                lbMechanicCompetences.Items.Refresh();

                lbCompetences.ItemsSource = _mechanicService.GetRemainingCompetences(LoggedInUserService.Mechanic);
                _dbMechanics.SaveMechanicList(db.CurrentMechanics, "CurrentMechanics.json");
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
                RefreshLists();
                ClearPage();
            }
        }
        private void btnAvbryt_Click(object sender, RoutedEventArgs e)
        {
            ClearPage();
        }

        private void ClearPage()
        {
            cbPågående.SelectedItem = null;
            cbPågående.Items.Refresh();
            cbKlara.SelectedItem = null;
            cbKlara.Items.Refresh();
            tbErrandVehicleType.Text = string.Empty;
            tbErrandRegistrationdate.Text = string.Empty;
            tbErrandProblem.Text = string.Empty;
            tbErrandOdometer.Text = string.Empty;
            tbErrandModelName.Text = string.Empty;
            tbErrandLicensePlate.Text = string.Empty;
            tbErrandFuelType.Text = string.Empty;
            tbErrandDescription.Text = string.Empty;

            tbErrandChangeable.Text = string.Empty;
            tbErrandChangeable.Visibility = Visibility.Hidden;
            lblErrandChangeable.Content = string.Empty;
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
            tbErrandModelName.Text = vehicle.ModelName;
            tbErrandLicensePlate.Text = vehicle.LicensePlate;
            tbErrandFuelType.Text = vehicle.FuelType.ToString();
            tbErrandRegistrationdate.Text = vehicle.RegistrationDate.ToShortDateString();
            tbErrandOdometer.Text = vehicle.Odometer.ToString();
            tbErrandDescription.Text = errand.Description;
            tbErrandProblem.Text = errand.Problem.ToString();
        }

        private void cbPågående_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPågående.SelectedItem != null)
            {
                cbKlara.SelectedItem = null;
                cbKlara.Items.Refresh();
                var errand = cbPågående.SelectedItem as Errand;
                GetErrandInfo(errand);

                lblErrandChangeable.Visibility = Visibility.Visible;
                tbErrandChangeable.Visibility = Visibility.Visible;
                btnÄrendeKlart.Visibility = Visibility.Visible;
                btnAvbryt.Visibility = Visibility.Visible;
            }
        }
        private void cbKlara_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbKlara.SelectedItem != null)
            {
                cbPågående.SelectedItem = null;
                cbPågående.Items.Refresh();

                var errand = cbKlara.SelectedItem as Errand;
                GetErrandInfo(errand);
            }
        }

        private void btnLoggaUt_Click(object sender, RoutedEventArgs e)
        {
            var loginPage = new LoginPage();
            this.NavigationService.Navigate(loginPage);
        }
    }
}
