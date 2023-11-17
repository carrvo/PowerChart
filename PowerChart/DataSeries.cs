using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace PowerChart
{
    public abstract class DataSeries
    {
        protected IList<Point> DataPoints { get; }
        public Pen Pen { get; set; }

        public Int32 xMin { get; protected set; } = Int32.MaxValue;
        public Int32 xMax { get; protected set; } = Int32.MinValue;
        public Int32 xRange => DataPoints.Count > 1 ? xMax - xMin : 1;
        public Int32 yMin { get; protected set; } = Int32.MaxValue;
        public Int32 yMax { get; protected set; } = Int32.MinValue;
        public Int32 yRange => DataPoints.Count > 1 ? yMax - yMin : 1;

        public DataSeries(Pen pen)
        {
            DataPoints = new List<Point>();
            Pen = pen;
        }

        public abstract void Draw(Graphics g, Func<Point, Point> scale);
    }
}
