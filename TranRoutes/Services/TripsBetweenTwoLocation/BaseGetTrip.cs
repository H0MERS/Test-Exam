using TranRoutes.Domains;

namespace TranRoutes.Services.TripsBetweenTwoLocation
{
    public abstract class BaseGetTrips
    {
        public ICollection<Route> _records = new List<Route>();
        public List<string> _trips = new List<string>();
        protected List<string> tracingRoute = new List<string>();
        public abstract int Get(string source, string destination, int stops);
    }
}
