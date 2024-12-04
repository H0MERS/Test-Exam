using TranRoutes.Domains;
using TranRoutes.Infrastructure;

namespace TranRoutes.Services
{
    public interface IShortestPathService
    {
        int Find(string source, string destination);
    }
    public class ShortestPathService : IShortestPathService
    {
        readonly IRouteRepository _repo;
        public ShortestPathService(IRouteRepository repo)
        {
            _repo = repo;
        }
        ICollection<Route> _records = new HashSet<Route>();

        HashSet<(string Path, int Distance)> toDestination = new HashSet<(string, int)>();
        HashSet<(string Path, int Distance)> shortestPath = new();
        public int Find(string source, string destination)
        {
            _records = _repo.Get();
            toDestination.Add((source, 0));
            Analyze(source, destination);
            return shortestPath.Min(m => m.Distance);

        }

        void Analyze(string current, string destination)
        {
            foreach (var route in _records.Where(w => w.Source == current))
            {
                var sort = route.NearList.OrderBy(o => o.Distance);

                foreach (var near in sort)
                {
                    toDestination.Add((near.Destination, near.Distance));
                    int distance = toDestination.Sum(s => s.Distance);
                    string p = String.Join("=>", toDestination.Select(s => s.Path));
                    var newPath = (p, distance);
                    if ((near.Destination == destination))
                    {
                        shortestPath.Add(newPath);
                        return;
                    }
                    if (!shortestPath.Contains(newPath))
                        Analyze(near.Destination, destination);

                }
            }
        }


    }
}
