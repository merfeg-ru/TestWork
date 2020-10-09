using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string PhoneNumber { get; set; }
        string EMail { get; set; }
    }
}
