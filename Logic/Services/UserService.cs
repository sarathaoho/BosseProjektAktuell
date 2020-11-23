using Logic.DAL;
using Logic.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Logic.Database;


namespace Logic.Services
{
    // Klass som tar hand om logiken för användare
    public class UserService
    {
        private UserDataAccess<User> _dbUsers;
        //private readonly string UsersPath = @"Users.json";

        public UserService()
        {
            _dbUsers = new UserDataAccess<User>();
        }
        public bool Regexx(string username)
        {
            string emailPattern = @"^([\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$";
            if (!Regex.IsMatch(username, emailPattern))
            {
                return false;
            }
            return true;
        }

        public User GetAssignedUserFromMechanic(Mechanic mechanic)
        {
<<<<<<< HEAD
            return _dbUsers.LoadList().FirstOrDefault(user => user.ID.Equals(mechanic.UserID));
=======
            return _dbUsers.LoadList().FirstOrDefault(user => user.MechanicID.Equals(mechanic.ID));
>>>>>>> parent of 9734d64... Buggfix
        }

        public string CreateAndSaveUser(string userName, string password)
        {
            var user = new User()
            {
                Username = userName,
                Password = password,
                IsAdmin = false,
                
            };
            db.Users.Add(user);
            _dbUsers.SaveList(db.Users);

            return user.ID;
        }
        public void RemoveUser(User user)
        {
            db.Users.Remove(db.Users.FirstOrDefault(x => x.ID == user.ID));
            _dbUsers.SaveList(db.Users);
        }
    }
    public class NoUserSelectedException : Exception
    {
        public string UserMessage { get; }

        public NoUserSelectedException() { }

        public NoUserSelectedException(string message)
            : base(message) { }

        public NoUserSelectedException(string message, Exception inner)
            : base(message, inner) { }

        public NoUserSelectedException(string message, string userMessage)
            : this(message)
        {
            UserMessage = userMessage;
        }
    }
}
