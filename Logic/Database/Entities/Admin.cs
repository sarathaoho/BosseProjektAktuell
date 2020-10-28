using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Database.Entities
{
    public class Admin : User
    {        
        public Admin() : base()
        {
            Username = "Bosse";
            Password = "Meckarn123";
            Privilege = UserPrivilege.Admin;
        }

        public void AddMechanic()
        {

        }
        

    }
}
