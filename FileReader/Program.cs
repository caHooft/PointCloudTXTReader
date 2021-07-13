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

            Console.WriteLine("Console TXT reader\r");
            Console.WriteLine("------------------------\n");

            while (app)
            {
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - ReadFileInBatch");
                Console.WriteLine("\ts - Do nothing");
                Console.Write("Your option? ");

                switch (Console.ReadLine())
                {
                    case "a":
                        Console.WriteLine($"Your result: = ");
                        ReadFileInBatch(Path, Limit, cameraPoint);// Read file in chunks
                        Console.WriteLine("\n");
                        break;

                    case "s":
                        Console.WriteLine($"Your doing nothing succesfully ");
                        Console.WriteLine("\n");

                        break;

                    default:
                        Console.WriteLine($"please enter a,s,d or f");
                        break;
                }
                
                //ReadFullFile(Path); // Read full file                 

                Console.Write("Press 'c' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "c") app = false;

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

        /* Read file in chunks */
        internal static void ReadFileInBatch(string Path, int Limit, Point cameraPoint)
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

                    int checkColumns = table.Columns.Count;

                    int checkRows = table.Rows.Count;
                    
                    var points = new Point[4122564];

                    var xArray = new double[4122564];
                    var yArray = new double[4122564];
                    var zArray = new double[4122564];

                    var distancesFromCamera = new Point[4122564];

                    var distancesFromRay = new Point[4122564];

                    DataView view = new DataView(table);
                    DataTable table2 = view.ToTable(false,"X", "Y", "Z");
                    DataTable table3 = view.ToTable(false, "X");

                    for (int i = 0; i < table2.Rows.Count; i++)
                    {
                        double x = 1;
                        double y = 1;
                        double z = 1;

                        points[i] = new Point(x, y, z);                        

                        double temp;

                        double.TryParse(table3.Rows[i]["X"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        xArray[i] = temp;

                        double.TryParse(table2.Rows[i]["X"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].X = temp;
                        
                        double.TryParse(table2.Rows[i]["Y"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].Y = temp;

                        double.TryParse(table2.Rows[i]["Z"].ToString(), NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out temp);
                        points[i].Z = temp;

                    }

                    for (int i = 0; i < table2.Rows.Count; i++)
                    {
                        double x = 1;
                        double y = 1;
                        double z = 1;

                        distancesFromCamera[i] = new Point(x, y, z);

                        distancesFromCamera[i].X = points[i].X - cameraPoint.X;
                        distancesFromCamera[i].Y = points[i].Y - cameraPoint.Y;
                        distancesFromCamera[i].Z = points[i].Z - cameraPoint.Z;

                        xArray[i] = points[i].X - cameraPoint.X;
                        yArray[i] = points[i].Y - cameraPoint.Y;
                        zArray[i] = points[i].Z - cameraPoint.Z;

                        /*
                        double target = 0;
                        double distance = Math.Abs(distancesFromCamera[0].X - target);
                        double idx = 0;
                        for (int c = 1; c < distancesFromCamera.Length; c++)
                        {
                            double cdistance = Math.Abs(distancesFromCamera[c].X - target);
                            if (cdistance < distance)
                            {
                                idx = c;
                                distance = cdistance;
                            }
                        }
                        double theNumber = distancesFromCamera[idx].X;
                        */
                    }

                    var nearestX = FindNearest(1, xArray);
                    var nearestY = FindNearest(1, yArray);
                    var nearestZ = FindNearest(1, zArray);

                    //double targetNumber = 0;
                    //var nearest = distancesFromCamera.Aggregate((current, next) => Math.Abs(current.X - targetNumber) < Math.Abs(next.X - targetNumber) ? current : next);

                    //double targetNumber = 0;                
                    //var nearestX = distancesFromCamera.Min(a => Math.Abs(a.X - targetNumber));
                    //var nearestY = distancesFromCamera.Min(a => Math.Abs(a.Y - targetNumber));
                    //var nearestZ = distancesFromCamera.Min(a => Math.Abs(a.Z - targetNumber));

                    //    foreach (DataRow row in table2.Rows)
                    //{

                    //    string x = row["X"].ToString();
                    //    string y = row["Y"].ToString();
                    //    string z = row["Z"].ToString();
                    //}

                    //var stringArr = table3.AsEnumerable().Select(r => r.Field<string>("X")).ToArray();



                    //datatable1.AsEnumerable().Select(r => r.Field<string>("Name")).ToArray();


                    //double target = 0;

                    //var nearest = xArray.MinBy(x => Math.Abs(x - target));

                    //table.TableToFile(@"C:\TestFolderTXTReader\Output.txt");
                    table2.TableToFile(@"C:\TestFolderTXTReader\Output2.txt");
                }
            }

            else
            {
                Console.WriteLine("Not a correct File path");
            }            
        }
        /*
        private static double FindNearest(double targetNumber, Point[] distancesFromCamera)
        {
            
            double nearestValueX;
            //Point nearestValueY;
            //Point nearestValueZ;

            if (distancesFromCamera.Any(ab => ab.X == targetNumber))
            {
                nearestValueX = 
                nearestValueX = distancesFromCamera.FirstOrDefault(i => i.X == targetNumber);

                //nearestValueY = results.FirstOrDefault(i => i.Y == targetNumber);

                //nearestValueZ = results.FirstOrDefault(i => i.Z == targetNumber);
            }
                
            else
            {
                double greaterThanTarget = 0;
                double lessThanTarget = 0;

                if (results.Any(ab => ab.X > targetNumber))
                {
                    greaterThanTarget = results.Where(i => i.X > targetNumber).Min();
                }
                if (results.Any(ab => ab.X < targetNumber))
                {
                    lessThanTarget = results.Where(i => i.X < targetNumber).Max();
                }

                if (lessThanTarget == 0)
                {
                    nearestValueX = greaterThanTarget;
                }
                else if (greaterThanTarget == 0)
                {
                    nearestValueX = lessThanTarget;
                }
                else if (targetNumber - lessThanTarget < greaterThanTarget - targetNumber)
                {
                    nearestValueX = lessThanTarget;
                }
                else
                {
                    nearestValueX = greaterThanTarget;
                }
            }
            return nearestValueX;
           
        }
        */
        public static double FindNearest(double targetNumber, IEnumerable<double> collection)
        {
            var results = collection.ToArray();
            double nearestValue;

            if (results.Any(ab => ab == targetNumber))
            {

                nearestValue = results.FirstOrDefault(i => i == targetNumber);
            }
            else
            {
                double greaterThanTarget = 0;
                double lessThanTarget = 0;

                if (results.Any(ab => ab > targetNumber))
                {
                    greaterThanTarget = results.Where(i => i > targetNumber).Min();
                }

                if (results.Any(ab => ab < targetNumber))
                {
                    lessThanTarget = results.Where(i => i < targetNumber).Max();
                }

                if (lessThanTarget == 0)
                {
                    nearestValue = greaterThanTarget;
                }

                else if (greaterThanTarget == 0)
                {
                    nearestValue = lessThanTarget;
                }

                else if (targetNumber - lessThanTarget < greaterThanTarget - targetNumber)
                {
                    nearestValue = lessThanTarget;
                }

                else
                {
                    nearestValue = greaterThanTarget;
                }
            }
            return nearestValue;
        }
    }
}
