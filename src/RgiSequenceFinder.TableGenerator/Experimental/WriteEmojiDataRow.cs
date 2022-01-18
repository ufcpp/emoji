using RgiSequenceFinder.TableGenerator.Data;
using System.Text;

namespace RgiSequenceFinder.TableGenerator.Experimental;

class WriteEmojiDataRow
{
    public static void Write()
    {
        var data = Cache.Data;

        //var w = Console.Out;
        using var w = new StreamWriter("a.csv", false, Encoding.UTF8);

        foreach (var r in data)
        {
            write(w, r);
            w.WriteLine();
        }

        static void write(TextWriter w, EmojiDataRow r)
        {
            w.Write(r.Utf16);

            w.Write(", ");
            w.Write(r.Index);

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

    public static void Categorized()
    {
        var categorized = new CategorizedEmoji(Cache.Data);

        Console.WriteLine($"keycap: {categorized.Keycaps.Count}");
        Console.WriteLine($"region: {categorized.RegionFlags.Count}");
        Console.WriteLine($"tag   : {categorized.TagFlags.Count}");

        Console.WriteLine("no skin variation");
        for (int len = 0; len < 4; len++) write(categorized.OtherNoSkin, len);

        Console.WriteLine("one skin variation");
        for (int len = 0; len < 2; len++) write(categorized.OtherOneSkin, len);

        Console.WriteLine("two skin variation (normal)");
        for (int len = 0; len < 4; len++) write(categorized.OtherTwoSkin, len);

        Console.WriteLine("two skin variation (holding hands)");
        for (int len = 0; len < 4; len++) write(categorized.OtherVarTwoSkin, len);

        static void write(List<(ushort[] emoji, int index)>?[] array, int len)
        {
            var list = array[len];
            if (list is null) return;
            Console.WriteLine($"  len: {len + 1}, count: {list.Count}");
        }
    }
}
