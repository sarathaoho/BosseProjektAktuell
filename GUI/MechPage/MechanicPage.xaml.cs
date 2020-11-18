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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace GUI.MechPage
{
    /// <summary>
    /// Interaction logic for MechanicPage.xaml
    /// </summary>
    public partial class MechanicPage : Page
    {
        private readonly MechanicService _mechanicService;
        private readonly UserService _userService;
        private readonly ErrandService _errandService;
        private readonly VehicleService _vehicleService;

        private readonly List<VehiclePart> _mechanicCompetences;
        private readonly List<VehiclePart> _competences;

        private readonly UserDataAccess<Mechanic> _dbMechanics;
        private readonly UserDataAccess<Errand> _dbErrands;

        private readonly UserDataAccess<Car> _dbCars;
        private readonly UserDataAccess<Motorcycle> _dbMotorCycles;
        private readonly UserDataAccess<Bus> _dbBuses;
        private readonly UserDataAccess<Truck> _dbTrucks;


        public MechanicPage()
        {
            InitializeComponent();

            _mechanicService = new MechanicService();
            _userService = new UserService();
            _errandService = new ErrandService();
            _vehicleService = new VehicleService();

            _dbCars = new UserDataAccess<Car>();
            _dbMotorCycles = new UserDataAccess<Motorcycle>();
            _dbBuses = new UserDataAccess<Bus>();
            _dbTrucks = new UserDataAccess<Truck>();

            _mechanicCompetences = new List<VehiclePart>();
            _competences = new List<VehiclePart>();
            _dbMechanics = new UserDataAccess<Mechanic>();
            _dbErrands = new UserDataAccess<Errand>();

            UpdateAddMechanicPage();
            lbMechanicCompetences.ItemsSource = _mechanicCompetences;
            lbCompetences.ItemsSource = _competences;
        }

        #region Skapa mekaniker

        private void btnAddMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMechanicFirstName.Text)
                || string.IsNullOrWhiteSpace(tbMechanicLastName.Text)
                || dpMechanicDateOfBirth.SelectedDate == null
                || dpMechanicDateOfEmployment.SelectedDate == null)
            //|| string.IsNullOrWhiteSpace(tbDateOfBirth.Text)
            //|| string.IsNullOrWhiteSpace(tbDateOfEmployment.Text))
            {
                System.Windows.MessageBox.Show("Felaktig inmatning");
            }

            else
            {
                DateTime dateOfBirth = (DateTime)dpMechanicDateOfBirth.SelectedDate;
                DateTime dateOfEmployment = (DateTime)dpMechanicDateOfEmployment.SelectedDate;

                string firstName = tbMechanicFirstName.Text;
                string lastName = tbMechanicLastName.Text;
                //string dateOfBirth = tbDateOfBirth.Text;
                //string dateOfEmployment = tbDateOfEmployment.Text;


                _mechanicService.CreateAndSaveMechanic(firstName, lastName, dateOfBirth, dateOfEmployment, _mechanicCompetences);


                System.Windows.MessageBox.Show("Mekaniker skapad");
                UpdateAddMechanicPage();
            }
        }
        private void btnLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if (lbCompetences.SelectedItem != null)
            {
                VehiclePart competence = (VehiclePart)lbCompetences.SelectedItem;


                _mechanicCompetences.Add(competence);
                _competences.Remove(competence);

                lbMechanicCompetences.Items.Refresh();
                lbCompetences.Items.Refresh();
            }

        }
        private void btnRightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (lbMechanicCompetences.SelectedItem != null)
            {
                VehiclePart competence = (VehiclePart)lbMechanicCompetences.SelectedItem;

                _mechanicCompetences.Remove(competence);
                _competences.Add(competence);

                lbMechanicCompetences.Items.Refresh();
                lbCompetences.Items.Refresh();
            }
        }

        #endregion

        #region Ändra/lista mekaniker

        private void cbMechanics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMechanics.SelectedItem is Mechanic mechanic)
            {
                ClearErrandInfo();
                UpdateOldMechanic();
                GetMechanicInfo(mechanic);
            }
        }
        private void cbOldMechanics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbOldMechanics.SelectedItem is Mechanic oldMechanic)
            {
                ClearErrandInfo();
                UpdateCurrentMechanic();
                ViewOldMechanic(oldMechanic);
            }
        }

        private void UpdateCurrentMechanic()
        {
            cbMechanics.SelectedItem = null;
            tbFirstNameToChange.Text = string.Empty;
            tbFirstNameToChange.Watermark = string.Empty;
            tbLastNameToChange.Text = string.Empty;
            tbLastNameToChange.Watermark = string.Empty;
            dpDateOfBirthToChange.SelectedDate = null;
            dpDateOfEmploymentToChange.SelectedDate = null;
            tbUserID.Text = string.Empty;

            lbMechanicCompetences2.ItemsSource = null;
            lbCompetences2.ItemsSource = null;
        }

        private void GetMechanicInfo(Mechanic mechanic)
        {
            tbFirstNameToChange.Watermark = mechanic.FirstName;
            tbLastNameToChange.Watermark = mechanic.LastName;
            dpDateOfBirthToChange.SelectedDate = mechanic.DateOfBirth;
            dpDateOfEmploymentToChange.SelectedDate = mechanic.DateOfEmployment;
            lbMechanicCompetences2.ItemsSource = mechanic.Competences;
            lbCompetences2.ItemsSource = _mechanicService.GetRemainingCompetences(mechanic);

            var user = _userService.GetAssignedUserFromMechanic(mechanic);
            tbUserID.Text = user != null ? user.Username : "Ingen användare";

            var errands = _errandService.GetMechanicErrands(mechanic);
            cbCurrentErrands.ItemsSource = errands.Where(x => x.ErrandStatus == ErrandStatus.Gul);
            cbFinishedErrands.ItemsSource = errands.Where(x => x.ErrandStatus == ErrandStatus.Grön);
        }
        private void ViewOldMechanic(Mechanic oldMechanic)
        {
            lbOldMechanicCompetences.ItemsSource = oldMechanic.Competences;
            tbOldMechanicFirstname.Text = oldMechanic.FirstName;
            tbOldMechanicLastname.Text = oldMechanic.LastName;
            tbOldMechanicDateOfEmployment.Text = oldMechanic.DateOfEmployment.ToShortDateString();
            tbOldMechanicLastDate.Text = oldMechanic.LastDate.ToShortDateString();
            tbOldMechanicDateOfBirth.Text = oldMechanic.DateOfBirth.ToShortDateString();

            var errands = _errandService.GetMechanicErrands(oldMechanic);
            cbCurrentErrands.ItemsSource = errands.Where(x => x.ErrandStatus == ErrandStatus.Gul);
            cbFinishedErrands.ItemsSource = errands.Where(x => x.ErrandStatus == ErrandStatus.Grön);

        }

        // Lägg till kompetenser hos mekaniker som redan existerar
        private void btnLeftArrow2_Click(object sender, RoutedEventArgs e)
        {

            if (lbCompetences2.SelectedItem != null)
            {
                var mechanic = cbMechanics.SelectedItem as Mechanic;

                VehiclePart competence = (VehiclePart)lbCompetences2.SelectedItem;
                _mechanicService.AddCompetence(mechanic, competence);

                lbMechanicCompetences2.Items.Refresh();
                lbCompetences2.ItemsSource = _mechanicService.GetRemainingCompetences(mechanic);
                _dbMechanics.SaveMechanicList(db.CurrentMechanics, "CurrentMechanics.json");
            }
        }
        private void btnRightArrow2_Click(object sender, RoutedEventArgs e)
        {

            if (lbMechanicCompetences2.SelectedItem != null)
            {
                var mechanic = cbMechanics.SelectedItem as Mechanic;

                VehiclePart competence = (VehiclePart)lbMechanicCompetences2.SelectedItem;
                _mechanicService.RemoveCompetence(mechanic, competence);

                lbMechanicCompetences2.Items.Refresh();

                lbCompetences2.ItemsSource = _mechanicService.GetRemainingCompetences(mechanic);
                _dbMechanics.SaveMechanicList(db.CurrentMechanics, "CurrentMechanics.json");
            }

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (!(cbMechanics.SelectedItem is Mechanic mechanic))
                System.Windows.MessageBox.Show("Du måste välja en mekaniker", "Ingen mekaniker vald");

            else
            {
                bool mechanicHasErrand = mechanic.CurrentErrands.Count > 0;

                MessageBoxResult result;

                if (mechanicHasErrand)
                {
                    result = System.Windows.MessageBox.Show($"Mekanikern {mechanic.FirstName} {mechanic.LastName} har minst ett ärende pågående just nu. " +
                        $"Vill du fortfarande ta bort mekanikern?",
                    "Ta bort mekaniker", MessageBoxButton.YesNo);
                }
                else
                {
                    result = System.Windows.MessageBox.Show($"Är du säker på att du vill ta bort {mechanic.FirstName} {mechanic.LastName}?",
                        "Ta bort mekaniker", MessageBoxButton.YesNo);
                }

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        _mechanicService.RemoveMechanic(mechanic);
                        _mechanicService.RemoveMechanicFromErrand(mechanic);

                        cbMechanics.Items.Refresh();
                        System.Windows.MessageBox.Show($"Tog bort {mechanic.FirstName} {mechanic.LastName}");
                        ClearErrandInfo();
                        UpdateEditPage();
                        RemoveErrandInfo();
                        break;

                    case MessageBoxResult.No:
                        ClearErrandInfo();
                        UpdateEditPage();
                        RemoveErrandInfo();
                        break;
                }
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var isSomethingChanged = EditMechanic();
            if (isSomethingChanged) System.Windows.MessageBox.Show($"Ändringar sparade");
            UpdateEditPage();
            RemoveErrandInfo();
        }


        #endregion

        private void UpdateOldMechanic()
        {
            cbOldMechanics.SelectedItem = null;
            lbOldMechanicCompetences.ItemsSource = null;
            tbOldMechanicFirstname.Text = string.Empty;
            tbOldMechanicLastname.Text = string.Empty;
            tbOldMechanicDateOfBirth.Text = string.Empty;
            tbOldMechanicDateOfEmployment.Text = string.Empty;
            tbOldMechanicLastDate.Text = string.Empty;
        }
        private void UpdateEditPage()
        {
            RefreshLists();
            cbMechanics.SelectedItem = null;
            cbMechanics.Items.Refresh();

            lbMechanicCompetences2.ItemsSource = null;
            lbCompetences2.ItemsSource = null;

            tbFirstNameToChange.Watermark = string.Empty;
            tbFirstNameToChange.Text = string.Empty;
            tbLastNameToChange.Watermark = string.Empty;
            tbLastNameToChange.Text = string.Empty;
            tbUserID.Text = string.Empty;

            dpDateOfBirthToChange.SelectedDate = null;
            dpDateOfEmploymentToChange.SelectedDate = null;
        }
        private void UpdateAddMechanicPage()
        {
            RefreshLists();
            ClearErrandInfo();

            cbMechanics.Items.Refresh();
            cbCurrentErrands.Items.Refresh();
            cbFinishedErrands.Items.Refresh();

            tbMechanicFirstName.Text = string.Empty;
            tbMechanicLastName.Text = string.Empty;
            dpMechanicDateOfBirth.SelectedDate = null;
            dpMechanicDateOfEmployment.SelectedDate = DateTime.Now;
            //tbDateOfBirth.Text = string.Empty;
            //tbDateOfEmployment.Text = string.Empty;

            _competences.Clear();
            _competences.AddRange(db.Competences);
            _mechanicCompetences.Clear();

            lbCompetences.Items.Refresh();
            lbMechanicCompetences.Items.Refresh();
        }
        private void RemoveErrandInfo()
        {
            cbCurrentErrands.SelectedItem = null;
            cbFinishedErrands.SelectedItem = null;
            tbModelName.Text = string.Empty;
            tbVehicleType.Text = string.Empty;
            tbLicensePlate.Text = string.Empty;
            tbProblem.Text = string.Empty;
            tbDescription.Text = string.Empty;
        }
        private bool EditMechanic()
        {
            bool hasSomethingChanged = false;
            if (cbMechanics.SelectedItem != null)
            {
                var mechanic = cbMechanics.SelectedItem as Mechanic;
                var dateOfBirthToChange = (DateTime)dpDateOfBirthToChange.SelectedDate;
                var dateOfEmploymentToChange = (DateTime)dpDateOfEmploymentToChange.SelectedDate;

                var isFirstnameChanged = IsChanged(mechanic.FirstName, tbFirstNameToChange.Text);
                var isLastnameChanged = IsChanged(mechanic.LastName, tbLastNameToChange.Text);

                var isDateOfBirthChanged = IsChanged(mechanic.DateOfBirth.ToShortDateString(), dateOfBirthToChange.ToShortDateString());
                var isDateOfEmploymentChanged = IsChanged(mechanic.DateOfEmployment.ToShortDateString(), dateOfEmploymentToChange.ToShortDateString());


                if (isFirstnameChanged)
                {
                    mechanic.FirstName = tbFirstNameToChange.Text;
                    hasSomethingChanged = true;
                }

                if (isLastnameChanged)
                {
                    mechanic.LastName = tbLastNameToChange.Text;
                    hasSomethingChanged = true;
                }

                if (isDateOfBirthChanged)
                {
                    mechanic.DateOfBirth = dateOfBirthToChange;
                    hasSomethingChanged = true;
                }

                if (isDateOfEmploymentChanged)
                {
                    mechanic.DateOfEmployment = dateOfEmploymentToChange;
                    hasSomethingChanged = true;
                }
                _dbMechanics.SaveMechanicList(db.CurrentMechanics, "CurrentMechanics.json");
            }
            return hasSomethingChanged;
        }
        private bool IsChanged(string original, string input)
        {
            return original != input && !string.IsNullOrWhiteSpace(input);
        }
        private void RefreshLists()
        {
            db.CurrentMechanics = _dbMechanics.LoadCurrentMechanics();
            db.OldMechanics = _dbMechanics.LoadOldMechanics();
            db.Errands = _dbErrands.LoadList();

            db.Cars = _dbCars.LoadList();
            db.Motorcycles = _dbMotorCycles.LoadList();
            db.Buses = _dbBuses.LoadList();
            db.Trucks = _dbTrucks.LoadList();

            cbMechanics.ItemsSource = db.CurrentMechanics;
            cbOldMechanics.ItemsSource = db.OldMechanics;
        }

        private void dpMechanicDateOfBirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbCurrentErrands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (cbCurrentErrands.SelectedItem != null)
            {
                cbFinishedErrands.SelectedItem = null;
                var errand = cbCurrentErrands.SelectedItem as Errand;
                var vehicle = _vehicleService.GetVehicleFromErrand(errand);

                if (vehicle != null)
                    SetErrandInfo(errand, vehicle);
            }
        }


        private void cbFinishedErrands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFinishedErrands.SelectedItem != null)
            {
                cbCurrentErrands.SelectedItem = null;
                var errand = cbFinishedErrands.SelectedItem as Errand;
                var vehicle = _vehicleService.GetVehicleFromErrand(errand);

                if (vehicle != null)
                    SetErrandInfo(errand, vehicle);
            }
        }
        private void SetErrandInfo(Errand errand, Vehicle vehicle)
        {


            if (cbMechanics.SelectedItem != null)
            {
                tbModelName.Text = vehicle.ModelName;
                tbLicensePlate.Text = vehicle.LicensePlate;
                tbProblem.Text = errand.Problem.ToString();
                tbDescription.Text = errand.Description;

                if (vehicle is Car)
                    tbVehicleType.Text = "Bil";

                else if (vehicle is Motorcycle)
                    tbVehicleType.Text = "Motorcykel";

                else if (vehicle is Bus)
                    tbVehicleType.Text = "Buss";

                else if (vehicle is Truck)
                    tbVehicleType.Text = "Lastbil";

            }

            else if (cbOldMechanics.SelectedItem != null)
            {
                tbModelName.Text = vehicle.ModelName;
                tbLicensePlate.Text = vehicle.LicensePlate;
                tbProblem.Text = errand.Problem.ToString();
                tbDescription.Text = errand.Description;

                if (vehicle is Car)
                    tbVehicleType.Text = "Bil";

                else if (vehicle is Motorcycle)
                    tbVehicleType.Text = "Motorcykel";

                else if (vehicle is Bus)
                    tbVehicleType.Text = "Buss";

                else if (vehicle is Truck)
                    tbVehicleType.Text = "Lastbil";
            }
        }

        private void ClearErrandInfo()
        {
            cbCurrentErrands.SelectedItem = null;
            cbFinishedErrands.SelectedItem = null;

            tbModelName.Text = string.Empty;
            tbLicensePlate.Text = string.Empty;
            tbProblem.Text = string.Empty;
            tbDescription.Text = string.Empty;
            tbVehicleType.Text = string.Empty;
        }

        private void btnBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            var homePage = new HomePage();
            this.NavigationService.Navigate(homePage);
        }
    }
}
