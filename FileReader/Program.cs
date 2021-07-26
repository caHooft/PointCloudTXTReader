using System;
using System.Collections.Generic;
using System.Linq;
using FileReader.Core;
using System.IO;
using System.Globalization;

namespace FileReader
{
    class Program
    {
        private static Point[] points;

        static void Main(string[] args)
        {          

            bool app = true;
            string Path = @"C:\TestFolderTXTReader\TestRead.txt";
            int Limit = 100000;

            Point cameraPoint = new Point(131453.074, 398786.554, 16.889);

            
            Dictionary<Point, double> distanceFromRayDictionary = new Dictionary<Point, double>();
            Dictionary<Point, double> distanceFromCameraDictionary = new Dictionary<Point, double>();

            Console.WriteLine("Console TXT reader\r");
            Console.WriteLine("------------------------\n");

            while (app)
            {
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tq - ReadFileInBatch");
                Console.WriteLine("\tw - Measure distance from the points to the camera");
                Console.WriteLine("\te - Measure distance from the points to the ray");
                Console.WriteLine("\ta - Sort Dictionary based on distance from point to camera");
                Console.WriteLine("\ts - Sort Dictionary based on distance from point to ray");  
                Console.WriteLine("\tl - Quit application");
                

                Console.Write("Your option? ");

                switch (Console.ReadLine())
                {
                    case "q":
                        Console.WriteLine($"Your result: = ");
                        ReadFileInBatch(Path, Limit, cameraPoint, distanceFromCameraDictionary);  // Read file in chunks
                        Console.WriteLine("\n");
                        break;

                    case "w":
                        Console.WriteLine($"Measure distance from the points to the camera");
                        MeasureDistanceToCamera(distanceFromCameraDictionary, cameraPoint, points);
                        //MeasureDistanceToCamera(dictonary, cameraPoint, points, iterations);
                        Console.WriteLine("\n");
                        break;

                    case "e":
                        Console.WriteLine($"Measure distance from the points to the ray");

                        Point a = new Point(4, 2, 1);
                        Point b = new Point(8, 4, 2);
                        Point c = new Point(2, 2, 2);

                        Console.WriteLine("Shortest distance is : " + ShortDistance(a, b, c));

                        Console.WriteLine("\n");

                        break;

                    case "a":
                        //Console.WriteLine($"Sorting dictionary please stand by ");
                        //Dictionary<Point, double> sortedDictionary = Extension.SortByDistance(distanceFromCameraDictionary);

                        //foreach (var value in sortedDictionary)
                        //{
                        //    KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
                        //    Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
                        //}
                        //Console.WriteLine($"Finished sorting dictionary ");

                        Console.WriteLine($"Sorting dictionary based on distance to camera please stand by ");
                        Console.WriteLine($"The closest 10 points based on camera position are = ");
                        Dictionary<Point, double> ClosestPointsToCamera = Extension.GetClosestPointsToTheCamera(distanceFromCameraDictionary, 10);

                        Console.WriteLine("\n");
                        Console.WriteLine("\n");
                        break;

                    case "s":
                        //Console.WriteLine($"Sorting dictionary please stand by ");
                        //Dictionary<Point, double> sortedDictionary = Extension.SortByDistance(distanceFromCameraDictionary);

                        //foreach (var value in sortedDictionary)
                        //{
                        //    KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
                        //    Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
                        //}
                        //Console.WriteLine($"Finished sorting dictionary ");
                        Console.WriteLine($"Sorting dictionary based on distance to ray please stand by ");
                        Console.WriteLine($"The closest 10 points are = ");
                        Dictionary<Point, double> ClosestPointsToRay = Extension.GetClosestPointsToTheCamera(distanceFromCameraDictionary, 10);
                        
                        Console.WriteLine("\n");
                        Console.WriteLine("\n");
                        break;

                    case "l":
                        app = false;
                        break;

                    default:
                        Console.WriteLine($"please enter a,s,d,q or f");
                        break;
                }
              
                if (app == true)
                {
                    Console.Write("Press 'c' and Enter to close the app, or press any other key and Enter to continue: ");
                    if (Console.ReadLine() == "c")
                    {
                        app = false;
                    }
                }

                Console.WriteLine("\n");
            }
        }
        internal static void PickPoint(Point cameraPoint)
        /// <summary>
        /// Make a cone from the camera position
        /// Take all points within the where te angle is less than angle tollerance(2-5 degrees)
        /// Sort all of these points based on the shortest distance to the camera position
        /// build a selection based on distance based on x,y,z distance to camera positiom
        /// Than sort selection with the shortest distance at the start of the array/ list/ collection/ other way to safe information'
        /// </summary>
        {            

        }
        //attempt at converting C++ calculations to C#
        internal static double ShortDistance(Point line_point1, Point line_point2, Point point)
        {
            Point AB = new Point((line_point2.X - line_point1.X),(line_point2.Y - line_point1.Y),(line_point2.Z - line_point1.Z));
            Point AC = new Point((point.X - line_point1.X), (point.Y - line_point1.Y), (point.Z - line_point1.Z));
            var area = Extension.CrossProduct(AB, AC).magnitude();
            var CD = area / AB.magnitude();

            return CD;
        }

        /* Read file in chunks */
        internal static void ReadFileInBatch(string Path, int Limit, Point cameraPoint, Dictionary<Point,double> distance)
        {
            if (File.Exists(Path)) // Check if local path is valid
            {
                int TotalRows = File.ReadLines(Path).Count(); // Count the number of rows

                for (int Offset = 0; Offset < TotalRows; Offset += Limit)
                {
                    // Print Log into console
                    string Logs = string.Format("Processing :: Rows {0} of Total {1} :: Offset {2} : Limit : {3}",
                        (Offset + Limit) < TotalRows ? Offset + Limit : TotalRows,
                        TotalRows, Offset, Limit
                    );

                    Console.WriteLine(Logs);

                    var table = Path.FileToTable(heading: true, delimiter: '\t', offset: Offset, limit: Limit);

                    Dictionary<Point, double> distanceFromCameraDictionary = new Dictionary<Point, double>();

                    //int checkColumns = table.Columns.Count;
                   // int checkRows = table.Rows.Count;

                    int iterations = 0;
                    points = new Point[TotalRows];
                    //Point[] points = new Point[TotalRows];

                    //DataView view = new DataView(table);
                    //DataTable table2 = view.ToTable(false, "X", "Y", "Z");

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        points[i] = new Point(1, 1, 1);

                        double temp;

                        //double.TryParse(table2.Rows[i]["X"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        //points[i].X = temp;

                        //double.TryParse(table2.Rows[i]["Y"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        //points[i].Y = temp;

                        //double.TryParse(table2.Rows[i]["Z"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        //points[i].Z = temp;

                        double.TryParse(table.Rows[i]["X"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].X = temp;

                        double.TryParse(table.Rows[i]["Y"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].Y = temp;

                        double.TryParse(table.Rows[i]["Z"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].Z = temp;

                        //if (i < (Limit - 1))
                        //{
                        //    iterations = 22563;
                        //}
                        //else
                        //{
                        //    iterations = Limit;
                        //}
                    }                   

                    // prints entire datatable
                    //table.TableToFile(@"C:\TestFolderTXTReader\Output.txt");

                    // prints x,y,z values of datatable
                    //table2.TableToFile(@"C:\TestFolderTXTReader\Output2.txt");
                }
            }

            else
            {
                Console.WriteLine("Not a correct File path");
            }
            
        }

        /* Read Full file does not work for pointclouds*/
        internal static void ReadFullFile(string Path)
        {
            if (File.Exists(Path)) // Check if local path is valid
            {
                var table = Path.FileToTable(heading: true, delimiter: '\t');

                // Do stuff here like filtering

                table.TableToFile(@"C:\TestFolderTXTReader\Output.txt");
            }
        }

        internal static Dictionary<Point, double> MeasureDistanceToCamera(Dictionary<Point, double> distanceToCameraDictionary, Point cameraPoint, Point[] points)
        {           
            double[] ArrayAbs = new double[points.Length];

            for (int i = 0; i < points.Length; i++)
            {

                ArrayAbs[i] = (Math.Abs(points[i].X - cameraPoint.X) + Math.Abs(points[i].Y - cameraPoint.Y) + Math.Abs(points[i].Z - cameraPoint.Z));

                distanceToCameraDictionary.Add(points[i], ArrayAbs[i]);
            }

            //Debugging the not sorted Dictionary with distances can be done here
            //foreach (var value in distance)
            //{
            //    KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
            //    Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
            //}

                return distanceToCameraDictionary;
            }


        // This function does not work beacause it is incomplete
        // The example uses 2 functions that are not in the example
        // 1 does a constraint (but i dont know what it constains)
        // The other one calculates the sqaure distance
        // When working this function takes a point and the beginnen and end of a line
        // Based on this it calculates the distance from the point to the line
        // 
        internal static double DistanceFromLineToPoint(Point point, Point lineStart, Point lineEnd)
        {
            double line_dist = dist_sq(lineStart.X, lineStart.Y, lineStart.Z, lineEnd.X, lineEnd.Y, lineEnd.Z);

            if (line_dist != 0)
            {
                double t = ((point.X - lineStart.X) * (lineEnd.X - lineStart.X) + (point.Y - lineStart.Y) * (lineEnd.Y - lineStart.Y) + (point.Z - lineStart.Z) * (lineEnd.Z - lineStart.Z)) / line_dist;

                t = constrain(t, 0, 1);

                return dist_sq(point.X, point.Y, point.Z, lineStart.X + t * (lineEnd.X - lineStart.X), lineStart.Y + t * (lineEnd.Y - lineStart.Y), lineStart.Z + t * (lineEnd.Z - lineStart.Z));
            }

            else
            {
                Console.WriteLine("Line distance = 0 which caused this error");
                return point.X;
            }
        }
        //function required for DistanceFromLineToPoint
        private static double constrain(double t, int v1, int v2)
        {
            throw new NotImplementedException();
        }
        //function required for DistanceFromLineToPoint
        private static double dist_sq(double x, double y, double z, double v1, double v2, double v3)
        {
            throw new NotImplementedException();
        }
        // javascript version of above script
        //float dist_to_segment_squared(float px, float py, float pz, float lx1, float ly1, float lz1, float lx2, float ly2, float lz2)
        //{
        //    float line_dist = dist_sq(lx1, ly1, lz1, lx2, ly2, lz2);

        //    if (line_dist == 0) return dist_sq(px, py, pz, lx1, ly1, lz1);

        //    float t = ((px - lx1) * (lx2 - lx1) + (py - ly1) * (ly2 - ly1) + (pz - lz1) * (lz2 - lz1)) / line_dist;

        //    t = constrain(t, 0, 1);

        //    return dist_sq(px, py, pz, lx1 + t * (lx2 - lx1), ly1 + t * (ly2 - ly1), lz1 + t * (lz2 - lz1));
        //}
    }
}
