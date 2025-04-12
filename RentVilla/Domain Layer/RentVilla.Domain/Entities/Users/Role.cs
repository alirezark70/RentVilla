using RentVilla.Domain.Entities.Common;

namespace RentVilla.Domain.Entities.Users
{
    public class Role : CommonEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public  ICollection<UserRole> UserRoles { get; set; }
        public  ICollection<RolePermission> RolePermissions { get; set; }
    }
}
