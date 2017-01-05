namespace JPS
{
    internal class PathingNode : FastPriorityQueueNode
    {
        public PathingNode(int x, int y)
            : this(new GridLocation(x, y))
        {
        }

        public PathingNode(GridLocation location)
        {
            Location = location;
        }

        public GridLocation Location { get; private set; }

        public double? H { get; set; }
        public double F { get; set; }
        public double G { get; set; }
        public bool Opened { get; set; }
        public bool Closed { get; set; }
        //public bool IsNavigable { get; set; }
        public PathingNode Parent { get; set; }
    }
}