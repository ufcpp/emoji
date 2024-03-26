using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EmojiData;

/// <summary>
/// emoji.json の JSON データ構造確認。
/// </summary>
class JsonDocChecker
{
    public static void Check(JsonDocument doc)
    {
        Debug.Assert(doc.RootElement.ValueKind == JsonValueKind.Array);

        var list = new List<string>();

        var objCount = 0;
        var imageCount = 0;
        var maxX = 0;
        var maxY = 0;

        void readImage(JsonElement elem)
        {
            if (elem.TryGetProperty("image", out var image))
            {
                ++imageCount;
            }

            if (elem.TryGetProperty("sheet_x", out var xProp) && xProp.TryGetInt32(out var x))
            {
                maxX = Math.Max(maxX, x);
            }

            if (elem.TryGetProperty("sheet_y", out var yProp) && yProp.TryGetInt32(out var y))
            {
                maxY = Math.Max(maxY, y);
            }

            if (elem.TryGetProperty("unified", out var unified) && unified.GetString() is { } us)
            {
                list.Add(us);
            }
        }

        foreach (var elem in doc.RootElement.EnumerateArray())
        {
            Debug.Assert(elem.ValueKind == JsonValueKind.Object);

            readImage(elem);

            if (elem.TryGetProperty("skin_variations", out var skinVariations))
            {
                foreach (var variation in skinVariations.EnumerateObject())
                {
                    readImage(variation.Value);
                }
            }
            ++objCount;
        }

        Console.WriteLine("obj count: " + objCount);
        Console.WriteLine("json image count:" + imageCount);
        Console.WriteLine("unified list count:" + list.Count);
        Console.WriteLine((maxX, maxY));
    }

    public static void CheckSkinVariations(JsonDocument doc)
    {
        Debug.Assert(doc.RootElement.ValueKind == JsonValueKind.Array);

        var list = new List<string>();

        var regSkinTone = new Regex(@"\-1F3F[B-F]");
        var regFe0f = new Regex(@"\-FE0F");

        int getSkinTone(Match m) => m.Value.Last() - 'B';

        string readUnified(JsonElement elem)
        {
            if (elem.TryGetProperty("unified", out var unified) && unified.GetString() is { } us)
            {
                return us.ToUpper();
            }
            throw new Exception("来ないはず");
        }

        foreach (var elem in doc.RootElement.EnumerateArray())
        {
            Debug.Assert(elem.ValueKind == JsonValueKind.Object);

            var baseUnified = readUnified(elem);

            if (elem.TryGetProperty("skin_variations", out var skinVariations))
            {
                var specialPattern = false;

                var offset = 0;
                foreach (var variation in skinVariations.EnumerateObject())
                {
                    var unified = readUnified(variation.Value);

                    var m = regSkinTone.Matches(unified);
                    var mc = m.Count;

                    // 元になる文字 + skin tone でインデックスを計算できそう。
                    if (mc == 1)
                    {
                        // skin tone が1個のやつ、1F3FB～1F3FF が漏れなくこの順で並んでる。
                        var tone1 = getSkinTone(m[0]);

                        Debug.Assert(offset == tone1, "offset が期待通りか");
                    }
                    else if (mc == 2)
                    {
                        var tone1 = getSkinTone(m[0]);
                        var tone2 = getSkinTone(m[1]);

                        var offsetFromTone = 5 * tone1 + tone2;

                        if (baseUnified is "1F46B" or "1F46C" or "1F46D")
                        {
                            // やべーやつらのオフセットだけ別計算…
                            // tone1 == tone2 なのを除いて並んでる。
                            // 一応計算で出せはするみたいなので、
                            //
                            // 1F46B → 1F469-200D-1F91D-200D-1F468
                            // 1F46C → 1F468-200D-1F91D-200D-1F468
                            // 1F46D → 1F469-200D-1F91D-200D-1F469
                            //
                            // の3文字を追加で入れておけば絵は出せそう。
                            offsetFromTone =
                                5 // 1F46B とかに直接 skin tone が付いてる分5つ
                                + 4 * tone1 + tone2 - (tone1 < tone2 ? 1 : 0); // tone1 == tone2 の時を除いた4×4個
                        }

                        Debug.Assert(offset == offsetFromTone, "offset が期待通りか");
                    }

                    Debug.Assert(mc <= 2, "RGI 内に2個以上の skin tone の入ったシーケンスないはず");

                    // skin tone 1個目は必ず2文字目。
                    if (mc >= 1)
                    {
                        // この場合、skin tone の位置は必ず2文字目。
                        if (unified.StartsWith("1F"))
                        {
                            // SMP (1F000 台)の後ろなので - の位置が5
                            Debug.Assert(m[0].Index == 5);
                        }
                        else
                        {
                            // BMP (2000 台)の後ろなので - の位置が4
                            Debug.Assert(m[0].Index == 4);
                        }
                    }

                    // skin tone 2個目は必ず末尾。
                    if (mc >= 2)
                    {
                        Debug.Assert(m[1].Index + m[1].Length == unified.Length);
                    }

                    var variationRemoved = regSkinTone.Replace(unified, "");

                    if (baseUnified != variationRemoved)
                    {
                        // もし差があるとしたら、
                        // まず、base 側の2文字目が FE0F のパターン。
                        var firstFe0fRemoved = regFe0f.Replace(baseUnified, m => m.Index <= 5 ? "" : m.Value);

                        if (firstFe0fRemoved != variationRemoved)
                        {
                            // それでも差があるやつ
                            // 👫 みたいにポリコレ仕様が入る前からある「固定の性別・固定の肌色」に1符号点割当たってるやつだと思う。
                            specialPattern = true;

                            Debug.Assert(unified.Contains("200D-1F91D-200D"), "特殊対応が必要な文字は 200D-1F91D-200D (ZWJ 🤝 ZWJ) を含むはず");
                        }
                    }

                    ++offset;
                }

                if (specialPattern)
                {
                    Debug.Assert(baseUnified is "1F46B" or "1F46C" or "1F46D", "特殊対応した文字は 👫 (IF46B) 👬 (1F46C) 👭 (1F46D) のどれかのはず");
                }
            }
        }
    }
}
