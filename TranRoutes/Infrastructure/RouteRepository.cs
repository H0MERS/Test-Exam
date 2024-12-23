﻿using TranRoutes.Domains;
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
                _routes = new HashSet<Route>(content.Split(Environment.NewLine, StringSplitOptions.None)
                    .Select(s =>
                    {
                        var sp = s.Split(",");                        
                        return new Route(sp[0], sp[1], Convert.ToInt32(sp[2]));
                    }));


            }
            return _routes;
        }
    }
}
