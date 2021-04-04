using EmojiData;
using System.IO;
using System.Text;

namespace RgiSequenceFinder.TableGenerator.Experimental
{
    class WriteEmojiDataRow
    {
        public static async System.Threading.Tasks.Task WriteAsync()
        {
            var doc = await Loader.LoadJsonDocAsync();

            //var w = Console.Out;
            using var w = new StreamWriter("a.csv", false, Encoding.UTF8);

            foreach (var r in EmojiDataRow.Load(doc))
            {
                Write(w, r);
                w.WriteLine();
            }

            static void Write(TextWriter w, EmojiDataRow r)
            {
                w.Write(r.Utf16);

                w.Write(", ");
                w.Write(r.SkinVariation);

                w.Write(",");
                foreach (var c in r.Utf32)
                {
                    w.Write(" ");
                    w.Write(c.Value.ToString("X4"));
                }

                w.Write(",");
                foreach (var c in r.EmojiString.Raw)
                {
                    w.Write(" ");
                    w.Write(c.ToString("X4"));
                }

                if (r.VariantEmojiString.Raw.Length > 0)
                {
                    w.Write(",");
                    foreach (var c in r.VariantEmojiString.Raw)
                    {
                        w.Write(" ");
                        w.Write(c.ToString("X4"));
                    }
                }
            }
        }
    }
}
