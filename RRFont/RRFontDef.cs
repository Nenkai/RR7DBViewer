using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData;

namespace RRFont
{
    public class RRFontDef
    {
        public List<RRCharTextureMapping> CharTextureMappings = new();
        public Dictionary<int, RRCharDefinition> CharDefs { get; set; } = new();

        public void Read(BinaryStream bs)
        {
            int charDefCount = bs.ReadInt32();
            int charDefOffset = bs.ReadInt32();
            int charTextureMappingOffset = bs.ReadInt32();

            for (int i = 0; i < charDefCount; i++)
            {
                bs.Position = charDefOffset + (i * 8);
                var charMapping = new RRCharTextureMapping();
                charMapping.X1 = bs.ReadUInt32();
                charMapping.X2 = bs.ReadUInt32();
                charMapping.Y1 = bs.ReadUInt32();
                charMapping.Y2 = bs.ReadUInt32();

                CharTextureMappings.Add(charMapping);
            }

            for (int i = 0; i < charDefCount; i++)
            {
                bs.Position = charTextureMappingOffset + (i * 4);
                var charDef = new RRCharDefinition();
                charDef.SourceCharacter = (char)bs.ReadInt16();
                charDef.TargetID = bs.ReadUInt16();

                CharDefs.Add(charDef.TargetID, charDef);
            }
        }
    }
}
