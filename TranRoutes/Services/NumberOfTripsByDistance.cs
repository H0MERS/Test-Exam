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
        HashSet<(string Path, int Distance)> toDestination = new();

        string _source = "";
        string _destination = "";
        int _distance = 0;

        HashSet<(string Path, int Distance)> _result = new();
        HashSet<(string Path, int Distance, int Stops)> _routing = new();
        HashSet<(string Value, int Distance)> _visited = new();
        public NumberOfTripsByDistance(IRouteRepository repo)
        {
            _repo = repo;
        }
        public int Get(string source, string destination, int distance)
        {
            _source = source;
            _destination = destination;
            _distance = distance;
            _records = _repo.Get();

            var result = new List<(string Path, int Distance)>();
            Find(source, 0, new List<(string Path, int Distance)>(), result);
            return 0;
        }


        void Find(string source, int distance, List<(string Path, int Distance)> currentRoute, List<(string Path, int Distance)> r)
        {
            var current = (source, distance);

            if (_visited.Contains(current))
            {
                string path = String.Join("=>", currentRoute.Select(s => s.Path));
                int totalDistance = currentRoute.Sum(s => s.Distance);
                int stops = currentRoute.Count() - 1;
                _routing.Add((path, totalDistance, stops));
                return;
            }

            _visited.Add(current);
            currentRoute.Add(current);

            var record = _records.SingleOrDefault(f => f.Source == source);
            if (record == null)
            {
                //r.AddRange(currentRoute);
                return;
            }
            else
            {
                foreach (var item in record.NearList)
                {
                    Find(item.Destination, item.Distance, currentRoute, r);
                }
            }

            currentRoute.RemoveAt(currentRoute.Count - 1);
            _visited.Remove(current);

        }
        //void Find(string source, int distance)
        //{
        //    var current = (source, distance);
        //    toDestination.Add(current);
        //    int sumDistance = toDestination.Sum(s => s.Distance);
        //    string p = String.Join("=>", toDestination.Select(s => s.Path));
        //    var newPath = (p, sumDistance);
        //    if (_routing.Contains(newPath))
        //        return;
        //    _routing.Add(newPath);

        //    var route = _records.SingleOrDefault(f => f.Source == source);
        //    if (route == null) return;
        //    foreach (var near in route.NearList)
        //        Find(near.Destination, near.Distance, r);
        //}

        //void Analayze(string current)
        //{
        //    var record = _records.SingleOrDefault(w => w.Source == current);
        //    if (record == null) return;

        //    foreach (var near in record.NearList)
        //    {
        //        toDestination.Add((near.Destination, near.Distance));
        //        int sumDistance = toDestination.Sum(s => s.Distance);
        //        string p = String.Join("=>", toDestination.Select(s => s.Path));
        //        var newPath = (p, sumDistance);
        //        if (sumDistance > _distance)
        //        {
        //            toDestination.Clear();
        //            toDestination.Add((_source, 0));
        //            continue;
        //        }
        //        if (_routing.Contains(newPath))
        //        {
        //            return;
        //        }

        //        _routing.Add(newPath);
        //        Analayze(near.Destination);

        //        //if ((near.Destination == _destination) && (sumDistance <= _distance))
        //        //{
        //        //    if (_result.Contains(newPath)) return;
        //        //    _result.Add(newPath);
        //        //}
        //        //if (sumDistance > _distance)
        //        //{
        //        //    toDestination.Clear();
        //        //    toDestination.Add((_source, 0));
        //        //    return;
        //        //}
        //        //Analayze(near.Destination);
        //    }
        //}
    }
}
