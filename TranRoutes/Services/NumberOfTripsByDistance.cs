using TranRoutes.Domains;
using TranRoutes.Infrastructure;

namespace TranRoutes.Services
{
    public interface INumberOfTripsByDistance
    {
        int Get(string source, string destination, int distance);
    }
    public class NumberOfTripsByDistance : INumberOfTripsByDistance
    {
        readonly IRouteRepository _repo;
        HashSet<Route> _records = new();
        List<(string Path, int Distance)> _tracingRoute = new();
        string _destination = "";
        int _distance = 0;

        HashSet<(string Path, int Distance, int Stops)> _routing = new();
        public NumberOfTripsByDistance(IRouteRepository repo)
        {
            _repo = repo;
        }
        public int Get(string source, string destination, int distance)
        {
            _destination = destination;
            _distance = distance;
            _records = _repo.Get();

            Find(source, 0);
            return 0;
        }

        void Find(string source, int distance)
        {
            _tracingRoute.Add((source, distance));

            string path = String.Join("=>", _tracingRoute.Select(s => s.Path));
            int totalDistance = _tracingRoute.Sum(s => s.Distance);

            if (source == _destination && _tracingRoute.Count > 1 && totalDistance < _distance)
            {
                _routing.Add((path, totalDistance, _tracingRoute.Count - 1));
            }

            foreach (var record in _records.Where(rec => rec.Source == source))
            {
                if (totalDistance + record.Distance < _distance)
                {
                    Find(record.Destination, record.Distance);
                }
            }

            // Backtrack to explore other paths
            _tracingRoute.RemoveAt(_tracingRoute.Count - 1);
        }

    }
}
