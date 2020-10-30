using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Logic.Models
{

    //public enum UserPrivilege
    //{
    //    Basic = 1,
    //    Admin = 2
    //}

    public enum VehiclePart
    {
        Kaross = 1,
        Bromsar = 2,
        Motor = 3,
        Hjul = 4,
        Vindruta = 5
    }

    public enum ErrandStatus
    {
        Grön = 1,
        Gul = 2,
        Röd = 3
    }

    public enum CarType
    {
        Sedan = 1,
        Herrgårdsvagn = 2,
        Cabriolet = 3,
        Halvkombi = 4
    }

    public enum Fuel
    {
        Bensin = 1,
        Diesel = 2,
        Elektrisk = 3
    }

}
