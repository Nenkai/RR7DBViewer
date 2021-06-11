using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR7DBViewer
{
	public class RRDBColumnInfo
	{
		public int RowColumnOffset { get; set; }
		public int RowNameOffset { get; set; }
		public string Name { get; set; }
		public RRDBColumnType Type { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}

	public enum RRDBColumnType
	{
		Byte = 1,
		Short = 2,
		Integer = 3,
		Float = 4,
		String = 5,
	}
}
