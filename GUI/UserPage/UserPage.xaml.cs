﻿using GUI.UsersPage;
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
        readonly MechanicService mechanicService = new MechanicService();

        private const string _usersPath = @"DAL\Files\Users.json";
        private const string _currentMechanicsPath = @"DAL\Files\CurrentMechanics.json";

        public UserPage()
        {
            InitializeComponent();

            cbMechanics.ItemsSource = db.CurrentMechanics;
        }

        private void cbMechanics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Drop down box för Mekaniker att binda till en Användare
            var mechanic = cbMechanics.SelectedItem as Mechanic;
            if (mechanic != null)
            {
                //lbMechanicCompetences2.ItemsSource = mechanic.Competences;
                //lbCompetences2.ItemsSource = Database.Competences.Where(competence => !mechanic.Competences.Contains(competence));
                //tbFirstNameToChange.Watermark = mechanic.FirstName;
                //tbLastNameToChange.Watermark = mechanic.LastName;
                //tbDateOfEmploymentToChange.Watermark = mechanic.DateOfEmployment;
                //tbDateOfBirthToChange.Watermark = mechanic.DateOfBirth;
                var mechanics = db.CurrentMechanics.FirstOrDefault(user => user.ID.Equals(user.UserID));
                tbMechanicID.Text = mechanics != null ? mechanic.FirstName : "Ingen användare";
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateEditPageCopy();
            //Lägg till användare knappen
            {
                // ComboBoxen är nu vald som User, lägg till val för Mekaniker/Admin
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

                // Ger användaren ett GUID
                user.MechanicID = mechanic.ID;

                // Lägger till användaren i Users
                db.Users.Add(user);

                // Skriver ut till fil
                JsonHelper.WriteFile<User>(db.Users, _usersPath);
                MessageBox.Show("Användare tillagd.");
            }
        }

        //private void ListUsers_Click(object sender, RoutedEventArgs e)
        //{
        //    //Listknappen
        //    JsonHelper.ReadFile<List>(_usersPath);
        //    //var json = File.ReadAllText(@"DAL\Files\Users.json");
        //    //var json = JsonSerializer.Serialize(Listor.Users);
        //    //Kanske ha en dropdownMeny som listar användare istället?
        //    //ska man ha detta ens eller är det bara för mekaniker man ska kunna lista ut dem?
        //}

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Lägg till kod för att ta bort användare
        }
        private void UpdateEditPageCopy()
        {
            cbListUsers.SelectedItem = null;
            cbListUsers.Items.Refresh();

            tbUserNameSwap.Watermark = string.Empty;
            tbUserNameSwap.Text = string.Empty;
            tbPasswordSwap.Watermark = string.Empty;
            tbPasswordSwap.Text = string.Empty;
            
            tbMechanicID.Text = string.Empty;
        }
        //private void UpdateAddUserPage()
        //{
        //    cbMechanics.Items.Refresh();
        //    tbMechanicFirstName.Text = string.Empty;
        //    tbMechanicLastName.Text = string.Empty;
        //    tbDateOfBirth.Text = string.Empty;
        //    tbDateOfEmployment.Text = string.Empty;

        //    _competences.Clear();
        //    _competences.AddRange(Database.Competences);
        //    _mechanicCompetences.Clear();

        //    lbCompetences.Items.Refresh();
        //    lbMechanicCompetences.Items.Refresh();
        //}

        private bool IsChanged(string original, string input)
        {
            return original != input && !string.IsNullOrWhiteSpace(input);
        }

        private void EditUser()
        {
            var users = cbListUsers.SelectedItem as User;

            var isUserNameChanged = IsChanged(users.Username, tbUserNameSwap.Text);
            var isPasswordChanged = IsChanged(users.Password, tbPasswordSwap.Text);
          

            if (isUserNameChanged)
            {
                users.Username = tbUserNameSwap.Text;
            }

            if (isPasswordChanged)
            {
                users.Password = tbPasswordSwap.Text;
            }
        }

        private void cbListUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var users = cbListUsers.SelectedItem as User;
            if (users != null)
            {

                tbUserNameSwap.Watermark = users.Username;
                tbPasswordSwap.Watermark = users.Password;
                
                var user = db.Users.FirstOrDefault(user => user.ID.Equals(users.MechanicID));
                tbMechanicID.Text = user != null ? user.Username : "Ingen användare";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditUser();
            UpdateEditPageCopy();

            MessageBox.Show($"Ändringar sparade");
        }

    }
}
