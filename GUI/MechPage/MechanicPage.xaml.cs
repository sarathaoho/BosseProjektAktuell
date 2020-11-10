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
            var mech = new Mechanic()
            {
                FirstName = "Bob",
                LastName = "Johnson",
                Competences = new List<VehiclePart>() { VehiclePart.Bromsar, VehiclePart.Hjul },
                LastDate = DateTime.Now.ToString("yyyy-MM-dd"),
                DateOfEmployment = "1999-05-24",
                DateOfBirth = "1952-08-02"
            };

            db.OldMechanics.Add(mech);

            InitializeComponent();

            UpdateAddMechanicPage();
            lbMechanicCompetences.ItemsSource = _mechanicCompetences;
            lbCompetences.ItemsSource = _competences;
            cbMechanics.ItemsSource = db.CurrentMechanics;
            cbOldMechanics.ItemsSource = db.OldMechanics;
        }

        #region Skapa mekaniker

        private void btnAddMechanic_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMechanicFirstName.Text)
                || string.IsNullOrWhiteSpace(tbMechanicLastName.Text)
                || string.IsNullOrWhiteSpace(tbDateOfBirth.Text)
                || string.IsNullOrWhiteSpace(tbDateOfEmployment.Text))
            {
                System.Windows.MessageBox.Show("Felaktig inmatning");
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

                db.CurrentMechanics.Add(mechanic);
                JsonHelper.WriteFile(db.CurrentMechanics, _currentMechanicsPath);

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
                lbMechanicCompetences2.ItemsSource = mechanic.Competences;
                //lbCompetences.ItemsSource = Database.Competences;

                lbCompetences2.ItemsSource = db.Competences.Where(competence => !mechanic.Competences.Contains(competence));

                tbFirstNameToChange.Watermark = mechanic.FirstName;
                tbLastNameToChange.Watermark = mechanic.LastName;

                tbDateOfEmploymentToChange.Watermark = mechanic.DateOfEmployment;
                tbDateOfBirthToChange.Watermark = mechanic.DateOfBirth;

                var user = db.Users.FirstOrDefault(user => user.ID.Equals(mechanic.UserID));
                tbUserID.Text = user != null ? user.Username : "Ingen användare";
            }
        }

        private void cbOldMechanics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbOldMechanics.SelectedItem is Mechanic oldMechanic)
            {
                ViewOldMechanic(oldMechanic);
            }
        }

        private void ViewOldMechanic(Mechanic oldMechanic)
        {
            lbOldMechanicCompetences.ItemsSource = oldMechanic.Competences;
            tbOldMechanicFirstname.Text = oldMechanic.FirstName;
            tbOldMechanicLastname.Text = oldMechanic.LastName;
            tbOldMechanicDateOfEmployment.Text = oldMechanic.DateOfEmployment;
            tbOldMechanicLastDate.Text = oldMechanic.LastDate;
            tbOldMechanicDateOfBirth.Text = oldMechanic.DateOfBirth;
        }

        // Lägg till kompetenser hos mekaniker som redan existerar
        //TODO: Fixa try/catch om någon bara klickar på knappen utan att ha valt ett objekt
        private void btnLeftArrow2_Click(object sender, RoutedEventArgs e)
        {

            if (lbCompetences2.SelectedItem != null)
            {
                var mechanic = cbMechanics.SelectedItem as Mechanic;

                VehiclePart competence = (VehiclePart)lbCompetences2.SelectedItem;
                _mechanicService.AddCompetence(mechanic, competence);

                lbMechanicCompetences2.Items.Refresh();
                lbCompetences2.ItemsSource = db.Competences.Where(x => !mechanic.Competences.Contains(x));
            }
        }

        private void btnRightArrow2_Click(object sender, RoutedEventArgs e)
        {

            if (lbCompetences2.SelectedItem != null)
            {
                var mechanic = cbMechanics.SelectedItem as Mechanic;
                
                VehiclePart competence = (VehiclePart)lbMechanicCompetences2.SelectedItem;

                _mechanicService.RemoveCompetence(mechanic, competence);

                lbMechanicCompetences2.Items.Refresh();

                lbCompetences2.ItemsSource = db.Competences.Where(x => !mechanic.Competences.Contains(x));
            }

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (!(cbMechanics.SelectedItem is Mechanic mechanic))
                System.Windows.MessageBox.Show("Fel: Du måste välja en mekaniker");

            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show($"Är du säker på att du vill ta bort {mechanic.FirstName} {mechanic.LastName}?",
                    "Ta bort mekaniker", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        mechanic.LastDate = DateTime.Now.ToString("yyyy-MM-dd");
                        db.CurrentMechanics.Remove(mechanic);
                        db.OldMechanics.Add(mechanic);

                        // HOWTO: Behöver jag göra såhär?
                        JsonHelper.WriteFile(db.OldMechanics, _oldMechanicsPath);
                        JsonHelper.WriteFile(db.CurrentMechanics, _currentMechanicsPath);

                        cbMechanics.Items.Refresh();
                        System.Windows.MessageBox.Show($"Tog bort {mechanic.FirstName} {mechanic.LastName}");
                        UpdateEditPage();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            EditMechanic();
            UpdateEditPage();

            System.Windows.MessageBox.Show($"Ändringar sparade");
        }
        #endregion

        private void UpdateEditPage()
        {
            cbMechanics.SelectedItem = null;
            cbMechanics.Items.Refresh();

            lbMechanicCompetences2.ItemsSource = null;
            lbCompetences2.ItemsSource = null;


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
            _competences.AddRange(db.Competences);
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
