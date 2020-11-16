using Logic.Database.Entities;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Logic.Database;
using Logic.DAL;

namespace Logic.Services
{
    // Klass för att sköta all mekanikerlogik
    public class MechanicService
    {
        private readonly UserDataAccess<Mechanic> _dbCurrentMechanics;
        private readonly UserDataAccess<Mechanic> _dbOldMechanics;
        private readonly UserDataAccess<Errand> _dbErrands;

        private readonly List<VehiclePart> _mechanicCompetences;

        private readonly string currentMechanicsPath = @"CurrentMechanics.json";
        private readonly string oldMechanicsPath = @"OldMechanics.json";

        public MechanicService()
        {
            _dbCurrentMechanics = new UserDataAccess<Mechanic>();
            _dbOldMechanics = new UserDataAccess<Mechanic>();
            _dbErrands = new UserDataAccess<Errand>();
            _mechanicCompetences = new List<VehiclePart>();
        }


        /// <summary>
        /// Adds a competence to the mechanics competences list, if the competence isn't already in the list.
        /// </summary>
        /// <param name="competence">Competence to add</param>
        public void AddCompetence(Mechanic mechanic, VehiclePart competence)
        {
            if (!mechanic.Competences.Contains(competence))
                mechanic.Competences.Add(competence);
        }

        /// <summary>
        /// Removes a competence if it is in the mechanic's competence list.
        /// </summary>
        /// <param name="competenceToRemove">Competence to remove</param>
        public void RemoveCompetence(Mechanic mechanic, VehiclePart competenceToRemove)
        {
            if (mechanic.Competences.Contains(competenceToRemove))
                mechanic.Competences.Remove(competenceToRemove);

            _dbCurrentMechanics.SaveMechanicList(db.CurrentMechanics, currentMechanicsPath);
        }

        /// <summary>
        /// Adds an errand to the mechanic's current errands if the list isn't full (2 errands in the list).
        /// </summary>
        /// <param name="mechanicID">Mechanic's ID for finding the mechanic to add to.</param>
        /// <param name="errandID">Errand's ID for finding the errand to add.</param>
        public void AddCurrentErrand(string mechanicID, string errandID)
        {
            var mechanic = db.CurrentMechanics.FirstOrDefault(x => x.ID == mechanicID);
            var errand = db.Errands.FirstOrDefault(x => x.ID == errandID);

            if (mechanic.CurrentErrands.Count < 2)
            {
                mechanic.CurrentErrands.Add(errandID);
                errand.ErrandStatus = ErrandStatus.Gul;
            }

            _dbCurrentMechanics.SaveMechanicList(db.CurrentMechanics, currentMechanicsPath);
            _dbErrands.SaveList(db.Errands);
        }

        /// <summary>
        /// Removes an errand from the mechanic's current errands.
        /// </summary>
        /// <param name="mechanicID"></param>
        /// <param name="errandID"></param>
        public void RemoveCurrentErrand(Mechanic mechanic, Errand errand)
        {
            //var mechanic = db.CurrentMechanics.FirstOrDefault(x => x.ID == mechanicID);
            //var errand = db.Errands.FirstOrDefault(x => x.ID == errandID);
            mechanic.CurrentErrands.Remove(errand.ID);
            errand.ErrandStatus = ErrandStatus.Grön;

            _dbCurrentMechanics.SaveMechanicList(db.CurrentMechanics, currentMechanicsPath);
            _dbErrands.SaveList(db.Errands);
        }

        /// <summary>
        /// Gets all available mechanics if the mechanic is avialable and the mechanic has the competence to fix the problem.
        /// </summary>
        /// <param name="problem"></param>
        /// <returns></returns>
        public IEnumerable<Mechanic> GetAvailableMechanic(VehiclePart problem)
        {
            //return _dbCurrentMechanics.LoadCurrentMechanics().Where(mech => mech.IsAvailable == true && mech.Competences.Contains(problem));
            return db.CurrentMechanics.Where(mech => mech.IsAvailable == true && mech.Competences.Contains(problem));
        }

        /// <summary>
        /// Gets the mechanic who's working on the errand.
        /// </summary>
        /// <param name="errandID"></param>
        /// <returns></returns>
        public Mechanic GetMechanicFromErrand(string errandID)
        {
            //return _dbCurrentMechanics.LoadCurrentMechanics().FirstOrDefault(mech => mech.CurrentErrands.Contains(errandID));
            return db.CurrentMechanics.FirstOrDefault(mech => mech.CurrentErrands.Contains(errandID));
        }

        /// <summary>
        /// Gets the mechanic who finished the errand.
        /// </summary>
        /// <param name="errand"></param>
        /// <returns></returns>
        public Mechanic GetMechanicWhoFinishedErrand(Errand errand)
        {
            var mechanic = _dbCurrentMechanics.LoadCurrentMechanics().FirstOrDefault(mechanic => mechanic.ID == errand.MechanicID);
            if (mechanic != null)
            {
                return mechanic;
            }

            else
            {
                mechanic = _dbOldMechanics.LoadOldMechanics().FirstOrDefault(mechanic => mechanic.ID == errand.MechanicID);

                if (mechanic != null)
                {
                    return mechanic;
                }
            }

            return null;
        }
        /// <summary>
        /// Creates and saves a mechanic with the provided parameters.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="dateOfEmployment"></param>
        /// <param name="competences"></param>
        public void CreateAndSaveMechanic(string firstName, string lastName, DateTime dateOfBirth, DateTime dateOfEmployment, List<VehiclePart> competences)
        {
            var mechanic = new Mechanic()
            {
                FirstName = firstName,
                LastName = lastName,
                //DateOfBirth = dateOfBirth,
                //DateOfEmployment = dateOfEmployment,
                DateOfBirth = dateOfBirth,
                DateOfEmployment = dateOfEmployment
            };

            competences.ForEach(competence => AddCompetence(mechanic, competence));

            db.CurrentMechanics.Add(mechanic);
            _dbCurrentMechanics.SaveMechanicList(db.CurrentMechanics, currentMechanicsPath);
        }

        /// <summary>
        /// Gets the competences that the mechanic doesn't have.
        /// </summary>
        /// <param name="mechanic"></param>
        /// <returns></returns>
        public IEnumerable<VehiclePart> GetRemainingCompetences(Mechanic mechanic)
        {
            return db.Competences.Where(competence => !mechanic.Competences.Contains(competence));
        }
        /// <summary>
        /// Removes the mechanic from current mechanics, sets the LastDate property to the current date and adds the mechanic to the OldMechanics list.
        /// </summary>
        /// <param name="mechanic"></param>
        public void RemoveMechanic(Mechanic mechanic)
        {
            mechanic.LastDate = DateTime.Now;
            if (mechanic.CurrentErrands.Count > 0)
            {
                mechanic.CurrentErrands = null;
            }
            db.CurrentMechanics.Remove(mechanic);
            db.OldMechanics.Add(mechanic);

            _dbCurrentMechanics.SaveMechanicList(db.CurrentMechanics, currentMechanicsPath);
            _dbOldMechanics.SaveMechanicList(db.OldMechanics, oldMechanicsPath);
        }

        /// <summary>
        /// Removes the assigned mechanic from the errand.
        /// </summary>
        /// <param name="mechanic"></param>
        public void RemoveMechanicFromErrand(Mechanic mechanic)
        {
            var errands = db.Errands.Where(x => x.MechanicID == mechanic.ID).ToList();
            if (errands.Count != 0)
            {
                errands.ForEach(errand => ChangeErrandStatusAndMechanicID(errand));

                _dbErrands.SaveList(db.Errands);
            }
        }

        private void ChangeErrandStatusAndMechanicID(Errand errand)
        {
            if (errand.ErrandStatus == ErrandStatus.Gul)
            {
                errand.MechanicID = null;
                errand.ErrandStatus = ErrandStatus.Röd;
            }
        }


        public bool IsBirthday(Mechanic mechanic)
        {
            var day = mechanic.DateOfBirth.Day;
            var month = mechanic.DateOfBirth.Month;

            if (day == DateTime.Today.Day && month == DateTime.Today.Month)
            {
                return true;
            }

            return false;
        }
    }
}
