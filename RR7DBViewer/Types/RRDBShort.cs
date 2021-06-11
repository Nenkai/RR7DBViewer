using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR7DBViewer.Types
{
	public class RRDBShort : IRRDBCell
	{
		public short Value { get; set; }
		public RRDBShort(short val)
		{
			Value = val;
		}

		public override string ToString()
			=> Value.ToString();
	}
}
