using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSchool.Application.Contracts;
using PSchool.Persistence.Data;
using PSchool.Persistence.Repositories;

namespace PSchool.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SchoolDbContext>(options => options.UseSqlite("Data Source=PSChool.db", b => b.MigrationsAssembly("PSchool.Presentation.WepApi")));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IParentRepository, ParentRepository>();

        return services;
    }
}
