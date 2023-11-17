using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace PowerChart.Scatter
{
    public sealed class ScatterSeries : DataSeries
    {
        public ScatterSeries(Pen pen)
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
                    var position = new Rectangle(chartPoint, new Size(2, 2));
                    g.DrawEllipse(Pen, position);
                }
            }
        }
    }
}
