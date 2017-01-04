using System;

namespace JPS
{
    public struct StepDirection
    {
        public readonly int X;
        public readonly int Y;
        public readonly double Cost;
        public readonly bool IsDiagonal;

        public StepDirection(int x, int y)
        {
            X = x;
            Y = y;

            IsDiagonal = x != 0 && y != 0;
            Cost = IsDiagonal ? Math.Sqrt(2) : 1;
        }
    }
}