using System;
using System.Drawing;
using System.Linq;

namespace PowerChart.Line
{
    public sealed class LineSeries : DataSeries
    {
        public LineSeries(Pen pen)
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
                /*
                Point previous = scale(DataPoints.FirstOrDefault(new Point(0, 0)));
                foreach (var point in DataPoints)
                {
                    var chartPoint = scale(point);
                    g.DrawLine(Pen, previous, chartPoint);
                    previous = chartPoint;
                }
                */
                g.DrawLines(Pen, DataPoints.OrderBy(point => point.X).Select(point => scale(point)).ToArray());
            }
        }
    }
}
