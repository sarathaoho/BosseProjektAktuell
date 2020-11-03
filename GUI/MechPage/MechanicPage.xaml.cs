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
        private const string _mechanicsPath = @"DAL\Files\Mechanics.json";

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

            ResetPage();
            lbMechanicCompetences.ItemsSource = _mechanicCompetences;
            lbCompetences.ItemsSource = _competences;
            cbMechanics.ItemsSource = Database.AllMechanics;
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
                Database.AllMechanics.Add(mechanic);
                JsonHelper.WriteFile(Database.AllMechanics, _mechanicsPath);

                MessageBox.Show("Mekaniker skapad");
                cbMechanics.Items.Refresh();
                ResetPage();
            }
        }

        // Gör som den låter :~)
        private void ResetPage()
        {
            _competences.Clear();
            _competences.AddRange(Database.Competences);
            _mechanicCompetences.Clear();

            lbCompetences.Items.Refresh();
            lbMechanicCompetences.Items.Refresh();

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
            MessageBoxResult result = MessageBox.Show($"Är du säker på att du vill ta bort {mechanic.FirstName} {mechanic.LastName}?", 
                "Ta bort mekaniker", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Database.AllMechanics.Remove(mechanic);

                    cbMechanics.Items.Refresh();
                    MessageBox.Show($"Tog bort {mechanic.FirstName} {mechanic.LastName}");
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void cbMechanics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mechanic = cbMechanics.SelectedItem as Mechanic;
            if (mechanic != null)
            {
                lbMechanicCompetences2.ItemsSource = mechanic.Competences;
                lbCompetences2.ItemsSource = Database.Competences.Where(competence => !mechanic.Competences.Contains(competence));
            }
        }


        // Lägg till kompetenser hos mekaniker som redan existerar
        private void btnLeftArrow2_Click(object sender, RoutedEventArgs e)
        {
            var mechanic = cbMechanics.SelectedItem as Mechanic;
            VehiclePart competence = (VehiclePart)lbCompetences2.SelectedItem;

            _mechanicService.AddCompetence(mechanic, competence);

            lbMechanicCompetences2.Items.Refresh();
            lbCompetences2.ItemsSource = Database.Competences.Where(x => !mechanic.Competences.Contains(x));

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
            var mechanic = cbMechanics.SelectedItem as Mechanic;

            MessageBox.Show($"Ändringar sparade");
        }
    }
}
