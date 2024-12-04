using Microsoft.Extensions.DependencyInjection;
using TranRoutes.Infrastructure;
using TranRoutes.Services;
using TranRoutes.Services.TripsBetweenTwoLocation;
using TranRoutes.Utils;

namespace TranRoutesUnitTest
{
    public class TestTripsBetweenTwoLocationService
    {
        readonly ITripsBetweenTwoLocationService _svc;
        readonly INumberOfTripsByDistance _numberOfTripsByDistanceSvc;
        public TestTripsBetweenTwoLocationService()
        {
            var services = new ServiceCollection();
            services.AddTransient<FileReader>();
            services.AddScoped<INumberOfTripsByDistance, NumberOfTripsByDistance>();
            services.AddScoped<ITripsBetweenTwoLocationService, TripsBetweenTwoLocationService>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            var serviceProvider = services.BuildServiceProvider();

            _svc = serviceProvider.GetService<ITripsBetweenTwoLocationService>()!;
            _numberOfTripsByDistanceSvc = serviceProvider.GetService<INumberOfTripsByDistance>();
        }

        [Fact]
        public void Test6()
        {
            var result = _svc.Get<HavingMaximumTrips>("C", "C", 3);
            Assert.True(result == 2);

            //var result = _svc.Get("C", "C", f=> f <= 3);
            //Assert.True(result == 2);
        }

        [Fact]
        public void Test7()
        {
            var result = _svc.Get<HavingExactTrips>("A", "C", 4);
            Assert.True(result == 3);

            //var result = _svc.Get("A", "C", f => f == 4);
            //Assert.True(result == 3);
        }
        
    }
}
