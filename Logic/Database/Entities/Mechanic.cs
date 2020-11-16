using Logic.Models;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Logic.Database.Entities
{
    public class Mechanic : AEntity
    {
        private bool _isAvailable;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime LastDate { get; set; }
        public string UserID { get; set; }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (today < DateOfBirth.AddYears(age))
                {
                    return age--;
                }

                return age;
            }
        }


        public List<VehiclePart> Competences { get; set; }
        public List<string> CurrentErrands { get; set; }

        // Om CurrentErrands har två ärenden i sig så sätts IsAvailable till false, annars till true
        public bool IsAvailable
        {
            get { return CurrentErrands.Count < 2 ? true : false; }

            set { _isAvailable = value; }
        }


        public Mechanic()
        {
            Competences = new List<VehiclePart>();
            CurrentErrands = new List<string>();
        }
    }
}
