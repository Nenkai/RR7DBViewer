using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Syroot.BinaryData;

using RR7DBViewer.Types;

namespace RR7DBViewer
{
    public class Table
    {
		public List<RRDBColumnInfo> Columns = new List<RRDBColumnInfo>();
		public List<RRDBRowData> Rows = new List<RRDBRowData>();

		private string _path;

		public Table(string path)
        {
			_path = path;
        }

        public void Read()
        {
			using var fs = new FileStream(_path, FileMode.Open);
			using var bs = new BinaryStream(fs, ByteConverter.Big);

			bs.ReadByte();
			bs.ReadByte();
			bs.ReadByte();
			int columnCount = bs.ReadByte();

			int rowCount = bs.ReadInt32();
			int rowDataStartOffset = bs.ReadInt32();
			int rowLengthPadded = bs.ReadInt32();

			// Read column defs
			for (int i = 0; i < columnCount; i++)
			{
				var col = new RRDBColumnInfo();
				col.RowColumnOffset = bs.ReadInt32();
				Columns.Add(col);
			}

			// Read column names
			for (int i = 0; i < columnCount; i++)
			{
				int colNameOffset = bs.ReadInt32();
				using (var seek = bs.TemporarySeek(colNameOffset, SeekOrigin.Begin))
					Columns[i].Name = seek.Stream.ReadString(StringCoding.ZeroTerminated);
			}

			// Read Column types
			for (int i = 0; i < columnCount; i++)
				Columns[i].Type = (RRDBColumnType)bs.Read1Byte();

			bs.Position = rowDataStartOffset;

			// Read rows & cells
			for (int i = 0; i < rowCount; i++)
			{
				int basePos = rowDataStartOffset + (rowLengthPadded * i);
				RRDBRowData row = new RRDBRowData();

				foreach (var col in Columns)
				{
					bs.Position = basePos + col.RowColumnOffset;
					IRRDBCell cell = null;
					if (col.Type == RRDBColumnType.String)
					{
						uint strPos = bs.ReadUInt32();
						string str;
						using (var seek = bs.TemporarySeek(strPos, SeekOrigin.Begin))
							str = seek.Stream.ReadString(StringCoding.ZeroTerminated);
						cell = new RRDBString(str);
					}
					else if (col.Type == RRDBColumnType.Integer)
					{
						cell = new RRDBInt(bs.ReadInt32());
					}
					else if (col.Type == RRDBColumnType.Float)
					{
						cell = new RRDBFloat(bs.ReadSingle());
					}
					else if (col.Type == RRDBColumnType.Byte)
					{
						cell = new RRDBByte(bs.Read1Byte());
					}
					else if (col.Type == RRDBColumnType.Short)
					{
						cell = new RRDBShort(bs.ReadInt16());
					}
					else
					{
						throw new Exception($"Type {col.Type}, row offset {col.RowColumnOffset.ToString("X8")}");
					}
					row.Cells.Add(cell);
				}

				Rows.Add(row);
			}
		}

		public void ToCSV()
        {
			string csvPath = _path + ".csv";

			using var sw = new StreamWriter(csvPath);
			sw.WriteLine(string.Join(',', Columns.Select(e => e.Name)));

			foreach (var row in Rows)
				sw.WriteLine(string.Join(',', row.Cells.Select(c => c.ToString())));
		}
    }
}
