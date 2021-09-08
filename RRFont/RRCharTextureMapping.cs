using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRFont
{
    public struct RRCharTextureMapping
    {
        public uint X1 { get; set; }
        public uint X2 { get; set; }
        public uint Y1 { get; set; }
        public uint Y2 { get; set; }

        public override string ToString()
            => $"X1: {X1}, X2: {X2}, Y1:{Y1}, Y2:{Y2}";
    }
}
