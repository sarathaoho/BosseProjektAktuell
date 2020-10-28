using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Database.Entities
{
    public static class Users
    {
        private static List<User> _userList;
        
        public static List<User> UserList 
        {
            get
            {
                if (_userList == null)
                {
                    _userList = new List<User>();
                }
                return _userList;
            }

            set
            {
                _userList = value;
            }
        }

    }
}
