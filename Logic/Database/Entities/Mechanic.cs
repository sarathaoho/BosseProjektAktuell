using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Logic.Database.Entities
{
    public class Mechanic
    {
        private bool _isAvailable;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfEmployment { get; set; }
        public string LastDate { get; set; }
        public string ID { get; set; }

        // Se mappen Models för alla enums
        public List<VehiclePart> Competences { get; set; }
        public List<Errand> CurrentErrands { get; set; } // Nuvarande ärenden

        // Om CurrentErrands har två ärenden i sig (båda är true) så sätts IsAvailable till false, annars till true
        public bool IsAvailable
        {
            get
            {
                if (CurrentErrands.Count < 2)
                {
                    _isAvailable = true;
                }
                else
                {
                    _isAvailable = false;
                }

                return _isAvailable;

            }

            set { value = _isAvailable; }
        }

        // HOWTO: Ska vi lägga alla metoder för mekanikern här?

        // Dessa metoder är förövrigt inte helt fullständiga (kanske ska lägga in en bool som retur på många av dem?)
        // Exempelvis public bool TryAddCompetence(VehiclePart competence)
        // OM vi kan lägga till SÅ returnera TRUE
        // ANNARS returnera FALSE


        /// <summary>
        /// Lägger till kompetens om listan med kompetenser inte är full (5st) och om listan inte redan innehåller kompetensen.
        /// </summary>
        /// <param name="competence">Kompetensen som ska läggas till</param>
        public void AddCompetence(VehiclePart competence)
        {
            if (Competences.Count < 6 && !Competences.Contains(competence))
                Competences.Add(competence);
        }

        /// <summary>
        /// Tar bort kompetens om den valda kompetensen finns i listan.
        /// </summary>
        /// <param name="competenceToRemove">Kompetens som ska tas bort</param>
        public void RemoveCompetence(VehiclePart competenceToRemove)
        {
            if (Competences.Contains(competenceToRemove))
            {
                Competences.Remove(competenceToRemove);
            }
        }

        /// <summary>
        /// Lägger till ärende i listan med nuvarande ärenden, om listan inte är full (det finns 2 ärenden i listan redna)
        /// </summary>
        /// <param name="errand"></param>
        public void AddErrand(Errand errand)
        {
            if (CurrentErrands.Count < 3)
                CurrentErrands.Add(errand);
        }

        public void RemoveErrand(Errand errand)
        {
            CurrentErrands.Remove(errand);
        }

        public Errand ChangeErrandStatus(Errand errand, ErrandStatus statusToReturn)
        {
            errand.ErrandStatus = statusToReturn;
            return errand;

        }



        public Mechanic()
        {
            Competences = new List<VehiclePart>();
            CurrentErrands = new List<Errand>();
        }
    }
}
