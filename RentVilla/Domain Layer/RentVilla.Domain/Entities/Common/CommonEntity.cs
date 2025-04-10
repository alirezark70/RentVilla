using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Common
{
    public class CommonEntity<TIdentity>: ICommonEntity where TIdentity : struct
    {
        public TIdentity Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }

    public interface ICommonEntity { }
}
