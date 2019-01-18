using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace InfraredAnalyze
{
    class Drawing
    {
        public Point[] Draw_Cross(Point point)
        {
            Point[] points = new Point[8];
            Point temp_Point1 = new Point(point.X - 35, point.Y);
            Point temp_Point2 = new Point(point.X - 5, point.Y);
            Point temp_Point3 = new Point(point.X + 5, point.Y);
            Point temp_Point4 = new Point(point.X + 35, point.Y);
            Point temp_Point5 = new Point(point.X, point.Y - 5);
            Point temp_Point6 = new Point(point.X, point.Y - 35);
            Point temp_Point7 = new Point(point.X, point.Y + 5);
            Point temp_Point8 = new Point(point.X, point.Y + 35);
            points[0] = temp_Point1;
            points[1] = temp_Point2;
            points[2] = temp_Point3;
            points[3] = temp_Point4;
            points[4] = temp_Point5;
            points[5] = temp_Point6;
            points[6] = temp_Point7;
            points[7] = temp_Point8;
            return points;
        }

      
    }
}
