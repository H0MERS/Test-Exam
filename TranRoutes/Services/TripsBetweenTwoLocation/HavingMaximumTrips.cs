namespace TranRoutes.Services.TripsBetweenTwoLocation
{
    public class HavingMaximumTrips : BaseGetTrips
    {
        public override void AnalysTrips(string current, string destination, string path, int trip, Action<string, int> result)
        {
            foreach (var r in _records.Where(w => w.Source == current))
            {
                foreach (var n in r.NearList)
                {
                    var p = path + "=>" + n.Destination;
                    var splited = p.Split("=>");
                    var len = splited.Length;
                    if ((n.Destination == destination) && (len <= trip))
                    {
                        result(p, len - 1);
                        return;
                    }
                    AnalysTrips(n.Destination, destination, p, trip, result);

                }
            }
        }
    }
}
