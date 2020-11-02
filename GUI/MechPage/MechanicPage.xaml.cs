using Logic.Database;
using Logic.Database.Entities;
using Logic.Helpers;
using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
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
        private const string mechanicsPath = @"DAL\Files\Mechanics.json";

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

            
            lbMechanicCompetences.ItemsSource = _mechanicCompetences;
            lbCompetences.ItemsSource = _competences;

            cbMechanics.ItemsSource = Listor.Mechanics;
        }

        
        private void bAddMechanic_Click(object sender, RoutedEventArgs e)
        {
            var mechanic = new Mechanic()
            {
                FirstName = tbMechanicFirstName.Text,
                LastName = tbMechanicLastName.Text,
                DateOfBirth = tbDateOfBirth.Text,
                DateOfEmployment = tbDateOfEmployment.Text,
            };

            // Lägger till kompetenserna från listan
            foreach (var item in _mechanicCompetences)
            {
                _mechanicService.AddCompetence(mechanic, item);
            }

            Listor.Mechanics.Add(mechanic);

            JsonHelper.WriteFile<Mechanic>(Listor.Mechanics, mechanicsPath);
            MessageBox.Show("Mekaniker skapad");
           
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
            var mechanicToRemove = cbMechanics.SelectedItem as Mechanic;
            Listor.Mechanics.Remove(mechanicToRemove);

            cbMechanics.Items.Refresh();
            MessageBox.Show($"Tog bort {mechanicToRemove.FirstName} {mechanicToRemove.LastName}");

        }
    }
}
