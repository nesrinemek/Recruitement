using library_domain.Services;
using library_infra.IRepositories;
using library_infra.Repositories;
using Library_service.IServices;

namespace library_Api;

public class StartupConfigurationEvolutive
{
    public static void InjecterServices(IServiceCollection services)
    {
        services.AddSingleton<ILibrary, Library>();

    }
    public static void InjecterRepositories(IServiceCollection services)
    {
        services.AddSingleton(typeof(IBookRepository), typeof(BookRepository));
    }

}
