using GUI.MechPage;
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
        
        readonly UserService _userService = new UserService();
       
        
        private const string _usersPath = @"DAL\Files\Users.json";

        


        public HomePage()
        {
            InitializeComponent();

            #region Kod för Användarsidan
            Mechanic mechanic = new Mechanic()
            {
                FirstName = "Peter",
                LastName = "Wallenäs",
                ID = "1"
            };
            Listor.Mechanics.Add(mechanic);

            mechanic = new Mechanic()
            {
                FirstName = "Julia",
                LastName = "Berglund",
                ID = "2",

            };
            Listor.Mechanics.Add(mechanic);

            mechanic = new Mechanic()
            {
                FirstName = "Calle",
                LastName = "Maelan",
                ID = "3",
                UserID = "test"
            };
            Listor.Mechanics.Add(mechanic);

           

            // Comboboxen är kopplad till Listor.Mechanics
            cbMechanics.ItemsSource = Listor.Mechanics.Where(mechanic => mechanic.UserID == null).ToList();
            #endregion

            
        }

        #region Metoder för användarrutan
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Tar emot den valda mekanikern från comboboxen och gör om det objektet till en mekaniker
            var mechanic = cbMechanics.SelectedItem as Mechanic;

            // TEST: För skapande av användare
            string userName = tbUserName.Text;
            string password = tbPassword.Text;

            // Skapar upp en ny användare
            User user = new User()
            {
                Username = userName,
                Password = password,
                IsAdmin = false
            };

            // Kopplar samman användaren med mekanikern
            mechanic.UserID = user.ID;

            // Lägger till användaren i Users
            Listor.Users.Add(user);

            // Skriver ut till fil
            JsonHelper.WriteFile<User>(Listor.Users, _usersPath);
            MessageBox.Show("Användare tillagd.");
        }

        #endregion

 

        private void btnMechanicPage_Click(object sender, RoutedEventArgs e)
        {
            var mechanicPage = new MechanicPage();
            this.NavigationService.Navigate(mechanicPage);
        }
    }
}
