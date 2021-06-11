using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR7DBViewer.Types
{
	public class RRDBInt : IRRDBCell
	{
		public int Value { get; set; }
		public RRDBInt(int val)
		{
			Value = val;
		}

		public override string ToString()
			=> Value.ToString();
	}
}
