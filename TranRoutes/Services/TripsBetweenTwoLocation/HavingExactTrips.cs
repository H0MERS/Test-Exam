namespace TranRoutes.Services.TripsBetweenTwoLocation
{
    public class HavingExactTrips : BaseGetTrips
    {

        public override int Get(string source, string destination, int stops)
        {
            Find(source, destination, stops);
            return _trips.Count;
        }

        void Find(string source, string destination, int stops)
        {
            tracingRoute.Add(source);

            string path = String.Join("=>", tracingRoute);
            int len = tracingRoute.Count - 1;

            if ((source == destination) && (len == stops) && (tracingRoute.Count > 1))
            {
                _trips.Add(path);
            }

            foreach (var record in _records.Where(w => w.Source == source))
            {
                if (len <= stops)
                    Find(record.Destination, destination, stops);
            }

            tracingRoute.RemoveAt(tracingRoute.Count - 1);
        }

       
    }
}
