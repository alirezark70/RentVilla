using RentVilla.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Users
{
    public class PersonProfile:CommonEntity<int>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; } 
        public DateTime? BirthDate { get; set; }
        public string ProfilePictureUrl { get; set; }

        public User User { get; set; }
    }
}
