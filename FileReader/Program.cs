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

namespace FileReader
{
    class Program
    {
        static void Main(string[] args)
        {          

            bool app = true;
            string Path = @"C:\TestFolderTXTReader\TestRead.txt";
            int Limit = 100000;

            Point cameraPoint = new Point(131453.074, 398786.554, 16.889);

            Dictionary<Point, double> distanceFromCameraDictionary = new Dictionary<Point, double>();

            Console.WriteLine("Console TXT reader\r");
            Console.WriteLine("------------------------\n");

            while (app)
            {
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - ReadFileInBatch");
                Console.WriteLine("\ts - Sort Dictionary");
                Console.WriteLine("\td - Do nothing");
                Console.WriteLine("\tf - Quit application");

                Console.Write("Your option? ");

                switch (Console.ReadLine())
                {
                    case "a":
                        Console.WriteLine($"Your result: = ");
                        ReadFileInBatch(Path, Limit, cameraPoint, distanceFromCameraDictionary);  // Read file in chunks
                        Console.WriteLine("\n");
                        break;

                    case "s":
                        Console.WriteLine($"Your result: = ");
                        Dictionary<Point, double> sortedDictionary = Extension.SortByDistance(distanceFromCameraDictionary);

                        foreach (var value in sortedDictionary)
                        {
                            KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
                            //Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
                        }
                        Console.WriteLine("\n");
                        break;

                    case "d":
                        Console.WriteLine($"Your doing nothing succesfully ");
                        Console.WriteLine("\n");
                        break;

                    case "f":
                        app = false;
                        break;

                    default:
                        Console.WriteLine($"please enter a,s,d or f");
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

                    //Dictionary<Point, double> distanceFromCameraDictionary = new Dictionary<Point, double>();

                    int checkColumns = table.Columns.Count;
                    int checkRows = table.Rows.Count;
                    int iterations = 0;                    

                    Point[] points = new Point[4122564];                   

                    //var closestPoints = new Point[10];
                    //closestPoints[10] = new Point(1, 1, 1);

                    DataView view = new DataView(table);
                    DataTable table2 = view.ToTable(false,"X", "Y", "Z");

                    for (int i = 0; i < table2.Rows.Count; i++)
                    {
                        double x = 1;
                        double y = 1;
                        double z = 1;

                        points[i] = new Point(x, y, z);                        

                        double temp;

                        double.TryParse(table2.Rows[i]["X"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].X = temp;
                        
                        double.TryParse(table2.Rows[i]["Y"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].Y = temp;

                        double.TryParse(table2.Rows[i]["Z"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].Z = temp;                       

                        if (i < (Limit - 1))
                        {
                            iterations = 22563;
                        }
                        else
                        {
                            iterations = Limit;
                        }                        
                    }
                    
                    fillDictionary(distance, cameraPoint, points, iterations);                 

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

        internal static Dictionary<Point, double> fillDictionary(Dictionary<Point, double> distance, Point cameraPoint, Point[] points, int iterations)
        {
            double[] ArrayAbs  = new double[4122564];
            double[] xArrayAbs = new double[4122564];
            double[] yArrayAbs = new double[4122564];
            double[] zArrayAbs = new double[4122564];

            for (int i = 0; i < iterations; i++)
            {
                xArrayAbs[i] = Math.Abs(points[i].X - cameraPoint.X);
                yArrayAbs[i] = Math.Abs(points[i].Y - cameraPoint.Y);
                zArrayAbs[i] = Math.Abs(points[i].Z - cameraPoint.Z);

                ArrayAbs[i] = (Math.Abs(points[i].X - cameraPoint.X) + Math.Abs(points[i].Y - cameraPoint.Y) + Math.Abs(points[i].Z - cameraPoint.Z));

                distance.Add(points[i], ArrayAbs[i]);
            }
            
            //Debugging the not sorted Dictionary can be done here
            //foreach (var value in distance)
            //{
            //    KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
            //    Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
            //}

            return distance;
        }
    }
}
