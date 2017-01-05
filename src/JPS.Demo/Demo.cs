using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace JPS.Demo
{
    public sealed partial class Demo : Form
    {
        private bool[,] _map;

        public Demo()
        {
            InitializeComponent();
            DoubleBuffered = true;

            CreateGrid(this, null);
        }

        private void CreateGrid(object sender, EventArgs e)
        {
            var rnd = new Random();

            ctlMap.ClearCells();
            ctlMap.ClearPath();

            _map = new bool[ctlMap.Cols, ctlMap.Rows];
            for (var x = 0; x <= _map.GetUpperBound(0); x++)
                for (var y = 0; y < _map.GetUpperBound(1); y++)
                {
                    _map[x, y] = rnd.Next(0, 500) <= 490;
                    if (!_map[x, y])
                        ctlMap.SetCell(new GridLocation(x, y), Color.Bisque);
                }
        }

        private void LoadImage(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            ctlMap.ClearCells();
            ctlMap.ClearPath();

            using (var mazeImage = (Bitmap) Image.FromFile(openFileDialog.FileName))
            {
                ctlMap.Cols = mazeImage.Width;
                ctlMap.Rows = mazeImage.Height;
                _map = new bool[ctlMap.Cols, ctlMap.Rows];
                for (var x = 0; x <= _map.GetUpperBound(0); x++)
                    for (var y = 0; y < _map.GetUpperBound(1); y++)
                    {
                        Color c = mazeImage.GetPixel(x, y);

                        Int32 gs = (Int32) (c.R*0.3 + c.G*0.59 + c.B*0.11);
                        
                        _map[x, y] =  gs > 200;
                        if (!_map[x, y])
                            ctlMap.SetCell(new GridLocation(x, y), Color.Coral);
                    }
            }
        }

        private GridLocation _start = GridLocation.Empty;
        private GridLocation _goal =  GridLocation.Empty;

        private void FindPath(object sender, EventArgs e)
        {
            if (_start == GridLocation.Empty)
            {
                MessageBox.Show(@"Please select a Start location!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (_goal == GridLocation.Empty)
            {
                MessageBox.Show(@"Please select a Goal location!", Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            var grid = new Grid(_map);

            ctlMap.ClearPath();

            var pathfinder = new Pathfinder(_start, _goal, grid);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            List<GridLocation> path = pathfinder.FindPath();
            stopwatch.Stop();
            if (path != null)
            {
                ctlMap.SetPath(path);
                MessageBox.Show($"Path Found, Nodes {path.Count} in {stopwatch.ElapsedMilliseconds}ms", "Pathing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No path found!", "Pathing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void CellMouseDown(object sender, CellMouseDownEventArgs e)
        {
            if (rdoWall.Checked)
            {
                _map[e.Location.X, e.Location.Y] = !_map[e.Location.X, e.Location.Y];

                if (_map[e.Location.X, e.Location.Y])
                    ctlMap.ClearCell(e.Location);
                else
                    ctlMap.SetCell(e.Location, Color.Gray);
            }
            else if (rdoGoal.Checked)
            {
                ctlMap.Goal = e.Location;
                _goal = e.Location;

            }
            else if (rdoStart.Checked)
            {
                ctlMap.Start = e.Location;
                _start = e.Location;
            }

        }
    }
}