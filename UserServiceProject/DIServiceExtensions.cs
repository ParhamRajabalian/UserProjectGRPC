using DataAccessLayer;
using DataAccessLayer.Contracts;
using DataAccessLayer.Contracts.Providers;
using DataAccessLayer.Entities;
using DataAccessLayer.Oprations;
using DataAccessLayer.Oprations.Providers;
using UserProjectBusinessLogicLayer.Contracts.Services;
using UserProjectBusinessLogicLayer.Contracts.Validations;
using UserProjectBusinessLogicLayer.Oprations.Services;
using UserProjectBusinessLogicLayer.Oprations.Validations;

namespace UserServiceProject;
public static class DIServiceExtensions
{
    public static IServiceCollection AddUserServicesProjectDI(this IServiceCollection services)
    {
        services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
        services.AddSingleton<DbConnectionManager>();
        services.AddTransient(typeof(DapperRepository<>));
        services.AddTransient(typeof(FileRepository<>));
        services.AddSingleton<DbConnectionManager>();
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserValidator, UserValidator>();
        services.AddSingleton<IDapperProvider, DapperProvider>();
        services.AddScoped(typeof(IRepository<>), typeof(FileRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(DapperRepository<>));
        services.AddGrpc();
        return services;
    }
}
