using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace RR7DBViewer
{
    public class DataBaseManager
    {
        public List<Table> Tables = new List<Table>();

        public static DataBaseManager FromDirectory(string directory)
        {
            var db = new DataBaseManager();

            foreach (var file in Directory.GetFiles(directory))
            {
                var table = new Table(file);
                table.Read();
                db.Tables.Add(table);
            }

            return db;
        }


        public void ExportAllToCSV()
        {
            foreach (var table in Tables)
                table.ToCSV();
            Console.WriteLine("All tables successfully exported to CSV.");
        }
    }
}
