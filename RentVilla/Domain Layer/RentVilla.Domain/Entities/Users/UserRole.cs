using RentVilla.Domain.Entities.Common;

namespace RentVilla.Domain.Entities.Users
{
    public class UserRole : CommonEntity<int>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public  User User { get; set; }
        public  Role Role { get; set; }
    }
}
