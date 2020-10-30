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
        //public string MechanicID { get; set; } // Gör en service av att ta emot ID, och sen koppla till mekanikerns id
        public bool IsAdmin { get; set; }
    }
}
