using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR7DBViewer.Types
{
	public class RRDBByte : IRRDBCell
	{
		public byte Value { get; set; }
		public RRDBByte(byte val)
		{
			Value = val;
		}

		public override string ToString()
			=> Value.ToString();
	}
}
