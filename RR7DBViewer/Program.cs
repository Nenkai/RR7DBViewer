﻿using System;
using System.IO;

using CommandLine;

namespace RR7DBViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RR7DBViewer by Nenkai#9075");

            if (args.Length == 1)
            {
                if (args[0].EndsWith("db") && File.Exists(args[0]))
                {
                    RRDatabaseManager db = RRDatabaseManager.FromSQLite(args[0]);

                    Console.WriteLine("Import SQLite file as RRDB as Little-Endian? (Y/N) (PS3/XBOX360 games are Big-Endian, anything else is Little)");

                    bool bigEndian = true;
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                        bigEndian = false;

                    string outputDir = Path.GetFileNameWithoutExtension(args[0]);
                    Directory.CreateDirectory(outputDir);
                    db.Save(outputDir, bigEndian);
                    return;
                }
            }

            Parser.Default.ParseArguments<ExportVerbs, ImportVerbs>(args)
                .WithParsed<ExportVerbs>(Export)
                .WithParsed<ImportVerbs>(Import);
        }

        public static void Export(ExportVerbs options)
        {
            RRDatabaseManager db;
            if (Directory.Exists(options.InputPath))
            {
                db = RRDatabaseManager.FromDirectory(options.InputPath);
                db.ExportAllToCSV();
            }
            else if (File.Exists(options.InputPath))
            {
                db = new RRDatabaseManager();
                var table = new Table(options.InputPath);
                table.Read();

                db.Tables.Add(table);
            }
            else
            {
                Console.WriteLine("File does not exist.");
                return;
            }


            if (options.ExportAs == "CSV")
            {
                db.ExportAllToCSV();
            }
            else if (options.ExportAs == "SQLite")
            {
                SQLiteExporter exporter = new SQLiteExporter(db);
                exporter.ExportToSQLite(options.OutputPath);
            }
            else
            {
                Console.WriteLine("Invalid export method");
            }
        }

        public static void Import(ImportVerbs options)
        {
            RRDatabaseManager db;
            if (File.Exists(options.InputPath))
            {
                db = RRDatabaseManager.FromSQLite(options.InputPath);
            }
            else
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            Directory.CreateDirectory(options.OutputPath);
            db.Save(options.OutputPath, !options.LittleEndian);
        }

        [Verb("export", HelpText = "Exports a RR database file to SQLite or CSV.")]
        public class ExportVerbs
        {
            [Option('i', "input", Required = true, HelpText = "Input file or folder.")]
            public string InputPath { get; set; }

            [Option('o', "output", Required = true, HelpText = "Output file.")]
            public string OutputPath { get; set; }

            [Option("export-as", Default = "SQLite", HelpText = "How to export. Defaults to SQLite.")]
            public string ExportAs { get; set; }
        }

        [Verb("import", HelpText = "Imports a database file as SQLite and exports it to a RR database file.")]
        public class ImportVerbs
        {
            [Option('i', "input", Required = true, HelpText = "Input file or folder.")]
            public string InputPath { get; set; }

            [Option('o', "output", Required = true, HelpText = "Output file.")]
            public string OutputPath { get; set; }

            [Option("little-endian", HelpText = "Whether to import the database as little-endian (for PS Vita Ridge Racer). Defaults to false (BE).")]
            public bool LittleEndian { get; set; }
        }
    }
}
