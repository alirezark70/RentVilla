using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Common.Mapper
{
    public interface IMapperConfiguration
    {
        IMapperConfiguration CreateMap<TSource, TDestination>();
        IMapperConfiguration ForMember<TSource, TDestination>(
            Expression<Func<TDestination, object>> destinationMember,
            Func<TSource, object> memberValue);
    }
}
