using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JPS.Demo
{
    public partial class Demo : Form
    {
        private Grid _grid;

        public Demo()
        {
            InitializeComponent();
        }

        private void CreateGrid(object sender, EventArgs e)
        {
            _grid = new Grid(1000, 1000);
        }

        private void FindPath(object sender, EventArgs e)
        {
            if (_grid == null)
                return;

            var start = new GridLocation(1, 1);
            var goal = new GridLocation(500, 500);

            var pathfinder = new Pathfinder(start, goal, _grid);
            List<GridLocation> path = pathfinder.FindPath();
        }
    }
}