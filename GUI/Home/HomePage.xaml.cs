using Logic.Database;
using Logic.Database.Entities;
using Logic.Database.Entities.Vehicles;
using Logic.Helpers;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                ID = "3"
            };
            Listor.Mechanics.Add(mechanic);

            
            // Comboboxen är kopplad till Listor.Mechanics
            cbMechanics.ItemsSource = Listor.Mechanics;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Tar emot den valda mekanikern från comboboxen och gör om det objektet till en mekaniker
            var mechanic = cbMechanics.SelectedItem as Mechanic;

            // TEST: För skapande av användare
            string userName = tbUserName.Text;
            string password = tbPassword.Text;
            string ID = mechanic.ID; // Hämtar ID-propertyn från mekanikern


            // Skapar upp en ny användare
            User user = new User()
            {
                Username = userName,
                Password = password,
                MechanicID = ID,
                IsAdmin = false
            };
            
            // Lägger till användaren i Users
            Listor.Users.Add(user);
            
            // TODO: Använd JsonHelper.WriteFile istället
            string json = JsonSerializer.Serialize(Listor.Users);

            FileStream fs = File.OpenWrite(@"DAL\Files\UsersTestJson.json");
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(json);
            sw.Close();

            MessageBox.Show("Användare tillagd.");
        }

       
    }
}
