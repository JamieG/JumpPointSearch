using System;

namespace JPS
{
    public struct GridLocation 
    {
        public static GridLocation Empty = new GridLocation(-1, -1);

        public bool Equals(GridLocation other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is GridLocation && Equals((GridLocation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public readonly int X;
        public readonly int Y;

        public GridLocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(GridLocation a, GridLocation b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(GridLocation a, GridLocation b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", X, Y);
        }
    }
}