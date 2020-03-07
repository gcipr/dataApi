using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public DateTime DateOfFirstRegistration { get; set; }
        public DateTime DateOfLasLogin { get; set; }
        //public Address Address { get; set; }
    }
}
