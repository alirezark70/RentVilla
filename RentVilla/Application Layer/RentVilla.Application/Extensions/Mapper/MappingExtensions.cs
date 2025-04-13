using RentVilla.Application.Common.Mapper;
using RentVilla.Application.Dtos.Base;
using RentVilla.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Extensions.Mapper
{
    public static class MappingExtensions
    {

        public static TDto? MapToDto<TDto>(this ICommonEntity source, IMapper mapper)
            where TDto : IBaseDto, new()
        {
            if (source == null) return default;
            return mapper.Map<ICommonEntity, TDto>(source);
        }

        public static TEntity? MapToEntity<TEntity>(this IBaseDto source, IMapper mapper)
            where TEntity : ICommonEntity, new()
        {
            if (source == null) return default;
            return mapper.Map<IBaseDto, TEntity>(source);
        }

        public static IEnumerable<IBaseDto>? MapToDtos<IBaseDto>(
            this IEnumerable<ICommonEntity> source,
            IMapper mapper)
        {
            if (source == null) return default;
            return mapper.Map<IEnumerable<ICommonEntity>, IEnumerable<IBaseDto>>(source);
        }

        public static IEnumerable<ICommonEntity>? MapToEntities<ICommonEntity>(
             this IEnumerable<IBaseDto> source,
             IMapper mapper)
        {
            if (source == null) return default;
            return mapper.Map<IEnumerable<IBaseDto>, IEnumerable<ICommonEntity>>(source);
        }
    }
}
