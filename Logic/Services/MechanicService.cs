using Logic.Database.Entities;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Logic.Database;

namespace Logic.Services
{
    // Klass för att sköta all mekanikerlogik
    public class MechanicService
    {
        /// <summary>
        /// Lägger till kompetens om listan med kompetenser inte är full (5st) och om listan inte redan innehåller kompetensen.
        /// </summary>
        /// <param name="competence">Kompetensen som ska läggas till</param>
        public void AddCompetence(Mechanic mechanic, VehiclePart competence)
        {
            if (mechanic.Competences.Count < 6 && !mechanic.Competences.Contains(competence))
                mechanic.Competences.Add(competence);
        }


        /// <summary>
        /// Tar bort kompetens om den valda kompetensen finns i listan.
        /// </summary>
        /// <param name="competenceToRemove">Kompetens som ska tas bort</param>
        public void RemoveCompetence(Mechanic mechanic, VehiclePart competenceToRemove)
        {
            if (mechanic.Competences.Contains(competenceToRemove))
            {
                mechanic.Competences.Remove(competenceToRemove);
            }
        }

        /// <summary>
        /// Lägger till ärende i listan med nuvarande ärenden, om listan inte är full (det finns 2 ärenden i listan redan)
        /// </summary>
        /// <param name="errand"></param>
        public void AddErrand(string mechanicID, string errandID)
        {
            var mechanic = db.CurrentMechanics.FirstOrDefault(x => x.ID == mechanicID);
            var errand = db.Errands.FirstOrDefault(x => x.ID == errandID);

            if (mechanic.CurrentErrands.Count < 2)
            {
                mechanic.CurrentErrands.Add(errandID);
                errand.ErrandStatus = ErrandStatus.Gul;
            }
        }

        // Tar bort ärende från mekanikerns lista med ärenden
        public void RemoveErrand(string mechanicID, string errandID)
        {
            var mechanic = db.CurrentMechanics.FirstOrDefault(x => x.ID == mechanicID);
            var errand = db.Errands.FirstOrDefault(x => x.ID == errandID);
            
            mechanic.CurrentErrands.Remove(errand.ID);
            errand.ErrandStatus = ErrandStatus.Grön;
        }

        //Vet inte om denna funkar som den ska än
        //public void ChangeErrandStatus(Errand errand, ErrandStatus statusToReturn)
        //{
        //    errand.ErrandStatus = statusToReturn;
        //}


    }
}
