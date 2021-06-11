using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RR7DBViewer.Types;

namespace RR7DBViewer
{
    public class RRDBRowData
    {
        public List<IRRDBCell> Cells { get; set; } = new List<IRRDBCell>();
    }
}
