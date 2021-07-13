using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReader.Core;
using System.IO;
using System.Data;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            bool app = false;
            string Path = @"C:\TestFolderTXTReader\TestRead.txt";
            int Limit = 100000;

            Console.WriteLine("Console TXT reader\r");
            Console.WriteLine("------------------------\n");

            while (!app)
            {
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - ReadFileInBatch");
                Console.WriteLine("\ts - ReadFileInBatch");
                Console.Write("Your option? ");

                switch (Console.ReadLine())
                {
                    case "a":
                        Console.WriteLine($"Your result: = ");
                        ReadFileInBatch(Path, Limit);// Read file in chunks
                        break;

                    case "s":
                        Console.WriteLine($"Your result: = ");
                        ReadFileInBatch(Path, Limit);// Read file in chunks
                        break;

                    default:
                        Console.WriteLine($"please enter a,s,d or f");
                        break;
                }
                
                //ReadFullFile(Path); // Read full file                 

                Console.Write("Press 'c' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "c") app = true;

                Console.WriteLine("\n");
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

        /* Read file in chunks */
        internal static void ReadFileInBatch(string Path, int Limit)
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

                    // Do all your processing here and with limit and offset and save to drive in append mode
                    // The append mode will write the output in same file for each processed batch.

                    DataView view = new DataView(table);
                    DataTable table2 = view.ToTable(false,"X", "Y", "Z");

                    for (int i = 0; i < table2.Rows.Count; i++)
                    {
                        string x = "h";
                        string y = "o";
                        string z = "i";
                        points[i] = new Point(x,y,z);

                        points[i].X = table2.Rows[i]["X"].ToString();
                        points[i].Y = table2.Rows[i]["Y"].ToString();
                        points[i].Z = table2.Rows[i]["Z"].ToString();
                    }

                    //foreach (DataRow row in table2.Rows)
                    //{                         

                    //    string name = row["X"].ToString();
                    //    string description = row["Y"].ToString();
                    //    string icoFileName = row["Z"].ToString();
                    //}

                    //table.TableToFile(@"C:\TestFolderTXTReader\Output.txt");
                    table2.TableToFile(@"C:\TestFolderTXTReader\Output2.txt");
                }
            }

            else
            {
                Console.WriteLine("Not a correct File path");
            }            
        }
    }
}
