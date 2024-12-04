using TranRoutes.Domains;

namespace TranRoutes.Services.TripsBetweenTwoLocation
{
    public abstract class BaseGetTrips
    {
        public ICollection<Route> _records;
        public abstract void AnalysTrips(string source, string destination, string path, int trip, Action<string, int> result);
    }
}
