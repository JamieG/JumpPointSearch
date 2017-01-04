using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JPS
{
    public class Pathfinder
    {
        public static readonly double Sqrt2 = Math.Sqrt(2);
        private readonly GridLocation _goal;
        private readonly Grid _grid;

        private readonly FastPriorityQueue<PathingNode> _open;
        private readonly GridLocation _start;

        public Pathfinder(GridLocation start, GridLocation goal, Grid grid)
        {
            _start = start;
            _goal = goal;
            _grid = grid;

            _open = new FastPriorityQueue<PathingNode>(1000);
        }

        public List<GridLocation> FindPath()
        {
            var startNode = new PathingNode(_start) {F = 0, G = 0, Opened = true};

            _open.Enqueue(startNode, startNode.F);

            while (_open.Count != 0)
            {
                PathingNode node = _open.Dequeue();

                node.Closed = true;

                if (node.Location == _goal)
                    return Trace(node);

                IdentitySuccessors(node);
            }

            return null;
        }

        private List<GridLocation> Trace(PathingNode node)
        {
            var path = new List<GridLocation> {node.Location};
            while (node.Parent != null)
            {
                node = node.Parent;
                path.Add(node.Location);
            }
            path.Reverse();
            return path;
        }

        private void IdentitySuccessors(PathingNode node)
        {
            foreach (PathingNode neighbour in _grid.Neighbours(node))
            {
                GridLocation jumpPoint = Jump(neighbour.Location, node.Location);
                if (jumpPoint != GridLocation.Empty)
                {
                    PathingNode jumpNode = _grid[jumpPoint];

                    if (jumpNode.Closed)
                        continue;

                    double d = Heuristic(Math.Abs(jumpPoint.X - node.Location.X), Math.Abs(jumpPoint.Y - node.Location.Y));
                    double ng = node.G + d;

                    if (!jumpNode.Opened || ng < jumpNode.G)
                    {
                        jumpNode.G = ng;
                        if (!jumpNode.H.HasValue)
                            jumpNode.H = Heuristic(Math.Abs(jumpPoint.X - _goal.X), Math.Abs(jumpPoint.Y - _goal.Y));
                        jumpNode.F = jumpNode.G + jumpNode.H.Value;
                        jumpNode.Parent = node;

                        if (!jumpNode.Opened)
                        {
                            _open.Enqueue(jumpNode, jumpNode.F);
                            jumpNode.Opened = true;
                        }
                        else
                        {
                            _open.UpdatePriority(jumpNode, jumpNode.F);
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double Heuristic(int dx, int dy)
        {
            return dx < dy ? (Sqrt2 - 1)*dx + dy : (Sqrt2 - 1)*dy + dx;
        }

        public GridLocation Jump(GridLocation current, GridLocation proposed)
        {
            int x = current.X;
            int y = current.Y;
            int dx = current.X - proposed.X;
            int dy = current.Y - proposed.Y;

            if (!_grid.IsNavigable(x, y))
                return GridLocation.Empty;

            if (_goal == current)
                return current;

            // Diagonal
            if (dx != 0 && dy != 0)
            {
                if ((_grid.IsNavigable(x - dx, y + dy) && !_grid.IsNavigable(x - dx, y)) ||
                    (_grid.IsNavigable(x + dx, y - dy) && !_grid.IsNavigable(x, y - dy)))
                    return current;

                if (Jump(new GridLocation(x + dx, y), current) != GridLocation.Empty ||
                    Jump(new GridLocation(x, y + dy), current) != GridLocation.Empty)
                    return current;
            }
            // Cardinal
            else
            {
                if (dx != 0)
                {
                    // Horizontal
                    if ((_grid.IsNavigable(x + dx, y + 1) && !_grid.IsNavigable(x, y + 1)) ||
                        (_grid.IsNavigable(x + dx, y - 1) && !_grid.IsNavigable(x, y - 1)))
                        return current;
                }
                else
                {
                    // Vertical
                    if ((_grid.IsNavigable(x + 1, y + dy) && !_grid.IsNavigable(x + 1, y)) ||
                        (_grid.IsNavigable(x - 1, y + dy) && !_grid.IsNavigable(x - 1, y)))
                        return current;
                }
            }

            if (_grid.IsNavigable(x + dx, y) || _grid.IsNavigable(x, y + dy))
                return Jump(new GridLocation(x + dx, y + dy), current);

            return GridLocation.Empty;
        }
    }
}