using GUI.Errands;
using GUI.Login;
using GUI.MechPage;
using GUI.UsersPage;
using Logic.DAL;
using Logic.Database;
using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
using Logic.Helpers;
using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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


namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {

        private UserDataAccess<Mechanic> _dbMechanics;
        private UserDataAccess<Errand> _dbErrands;
        private MechanicService _mechanicService;


        public HomePage()
        {
            InitializeComponent();
        }






        private void btnMechanicPage_Click(object sender, RoutedEventArgs e)
        {
            var mechanicPage = new MechanicPage();
            this.NavigationService.Navigate(mechanicPage);
        }

        private void btnUserPage_Click(object sender, RoutedEventArgs e)
        {
            UserPage userPage = new UserPage();
            this.NavigationService.Navigate(userPage);
        }

        private void btnErrandPage_Click(object sender, RoutedEventArgs e)
        {
            ErrandsPage errandsPage = new ErrandsPage();
            this.NavigationService.Navigate(errandsPage);
        }

        private void lbErrands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbErrands.SelectedItem != null)
            {
                var errand = lbErrands.SelectedItem as Errand;

                cbMechanics.ItemsSource = _mechanicService.GetAvailableMechanic(errand.Problem);
            }
        }

        private async void BtnAssign_Click(object sender, RoutedEventArgs e)
        {
            if (cbMechanics.SelectedItem != null)
            {
                if (lbErrands.SelectedItem != null)
                {
                    var mechanic = cbMechanics.SelectedItem as Mechanic;
                    var errand = lbErrands.SelectedItem as Errand;

                    _mechanicService.AddCurrentErrand(mechanic.ID, errand.ID);

                    MessageBox.Show("Ärende tilldelat mekaniker.");

                    lbErrands.SelectedItem = null;
                    cbMechanics.SelectedItem = null;

                    await RefreshLists();

                }
            }
        }

        private async void lbErrands_Initialized(object sender, EventArgs e)
        {
            await RefreshLists();
        }

        private async void lbBirthdays_Initialized(object sender, EventArgs e)
        {
            await RefreshLists();
        }

        private async Task RefreshLists()
        {
            _dbMechanics = new UserDataAccess<Mechanic>();
            _dbErrands = new UserDataAccess<Errand>();
            _mechanicService = new MechanicService();

            db.Errands = await _dbErrands.LoadListAsync();
            db.CurrentMechanics = _dbMechanics.LoadCurrentMechanics();

            lbErrands.ItemsSource = db.Errands.Where(x => x.ErrandStatus == ErrandStatus.Röd);
            lbBirthdays.ItemsSource = db.CurrentMechanics.Where(x => _mechanicService.IsBirthday(x) == true).ToList();

            lbErrands.Items.Refresh();
            lbBirthdays.Items.Refresh();

            lblTodaysDate.Content = DateTime.Now.ToShortDateString();
        }

        private void btnLoggaUt_Click(object sender, RoutedEventArgs e)
        {
            var loginPage = new LoginPage();
            this.NavigationService.Navigate(loginPage);
        }
    }
}
