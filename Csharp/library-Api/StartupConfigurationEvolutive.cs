using library_domain.IServices;
using library_domain.Services;
using library_infra.IRepositories;
using library_infra.Repositories;

namespace library_Api;

public class StartupConfigurationEvolutive
{
    public static void InjecterServices(IServiceCollection services)
    {
        services.AddSingleton<ILibrary, Library>();

        services.AddSingleton<IResidentService, ResidentService>();

        services.AddSingleton<IStudentService, StudentService>();

    }
    public static void InjecterRepositories(IServiceCollection services)
    {
        services.AddSingleton(typeof(IBookRepository), typeof(BookRepository));
    }

}
