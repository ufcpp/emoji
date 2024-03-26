using System;

namespace RgiSequenceFinder
{
    /// <summary>
    /// 絵文字用に符号点をずらした文字列。
    ///
    /// 200D (ZWJ: zero width joiner), FE0F (VS16: variation selector 16), 1F3FB～1F3FF (skin tone) は削る。
    /// </summary>
    /// <remarks>
    /// 1F000 台を 1000 台にずらしてるだけ。
    ///
    /// とはいえ UTF-16 とは違う符号体系なので char を使うのためらって ushort にしてある。
    /// ushort[] の slice (<see cref="MemoryExtensions.AsSpan{T}(T[])"/>)としてしか使わないので ref struct にしてある。
    ///
    /// keycap と国旗を除外(別処理)すれば、残りの絵文字候補は 00A9, 00AE, 200D～3300, FE0F, 1F000 台 しかない。
    /// 1F000 台を 1000 台に移すことでサロゲートペアを消す。
    ///
    /// VS16 は無視してもよさそう(VS16 の有無を弁別したところで大してメリットなさそう)なので、これで文字が 3300 未満に収まる。
    ///
    /// skin tone 付きの RGI 絵文字シーケンス判定は「tone なし絵文字で検索 + tone からのオフセット計算」で処理する。
    /// VS16 と skin tone を除外すると ZWJ 前後の文字は必ず1文字固定長になるはずなので、ZWJ を削っても判定は狂わない。
    /// </remarks>
    public readonly ref struct EmojiString
    {
        readonly ReadOnlySpan<ushort> _data;

        public EmojiString(ReadOnlySpan<ushort> data) => _data = data;

        public ReadOnlySpan<ushort> Raw => _data;

        /// <summary>
        /// ハッシュ値計算用の素数列。
        /// </summary>
        /// <remarks>
        /// ランダムに選んで昔たまたま衝突が少なかったやつをそのまま使ってるけど、
        /// その衝突チェックをした時はまだ VS16 削除とかをやってなかったので前提が崩れてる。
        /// (なので現状は「単にランダムに選んだ適当な素数」になってる。)
        ///
        /// 1文字の時は GetHashCode() => _data[0] になるように1要素目に1を入れてる。
        /// </remarks>
        private static ReadOnlySpan<byte> Primes => new byte[] { 1, 83, 223, 227, 131, 53, 149, 227, 229, 7, 23, 5, 47, 59, 53, 3 };

        private const int Diff = 0x1F000 - 0x1000;
        private const int MinSkinTone = 0x1F3FB - Diff;
        private const int MaxSkinTone = 0x1F3FF - Diff;

        /// <summary>
        /// この型にわたってくる時点で 1F000 台しかないはずのサロゲートペアを 1000 台にずらして返す。
        /// </summary>
        private static char Convert(char high, char low)
        {
            var cp = char.ConvertToUtf32(high, low);
            var replaced = cp - Diff;
            return (char)replaced;
        }

        /// <summary>
        /// <see cref="EmojiString"/> コメントにある通り、
        /// - 1F000 台を 1000 台に移動
        /// - ZWJ, VS16, skin tone 削除
        /// する。
        /// </summary>
        /// <returns><paramref name="emoji"/> に書き込んだ文字数。</returns>
        public static int FromUtf16(ReadOnlySpan<char> utf16, Span<ushort> emoji)
        {
            char high = default;
            var i = 0;

            foreach (var c in utf16)
            {
                if (c == '\uFE0F' || c == '\u200D') continue;

                if (char.IsHighSurrogate(c)) high = c;
                else if (char.IsLowSurrogate(c))
                {
                    var c1 = Convert(high, c);
                    if (c1 >= MinSkinTone && c1 <= MaxSkinTone) continue;
                    emoji[i++] = c1;
                }
                else emoji[i++] = c;

                if (i >= emoji.Length) return i;
            }

            return i;
        }

        /// <summary>
        /// <see cref="EmojiString"/> コメントにある通り、
        /// - 1F000 台を 1000 台に移動
        /// - ZWJ, VS16, skin tone 削除
        /// する。
        /// </summary>
        /// <returns><paramref name="emoji"/> に書き込んだ文字数。</returns>
        public static int FromUtf32(ReadOnlySpan<int> utf32, Span<ushort> emoji)
        {
            var i = 0;

            foreach (var c in utf32)
            {
                if (c == '\uFE0F' || c == '\u200D') continue;

                var c1 = c >= 0x10000 ? c - Diff : c;

                if (c1 >= MinSkinTone && c1 <= MaxSkinTone) continue;

                emoji[i++] = (ushort)c1;
            }

            return i;
        }

        public override int GetHashCode()
        {
            var primes = Primes;
            var s = _data;
            var hash = 0;
            for (int i = 0; i < s.Length && i < primes.Length; i++)
            {
                hash += s[i] * primes[i];
            }
            return hash;
        }

        public static int GetHashCode(ReadOnlySpan<char> utf16)
        {
            Span<ushort> buffer = stackalloc ushort[utf16.Length];
            var written = FromUtf16(utf16, buffer);
            return new EmojiString(buffer[..written]).GetHashCode();
        }

        public bool Equals(EmojiString other) => _data.SequenceEqual(other._data);

        public bool Equals(ReadOnlySpan<char> utf16)
        {
            Span<ushort> buffer = stackalloc ushort[utf16.Length];
            var written = FromUtf16(utf16, buffer);
            return _data.SequenceEqual(buffer[..written]);
        }

        public static bool Equals(ReadOnlySpan<ushort> emoji, ReadOnlySpan<char> utf16) => new EmojiString(emoji).Equals(utf16);

        public static bool Equals(ushort singleScalarEmoji, ReadOnlySpan<char> utf16)
        {
            Span<ushort> buffer = stackalloc ushort[2];
            var written = FromUtf16(utf16, buffer);
            if (written != 1) return false;
            return singleScalarEmoji == buffer[0];
        }
    }
}
