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
        public string DateOfBirth { get; set; } // Kör alla datumgrejer som string för att förenkla utskrift
        public string DateOfEmployment { get; set; }
        public string LastDate { get; set; }
        public string UserID { get; set; }


        public List<VehiclePart> Competences { get; set; } // Se mappen Models för alla enums
        public List<string> CurrentErrands { get; set; } // Nuvarande ärenden

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

            set { _isAvailable = value; }
        }

        
        public Mechanic()
        {
            Competences = new List<VehiclePart>();
            CurrentErrands = new List<string>();
        }
    }
}
