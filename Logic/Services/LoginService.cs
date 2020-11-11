using Logic.DAL;
using Logic.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Returnerar en användare (innan returnerade den bool)

        /// <summary>
        /// Returnerar en användare om den finns i listan med användare
        /// </summary>
        /// <param name="username">Användarnamn att söka på</param>
        /// <param name="password">Lösenord att söka på</param>
        /// <returns></returns>
        public User Login(string username, string password)
        {

            List<User> users = _dbUser.GetList();

            return users.FirstOrDefault(user => user.Username.Equals(username) && user.Password.Equals(password));







           
            }
        }

       









        
    
}
