using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JPS.Demo
{
    public sealed partial class Map : UserControl
    {
        private readonly SortedList<int, Brush> _brushes = new SortedList<int, Brush>();

        public readonly Color[,] _cells = new Color[4096, 4096];

        private GridLocation? _selectedCell;

        public event EventHandler<CellMouseDownEventArgs> CellMouseDown;

        public bool DrawGrid { get; set; }

        public int Margin = 4;

        public Map()
        {
            InitializeComponent();

            DoubleBuffered = true;

            MouseMove += Mousing;
            MouseDown += (s, e) => _mouseDown = true;
            MouseUp += (s, e) => _mouseDown = false;
        }

        private bool _mouseDown;
        private GridLocation _LastMouseDownLocation;
        private GridLocation _mouseDownLocation = GridLocation.Empty;
        
        public int Rows { get; set; }
        public int Cols { get; set; }

        public double RowHeight => (Height - Margin*2d)/Rows;
        public double ColWidth => (Width - Margin*2d)/Cols;

        public GridLocation? SelectedCell
        {
            get { return _selectedCell; }
            set
            {
                if (_selectedCell != value)
                {
                    _selectedCell = value;
                    Invalidate();
                }
            }
        }

        public void SetCell(GridLocation location, Color color)
        {
            _cells[location.X, location.Y] = color;
            Invalidate();
        }

        public void ClearCell(GridLocation location)
        {
            _cells[location.X, location.Y] = default(Color);
            Invalidate();
        }

        public void ClearCells()
        {
            for (var x = 0; x <= Cols; x++)
                for (var y = 0; y <= Rows; y++)
                    _cells[x,y] = default(Color);

            Invalidate();
        }

        private void Mousing(object sender, MouseEventArgs e)
        {
            var gridX = (int) (((double) e.X - Margin)/ColWidth);
            var gridY = (int) (((double) e.Y - Margin)/RowHeight);

            if (gridX >= 0 && gridX <= Cols - 1 && gridY >= 0 && gridY <= Rows - 1)
            {
                SelectedCell = new GridLocation(gridX, gridY);

                if (_mouseDown)
                {
                    _mouseDownLocation = SelectedCell.Value;

                    if (_LastMouseDownLocation != _mouseDownLocation)
                    {
                        _LastMouseDownLocation = _mouseDownLocation;
                        OnCellMouseDown(new CellMouseDownEventArgs(_LastMouseDownLocation));
                        _mouseDownLocation = default(GridLocation);
                    }
                }
            }
            else
                _mouseDown = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if (DrawGrid)
            {
                for (var x = 0; x <= Cols; x++)
                    e.Graphics.DrawLine(Pens.DarkGray,
                        (int) (x*ColWidth + Margin), Margin,
                        (int) (x*ColWidth + Margin), (int) (Rows*RowHeight + Margin));

                for (var y = 0; y <= Rows; y++)
                    e.Graphics.DrawLine(Pens.DarkGray, Margin, (int) (y*RowHeight + Margin),
                        (int) (Cols*ColWidth + Margin), (int) (y*RowHeight + Margin));
            }

            for (var x = 0; x <= Cols; x++)
                for (var y = 0; y <= Rows; y++)
                {
                    if (_cells[x, y] != default(Color))
                    {
                        Brush brush;
                        if (_brushes.ContainsKey(_cells[x, y].ToArgb()))
                            brush = _brushes[_cells[x, y].ToArgb()];
                        else
                            _brushes.Add(_cells[x, y].ToArgb(), brush = new SolidBrush(_cells[x, y]));

                        e.Graphics.FillRectangle(brush,
                            (int) (x*ColWidth + Margin),
                            (int) (y*RowHeight + Margin),
                            (int) ColWidth+2, (int) RowHeight+2);
                    }
                }

            if (_path != null && _path.Any())
                for (int i = 1; i < _path.Count; i++)
                    e.Graphics.DrawLine(Pens.Green,
                        (int) (_path[i - 1].X*ColWidth + Margin + (int) (ColWidth/2)), (int) (_path[i - 1].Y*RowHeight + Margin + (int) (RowHeight/2)),
                        (int) (_path[i].X*ColWidth + Margin + (int) (ColWidth/2)), (int) (_path[i].Y*RowHeight + Margin + (int) (RowHeight/2)));

            if (_start != GridLocation.Empty)
            {
                e.Graphics.FillRectangle(Brushes.DarkRed,
                    (int) (_start.X*ColWidth + Margin),
                    (int) (_start.Y*RowHeight + Margin),
                    (int) ColWidth+2, (int) RowHeight+2);
            }

            if (_goal != GridLocation.Empty)
            {
                e.Graphics.FillRectangle(Brushes.DarkGreen,
                    (int) (_goal.X*ColWidth + Margin),
                    (int) (_goal.Y*RowHeight + Margin),
                    (int) ColWidth+2, (int) RowHeight+2);
            }

            if (SelectedCell.HasValue)
            {
                e.Graphics.FillRectangle(Brushes.DodgerBlue,
                    (int) (SelectedCell.Value.X*ColWidth + Margin),
                    (int) (SelectedCell.Value.Y*RowHeight + Margin),
                    (int) ColWidth+2, (int) RowHeight+2);
            }
        }


        private void OnCellMouseDown(CellMouseDownEventArgs e)
        {
            CellMouseDown?.Invoke(this, e);
        }

        private List<GridLocation> _path;

        public void SetPath(List<GridLocation> path)
        {
            _path = path;
            Invalidate();
        }

        public void ClearPath()
        {
            _path = null;
            Invalidate();
        }

        private GridLocation _start;
        private GridLocation _goal;
       public GridLocation Start
        {
            get { return _start; }
            set { _start = value; Invalidate();}
        }

         public GridLocation Goal
        {
            get { return _goal; }
            set { _goal = value; Invalidate();}
        }
    }

    public class CellMouseDownEventArgs : EventArgs
    {
        public GridLocation Location { get; }

        public CellMouseDownEventArgs(GridLocation location)
        {
            Location = location;
        }
    }
}