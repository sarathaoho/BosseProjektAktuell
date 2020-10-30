﻿using Logic.Database.Entities;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    // Klass för att sköta all mekanikerlogik
    public static class MechanicService
    {
        /// <summary>
        /// Lägger till kompetens om listan med kompetenser inte är full (5st) och om listan inte redan innehåller kompetensen.
        /// </summary>
        /// <param name="competence">Kompetensen som ska läggas till</param>
        public static void AddCompetence(this Mechanic mechanic, VehiclePart competence)
        {
            if (mechanic.Competences.Count < 6 && !mechanic.Competences.Contains(competence))
                mechanic.Competences.Add(competence);
        }

        /// <summary>
        /// Tar bort kompetens om den valda kompetensen finns i listan.
        /// </summary>
        /// <param name="competenceToRemove">Kompetens som ska tas bort</param>
        public static void RemoveCompetence(this Mechanic mechanic, VehiclePart competenceToRemove)
        {
            if (mechanic.Competences.Contains(competenceToRemove))
            {
                mechanic.Competences.Remove(competenceToRemove);
            }
        }

        /// <summary>
        /// Lägger till ärende i listan med nuvarande ärenden, om listan inte är full (det finns 2 ärenden i listan redna)
        /// </summary>
        /// <param name="errand"></param>
        public static void AddErrand(this Mechanic mechanic, Errand errand)
        {
            if (mechanic.CurrentErrands.Count < 3)
                mechanic.CurrentErrands.Add(errand);
        }

        public static void RemoveErrand(this Mechanic mechanic, Errand errand)
        {
            mechanic.CurrentErrands.Remove(errand);
        }
        // Vet inte om denna funkar som den ska än
        public static Errand ChangeErrandStatus(Errand errand, ErrandStatus statusToReturn)
        {
            errand.ErrandStatus = statusToReturn;
            return errand;
        }


    }
}
