using RentVilla.Domain.Entities.Common;

namespace RentVilla.Domain.Entities.Users
{
    public class Permission : CommonEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
