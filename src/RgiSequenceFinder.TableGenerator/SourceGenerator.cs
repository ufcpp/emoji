﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RgiSequenceFinder.TableGenerator
{
    class SourceGenerator
    {
        private static StreamWriter OpenWrite(string path) => new(path, false, Encoding.UTF8);

        public static void Write(string basePath, GroupedEmojis emojis)
        {
            {
                using var writer = OpenWrite(Path.Combine(basePath, "RgiTable.Generated.Keycap.cs"));
                WriteHeader(writer);
                WriteKeycaps(writer, emojis.Keycaps);
                WriteFooter(writer);
            }
            {
                using var writer = OpenWrite(Path.Combine(basePath, "RgiTable.Generated.Region.cs"));
                WriteHeader(writer);
                WriteRegionFlags(writer, emojis.RegionFlags);
                WriteFooter(writer);
            }
            {
                using var writer = OpenWrite(Path.Combine(basePath, "RgiTable.Generated.Tag.cs"));
                WriteHeader(writer);
                WriteTagFlags(writer, emojis.TagFlags);
                WriteFooter(writer);
            }
            {
                using var writer = OpenWrite(Path.Combine(basePath, "RgiTable.Generated.Skin.cs"));
                WriteHeader(writer);
                WriteSkinTones(writer, emojis.SkinTones);
                WriteFooter(writer);
            }
            {
                using var writer = OpenWrite(Path.Combine(basePath, "RgiTable.Generated.Other.cs"));
                WriteHeader(writer);
                WriterOthers(writer, emojis.Singlulars, emojis.Others);
                WriteFooter(writer);
            }
        }

        private static void WriteHeader(StreamWriter writer)
        {
            writer.Write(@"// <auto-generated>
// RgiSequenceFinder.TableGenerator
// </auto-generated>

namespace RgiSequenceFinder
{
    partial class RgiTable
    {
");
        }

        private static void WriteFooter(StreamWriter writer)
        {
            writer.Write(@"    }
}
");
        }

        private static void WriteKeycaps(StreamWriter writer, List<(Keycap key, int index)> keycaps)
        {
            writer.Write(@"        private static int FindKeycap(Keycap key)
        {
            switch (key.Value)
            {
");

            foreach (var (key, index) in keycaps)
            {
                writer.Write("                case (byte)'");
                writer.Write((char)key.Value);
                writer.Write("': return ");
                writer.Write(index);
                writer.Write(@";
");
            }

            writer.Write(@"                default: return -1;
            }
        }
");
        }

        private static void WriteRegionFlags(StreamWriter writer, List<(RegionalIndicator code, int index)> regionFlags)
        {
            var dic = regionFlags.ToDictionary(t => (int)t.code.Value, t => t.index);

            writer.Write(@"        private static System.ReadOnlySpan<byte> _regionTable1 => new byte[] { ");

            for (int i = 0; i < 26 * 13; i++)
            {
                var index = dic.TryGetValue(i + 1, out var v) ? v : 0;
                writer.Write(index);
                writer.Write(", ");
            }

            writer.Write(@"};
        private static System.ReadOnlySpan<byte> _regionTable2 => new byte[] { ");

            for (int i = 0; i < 26 * 13; i++)
            {
                var index = dic.TryGetValue(i + 1 + 26 * 13, out var v) ? v - 128 : 0;
                writer.Write(index);
                writer.Write(", ");
            }

            writer.Write(@"};

        private static int FindRegion(RegionalIndicator region)
        {
            var v = (ushort)(region.Value - 1);
            if (v >= 26 * 26) return -1;

            if (v < 13 * 26)
            {
                var i = _regionTable1[v];
                if (i == 0) return -1;
                else return i;
            }
            else
            {
                var i = _regionTable2[v - 13 * 26];
                if (i == 0) return -1;
                else return i + 128;
            }
        }
");
        }

        private static void WriteTagFlags(StreamWriter writer, List<(TagSequence tag, int index)> tagFlags)
        {
            writer.Write(@"        private static int FindTag(TagSequence tags)
        {
            switch (tags.LongValue)
            {
");
            foreach (var (tags, index) in tagFlags)
            {
                writer.Write("                case 0x");
                writer.Write(tags.LongValue.ToString("X"));
                writer.Write("UL: return ");
                writer.Write(index);
                writer.Write(@";
");
            }

            writer.Write(@"                default: return -1;
            }
        }
");
        }

        private static void WriteSkinTones(StreamWriter writer, int[] skinToneIndexes)
        {
            writer.Write(@"        private const int _skinToneFirstIndex = ");
            writer.Write(skinToneIndexes[0]);
            writer.Write(@";

        private static int FindSkinTone(SkinTone skinTone) => _skinToneFirstIndex + (int)skinTone - 1;
");
        }

        private static void WriterOthers(StreamWriter writer, List<(char c, int index)>?[,] singulars, List<(string emoji, int index, byte skinVariationType)> others)
        {
            writer.Write(@"        private static CharDictionary[,] _singularTable = new CharDictionary[,]
        {
");

            for (int fe0f = 0; fe0f < 2; fe0f++)
            {
                writer.Write(@"            {
");

                for (int bmp = 0; bmp < 4; bmp++)
                {
                    var list = singulars[fe0f, bmp];

                    if (list is null)
                    {
                        writer.Write(@"                null,
");
                    }
                    else
                    {
                        var count = list.Count;
                        var bits = (int)Math.Round(Math.Log2(count));

                        // ビット数削るほど被り率上がるので、256/512 を境にビット数増やしてる。
                        bits = bits <= 7 ? bits + 2 : bits + 1;
                        var capacity = 1 << bits;

                        writer.Write(@"                new CharDictionary(");
                        writer.Write(capacity);
                        writer.Write(@",
                    new ushort[] { ");
                        foreach (var (c, _) in list)
                        {
                            writer.Write("0x");
                            writer.Write(((int)c).ToString("X4"));
                            writer.Write(", ");
                        }
                        writer.Write(@"},
                    new ushort[] { ");

                        foreach (var (_, index) in list)
                        {
                            writer.Write(index);
                            writer.Write(", ");
                        }

                        writer.Write(@"}),
");
                    }
                }

                writer.Write(@"            },
");
            }

            writer.Write(@"        };
");


            writer.Write(@"        private static StringDictionary _otherTable = new StringDictionary(
            """);

            foreach (var (s, _, _) in others)
            {
                foreach (var c in s)
                {
                    writer.Write("\\u");
                    writer.Write(((int)c).ToString("X4"));
                }
            }

            writer.Write(@""",
            new byte[] { ");

            foreach (var (s, _, _) in others)
            {
                writer.Write(s.Length);
                writer.Write(", ");
            }

            writer.Write(@"},
            new ushort[] { ");

            foreach (var (_, index, _) in others)
            {
                writer.Write(index);
                writer.Write(", ");
            }

            writer.Write(@"},
            new byte[] { ");

            foreach (var (_, _, variation) in others)
            {
                writer.Write(variation);
                writer.Write(", ");
            }

            writer.Write(@"}
            );
");
        }

#if false
// こういう実装も試してみたという形跡。
//
// ReadOnlySpan<char> 引数を付けつけできるように switch 化したもの。
// そんなに (a, b, c, ...) switch { ... } みたいなやつ、「1文字目が同じなら1つの if になる」みたいな最適化は掛かるけど、
// 代わりに goto が大量にあるコードになって、バイナリサイズは Dictionary<string, int> を持つ実装よりもでかくなった。
// 線形探索になりそうだし Dictionary 実装よりも遅そう。
//
// 素直に、TryGetValue(ReadOnlySpan<char>) ができる StringDictionary を書いた方がよさそう。

        private static void WriteOthers(StreamWriter writer, List<(string emoji, int index)> others)
        {
            writer.Write(@"        private static int FindOther(System.ReadOnlySpan<char> emoji) => emoji.Length switch
        {
");

            foreach (var g in others.GroupBy(x => x.emoji.Length).OrderBy(g => g.Key))
            {
                var length = g.Key;

                writer.Write("            ");
                writer.Write(length);
                writer.Write(" => ");

                for (int i = 0; i < length; i++)
                {
                    if (i == 0) writer.Write('(');
                    else writer.Write(", ");
                    writer.Write("emoji[");
                    writer.Write(i);
                    writer.Write("]");
                }

                writer.Write(@") switch
            {
");

                foreach (var (emoji, index) in g)
                {
                    writer.Write("                ");

                    for (int i = 0; i < length; i++)
                    {
                        if (i == 0) writer.Write('(');
                        else writer.Write(", ");
                        writer.Write("'\\u");
                        writer.Write(((int)emoji[i]).ToString("X4"));
                        writer.Write("'");
                    }

                    writer.Write(") => ");
                    writer.Write(index);
                    writer.Write(@",
");
                }

                writer.Write(@"                _ => -1,
            },
");
            }

            writer.Write(@"            _ => -1,
        };

");
        }
#endif
    }
}
