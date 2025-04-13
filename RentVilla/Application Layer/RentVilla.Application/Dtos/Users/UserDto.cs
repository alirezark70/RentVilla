using RentVilla.Application.Dtos.Base;
using RentVilla.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Dtos.Users
{
    public class UserDto:BaseDto<int>
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public UserType UserType { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
