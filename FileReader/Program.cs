using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReader.Core;
using System.IO;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            bool app = false;

            Console.WriteLine("Console TXT reader\r");
            Console.WriteLine("------------------------\n");

            while (!app)
            {
                string Path = @"C:\TestFolderTXTReader\TestRead.txt";
                int Limit = 100000;
                //ReadFullFile(Path); // Read full file

                ReadFileInBatch(Path,Limit); // Read file in chunks

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

                int TotalRows = File.ReadLines(Path).Count(); // Count the number of rows in file with lazy load
                
                for (int Offset = 0; Offset < TotalRows; Offset += Limit)
                {
                    // Print Log into console
                    string Logs = string.Format("Processing :: Rows {0} of Total {1} :: Offset {2} : Limit : {3}",
                        (Offset + Limit) < TotalRows ? Offset + Limit : TotalRows,
                        TotalRows, Offset, Limit
                    );

                    Console.WriteLine(Logs);

                    var table = Path.FileToTable(heading: true, delimiter: '\t', offset: Offset, limit: Limit);

                    // Do all your processing here and with limit and offset and save to drive in append mode
                    // The append mode will write the output in same file for each processed batch.

                    table.TableToFile(@"C:\TestFolderTXTReader\Output.txt");
                }
            }            
        }
    }
}
