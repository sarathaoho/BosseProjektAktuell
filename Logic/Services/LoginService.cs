using Logic.DAL;
using Logic.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class LoginService
    {
        private UserDataAccess<User> _dbUser;


        public LoginService()
        {

            _dbUser = new UserDataAccess<User>();
        }

        public bool Login(string username, string password)
        {

            List<User> users = _dbUser.GetList();

            return users.Exists(user => user.Username.Equals(username) && user.Password.Equals(password));
        }
    }
}
