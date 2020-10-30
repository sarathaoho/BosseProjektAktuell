using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Database.Entities
{
    public class User : AEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string MechanicID { get; set; } // Gör en service av att ta emot ID, och sen koppla till mekanikerns id
        public bool IsAdmin { get; set; }


        /// <summary>
        /// Den här metoden gpr x
        /// </summary>
        /// <param name="errand"></param>
        /// <returns>Bool</returns>
        //public virtual bool TryAddErrand(Errand errand)
        //{
        //    var success = false;

        //    var competence = UserMechanic.Competences.FirstOrDefault(competence => competence == errand.Problem);
        //    if (competence == 0)
        //    {

        //        throw new Exception($"The mechanic didn't have competence for handling {errand.Problem}");
        //    }

        //    if (UserMechanic.IsAvailable)
        //    {
        //        UserMechanic.AddErrand(errand);
        //        errand.ErrandStatus = ErrandStatus.Gul;
        //        return true;
        //    }

        //    return success;
        //}

        //public virtual void ChangeErrandStatus(Errand errand, ErrandStatus newStatus)
        //{
        //    errand.ErrandStatus = newStatus;

        //}


    }
}
