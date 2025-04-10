using RentVilla.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Common.BaseInterfaces
{
    public interface IGenericRepository<TEntitiy,TIdentity> where TEntitiy : ICommonEntity
    {
    }
}
