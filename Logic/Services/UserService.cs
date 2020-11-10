using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Logic.Services
{
    // Klass som tar hand om logiken för användare
    public class UserService
    {
        public bool Regexx(string username)
        {
            string emailPattern = @"^([\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$";
            if (!Regex.IsMatch(username, emailPattern))
            {

                return false;
                
                
                 
            }

            return true;
        }
    }
}
