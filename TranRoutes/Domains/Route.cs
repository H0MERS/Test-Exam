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

    //public class Route
    //{
    //    public string Source { get; private set; }
    //    public HashSet<Near> NearList { get; private set; } = new();
    //    public Route(string source)
    //    {
    //        Source = source;
    //    }

    //    public void AddNear(string destination, int distance)
    //    {
    //        NearList.Add(new Near(destination, distance));
    //    }
    //}
    //public class Near
    //{
    //    public string Destination { get; private set; }
    //    public int Distance { get; private set; }
    //    public Near(string destination, int distance)
    //    {
    //        Destination = destination;
    //        Distance = distance;
    //    }
    //}

}
