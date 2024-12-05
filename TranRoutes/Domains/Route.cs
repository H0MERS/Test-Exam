namespace TranRoutes.Domains
{

    public class Route
    {
        public string Source { get; private set; }
        public string Destination { get; private set; }
        public int Distance { get; private set; }
        public Route(string source, string destination, int distance)
        {
            Source = source;
            Destination = destination;
            Distance = distance;
        }
    }

}
