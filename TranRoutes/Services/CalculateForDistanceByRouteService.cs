﻿using System.ComponentModel;
using TranRoutes.Domains;
using TranRoutes.Infrastructure;

namespace TranRoutes.Services
{
    public interface ICalculateForDistanceByRouteService
    {
        int? Calculate(string path);
    }
    public class CalculateForDistanceByRouteService : ICalculateForDistanceByRouteService
    {
        readonly IRouteRepository _repo;
        HashSet<Route> _records = new();
        List<(string Path, int Distance)> _tracingRoute = new();
        (string Path, int Distance) _route = new();
        HashSet<string> _visited = new();
        bool found = false;
        public CalculateForDistanceByRouteService(IRouteRepository repo)
        {
            _repo = repo;
        }

        public int? Calculate(string path)
        {
            
            _records = _repo.Get();

            string[] spl = path.Split("=>");
            string source = spl.First();
            string destination = spl.Last();
            Find(source,destination,0,path);
            if (found)
                return _route.Distance;
            else
                return null;
        }

        void Find(string source, string destination, int currentDistance, string lookupPath)
        {
            _tracingRoute.Add((source, currentDistance));
            _visited.Add(source);

            string path = String.Join("=>", _tracingRoute.Select(s => s.Path));
            int totalDistance = _tracingRoute.Sum(s => s.Distance);

            if (source == destination && path == lookupPath)
            {
                found = true;
                _route = (path, totalDistance);
                return;
            }

            foreach (var record in _records.Where(rec => rec.Source == source))
            {
                if (!found && !_visited.Contains(record.Destination))
                {
                    Find(record.Destination, destination, record.Distance, lookupPath);
                }
            }

            // Backtrack to explore other paths
            _tracingRoute.RemoveAt(_tracingRoute.Count - 1);
            _visited.Remove(source);
        }

    }
}
