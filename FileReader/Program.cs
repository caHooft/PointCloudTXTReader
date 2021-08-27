using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using FileReader.Core;
using System.IO;
using System.Globalization;
using System.Data;
using System.Collections;
using System.Text;

namespace FileReader
{
    public class Program
    {
        static Point[] points = new Point[0];

        static void Main(string[] args)
        {
            bool app = true;
            string Path = @"C:\TestFolderTXTReader\TestRead.txt";
            var table = new DataTable();
            int Limit = 100000;

            Vector vector = new Vector(0.988964, 0.105971, -0.10354);

            Point cameraPoint = new Point(131453.074, 398786.554, 16.889);

            Dictionary<Point, double> distanceFromRayDictionary = new Dictionary<Point, double>();
            Dictionary<Point, double> distanceFromCameraDictionary = new Dictionary<Point, double>();
            Dictionary<Point, Tuple<double, double>> filteredDictionary = new Dictionary<Point, Tuple<double, double>>();
            Dictionary<Point, double> filteredDistanceFromCameraDictionary = new Dictionary<Point, double>();
            Dictionary<Point, Tuple<double, double>> distancesDictionary = new Dictionary<Point, Tuple<double, double>>();

            Console.WriteLine("Console TXT reader\r");
            Console.WriteLine("------------------------\n");

            while (app)
            {
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tm - Everthing in order with log(webservice function)");
                Console.WriteLine("\tq - ReadFileInBatch");
                Console.WriteLine("\tw - Measure distance from the points to the camera");
                Console.WriteLine("\te - Measure distance from the points to the ray");
                Console.WriteLine("\td - DynamicMeasure distance from the points to the ray");
                Console.WriteLine("\tr - Merging dictionaries");
                Console.WriteLine("\tt - Check if points are within filtering cone");
                Console.WriteLine("\ty - Get Nearest Object In Cone");
                Console.WriteLine("\ta - Sort Dictionary based on distance from point to camera");
                Console.WriteLine("\ts - Sort Dictionary based on distance from point to ray");
                Console.WriteLine("\tp - Static test distance from the points to the ray");
                Console.WriteLine("\to - Static test merging dictionaries");
                Console.WriteLine("\tk - Everything in order");
                Console.WriteLine("\tl - Quit application");

                Console.Write("Your option? ");

                switch (Console.ReadLine())
                {
                    case "m":
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("------------------Start of log--------------------");

                        sb.Append(Environment.NewLine);

                        File.AppendAllText("C:\\log\\DLLLog.txt", sb.ToString());

                        PickPoint(Path, Limit, cameraPoint, filteredDistanceFromCameraDictionary, distanceFromCameraDictionary, distanceFromRayDictionary, distancesDictionary, vector);
                        
                        break;
                    case "k":
                        Console.WriteLine($"Eerything in order ");

                        ReadFileInBatch(Path, Limit);  // Read file in chunks

                        Console.WriteLine("\n");
                        Console.WriteLine($"File is read ");

                        Console.WriteLine($"Measure distance from the points to the camera");

                        Extension.MeasureDistanceToCamera(distanceFromCameraDictionary, cameraPoint, points);

                        Console.WriteLine("\n");

                        Console.WriteLine($"Measure distance from the points to the ray");

                        Extension.DynamicMeasureDistanceToRay(distanceFromRayDictionary, cameraPoint, points,vector);

                        Console.WriteLine("\n");

                        Console.WriteLine($"Merging dictionaries");

                        distancesDictionary = DictionaryMerger(distanceFromCameraDictionary, distanceFromRayDictionary);

                        Console.WriteLine("\n");

                        Console.WriteLine($"Filtering points based on cone from ray");

                        FilterPoints(filteredDistanceFromCameraDictionary, distancesDictionary);

                        Console.WriteLine("\n");

                        Console.WriteLine($"Get Nearest Object In Cone");

                        GetNearestObjectInCone(filteredDistanceFromCameraDictionary);

                        Console.WriteLine("\n");
                        break;
                    case "q":
                        Console.WriteLine($"Your result: = ");

                        ReadFileInBatch(Path, Limit);  // Read file in chunks

                        Console.WriteLine("\n");
                        break;

                    case "w":
                        Console.WriteLine($"Measure distance from the points to the camera");

                        Extension.MeasureDistanceToCamera(distanceFromCameraDictionary, cameraPoint, points);

                        Console.WriteLine("\n");
                        break;

                    case "e":
                        Console.WriteLine($"Measure distance from the points to the ray");

                        Extension.MeasureDistanceToRay(distanceFromRayDictionary, cameraPoint, points);

                        Console.WriteLine("\n");
                        break;
                    case "d":
                        Console.WriteLine($"Measure distance from the points to the ray");

                        Extension.DynamicMeasureDistanceToRay(distanceFromRayDictionary, cameraPoint, points, vector);

                        Console.WriteLine("\n");
                        break;

                    case "t":
                        Console.WriteLine($"Filtering points based on cone from ray");

                        FilterPoints(filteredDistanceFromCameraDictionary, distancesDictionary);

                        Console.WriteLine("\n");
                        break;

                    case "y":
                        Console.WriteLine($"Get Nearest Object In Cone");

                        GetNearestObjectInCone(filteredDistanceFromCameraDictionary);

                        Console.WriteLine("\n");
                        break;

                    case "r":
                        Console.WriteLine($"Merging dictionaries");

                        distancesDictionary = DictionaryMerger(distanceFromCameraDictionary, distanceFromRayDictionary);

                        Console.WriteLine("\n");                       
                        break;

                    case "a":
                        Console.WriteLine($"Sorting dictionary based on distance to camera please stand by ");
                        Console.WriteLine($"The closest 10 points based on camera position are = ");

                        Dictionary<Point, double> ClosestPointsToCamera = Extension.GetClosestPoints(distanceFromCameraDictionary, 10);

                        Console.WriteLine("\n");
                        break;

                    case "s":
                        Console.WriteLine($"Sorting dictionary based on distance to ray please stand by ");
                        Console.WriteLine($"The closest 10 points are = ");

                        Dictionary<Point, double> ClosestPointsToRay = Extension.GetClosestPoints(distanceFromRayDictionary, 10);

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

                    case "o":
                        Console.WriteLine($"Merging dictionaries");

                        DictionaryMergerStatic();

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
        
        public static string PickPoint(string Path,int Limit, Point cameraPoint, Dictionary<Point, double> filteredDistanceFromCameraDictionary, Dictionary<Point, double> distanceFromCameraDictionary, Dictionary<Point, double> distanceFromRayDictionary, Dictionary<Point, Tuple<double,double>> distancesDictionary, Vector vector)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Environment.NewLine);

            sb.AppendFormat("Start Time = {0}", DateTime.Now.ToString());

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            Console.WriteLine($"Eerything in order function");

            ReadFileInBatch(Path, Limit);  // Read file in chunks

            sb.AppendFormat("Done with reading time = {0}", DateTime.Now.ToString());

            sb.Append(Environment.NewLine);

            Console.WriteLine("\n");

            Console.WriteLine($"Measure distance from the points to the camera");

            Extension.MeasureDistanceToCamera(distanceFromCameraDictionary, cameraPoint, points);

            sb.AppendFormat("Done with measuring distance to camera time = {0}", DateTime.Now.ToString());

            sb.Append(Environment.NewLine);

            Console.WriteLine("\n");

            Console.WriteLine($"Measure distance from the points to the ray");

            Extension.DynamicMeasureDistanceToRay(distanceFromRayDictionary, cameraPoint, points, vector);

            sb.AppendFormat("Done with measuring distance to ray time = {0}", DateTime.Now.ToString());

            sb.Append(Environment.NewLine);

            Console.WriteLine("\n");

            Console.WriteLine($"Merging dictionaries");

            distancesDictionary = DictionaryMerger(distanceFromCameraDictionary, distanceFromRayDictionary);

            sb.AppendFormat("Done with mergeing dictionaries = {0}", DateTime.Now.ToString());

            sb.Append(Environment.NewLine);

            Console.WriteLine("\n");

            Console.WriteLine($"Filtering points based on cone from ray");

            FilterPoints(filteredDistanceFromCameraDictionary, distancesDictionary);

            sb.AppendFormat("Done with filtering points = {0}", DateTime.Now.ToString());

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.AppendFormat("Amount of points inside filter cone = {0}", filteredDistanceFromCameraDictionary.Count());

            sb.Append(Environment.NewLine);

            Console.WriteLine("\n");

            Console.WriteLine($"Get Nearest Object In Cone");

            string result = " ";

            result = GetNearestObjectInCone(filteredDistanceFromCameraDictionary);
            sb.Append(Environment.NewLine);

            sb.AppendFormat("Done with picking a point = {0}", DateTime.Now.ToString());

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.AppendFormat("Picked point result = {0}", result);

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.AppendFormat("End Time = {0}", DateTime.Now.ToString());

            sb.Append(Environment.NewLine);

            sb.AppendFormat("------------------end of log--------------------");

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            File.AppendAllText("C:\\log\\DLLLog.txt", sb.ToString());
            
            return result;            
        }

        // Read file in chunks 
        // Every chunk is converted 
        public static void ReadFileInBatch(string Path, int Limit)
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
                    Console.WriteLine(DateTime.Now.ToString());
                    
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
                    }
                    // prints datatable
                    //table.TableToFile(@"C:\TestFolderTXTReader\Output\Output.txt");
                }
            }

            else
            {
                Console.WriteLine("Not a correct File path");
            }
        }

        /* Read Full file does not work for pointclouds*/
        public static void ReadFullFile(string Path)
        {            
            if (File.Exists(Path)) // Check if local path is valid
            {
                var table = Path.FileToTable(heading: true, delimiter: '\t');

                // Do stuff here like filtering

                table.TableToFile(@"C:\TestFolderTXTReader\Output.txt");
            }
        }

        public static string GetNearestObjectInCone(Dictionary<Point, double> filteredDistanceFromCameraDictionary)
        {
            filteredDistanceFromCameraDictionary = Extension.SortByDistance(filteredDistanceFromCameraDictionary);

            string result = "No Point found";
            bool found = false;

            for (int i = 0; i < filteredDistanceFromCameraDictionary.Count; i++)
            {
                if (i == filteredDistanceFromCameraDictionary.Count - 50)
                {
                    Console.WriteLine("stop");
                    return result;
                }
                else if (found == false && IsCoPlanar(filteredDistanceFromCameraDictionary, i))
                {
                    found = true;
                    result = "{" + "'X' :" + filteredDistanceFromCameraDictionary.ElementAt(i).Key.X + ",'Y' :" + filteredDistanceFromCameraDictionary.ElementAt(i).Key.Y + ",'Z' :" + filteredDistanceFromCameraDictionary.ElementAt(i).Key.Z + "}"; ;
                    
                    Console.WriteLine("X={0}: Y={1}: Z={2}", filteredDistanceFromCameraDictionary.ElementAt(i).Key.X, filteredDistanceFromCameraDictionary.ElementAt(i).Key.Y, filteredDistanceFromCameraDictionary.ElementAt(i).Key.Z);
                    return result;
                    //Console.WriteLine("X={0}: Y={1}: Z={2}: Angle={3}:", kvp.Key.X, kvp.Key.Y, kvp.Key.Z, Math.Abs(kvp.Value));
                }  

                //foreach (KeyValuePair<Point, double> kvp in filteredDistanceFromCameraDictionary)
                //{
                //    if (Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i+1).Value) < tol)
                //    {

                //        Console.WriteLine("X={0}: Y={1}: Z={2}: Angle={3}:", kvp.Key.X, kvp.Key.Y, kvp.Key.Z, Math.Abs(kvp.Value));
                //    }
                //}
            }
            
            return result;
        }

        public static bool IsCoPlanar (Dictionary<Point, double> filteredDistanceFromCameraDictionary, int i)
        {
            double tol = 0.01;
            int check = 20;
            int k = 1;

            Console.WriteLine("IsCoPlanar is called");

            while (k <= check)
            {                
                if (Math.Abs(filteredDistanceFromCameraDictionary.ElementAt(i).Value - filteredDistanceFromCameraDictionary.ElementAt(i + k).Value) < tol)
                {
                    //Console.WriteLine("k={0}: ",k);
                    k++;                    
                }

                else
                {
                    Console.WriteLine("k={0}: ", k);
                    Console.WriteLine("i={0}: does not have enough neighbouring points", i);
                    return false;
                }
            }
            Console.WriteLine("IsCoPlanar While loop is a succes");
            return true;
        }

        public static Dictionary<Point, Tuple<double, double>> DictionaryMerger(Dictionary<Point, double> distanceToCameraDictionary, Dictionary<Point, double> distanceToRayDictionary)
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

            //foreach (var value in resultsNew)
            //{
            //    KeyValuePair<Point, Tuple<double, double>> myVal = (KeyValuePair<Point, Tuple<double, double>>)value;
            //    Console.WriteLine(string.Format("{0}: {1}", myVal.Value.Item1, myVal.Value.Item2));
            //}

            return resultsNew;
        }

        public static double fromDegreesToRadians(double degrees)
        {
            double radian = (Math.PI /180) * degrees;

            return radian;
        }

        public static Dictionary<Point, double> FilterPoints(Dictionary<Point, double> filteredDictionary, Dictionary<Point, Tuple<double, double>> distancesDictionary)
        {
            double radians = fromDegreesToRadians(2);

            foreach (var value in distancesDictionary)
            {
                //Console.WriteLine(string.Format("distance to camera= {0}: distance to ray = {1}: arcsin(distance to ray/distance to camera)= {2}:", value.Value.Item1, value.Value.Item2, Math.Asin(value.Value.Item2 / value.Value.Item1)));

                if (Math.Asin(value.Value.Item2/value.Value.Item1) < radians)
                {
                    //filteredDictionary.Add(value.Key, new Tuple<double,double>(value.Value.Item1, value.Value.Item2));
                    filteredDictionary.Add(value.Key, value.Value.Item2);
                }
            }

            //Debugging 
            //foreach (var value in filteredDictionary)
            //{                
            //    KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
            //    Console.WriteLine(string.Format("X={0}: Y={1}: Z={2}: distance to camera={3}: ", myVal.Key.X, myVal.Key.Y, myVal.Key.Z, myVal.Value));
                
            //}

            return filteredDictionary;
        }

        public static void DictionaryMergerStatic()
        {
            Dictionary<double, double> testDict1 = new Dictionary<double, double>();
            Dictionary<double, double> testDict2 = new Dictionary<double, double>();
            Dictionary<double, Tuple<double, double>> resultsNew = new Dictionary<double, Tuple<double, double>>();

            testDict1.Add(1, 11);
            testDict1.Add(2, 12);
            testDict1.Add(3, 13);

            testDict2.Add(1, 111);
            testDict2.Add(2, 222);
            testDict2.Add(3, 333);

            foreach (KeyValuePair<double, double> kvp in testDict1)
            {
                resultsNew[kvp.Key] = new Tuple<double, double>(kvp.Value, 2);
            }

            foreach (KeyValuePair<double, double> kvp in testDict2)
            {
                resultsNew[kvp.Key] = new Tuple<double, double>(resultsNew[kvp.Key].Item1, kvp.Value);
            }

            foreach (var value in resultsNew)
            {
                KeyValuePair<double, Tuple<double, double>> myVal = (KeyValuePair<double, Tuple<double, double>>)value;
                Console.WriteLine(string.Format("{0}: {1}", myVal.Value.Item1, myVal.Value.Item2));
            }

            //Dictionary<double, double> results = new Dictionary<double, double>(testDict1);

            //foreach (KeyValuePair<double, double> kvp in testDict2)
            //{
            //    results[kvp.Key] = kvp.Value;
            //}

            //foreach (var value in results)
            //{
            //    KeyValuePair<double, double> myVal = (KeyValuePair<double, double>)value;
            //    Console.WriteLine(string.Format("{0}", myVal.Value));
            //}
        }
    }
}
