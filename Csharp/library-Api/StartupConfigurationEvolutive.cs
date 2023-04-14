using library_domain.IServices;
using library_domain.Services;
using library_infra.IRepositories;
using library_infra.Repositories;

namespace library_Api;

public class StartupConfigurationEvolutive
{
    public static void InjecterServices(IServiceCollection services)
    {
        services.AddScoped<ILibrary, Library>();

        services.AddScoped<IResidentService, ResidentService>();

        services.AddScoped<IStudentService, StudentService>();

    }
    public static void InjecterRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IBookRepository), typeof(BookRepository));
    }

}
