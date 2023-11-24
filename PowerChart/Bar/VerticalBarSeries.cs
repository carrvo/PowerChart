using System;
using System.Drawing;
using System.Linq;

namespace PowerChart.Bar
{
    public sealed class VerticalBarSeries : DataSeries
    {
        public VerticalBarSeries(Pen pen)
            : base(pen)
        { }

        public void AddPoint(Int32 x, Int32 y) => AddPoint(new Point(x, y));

        public void AddPoint(Point point)
        {
            lock (DataPoints)
            {
                DataPoints.Add(point);

                if (point.X < xMin) xMin = point.X;
                if (point.X > xMax) xMax = point.X;
                if (point.Y < yMin) yMin = point.Y;
                if (point.Y > yMax) yMax = point.Y;
            }
        }

        public override void Draw(Graphics g, Func<Point, Point> scale)
        {
            lock (DataPoints)
            {
                foreach (var point in DataPoints)
                {
                    var chartPoint = scale(point);
                    var axisPoint = scale(new Point(point.X, 0)); // on x axis
                    var barHeight = axisPoint.Y - chartPoint.Y;
                    var position = new Rectangle(chartPoint, new Size(2, barHeight));
                    g.DrawRectangle(Pen, position);
                }
            }
        }
    }
}
