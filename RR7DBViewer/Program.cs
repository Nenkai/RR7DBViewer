using System;

namespace RR7DBViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RR7DBViewer by Nenkai#9075");
            if (args.Length < 1)
            {
                Console.WriteLine("Missing input database directory.");
                return;
            }

            var db = DataBaseManager.FromDirectory(args[0]);
            db.ExportAllToCSV();
        }
    }
}
