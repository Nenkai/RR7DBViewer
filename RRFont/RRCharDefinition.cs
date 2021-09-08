using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRFont
{
    public class RRCharDefinition
    {
        public char SourceCharacter { get; set; }
        public ushort TargetID { get; set; }

        public override string ToString()
            => $"'{SourceCharacter}' - ID: {TargetID}";
    }
}
