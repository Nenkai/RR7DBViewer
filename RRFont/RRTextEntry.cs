using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRFont
{
    public class RRTextEntry
    {
        public List<ushort> CharData { get; set; } = new();
        public string ActualText { get; set; }
        public int Id { get; set; }

        public override string ToString()
            => $"Id: {Id} ({ActualText})";
    }
}
