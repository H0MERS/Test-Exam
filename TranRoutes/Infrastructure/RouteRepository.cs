using TranRoutes.Domains;
using TranRoutes.Utils;

namespace TranRoutes.Infrastructure
{
    public interface IRouteRepository
    {
        HashSet<Route> Get();
    }
    public class RouteRepository : IRouteRepository
    {
        readonly FileReader _reader;
        HashSet<Route> _routes = new();
        public RouteRepository(FileReader reader)
        {
            _reader = reader;
        }
        public HashSet<Route> Get()
        {
            if (_routes.Any()) return _routes;

            string file = AppContext.BaseDirectory + "/Assets/Input.txt";
            string content = _reader.ReadAsString(file);
            if (!string.IsNullOrWhiteSpace(content))
            {
                var path = content.Split(Environment.NewLine, StringSplitOptions.None)
                    .Select(s =>
                    {
                        var sp = s.Split(",");
                        return (from: sp[0], to: sp[1], distance: Convert.ToInt32(sp[2]));
                    });

                _routes = InitializeConnections(path);

            }
            return _routes;
        }

        HashSet<Route> InitializeConnections(IEnumerable<(string from, string to, int distance)> path)
        {
            var connections = new HashSet<Route>();
            foreach (var item in path)
            {
                if (!connections.Any(a => a.Source == item.from))
                    connections.Add(new Route(item.from));

                connections.First(f => f.Source == item.from).AddNear(item.to, item.distance);
            }
            return connections;
        }
    }
}
