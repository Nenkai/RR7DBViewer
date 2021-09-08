using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData;
using System.IO;

namespace RRFont
{
    public class RRTextDb
    {
        public List<RRFontDef> Fonts { get; set; } = new();
        public List<RRTextEntry> Entries { get; set; } = new();

        public static RRTextDb FromFile(string path)
        {
            using var fs = File.Open(path, FileMode.Open);
            using var bs = new BinaryStream(fs);
            int fontCount = bs.ReadInt32();
            int textDbEntryCount = bs.ReadInt32();
            int textDbEntryOffset = bs.ReadInt32();

            var db = new RRTextDb();

            for (int i = 0; i < fontCount; i++)
            {
                bs.Position = 0x10 + (i * 0x20);
                var font = new RRFontDef();
                font.Read(bs);

                db.Fonts.Add(font);
            }

            int textEntryDataOffset = textDbEntryOffset + (textDbEntryCount * 8);

            for (int i = 0; i < textDbEntryCount; i++)
            {
                bs.Position = textDbEntryOffset + (i * 8);

                RRTextEntry entry = new RRTextEntry();
                entry.Id = bs.ReadInt32();
                if (entry.Id == 17001023)
                    ;

                int dataOffset = bs.ReadInt32();

                bs.Position = textEntryDataOffset + (dataOffset * 2);
                bs.ReadInt16();

                ushort val = 1;
                while (val != 0)
                {
                    val = bs.ReadUInt16();
                    if (val == 0)
                        break;

                    entry.CharData.Add(val);
                }


                entry.ActualText = db.ToNormalString(entry.CharData);
                db.Entries.Add(entry);
            }

            return db;
        }

        public List<ushort> ToEncodedString(string str)
        {
            List<ushort> t = new List<ushort>();
            for (int i = 0; i < str.Length; i++)
            {
                char currentChar = str[i];
                ushort val = (ushort)Fonts[0].CharDefs.Where(e => e.Value.SourceCharacter == currentChar).FirstOrDefault().Key;
                t.Add(val);
            }

            t.Add(0);
            return t;
        }

        private string ToNormalString(List<ushort> data)
        {
            char[] output = new char[data.Count];
            for (int j = 0; j < data.Count; j++)
            {
                var charId = data[j];
                if (charId == 65534)
                    output[j] = ' ';
                else if (charId > 64000)
                {
                    output[j] = '❌';
                }
                else
                {
                    char sourceChar = Fonts[0].CharDefs[charId].SourceCharacter;
                    output[j] = sourceChar;
                }
            }

            return new string(output);
        }
    }
}
