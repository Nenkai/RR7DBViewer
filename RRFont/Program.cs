using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace RRFont
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Missing input file");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Input file does not exist");
                return;
            }

            /*
            var db = RRTextDb.FromFile(args[0]);
            File.WriteAllLines("text.txt", db.Entries.Select(e => e.ToString()));
            */
        }
    }
}
