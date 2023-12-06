using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


namespace API_MediBridge.Models.Class
{
    public class UserLoggedDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CF { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }

        public UserRoles[] roles { get; set; }
    }
}