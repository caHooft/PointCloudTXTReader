using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FileReader.Core;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;

namespace FileReader.Core
{
    public static class Extension
    {
        // Calculates the shortest distance between any Point given and any line given
        public static double ShortDistance(Point line_point1, Point line_point2, Point point)
        {
            Point AB = new Point((line_point2.X - line_point1.X), (line_point2.Y - line_point1.Y), (line_point2.Z - line_point1.Z));
            Point AC = new Point((point.X - line_point1.X), (point.Y - line_point1.Y), (point.Z - line_point1.Z));
            var area = Extension.CrossProduct(AB, AC).magnitude();
            var CD = area / AB.magnitude();

            return CD;
        }
        // This funciton takes a dictionary and calls SortByDistance
        // The dictionary it gets from sort by distance is sorted on the distance(value of the dictionary)
        // Than this function takes the 10 closest points and returns them
        public static Dictionary<Point,double> GetClosestPoints(Dictionary<Point,double> dictionary, int amount)
        {
            Dictionary<Point, double> sortedPoints = Extension.SortByDistance(dictionary);

            var closestPoints = sortedPoints.Take(amount);

            foreach (var it in closestPoints)
            {
                KeyValuePair<Point, double> myValue = (KeyValuePair<Point, double>)it;
                Console.WriteLine(string.Format("{0}: {1}", myValue.Key.GetPoints(), myValue.Value));
            }

            var closestPointsDictionary = closestPoints.ToDictionary();
            return closestPointsDictionary;
        }

        // Sorts a dictionary of type (key(point),distance(double)) by the distance( the value of the dictionary)
        public static Dictionary<Point, double> SortByDistance(Dictionary<Point, double> distance)
        {
            var myList = distance.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            foreach (var value in myList)
            {
                KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
            }
            
            var sortedDictionary = myList.ToDictionary();                        

            //Debugging the Dictionary can be done here

            //foreach (var value in sortedDictionary)
            //{
            //    KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
            //    Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
            //}

            return sortedDictionary;
        }

        public static Dictionary<Point, double> MeasureDistanceToCamera(Dictionary<Point, double> distanceToCameraDictionary, Point cameraPoint, Point[] points)
        {
            double[] ArrayAbs = new double[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                ArrayAbs[i] = (Math.Abs(points[i].X - cameraPoint.X) + Math.Abs(points[i].Y - cameraPoint.Y) + Math.Abs(points[i].Z - cameraPoint.Z));
                distanceToCameraDictionary.Add(points[i], ArrayAbs[i]);
            }

            return distanceToCameraDictionary;
        }

        public static Dictionary<Point, double> MeasureDistanceToRay(Dictionary<Point, double> distanceToRayDictionary, Point cameraPoint, Point[] points)
        {
            double[] ArrayDistToLine = new double[points.Length];
            Point endOfLine = new Point(131551.964, 398797151, 6.535);

            for (int i = 0; i < points.Length; i++)
            {

                ArrayDistToLine[i] = ShortDistance(cameraPoint, endOfLine, points[i]);
                distanceToRayDictionary.Add(points[i], ArrayDistToLine[i]);
            }
            return distanceToRayDictionary;
        }

        public static Dictionary<Point, double> DynamicMeasureDistanceToRay(Dictionary<Point, double> distanceToRayDictionary, Point cameraPoint, Point[] points, Vector vector)
        {
            double[] ArrayDistToLine = new double[points.Length];

            Vector squaredValues = new Vector(0,0,0);

            Point endOfLine = new Point(131551.964, 398797151, 6.535);

            //Console.WriteLine("X= old{0} Y= old{1} Z= old {2}", endOfLine.X, endOfLine.Y, endOfLine.Z);

            Console.WriteLine("X= vectorInput{0} Y= vectorInput{1} Z= vectorInput {2}", vector.X, vector.Y, vector.Z);

            squaredValues.X = Math.Pow(vector.X, 2);
            squaredValues.Y = Math.Pow(vector.Y, 2);
            squaredValues.Z = Math.Pow(vector.Z, 2);

            double magnitude = squaredValues.X + squaredValues.Y + squaredValues.Z;

            Console.WriteLine("X= squaredValues{0} Y= squaredValues{1} Z= squaredValues {2} Magnitude= {3}", squaredValues.X, squaredValues.Y, squaredValues.Z, magnitude);

            endOfLine.X = vector.X / Math.Sqrt(magnitude);
            endOfLine.Y = vector.Y / Math.Sqrt(magnitude);
            endOfLine.Z = vector.Z / Math.Sqrt(magnitude);

            Console.WriteLine("X= endOfLine{0} Y= endOfLine{1} Z= endOfLine {2}", endOfLine.X, endOfLine.Y, endOfLine.Z);

            for (int i = 0; i < points.Length; i++)
            {
                ArrayDistToLine[i] = ShortDistance(cameraPoint, endOfLine, points[i]);
                distanceToRayDictionary.Add(points[i], ArrayDistToLine[i]);
            }
            return distanceToRayDictionary;
        }

        // File to table that is used for chunk loading a file
        public static DataTable FileToTable(this string path, bool heading = true, char delimiter = '\t', int offset = 0, int limit = 100000)
        {
            var table = new DataTable(); // Make dataTable
            string headerLine = File.ReadLines(path).FirstOrDefault(); // Read the first row for headings

            string[] headers = headerLine.Split(delimiter); //Get headers

            int skip = 1;
            int num = 1;
            
            foreach (string header in headers) // Go through all found headers 
            {
                if (heading)
                {
                    table.Columns.Add(header); // Make Columns in Datatabel based on headers
                }

                else // Makes headers if none are found
                {
                    table.Columns.Add("Field" + num); // Create fields header if heading is false
                    num++;
                    skip = 0; // Don't skip the first row if heading is false
                }
            }

            foreach (string line in File.ReadLines(path).Skip(skip + offset).Take(limit)) //read *limit* amount of lines skip the alredy read lines
            {
                if (!string.IsNullOrEmpty(line))// If a line is not empty
                {
                    table.Rows.Add(line.Split(' ')); //Add the data that is in the line of the TXT
                }
            }
            return table;            
        }
        
        public static void TableToFile(this DataTable table, string path, bool append = true)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!File.Exists(path) || !append)
            {
                stringBuilder.AppendLine(string.Join("\t", table.Columns.Cast<DataColumn>().Select(arg => arg.ColumnName)));
            }                

            using (StreamWriter sw = new StreamWriter(path, append))
            {
                foreach (DataRow dataRow in table.Rows)
                {
                    stringBuilder.AppendLine(string.Join("\t", dataRow.ItemArray.Select(arg => arg.ToString())));
                }
                sw.Write(stringBuilder.ToString());
            }
        }

        public static Point CrossProduct(Point v1, Point v2)
        {
            double x, y, z;
            x = v1.Y * v2.Z - v2.Y * v1.Z;
            y = (v1.X * v2.Z - v2.X * v1.Z) * -1;
            z = v1.X * v2.Y - v2.X * v1.Y;

            var crossProduct = new Point(x, y, z);
            return crossProduct;
        }
    }
}