using Logic.DAL;
using Logic.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Services
{
    public interface ILoginService
    {
        User Login(string username, string password);
    }

    public class LoginService : ILoginService
    {
        private UserDataAccess<User> _dbUser;
        private UserService _userService;


        public LoginService()
        {
            _userService = new UserService();
            _dbUser = new UserDataAccess<User>();
        }

        // Returnerar en användare (innan returnerade den bool)

        /// <summary>
        /// Returns a user if it's in the list of users.
        /// </summary>
        /// <param name="username">Användarnamn att söka på</param>
        /// <param name="password">Lösenord att söka på</param>
        /// <returns></returns>
        public User Login(string username, string password)
        {
            // Fånga upp att vi skapar upp en fil (User.json) om filen inte körs
            List<User> users = _dbUser.LoadList();

            if (users.Count == 0)
            {
                List<User> tmpusers = new List<User>()
                {
                    new User(){IsAdmin = true, Username = "bosse@verkstaden.se", Password="Meckarn123" }
                };

                _dbUser.SaveList(tmpusers);
                users = tmpusers;
            }

            var user = users.FirstOrDefault(user => user.Username.Equals(username) && user.Password.Equals(password));

            if (!_userService.Regexx(username))
            {
                return null;
            }

            return user;
        }

    }
}
