using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PSchool.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
