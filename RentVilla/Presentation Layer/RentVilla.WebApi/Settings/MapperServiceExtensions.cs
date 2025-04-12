using Microsoft.Extensions.DependencyInjection;
using RentVilla.Application.Common.Mapper;
using RentVilla.Infrastructure.Mapping;

namespace RentVilla.WebApi.Settings
{
    public static class MapperServiceExtensions
    {
        public static IServiceCollection AddCustomMapper(this IServiceCollection services)
        {
            var mapper = new InternalMapper();

            

            services.AddSingleton<IMapper>(mapper);

            return services;
        }

    }
}