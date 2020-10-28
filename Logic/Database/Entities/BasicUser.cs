using Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities
{
    public class BasicUser : User
    {

        public BasicUser () : base()
        {
            Privilege = UserPrivilege.Basic;
        }

    }
}
