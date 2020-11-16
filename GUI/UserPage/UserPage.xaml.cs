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
        private readonly UserDataAccess<User> _dbUsers;
        private readonly UserDataAccess<Mechanic> _dbMechanics;
        readonly UserService _userService = new UserService();
        readonly MechanicService _mechanicService;

        //private const string _usersPath = @"DAL\Files\Users.json";
        //private const string _currentMechanicsPath = @"DAL\Files\CurrentMechanics.json";

        public UserPage()
        {
            InitializeComponent();
            _dbUsers = new UserDataAccess<User>();
            _dbMechanics = new UserDataAccess<Mechanic>();
            _mechanicService = new MechanicService();
            db.Users = _dbUsers.LoadList();
            db.CurrentMechanics = _dbMechanics.LoadCurrentMechanics();
            RefreshList();
            cbListUsers.ItemsSource = db.Users;
            cbMechanics.ItemsSource = db.CurrentMechanics.Where(mechanic => mechanic.UserID == null);
        }

        private void RefreshList()
        {
            db.Users = _dbUsers.LoadList();
            db.CurrentMechanics = _dbMechanics.LoadCurrentMechanics();

        }
        private void ClearList()
        {
            
            cbMechanics.ItemsSource = db.CurrentMechanics.Where(mechanic => mechanic.UserID == null);
        }

        private void cbMechanics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Drop down box för Mekaniker att binda till en Användare
            
            
            var mechanic = cbMechanics.SelectedItem as Mechanic;
            if (cbMechanics.SelectedItem is Mechanic mechanix && mechanic.UserID == null)
            {
                //RefreshList();
                //UpdateEditPageCopy();
                var mechanics = db.CurrentMechanics.FirstOrDefault(user => user.ID.Equals(user.UserID));
                //tbMechanicID.Text = mechanics != null ? mechanic.FirstName : "Ingen användare";
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //UpdateEditPageCopy();

            //Lägg till användare knappen
            {

                if (cbMechanics.SelectedItem != null)
                {
                    // Välj mellan existerande Mekaniker att binda till en användare
                    var mechanic = cbMechanics.SelectedItem as Mechanic;

                    // TEST: För skapande av användare

                    var emailIsValid = _userService.Regexx(tbUserName.Text);
                    if (emailIsValid != true)
                    {
                        MessageBox.Show("Fel input");
                    }
                    else
                    {
                        string userName = tbUserName.Text;
                        string password = tbPassword.Text;

                        var userID = _userService.CreateAndSaveUser(userName, password);
                        mechanic.UserID = userID;
                        _dbMechanics.SaveMechanicList(db.CurrentMechanics, "CurrentMechanics.json");
                        MessageBox.Show("Användare tillagd.");


                        UpdateEditPageCopy();
                        RefreshList();
                        cbListUsers.ItemsSource = null;
                        cbListUsers.ItemsSource = db.Users;
                        ClearList();
                        
                    }
                }                             
            }
        }

       

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            
            if (!(cbListUsers.SelectedItem is User user))
                MessageBox.Show("Du måste välja en användare.");

            else
            {
                MessageBoxResult result = MessageBox.Show($"Är du säker på att du vill ta bort {user.Username} ?",
                    "Ta bort användare", MessageBoxButton.YesNo);
                switch (result)
                {

                    case MessageBoxResult.Yes:
                        user = cbListUsers.SelectedItem as User;
                        _userService.RemoveUser(user);

                        RefreshList();
                        cbListUsers.ItemsSource = null;
                        cbListUsers.ItemsSource = db.Users;
                        MessageBox.Show($"Tog bort {user.Username} {user.Password}");
                        UpdateEditPageCopy();
                        
                        break;

                    case MessageBoxResult.No:
                        break;
                }
                
            }

        }
        private void UpdateEditPageCopy()
        {
            //cbListUsers.SelectedItem = null;
            //cbListUsers.Items.Refresh();

            tbUserNameSwap.Watermark = string.Empty;
            tbUserNameSwap.Text = string.Empty;
            tbPasswordSwap.Watermark = string.Empty;
            tbPasswordSwap.Text = string.Empty;

            tbUserName.Text = string.Empty;
            tbPassword.Text = string.Empty;

            cbListUsers.SelectedItem = null;
            cbMechanics.SelectedItem = null;
            
            //tbMechanicID.Text = string.Empty;
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
           
            if (cbListUsers.SelectedItem is User userx)
            {
                var user = cbListUsers.SelectedItem as User;
                tbUserNameSwap.Watermark = user.Username;
                tbPasswordSwap.Watermark = user.Password;
                
                //tbMechanicID.Watermark = users.MechanicID;
                var mechanic = db.CurrentMechanics.FirstOrDefault(XamlFormatter => XamlFormatter.UserID == user.ID);
                
                //var user = db.Users.FirstOrDefault(user => user.ID.Equals(mechanic.UserID));

                //tbMechanicID.Text = user != null ? user.Username : "Ingen användare";
                //JsonHelper.ReadFile<User>(_usersPath);
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
