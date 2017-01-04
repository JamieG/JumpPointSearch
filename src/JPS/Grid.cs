using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JPS
{
    public class Grid
    {
        private static readonly StepDirection[] Directions =
        {
            // Cardinal
            new StepDirection(-1, 0), // W
            new StepDirection(1, 0), // E
            new StepDirection(0, 1), // N 
            new StepDirection(0, -1), // S
            // Diagonal
            new StepDirection(-1, -1), // NW
            new StepDirection(-1, 1), // SW
            new StepDirection(1, -1), // NE
            new StepDirection(1, 1) // SE
        };

        private readonly int _boundsMaxX;
        private readonly int _boundsMaxY;

        private readonly int _boundsMinX;
        private readonly int _boundsMinY;

        private readonly PathingNode[,] _grid;

        public Grid(int width, int height)
        {
            _boundsMinX = 0;
            _boundsMaxX = width - 1;
            _boundsMinY = 0;
            _boundsMaxY = height - 1;

            // Initialise the Grid
            _grid = new PathingNode[width, height];
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    _grid[x, y] = new PathingNode(x, y) {IsNavigable = true};
        }

        public PathingNode this[int x, int y] { get { return _grid[x, y]; } }
        public PathingNode this[GridLocation location] { get { return _grid[location.X, location.Y]; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNavigable(int x, int y)
        {
            return InBounds(x, y) && this[x, y].IsNavigable;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool InBounds(int x, int y)
        {
            return x >= _boundsMinX && x <= _boundsMaxX &&
                   y >= _boundsMinY && y <= _boundsMaxY;
        }

        public IEnumerable<PathingNode> Neighbours(PathingNode node)
        {
            if (node.Parent != null)
            {
                GridLocation n = node.Location;
                GridLocation p = node.Parent.Location;

                GridLocation dNorm = new GridLocation(
                    (n.X - p.X)/Math.Max(Math.Abs(n.X - p.X), 1),
                    (n.Y - p.Y)/Math.Max(Math.Abs(n.Y - p.Y), 1));

                // Diagonal
                if (dNorm.X != 0 && dNorm.Y != 0)
                {
                    if (IsNavigable(n.X, n.Y + dNorm.Y))
                        yield return _grid[n.X, n.Y + dNorm.Y];

                    if (IsNavigable(n.X + dNorm.X, n.Y))
                        yield return _grid[n.X + dNorm.X, n.Y];

                    if (IsNavigable(n.X, n.Y + dNorm.Y) || IsNavigable(n.X + dNorm.X, n.Y))
                        yield return _grid[n.X + dNorm.X, n.Y + dNorm.Y];

                    if (!IsNavigable(n.X - dNorm.X, n.Y) && IsNavigable(n.X, n.Y + dNorm.Y))
                        yield return _grid[n.X - dNorm.X, n.Y + dNorm.Y];

                    if (!IsNavigable(n.X, n.Y - dNorm.Y) && IsNavigable(n.X + dNorm.X, n.Y))
                        yield return _grid[n.X + dNorm.X, n.Y - dNorm.Y];
                }
                // Cardinal
                else
                {
                    if (dNorm.X == 0)
                    {
                        if (IsNavigable(n.X, n.Y + dNorm.Y))
                        {
                            yield return _grid[n.X, n.Y + dNorm.Y];

                            if (!IsNavigable(n.X + 1, n.Y))
                                yield return _grid[n.X + 1, n.Y + dNorm.Y];

                            if (!IsNavigable(n.X - 1, n.Y))
                                yield return _grid[n.X - 1, n.Y + dNorm.Y];
                        }
                    }
                    else if (IsNavigable(n.X + dNorm.X, n.Y))
                    {
                        yield return _grid[n.X + dNorm.X, n.Y];

                        if (!IsNavigable(n.X, n.Y + 1))
                            yield return _grid[n.X + dNorm.X, n.Y + 1];

                        if (!IsNavigable(n.X, n.Y - 1))
                            yield return _grid[n.X + dNorm.X, n.Y - 1];
                    }
                }
            }
            else
            {
                for (var i = 0; i < Directions.Length; i++)
                {
                    int propX = node.Location.X + Directions[i].X;
                    int propY = node.Location.Y + Directions[i].Y;

                    if (IsNavigable(propX, propY))
                    {
                        yield return this[propX, propY];
                    }
                }
            }
        }
    }
}