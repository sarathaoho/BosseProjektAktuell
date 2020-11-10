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

namespace GUI.UsersPage
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        readonly UserService _userService = new UserService();


        private const string _usersPath = @"DAL\Files\Users.json";

        public UserPage()
        {
            InitializeComponent();
        }

        private void cbMechanics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Drop down box för typ av användare
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Lägg till användare knappen
            {
                // ComboBoxen är nu vald som User, lägg till val för Mekaniker/Admin
                var mechanic = cbUsers.SelectedItem as Mechanic;
                if (mechanic == null)
                {
                    MessageBox.Show("FEL!");
                }

                else
                {
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

                    // Ger användaren ett GUID
                    //user.MechanicID = mechanic.ID;

                    // Lägger till användaren i Users
                    db.Users.Add(user);

                    // Skriver ut till fil
                    JsonHelper.WriteFile<User>(db.Users, _usersPath);
                    MessageBox.Show("Användare tillagd.");
                }
            }
        }

        private void ListUsers_Click(object sender, RoutedEventArgs e)
        {
            //Listknappen
            JsonHelper.ReadFile<List>(_usersPath);
            //var json = File.ReadAllText(@"DAL\Files\Users.json");
            //var json = JsonSerializer.Serialize(Listor.Users);
            //Kanske ha en dropdownMeny som listar användare istället?
            //ska man ha detta ens eller är det bara för mekaniker man ska kunna lista ut dem?
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Lägg till kod för att ta bort användare
        }
    }
}
