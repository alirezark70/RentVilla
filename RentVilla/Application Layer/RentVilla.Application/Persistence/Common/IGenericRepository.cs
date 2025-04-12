using RentVilla.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Persistence.Common
{
    public interface IGenericRepository<TEntitiy,TIdentity> where TEntitiy : ICommonEntity
    {
        Task<TEntitiy> GetByIdAsync(int id);
        Task<IReadOnlyList<TEntitiy>> GetAll();

        Task UpdateAsync(TEntitiy tentitiy);

        Task DeleteByIdAsync(int id);
    }
}
