using TranRoutes.Domains;
using TranRoutes.Infrastructure;

namespace TranRoutes.Services
{
    public interface IShortestPathService
    {
        int Get(string source, string destination);
    }
    public class ShortestPathService : IShortestPathService
    {
        readonly IRouteRepository _repo;
        public ShortestPathService(IRouteRepository repo)
        {
            _repo = repo;
        }
        ICollection<Route> _records = new HashSet<Route>();
        (string Path, int Distance) _shortestPath = new("",int.MaxValue);
        int _previous = int.MaxValue;
        List<string> _visited = new List<string>();
        public int Get(string source, string destination)
        {
            _records = _repo.Get();
            Find(source, destination, 0, new List<string>());
            return _shortestPath.Distance;

        }

        void Find(string current, string destination, int cumulativeDistance, List<string> visited)
        {
            visited.Add(current);

            if (current == destination && visited.Count > 1)
            {
                if (cumulativeDistance < _previous)
                {
                    _previous = cumulativeDistance;
                    _shortestPath = (string.Join("=>", visited), cumulativeDistance);
                }
            }

            foreach (var record in _records.Where(e => e.Source == current))
            {
                if (!visited.Contains(record.Destination) || record.Destination == destination)
                {
                    Find(record.Destination, destination, cumulativeDistance + record.Distance, new List<string>(visited));
                }
            }
        }

    }
}
