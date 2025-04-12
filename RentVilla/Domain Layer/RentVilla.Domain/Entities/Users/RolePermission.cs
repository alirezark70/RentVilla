using RentVilla.Domain.Entities.Common;

namespace RentVilla.Domain.Entities.Users
{
    public class RolePermission : CommonEntity<int>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public  Role Role { get; set; }
        public  Permission Permission { get; set; }
    }
}
