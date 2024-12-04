// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TranRoutes.Infrastructure;
using TranRoutes.Services;
using TranRoutes.Utils;


var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<FileReader>();
        services.AddScoped<INumberOfTripsByDistance, NumberOfTripsByDistance>();
        services.AddScoped<IShortestPathService, ShortestPathService>();
        services.AddScoped<ICalculateForDistanceByRouteService, CalculateForDistanceByRouteService>();
        services.AddScoped<IRouteRepository, RouteRepository>();
    })
    .ConfigureLogging(l =>
    {
        l.ClearProviders();
        l.AddEventLog();
    });


IHost host = builder.Build();
await host.RunAsync();