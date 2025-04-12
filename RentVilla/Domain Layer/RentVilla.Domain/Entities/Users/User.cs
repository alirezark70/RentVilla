using RentVilla.Domain.Entities.Common;
using RentVilla.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Users
{
    public class User:CommonEntity<int>
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public UserType UserType { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
