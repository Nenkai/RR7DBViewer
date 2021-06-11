using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR7DBViewer.Types
{
	public class RRDBString : IRRDBCell
	{
		public string Value { get; set; }
		public RRDBString(string val)
		{
			Value = val;
		}

		public override string ToString()
			=> Value;
    }
}
