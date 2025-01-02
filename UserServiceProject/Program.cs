using ApplicationLayer.gRPC;
using DataAccessLayer.Entities;
using UserServiceProject;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.Configure<OptionConnectionManager>(
            builder.Configuration.GetSection(OptionConnectionManager.Name));
        builder.Services.AddUserServicesProjectDI();
        // Add services to the container.
        builder.Services.AddGrpc();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<UserServiceImpl>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}