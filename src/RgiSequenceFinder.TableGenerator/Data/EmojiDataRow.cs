using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace RgiSequenceFinder.TableGenerator;

public class EmojiDataRow
{
    public string Utf16 { get; }
    public int Index { get; }
    public int SkinVariation { get; }

    private readonly ushort[] _emojiStr;
    private readonly ushort[]? _varEmojiStr;

    private readonly Rune[] _utf32;

    public EmojiString EmojiString => new(_emojiStr);
    public EmojiString VariantEmojiString => new(_varEmojiStr);
    public ReadOnlySpan<Rune> Utf32 => _utf32;

    public EmojiDataRow(Rune[] utf32, int index, int skinVariation, Rune[]? varUtf32 = null)
    {
        Index = index;
        SkinVariation = skinVariation;
        _utf32 = utf32;

        Span<char> buffer = stackalloc char[Math.Max(utf32.Length, varUtf32?.Length ?? 0) * 2];

        Utf16 = ToUtf16(utf32, buffer);
        _emojiStr = ToEmoji(utf32, buffer);

        if (varUtf32 is not null) _varEmojiStr = ToEmoji(varUtf32, buffer);
    }

    public static IEnumerable<EmojiDataRow> Load(JsonDocument doc)
    {
        var index = 0;
        foreach (var elem in doc.RootElement.EnumerateArray())
        {
            var runes = parseUnified(elem);

            if (!elem.TryGetProperty("skin_variations", out var skinVariations))
            {
                yield return new(runes, index, 0);
                ++index;
                continue;
            }

            var count = skinVariations.EnumerateObject().Count();
            if (count == 5)
            {
                yield return new(runes, index, 1);
                index += 6;
            }
            else if (count == 25)
            {
                Rune[]? variantRunes = null;

                // holding hands 系特殊処理。
                if (runes.Length == 1)
                {
                    var variant = skinVariations.EnumerateObject().ElementAt(6);
                    variantRunes = parseUnified(variant.Value);
                }

                yield return new(runes, index, 2, variantRunes);

                index += 26;
            }
            else
            {
                throw new System.Exception("来ないはず");
            }
        }

        static Rune[] parseUnified(JsonElement elem) => Parse(ReadUnified(elem));
    }

    public static IEnumerable<string> LoadAllStrings(JsonDocument doc)
    {
        foreach (var elem in doc.RootElement.EnumerateArray())
        {
            yield return parseUnified(elem);

            if (!elem.TryGetProperty("skin_variations", out var skinVariations)) continue;

            foreach (var v in skinVariations.EnumerateObject())
            {
                yield return parseUnified(v.Value);
            }
        }

        static string parseUnified(JsonElement elem)
        {
            Span<char> buffer = stackalloc char[40];
            return ToUtf16(Parse(ReadUnified(elem)), buffer);
        }
    }

    private static string ToUtf16(Rune[] utf32, Span<char> buffer)
    {
        var len = 0;
        var utf16 = buffer;
        foreach (var r in utf32)
        {
            var w = r.EncodeToUtf16(utf16);
            utf16 = utf16[w..];
            len += w;
        }

        return new(buffer[..len]);
    }

    private static ushort[] ToEmoji(Rune[] utf32, Span<char> buffer)
    {
        var ubffer = MemoryMarshal.Cast<char, ushort>(buffer);
        var len = EmojiString.FromUtf32(MemoryMarshal.Cast<Rune, int>(utf32), ubffer);
        return ubffer[..len].ToArray();
    }

    private static string ReadUnified(JsonElement elem)
    {
        if (elem.TryGetProperty("unified", out var unified) && unified.GetString() is { } us) return us;
        throw new KeyNotFoundException();
    }

    private static Rune[] Parse(string hyphenatedCodePoints)
    {
        var count = hyphenatedCodePoints.Count(c => c == '-');
        var utf32 = new Rune[count + 1];

        var cp = 0;
        var i = 0;
        foreach (var c in hyphenatedCodePoints)
        {
            if (c is >= '0' and <= '9') cp = cp * 16 + (c - '0');
            else if (c is >= 'a' and <= 'f') cp = cp * 16 + (c - 'a' + 10);
            else if (c is >= 'A' and <= 'F') cp = cp * 16 + (c - 'A' + 10);
            else if (c is '-')
            {
                utf32[i++] = new(cp);
                cp = 0;
            }
        }
        utf32[i] = new(cp);

        return utf32;
    }
}
