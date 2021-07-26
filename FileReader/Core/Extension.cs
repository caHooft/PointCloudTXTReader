﻿using System;
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
    static class Extension
    {
        //FileToTable for loading everything at once
        

        internal static Dictionary<Point,double> GetClosestPointsToTheCamera(Dictionary<Point,double> dictionary, int amount)
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

        // Sorts a dictionary of type key(point),distance(double) by the distance
        internal static Dictionary<Point, double> SortByDistance(Dictionary<Point, double> distance)
        {
            var myList = distance.ToList();

            myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            foreach (var value in myList)
            {
                KeyValuePair<Point, double> myVal = (KeyValuePair<Point, double>)value;
                //Console.WriteLine(string.Format("{0}: {1}", myVal.Key.GetPoints(), myVal.Value));
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

                else // Not really used but should make headers if none are found
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
