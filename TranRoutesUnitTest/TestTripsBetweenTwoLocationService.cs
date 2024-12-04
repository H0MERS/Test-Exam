using Microsoft.Extensions.DependencyInjection;
using TranRoutes.Infrastructure;
using TranRoutes.Services;
//using TranRoutes.Services.TripsBetweenTwoLocation;
using TranRoutes.Utils;

namespace TranRoutesUnitTest
{
    public class TestTripsBetweenTwoLocationService
    {
        //readonly ITripsBetweenTwoLocationService _svc;
        readonly INumberOfTripsByDistance _numberOfTripsByDistanceSvc;
        public TestTripsBetweenTwoLocationService()
        {
            var services = new ServiceCollection();
            services.AddTransient<FileReader>();
            services.AddScoped<INumberOfTripsByDistance, NumberOfTripsByDistance>();
            //services.AddScoped<ITripsBetweenTwoLocationService, TripsBetweenTwoLocationService>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            var serviceProvider = services.BuildServiceProvider();

            //_svc = serviceProvider.GetService<ITripsBetweenTwoLocationService>()!;
            _numberOfTripsByDistanceSvc = serviceProvider.GetService<INumberOfTripsByDistance>();
        }

        [Fact]
        public void Test6()
        {
            //var result = _svc.Get<HavingMaximumTrips>("C", "C", 3);
            ////var result = _svc.Get("C", "C", f => f.Where(w => w.Length < 4).Count());
            //Assert.True(result == 2);
        }

        [Fact]
        public void Test7()
        {
            //var result = _svc.Get<HavingExactTrips>("A", "C", 4);
            ////var result = _svc.Get("A", "C", f => f.Where(w => w.Length == 4).Count());
            //Assert.True(result == 3);
        }

        [Fact]
        public void Test10()
        {
            //var result = _svc.Get<HavingMaximumTrips>("C", "C", 30);
            var result = _numberOfTripsByDistanceSvc.Get("C", "C", 30);
            Assert.True(result == 7);
        }
    }
}
