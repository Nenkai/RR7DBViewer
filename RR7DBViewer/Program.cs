using System;
using System.IO;

namespace RR7DBViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RR7DBViewer by Nenkai#9075");
            if (args.Length < 1)
            {
                Console.WriteLine("Missing input database directory or table.");
                return;
            }

            if (Directory.Exists(args[0]))
            {
                var db = DataBaseManager.FromDirectory(args[0]);
                db.ExportAllToCSV();
            }
            else if (File.Exists(args[0]))
            {
                var table = new Table(args[0]);
                table.Read();
                table.ToCSV();
                Console.WriteLine("Converted table to CSV.");
            }
            else
            {
                Console.WriteLine("Input database directory or table does not exist.");
            }
        }
    }
}
