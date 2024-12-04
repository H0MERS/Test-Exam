using TranRoutes.Domains;
using TranRoutes.Infrastructure;
using TranRoutes.Services.TripsBetweenTwoLocation;

namespace TranRoutes.Services
{
    public interface ITripsBetweenTwoLocationService
    {
        int Get<T>(string source, string destination, int trip) where T : BaseGetTrips, new();
        int Get(string source, string destination, Func<int,bool> func);
    }
    public class TripsBetweenTwoLocationService : ITripsBetweenTwoLocationService
    {
        readonly IRouteRepository _repo;

        ICollection<Route> _records = new HashSet<Route>();
        List<string> _trips = new List<string>();
        public TripsBetweenTwoLocationService(IRouteRepository repo)
        {
            _repo = repo;
        }



        public int Get<T>(string source, string destination, int stops) where T : BaseGetTrips, new()
        {
            T trips = new T() { _records = _repo.Get() };            
            return trips.Get(source, destination, stops);
        }

        public int Get(string source, string destination, Func<int, bool> func)
        {
            _records = _repo.Get();
            Find(source, destination, func, new List<string>());
            return _trips.Count;
        }

        void Find(string source, string destination, Func<int, bool> func, List<string> tracingRoute)
        {
            tracingRoute.Add(source);

            string path = String.Join("=>", tracingRoute);
            int len = tracingRoute.Count - 1;
            
            if ((source == destination) && func(len) && (tracingRoute.Count > 1))
            {
                _trips.Add(path);
            }

            foreach (var record in _records.Where(w=> w.Source == source))
            {
                if(func(len))
                    Find(record.Destination,destination,func, tracingRoute);
            }

            tracingRoute.RemoveAt(tracingRoute.Count-1);
        }
    }








}
