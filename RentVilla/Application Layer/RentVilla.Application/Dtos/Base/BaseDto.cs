using RentVilla.Application.Common.Mapper;
using RentVilla.Application.Dtos.Users;
using RentVilla.Application.Extensions.Mapper;
using RentVilla.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RentVilla.Application.Dtos.Base
{
    
    public class BaseDto<TIdentity> : IBaseDto where TIdentity :struct
    {
        public TIdentity Id { get; set; }
    }

    public interface IBaseDto { }


  
}
