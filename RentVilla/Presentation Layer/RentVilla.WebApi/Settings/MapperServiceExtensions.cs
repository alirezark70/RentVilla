using Microsoft.Extensions.DependencyInjection;
using RentVilla.Application.Common.Mapper;
using RentVilla.Application.Mappings;
using RentVilla.Infrastructure.Mapping;

namespace RentVilla.WebApi.Settings
{
    public static class MapperServiceExtensions
    {
        public static IServiceCollection AddCustomMapper(this IServiceCollection services)
        {
            var mapper = new InternalMapper();


            ApplicationMapingProfile.Configure(mapper);

            services.AddSingleton<IMapper>(mapper);

            return services;
        }

    }
}