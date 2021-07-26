using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileReader
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public string GetPoints()
        {
            return string.Format("{0};{1};{2}",X,Y,Z);
        }

        public double magnitude()
        {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        }
    }
}