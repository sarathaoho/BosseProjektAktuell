using GUI.Errands;
using GUI.MechPage;
using GUI.UsersPage;
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

        public HomePage()
        {
            InitializeComponent();

            // Dummies
            Mechanic mechanic = new Mechanic()
            {
                FirstName = "Peter",
                LastName = "Wallenäs",
                ID = "1"
            };
            db.CurrentMechanics.Add(mechanic);
            
            mechanic = new Mechanic()
            {
                FirstName = "Julia",
                LastName = "Berglund",
                ID = "2",

            };
            db.CurrentMechanics.Add(mechanic);
            

            mechanic = new Mechanic()
            {
                FirstName = "Calle",
                LastName = "Maelan",
                ID = "3",
                UserID = "test"
            };
            db.CurrentMechanics.Add(mechanic);
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
    }
}
