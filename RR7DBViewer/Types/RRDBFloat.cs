using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR7DBViewer.Types
{
	public class RRDBFloat : IRRDBCell
	{
		public float Value { get; set; }
		public RRDBFloat(float val)
		{
			Value = val;
		}

		public override string ToString()
			=> Value.ToString();
    }
}
