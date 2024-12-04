using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranRoutes.Infrastructure;
using TranRoutes.Services;
using TranRoutes.Utils;

namespace TranRoutesUnitTest
{
    public class TestNumberOfTripsByDistance
    {
        readonly INumberOfTripsByDistance _svc;
        public TestNumberOfTripsByDistance()
        {
            var services = new ServiceCollection();
            services.AddTransient<FileReader>();
            services.AddScoped<INumberOfTripsByDistance, NumberOfTripsByDistance>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            var serviceProvider = services.BuildServiceProvider();

            _svc = serviceProvider.GetService<INumberOfTripsByDistance>()!;
        }

        [Fact]
        public void Test10()
        {
            var result = _svc.Get("C", "C", 30);
            Assert.True(result == 7);
        }
    }
}
