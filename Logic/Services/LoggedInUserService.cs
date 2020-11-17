using Logic.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public static class LoggedInUserService
    {
        public static User User { get; set; }
        public static Mechanic Mechanic { get; set; }
    }
}
