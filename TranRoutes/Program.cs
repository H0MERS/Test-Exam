// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TranRoutes.Infrastructure;
using TranRoutes.Services;
using TranRoutes.Utils;


var edges = new List<(string, string, int)>
        {
            ("A", "B", 5),
            ("B", "C", 4),
            ("C", "D", 8),
            ("D", "C", 8),
            ("D", "E", 6),
            ("A", "D", 5),
            ("C", "E", 2),
            ("E", "B", 3),
            ("A", "E", 7)
        };

var routes = FindRoutes("C", "C", edges);

Console.WriteLine("Routes from C to C:");
foreach (var route in routes)
{
    Console.WriteLine(string.Join(" -> ", route));
}

static List<List<string>> FindRoutes(string start, string end, List<(string Source, string Destination, int Weight)> edges)
{
    var routes = new List<List<string>>();
    Find(start, end, edges, new List<string> { start }, routes, new HashSet<string> { start });
    return routes;
}

static void Find(
    string current,
    string end,
    List<(string Source, string Destination, int Weight)> edges,
    List<string> currentRoute,
    List<List<string>> routes,
    HashSet<string> visited)
{
    if (current == end && currentRoute.Count > 1)
    {
        // If we reach the end node and it's not the starting point alone, save the route
        routes.Add(new List<string>(currentRoute));
        return;
    }

    foreach (var edge in edges)
    {
        if (edge.Source == current && !visited.Contains(edge.Destination))
        {
            currentRoute.Add(edge.Destination);
            visited.Add(edge.Destination);

            Find(edge.Destination, end, edges, currentRoute, routes, visited);

            currentRoute.RemoveAt(currentRoute.Count - 1); // Backtrack
            visited.Remove(edge.Destination);             // Backtrack
        }
    }
}

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<FileReader>();
        services.AddScoped<INumberOfTripsByDistance, NumberOfTripsByDistance>();
        //services.AddScoped<IShortestPathService, ShortestPathService>();
        //services.AddScoped<ICalculateForDistanceByRouteService, CalculateForDistanceByRouteService>();
        services.AddScoped<IRouteRepository, RouteRepository>();
    })
    .ConfigureLogging(l =>
    {
        l.ClearProviders();
        l.AddEventLog();
    });


IHost host = builder.Build();
await host.RunAsync();