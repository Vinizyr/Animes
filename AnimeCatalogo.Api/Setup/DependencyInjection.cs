using AnimeCatalogo.Application.IService;
using AnimeCatalogo.Application.Service;
using AnimeCatalogo.Infrastructure.IRepository;
using AnimeCatalogo.Infrastructure.Repository;

namespace AnimeCatalogo.Api.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAnimeService, AnimeService>();
            services.AddScoped<IAnimeRepository, AnimeRepository>();
            
        }
    }
}
