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
        HashSet<Route> routes = new HashSet<Route>();
        public CalculateForDistanceByRouteService(IRouteRepository repo)
        {
            _repo = repo;
        }

        public int? Calculate(string path)
        {
            string[] paths = path.Split("=>");
            var records = _repo.Get();//.Where(w => w.From == paths[0] || w.To == paths[paths.Length - 1]);

            for (var c = 0; c < paths.Length - 1; c++)
            {
                var stack = new Stack<Route>(records);
                //FindChild(paths[c], paths[c + 1], stack);
            }
            if (routes.Count == (paths.Length - 1))
                return 0; // routes.Sum(s => s.Distance);
            else
                return null;
        }


        void FindChild(string from, string to, Stack<Route> stack)
        {

            //while (stack.Count > 0)
            //{
            //    var route = stack.Pop();
            //    if (route.From == from && route.To == to)
            //    {
            //        routes.Add(route);
            //    }
            //}
        }
    }
}
