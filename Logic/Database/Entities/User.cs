using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Database.Entities
{
    // TODO: Om min klass är abstract så kan jag inte deserialisera?
    public  class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Mechanic UserMechanic { get; set; } // Gör om till MechanicID

        public string MechanicID { get; set; }
        public UserPrivilege Privilege { get; set; } // TODO: ändra till bool?
        
        


        // Användaren kan endast lägga till kompetenser på sig själv (den mekaniker som är tilldelad användaren)
        // Behöver fånga upp om mekanikern har alla kompetenser redan

        // Behöver fånga upp om användaren inte har några kompetenser

        //public virtual void RemoveCompetence(Component competenceToRemove)
        //{
        //    if (UserMechanic.Competences.Count != 0)
        //    {
        //        for (int i = 0; i < UserMechanic.Competence.Length; i++)
        //        {
        //            if (competenceToRemove == UserMechanic.Competence[i])
        //            {
        //                UserMechanic.Competence[i] = null;
        //            }
        //        }
        //    }

        //}


        /// <summary>
        /// Den här metoden gpr x
        /// </summary>
        /// <param name="errand"></param>
        /// <returns>Bool</returns>
        public virtual bool TryAddErrand(Errand errand)
        {
            var success = false;

            var competence = UserMechanic.Competences.FirstOrDefault(competence => competence == errand.Problem);
            if (competence == 0)
            {

                throw new Exception($"The mechanic didn't have competence for handling {errand.Problem}");
            }

            if (UserMechanic.IsAvailable)
            {
                UserMechanic.AddErrand(errand);
                errand.ErrandStatus = ErrandStatus.Gul;
                return true;
            }

            return success;
        }

        public virtual void ChangeErrandStatus(Errand errand, ErrandStatus errandstatus)
        {
            errand.ErrandStatus = errandstatus;

        }

    
    }
}
