using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileReader
{
    public class Point
    {
        //    public double X { get; set; }
        //    public double Y { get; set; }
        //    public double Z { get; set; }
        //}

        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }

        public Point(string x, string y, string z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}