using System;
using System.Collections.Generic;
using System.Linq;
using FileReader.Core;
using System.IO;
using System.Globalization;
using System.Data;
using System.Collections;

namespace FileReader
{
    class Program
    {
        static Point[] points = new Point[0];

        static void Main(string[] args)
        {
            bool app = true;
            string Path = @"C:\TestFolderTXTReader\TestRead.txt";
            var table = new DataTable();
            int Limit = 100000;

            Point cameraPoint = new Point(131453.074, 398786.554, 16.889);

            Dictionary<Point, double> distanceFromRayDictionary = new Dictionary<Point, double>();
            Dictionary<Point, double> distanceFromCameraDictionary = new Dictionary<Point, double>();
            Dictionary<Point, Tuple<double, double>> distancesDictionary = new Dictionary<Point, Tuple<double, double>>();

            Console.WriteLine("Console TXT reader\r");
            Console.WriteLine("------------------------\n");

            while (app)
            {
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tq - ReadFileInBatch");
                Console.WriteLine("\tw - Measure distance from the points to the camera");
                Console.WriteLine("\te - Measure distance from the points to the ray");
                Console.WriteLine("\tt - Merging dictionaries");
                Console.WriteLine("\tr - Check if points are within filtering cone");
                Console.WriteLine("\ta - Sort Dictionary based on distance from point to camera");
                Console.WriteLine("\ts - Sort Dictionary based on distance from point to ray");
                Console.WriteLine("\tp - Static test distance from the points to the ray");
                Console.WriteLine("\to - Static test merging dictionaries");
                Console.WriteLine("\tl - Quit application");

                Console.Write("Your option? ");

                switch (Console.ReadLine())
                {
                    case "q":
                        Console.WriteLine($"Your result: = ");
                        ReadFileInBatch(Path, Limit, cameraPoint);  // Read file in chunks
                        Console.WriteLine("\n");
                        break;

                    //case "1":
                    //    Console.WriteLine($"Your result: = ");
                    //    //parseFromStringToDouble(points);  // convert table to doubles
                    //    Console.WriteLine("\n");
                    //    break;

                    case "w":
                        Console.WriteLine($"Measure distance from the points to the camera");
                        Extension.MeasureDistanceToCamera(distanceFromCameraDictionary, cameraPoint, points);

                        Console.WriteLine("\n");
                        break;

                    case "e":
                        Console.WriteLine($"Measure distance from the points to the camera");
                        Extension.MeasureDistanceToRay(distanceFromRayDictionary, cameraPoint, points);

                        Console.WriteLine("\n");
                        break;

                    case "p":
                        Console.WriteLine($"Measure distance from the points to the ray");

                        Point a = new Point(4, 2, 1);
                        Point b = new Point(8, 4, 2);
                        Point c = new Point(2, 2, 2);

                        Console.WriteLine("Shortest distance is : " + Extension.ShortDistance(a, b, c));
                        Console.WriteLine("\n");
                        break;

                    case "t":
                        Console.WriteLine($"Merging dictionaries");

                        DictionaryMerger(distanceFromCameraDictionary, distanceFromRayDictionary);

                        Console.WriteLine("\n");
                        Console.WriteLine("\n");
                        break;

                    case "r":
                        Console.WriteLine($"Filtering points based on cone from ray");

                        //Dictionary<Point, double> ClosestPointsToRay = Extension.GetClosestPoints(distanceFromRayDictionary, 10);

                        Console.WriteLine("\n");
                        Console.WriteLine("\n");
                        break;

                    case "a":
                        Console.WriteLine($"Sorting dictionary based on distance to camera please stand by ");
                        Console.WriteLine($"The closest 10 points based on camera position are = ");
                        Dictionary<Point, double> ClosestPointsToCamera = Extension.GetClosestPoints(distanceFromCameraDictionary, 10);

                        Console.WriteLine("\n");
                        Console.WriteLine("\n");
                        break;

                    case "s":
                        Console.WriteLine($"Sorting dictionary based on distance to ray please stand by ");
                        Console.WriteLine($"The closest 10 points are = ");
                        Dictionary<Point, double> ClosestPointsToRay = Extension.GetClosestPoints(distanceFromRayDictionary, 10);

                        Console.WriteLine("\n");
                        Console.WriteLine("\n");
                        break;

                    case "0":
                        Console.WriteLine($"Merging dictionaries");

                        DictionaryMerger(distanceFromCameraDictionary, distanceFromRayDictionary);

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

        // Read file in chunks 
        // Every chunk is converted 
        internal static void ReadFileInBatch(string Path, int Limit, Point cameraPoint)
        {
            if (File.Exists(Path)) // Check if local path is valid
            {
                int TotalRows = File.ReadLines(Path).Count(); // Count the number of rows
                points = new Point[TotalRows - 1];

                for (int Offset = 0; Offset < TotalRows; Offset += Limit)
                {
                    // Print Log into console
                    string Logs = string.Format("Processing :: Rows {0} of Total {1} :: Offset {2} : Limit : {3}",
                        (Offset + Limit) < TotalRows ? Offset + Limit : TotalRows,
                        TotalRows, Offset, Limit
                    );

                    Console.WriteLine(Logs);

                    //every limit amount of times this is called to get the data from file to this table
                    var table = Path.FileToTable(heading: true, delimiter: '\t', offset: Offset, limit: Limit);

                    //DataView view = new DataView(table);
                    //DataTable table2 = view.ToTable(false, "X", "Y", "Z");

                    for (int i = Offset; i < (table.Rows.Count + Offset); i++)
                    {
                        points[i] = new Point(1, 1, 1);

                        double temp;

                        double.TryParse(table.Rows[i - Offset]["X"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].X = temp;

                        double.TryParse(table.Rows[i - Offset]["Y"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].Y = temp;

                        double.TryParse(table.Rows[i - Offset]["Z"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].Z = temp;

                        //if (double.TryParse(table.Rows[i - Offset]["X"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp))
                        //{
                        //    points[i].X = temp;
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Unable to convert X"[i]);
                        //}

                        //if (double.TryParse(table.Rows[i - Offset]["Y"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp))
                        //{
                        //    points[i].Y = temp;
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Unable to convert Y"[i]);
                        //}

                        //if (double.TryParse(table.Rows[i - Offset]["Z"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp))
                        //{
                        //    points[i].Z = temp;
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Unable to convert Z"[i]);
                        //}

                        //if (i < (Limit - 1))
                        //{
                        //    iterations = 22563;
                        //}
                        //else
                        //{
                        //    iterations = Limit;
                        //}
                    }

                    //MeasureDistanceToCamera(distanceFromCameraDictionary, cameraPoint, points, iterations);

                    //Extension.MeasureDistanceToCamera(distanceFromCameraDictionary, cameraPoint, points, Limit);

                    //distanceFromCameraDictionary= Extension.MeasureDistanceToCamera(cameraPoint, points, offset: Offset, limit: Limit);

                    //MeasureDistanceToRay(DistanceFromRayDictionary, cameraPoint, points, iterations);

                    // prints entire datatable
                    //table.TableToFile(@"C:\TestFolderTXTReader\Output\Output.txt");

                    // prints x,y,z values of datatable2
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

        private class KeyEqualityComparer<T, U> : IEqualityComparer<KeyValuePair<T, U>>
        {
            public bool Equals(KeyValuePair<T, U> x, KeyValuePair<T, U> y)
            {
                return x.Key.Equals(y.Key);
            }

            public int GetHashCode(KeyValuePair<T, U> obj)
            {
                return obj.Key.GetHashCode();
            }
        }

        internal static void DictionaryMerger(Dictionary<Point, double> distanceToCameraDictionary, Dictionary<Point, double> distanceToRayDictionary)
        {
            Dictionary<Point, Tuple<double, double>> resultsNew = new Dictionary<Point, Tuple<double, double>>();            

            foreach (KeyValuePair<Point, double> kvp in distanceToCameraDictionary)
            {
                resultsNew[kvp.Key] = new Tuple<double, double>(kvp.Value, 2);
            }

            foreach (KeyValuePair<Point, double> kvp in distanceToRayDictionary)
            {
                resultsNew[kvp.Key] = new Tuple<double, double>(resultsNew[kvp.Key].Item1, kvp.Value);
            }

            foreach (var value in resultsNew)
            {
                KeyValuePair<Point, Tuple<double, double>> myVal = (KeyValuePair<Point, Tuple<double, double>>)value;
                Console.WriteLine(string.Format("{0}: {1}", myVal.Value.Item1, myVal.Value.Item2));
            }
        }



        internal static Dictionary<Point, double> FilterPoints(Dictionary<Point, double> filteredDictionary, Dictionary<Point, Tuple<double, double>> distancesDictionary)
        {          
            foreach (var value in distancesDictionary)
            {
                if (Math.Atan(value.Value.Item1/value.Value.Item2) < 5)
                {
                    filteredDictionary.Add(value.Key, value.Value.Item2);
                }
            }

            //Debugging 
            foreach (var value in filteredDictionary)
            {
                KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
                Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
            }

            return filteredDictionary;
        }

        internal static void DictionaryMergerStatic()
        {
            Dictionary<double, double> testDict1 = new Dictionary<double, double>();
            Dictionary<double, double> testDict2 = new Dictionary<double, double>();

            testDict1.Add(1, 11);
            testDict1.Add(2, 12);
            testDict1.Add(3, 13);
            testDict1.Add(4, 14);
            testDict1.Add(5, 15);

            testDict2.Add(1, 111);
            testDict2.Add(2, 222);
            testDict2.Add(3, 333);
            testDict2.Add(4, 444);
            testDict2.Add(5, 555);

            Dictionary<double, double> results = new Dictionary<double, double>(testDict1);

            Dictionary<double, Tuple<double, double>> resultsNew = new Dictionary<double, Tuple<double, double>>();

            foreach (KeyValuePair<double, double> kvp in testDict2)
            {
                results[kvp.Key] = kvp.Value;
            }

            foreach (KeyValuePair<double, double> kvp in testDict1)
            {
                resultsNew[kvp.Key] = new Tuple<double, double>(kvp.Value, 2);
            }

            foreach (KeyValuePair<double, double> kvp in testDict2)
            {
                resultsNew[kvp.Key] = new Tuple<double, double>(resultsNew[kvp.Key].Item1, kvp.Value);
            }

            foreach (var value in results)
            {
                KeyValuePair<double, double> myVal = (KeyValuePair<double, double>)value;
                Console.WriteLine(string.Format("{0}", myVal.Value));
            }

            foreach (var value in resultsNew)
            {
                KeyValuePair<double, Tuple<double, double>> myVal = (KeyValuePair<double, Tuple<double, double>>)value;
                Console.WriteLine(string.Format("{0}: {1}", myVal.Value.Item1, myVal.Value.Item2));
            }
        }
    }
}
