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
        HashSet<(string Path, int Distance)> _routing = new();
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

            //var r = _records.SingleOrDefault(w => w.Source == source);
            //if (r == null) return 0;
            //foreach (var n in r.NearList)
            //{
            //    toDestination.Clear();
            //    toDestination.Add((_source, 0));
            //    toDestination.Add((n.Destination, n.Distance));
            //    int sumDistance = toDestination.Sum(s => s.Distance);
            //    string p = String.Join("=>", toDestination.Select(s => s.Path));
            //    var newPath = (p, sumDistance);
            //    _routing.Add(newPath);
            //    Analayze(n.Destination);
            //}

            toDestination.Add((_source, 0));
            Analayze(source);
            return 0;
        }



        void Analayze(string current)
        {
            var record = _records.SingleOrDefault(w => w.Source == current);
            if (record == null) return;

            foreach (var near in record.NearList)
            {
                toDestination.Add((near.Destination, near.Distance));
                int sumDistance = toDestination.Sum(s => s.Distance);
                string p = String.Join("=>", toDestination.Select(s => s.Path));
                var newPath = (p, sumDistance);
                if (sumDistance > _distance)
                {
                    toDestination.Clear();
                    toDestination.Add((_source, 0));
                    continue;
                }
                if (_routing.Contains(newPath))
                {
                    return;
                }

                _routing.Add(newPath);
                Analayze(near.Destination);

                //if ((near.Destination == _destination) && (sumDistance <= _distance))
                //{
                //    if (_result.Contains(newPath)) return;
                //    _result.Add(newPath);
                //}
                //if (sumDistance > _distance)
                //{
                //    toDestination.Clear();
                //    toDestination.Add((_source, 0));
                //    return;
                //}
                //Analayze(near.Destination);
            }
        }
    }
}
