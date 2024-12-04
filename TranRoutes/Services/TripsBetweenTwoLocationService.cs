using TranRoutes.Domains;
using TranRoutes.Infrastructure;
using TranRoutes.Services.TripsBetweenTwoLocation;

namespace TranRoutes.Services
{
    public interface ITripsBetweenTwoLocationService
    {
        int Get<T>(string source, string destination, int trip) where T : BaseGetTrips, new();
        //int Get(string source, string destination, Func<HashSet<(string Path, int Length)>, int> func);
    }
    public class TripsBetweenTwoLocationService : ITripsBetweenTwoLocationService
    {
        readonly IRouteRepository _repo;
        IDictionary<string, int> _routes = new Dictionary<string, int>();

        ICollection<Route> _records = new HashSet<Route>();
        HashSet<(string Path, int Distance)> toDestination = new();
        HashSet<(string Path, int Length)> path = new();

        public TripsBetweenTwoLocationService(IRouteRepository repo)
        {
            _repo = repo;
        }



        public int Get<T>(string source, string destination, int trip) where T : BaseGetTrips, new()
        {
            T trips = new T() { _records = _repo.Get() };
            trips.AnalysTrips(source, destination, source, trip, (path, trip) =>
            {
                _routes.Add(path, trip);
            });

            return _routes.Count();
        }


        //public int Get<T>(string source, string destination, int trip) where T : BaseGetTrips, new()
        //public int Get2(string source, string destination, Func<HashSet<(string Path, int Length)>, int> func)
        //{
        //    _records = _repo.Get();
        //    toDestination.Add((source, 0));
        //    Analyze(source, source, destination);
        //    return func(path);
        //}

        //void Analyze(string current, string source, string destination)
        //{
        //    foreach (var route in _records.Where(w => w.Source == current))
        //    {
        //        foreach (var near in route.NearList)
        //        {

        //            toDestination.Add((near.Destination, near.Distance));
        //            var p = String.Join("=>", toDestination.Select(s => s.Path));
        //            var l = p.Split("=>").Length - 1;
        //            var newPath = (p, l);
        //            if (near.Destination == destination)
        //            {
        //                path.Add(newPath);
        //                toDestination.Clear();
        //                toDestination.Add((source, 0));
        //                return;
        //            }

        //            if (!path.Contains(newPath))
        //                Analyze(near.Destination, source, destination);
        //        }
        //    }
        //}



    }








}
