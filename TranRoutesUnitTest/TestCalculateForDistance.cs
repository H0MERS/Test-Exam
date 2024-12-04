using Microsoft.Extensions.DependencyInjection;
using TranRoutes.Infrastructure;
using TranRoutes.Services;
using TranRoutes.Utils;

namespace TranRoutesUnitTest
{
    public class TestCalculateForDistance
    {
        readonly ICalculateForDistanceByRouteService _svc;
        public TestCalculateForDistance()
        {
            var services = new ServiceCollection();
            services.AddTransient<FileReader>();
            services.AddScoped<ICalculateForDistanceByRouteService, CalculateForDistanceByRouteService>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            var serviceProvider = services.BuildServiceProvider();

            _svc = serviceProvider.GetService<ICalculateForDistanceByRouteService>()!;
        }

        [Fact]
        public void Test1()
        {
            var result = _svc.Calculate("A=>B=>C");
            Assert.True(result != null && result == 9);
        }

        [Fact]
        public void Test2()
        {
            var result = _svc.Calculate("A=>D");
            Assert.True(result != null && result == 5);
        }

        [Fact]
        public void Test3()
        {
            var result = _svc.Calculate("A=>D=>C");
            Assert.True(result != null && result == 13);
        }

        [Fact]
        public void Test4()
        {
            var result = _svc.Calculate("A=>E=>B=>C=>D");
            Assert.True(result != null && result == 22);
        }

        [Fact]
        public void Test5()
        {
            var result = _svc.Calculate("A=>E=>D");
            Assert.True(result != null && result > 0);
        }
    }
}