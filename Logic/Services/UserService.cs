﻿using Logic.DAL;
using Logic.Database;
using Logic.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Logic.Services
{
    // Klass som tar hand om logiken för användare
    public class UserService
    {
        private UserDataAccess<User> _dbUsers;

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
            return _dbUsers.LoadList().FirstOrDefault(user => user.ID.Equals(mechanic.UserID));
        }

        public string CreateAndSaveUser(string username, string password)
        {
            var user = new User()
            {
                Username = username,
                Password = password,
                IsAdmin = false
            };

            db.Users.Add(user);
            _dbUsers.SaveList(db.Users);

            return user.ID;
        }
    }
}
