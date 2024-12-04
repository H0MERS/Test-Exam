using Microsoft.Extensions.DependencyInjection;
using TranRoutes.Infrastructure;
using TranRoutes.Services;
using TranRoutes.Utils;

namespace TranRoutesUnitTest
{
    public class TestShortestPath
    {
        //readonly IShortestPathService _svc;
        //public TestShortestPath()
        //{
        //    var services = new ServiceCollection();
        //    services.AddTransient<FileReader>();
        //    services.AddScoped<IShortestPathService, ShortestPathService>();
        //    services.AddScoped<IRouteRepository, RouteRepository>();
        //    var serviceProvider = services.BuildServiceProvider();

        //    _svc = serviceProvider.GetService<IShortestPathService>()!;
        //}

        //[Fact]
        //public void Test8()
        //{
        //    var result = _svc.Find("A", "C");
        //}
        //[Fact]
        //public void Test9()
        //{
        //    var result = _svc.Find("B", "B");
        //}
    }
}
