using Logic.Database;
using Logic.Database.Entities;
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

namespace GUI.MechPage
{
    /// <summary>
    /// Interaction logic for MechanicPage.xaml
    /// </summary>
    public partial class MechanicPage : Page
    {
        readonly MechanicService _mechanicService = new MechanicService();
        private const string _currentMechanicsPath = @"DAL\Files\CurrentMechanics.json";
        private const string _oldMechanicsPath = @"DAL\Files\OldMechanics.json";


        private List<VehiclePart> _mechanicCompetences = new List<VehiclePart>();
        private List<VehiclePart> _competences = new List<VehiclePart>()
        {
            VehiclePart.Kaross,
            VehiclePart.Bromsar,
            VehiclePart.Motor,
            VehiclePart.Hjul,
            VehiclePart.Vindruta
        };

        public MechanicPage()
        {
            InitializeComponent();

            UpdateAddMechanicPage();
            lbMechanicCompetences.ItemsSource = _mechanicCompetences;
            lbCompetences.ItemsSource = _competences;
            cbMechanics.ItemsSource = Database.CurrentMechanics;
        }


        private void btnAddMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMechanicFirstName.Text)
                || string.IsNullOrWhiteSpace(tbMechanicLastName.Text)
                || string.IsNullOrWhiteSpace(tbDateOfBirth.Text)
                || string.IsNullOrWhiteSpace(tbDateOfEmployment.Text))
            {
                MessageBox.Show("Felaktig inmatning");
            }

            else
            {
                var mechanic = new Mechanic()
                {
                    FirstName = tbMechanicFirstName.Text,
                    LastName = tbMechanicLastName.Text,
                    DateOfBirth = tbDateOfBirth.Text,
                    DateOfEmployment = tbDateOfEmployment.Text,
                };

                _mechanicCompetences.ForEach(competence => _mechanicService.AddCompetence(mechanic, competence));

                Database.CurrentMechanics.Add(mechanic);
                JsonHelper.WriteFile(Database.CurrentMechanics, _currentMechanicsPath);

                MessageBox.Show("Mekaniker skapad");
                UpdateAddMechanicPage();
            }
        }


        private void btnLeftArrow_Click(object sender, RoutedEventArgs e)
        {
            VehiclePart competence = (VehiclePart)lbCompetences.SelectedItem;

            _mechanicCompetences.Add(competence);
            _competences.Remove(competence);

            lbMechanicCompetences.Items.Refresh();
            lbCompetences.Items.Refresh();
        }

        private void btnRightArrow_Click(object sender, RoutedEventArgs e)
        {
            VehiclePart competence = (VehiclePart)lbMechanicCompetences.SelectedItem;

            _mechanicCompetences.Remove(competence);
            _competences.Add(competence);

            lbMechanicCompetences.Items.Refresh();
            lbCompetences.Items.Refresh();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var mechanic = cbMechanics.SelectedItem as Mechanic;
            if (mechanic == null)
                MessageBox.Show("Fel: Du måste välja en mekaniker");

            else
            {
                MessageBoxResult result = MessageBox.Show($"Är du säker på att du vill ta bort {mechanic.FirstName} {mechanic.LastName}?",
                    "Ta bort mekaniker", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        mechanic.LastDate = DateTime.Now.ToString("yyyy-MM-dd");
                        Database.CurrentMechanics.Remove(mechanic);
                        Database.OldMechanics.Add(mechanic);

                        // HOWTO: Behöver jag göra såhär?
                        JsonHelper.WriteFile(Database.OldMechanics, _oldMechanicsPath);
                        JsonHelper.WriteFile(Database.CurrentMechanics, _currentMechanicsPath);

                        cbMechanics.Items.Refresh();
                        MessageBox.Show($"Tog bort {mechanic.FirstName} {mechanic.LastName}");
                        UpdateEditPage();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        // När man ändrar mekaniker i Combo boxen
        private void cbMechanics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mechanic = cbMechanics.SelectedItem as Mechanic;
            if (mechanic != null)
            {
                lbMechanicCompetences2.ItemsSource = mechanic.Competences;
                lbCompetences2.ItemsSource = Database.Competences.Where(competence => !mechanic.Competences.Contains(competence));
                tbFirstNameToChange.Watermark = mechanic.FirstName;
                tbLastNameToChange.Watermark = mechanic.LastName;
                tbDateOfEmploymentToChange.Watermark = mechanic.DateOfEmployment;
                tbDateOfBirthToChange.Watermark = mechanic.DateOfBirth;

                var user = Database.Users.FirstOrDefault(user => user.ID.Equals(mechanic.UserID));
                tbUserID.Text = user != null ? user.Username : "Ingen användare";
            }
        }


        // Lägg till kompetenser hos mekaniker som redan existerar
        //TODO: Fixa try/catch om någon bara klickar på knappen utan att ha valt ett objekt
        private void btnLeftArrow2_Click(object sender, RoutedEventArgs e)
        {
            var mechanic = cbMechanics.SelectedItem as Mechanic;

            if (lbCompetences2.SelectedItem == null)
            {
                MessageBox.Show("Du måste välja en kompetens ur listan");
            }
            else
            {
                VehiclePart competence = (VehiclePart)lbCompetences2.SelectedItem;
                _mechanicService.AddCompetence(mechanic, competence);

                lbMechanicCompetences2.Items.Refresh();
                lbCompetences2.ItemsSource = Database.Competences.Where(x => !mechanic.Competences.Contains(x));
            }



        }

        // Ta bort kompetenser från mekaniker som redan existerar
        private void btnRightArrow2_Click(object sender, RoutedEventArgs e)
        {
            var mechanic = cbMechanics.SelectedItem as Mechanic;
            VehiclePart competence = (VehiclePart)lbMechanicCompetences2.SelectedItem;

            _mechanicService.RemoveCompetence(mechanic, competence);

            lbMechanicCompetences2.Items.Refresh();

            lbCompetences2.ItemsSource = Database.Competences.Where(x => !mechanic.Competences.Contains(x));
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            EditMechanic();
            UpdateEditPage();

            MessageBox.Show($"Ändringar sparade");
        }

        private void UpdateEditPage()
        {
            cbMechanics.SelectedItem = null;
            cbMechanics.Items.Refresh();

            lbMechanicCompetences2.ItemsSource = null;

            tbFirstNameToChange.Watermark = string.Empty;
            tbFirstNameToChange.Text = string.Empty;
            tbLastNameToChange.Watermark = string.Empty;
            tbLastNameToChange.Text = string.Empty;
            tbDateOfBirthToChange.Watermark = string.Empty;
            tbDateOfBirthToChange.Text = string.Empty;
            tbDateOfEmploymentToChange.Watermark = string.Empty;
            tbDateOfEmploymentToChange.Text = string.Empty;
            tbUserID.Text = string.Empty;
        }
        private void UpdateAddMechanicPage()
        {
            cbMechanics.Items.Refresh();
            tbMechanicFirstName.Text = string.Empty;
            tbMechanicLastName.Text = string.Empty;
            tbDateOfBirth.Text = string.Empty;
            tbDateOfEmployment.Text = string.Empty;

            _competences.Clear();
            _competences.AddRange(Database.Competences);
            _mechanicCompetences.Clear();

            lbCompetences.Items.Refresh();
            lbMechanicCompetences.Items.Refresh();
        }

        private void EditMechanic()
        {
            var mechanic = cbMechanics.SelectedItem as Mechanic;

            var isFirstnameChanged = IsChanged(mechanic.FirstName, tbFirstNameToChange.Text);
            var isLastnameChanged = IsChanged(mechanic.LastName, tbLastNameToChange.Text);
            var isDateOfBirthChanged = IsChanged(mechanic.DateOfBirth, tbDateOfBirthToChange.Text);
            var isDateOfEmploymentChanged = IsChanged(mechanic.DateOfEmployment, tbDateOfEmploymentToChange.Text);

            if (isFirstnameChanged)
            {
                mechanic.FirstName = tbFirstNameToChange.Text;
            }

            if (isLastnameChanged)
            {
                mechanic.LastName = tbLastNameToChange.Text;
            }

            if (isDateOfBirthChanged)
            {
                mechanic.DateOfBirth = tbDateOfBirthToChange.Text;
            }

            if (isDateOfEmploymentChanged)
            {
                mechanic.DateOfEmployment = tbDateOfEmploymentToChange.Text;
            }
        }

        private bool IsChanged(string original, string input)
        {
            return original != input && !string.IsNullOrWhiteSpace(input);
        }


    }
}
