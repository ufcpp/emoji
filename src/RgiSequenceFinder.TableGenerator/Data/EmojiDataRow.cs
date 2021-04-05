﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace RgiSequenceFinder.TableGenerator
{
    public class EmojiDataRow
    {
        public string Utf16 { get; }
        public int Index { get; }
        public int SkinVariation { get; }

        private ushort[] _emojiStr;
        private ushort[]? _varEmojiStr;

        private Rune[] _utf32;

        public EmojiString EmojiString => new(_emojiStr);
        public EmojiString VariantEmojiString => new(_varEmojiStr);
        public ReadOnlySpan<Rune> Utf32 => _utf32;

        public EmojiDataRow(Rune[] utf32, int index, int skinVariation, Rune[]? varUtf32 = null)
        {
            Index = index;
            SkinVariation = skinVariation;
            _utf32 = utf32;

            Span<char> buffer = stackalloc char[Math.Max(utf32.Length, varUtf32?.Length ?? 0) * 2];

            var len = 0;
            var utf16 = buffer;
            foreach (var r in utf32)
            {
                var w = r.EncodeToUtf16(utf16);
                utf16 = utf16[w..];
                len += w;
            }

            Utf16 = new(buffer[..len]);

            var estr = MemoryMarshal.Cast<char, ushort>(buffer);
            len = EmojiString.FromUtf32(MemoryMarshal.Cast<Rune, int>(utf32), estr);

            _emojiStr = estr[..len].ToArray();

            if (varUtf32 is not null)
            {
                len = EmojiString.FromUtf32(MemoryMarshal.Cast<Rune, int>(varUtf32), estr);
                _varEmojiStr = estr[..len].ToArray();
            }
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

                    //todo: holding hands 系特殊対応。
                    // varEmojiStr 取得
                    index += 26;
                }
                else
                {
                    throw new System.Exception("来ないはず");
                }
            }

            string readUnified(JsonElement elem)
            {
                if (elem.TryGetProperty("unified", out var unified) && unified.GetString() is { } us) return us;
                throw new KeyNotFoundException();
            }

            Rune[] parseUnified(JsonElement elem) => Parse(readUnified(elem));
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
}
