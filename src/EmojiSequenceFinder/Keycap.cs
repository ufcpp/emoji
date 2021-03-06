using System;

namespace RgiSequenceFinder
{
    /// <summary>
    /// RGI emoji keycap sequence の1文字目。
    /// 参考: https://unicode.org/reports/tr51/#def_std_emoji_keycap_sequence_set
    /// </summary>
    /// <remarks>
    /// 絵文字シーケンスの中で唯一 ASCII 文字開始だし、skin tone/FE0F 以外の Extend を含むタチの悪いシーケンスなので独立して判定。
    ///
    /// 先頭の1文字だけ返せば弁別可能で、その1文字は絶対に ASCII。
    /// なので専用の構造体を作らず素の char とか byte で扱ってもいいんだけど、範囲チェックとか ToString とかを足しとく。
    /// </remarks>
    public readonly struct Keycap
    {
        public readonly byte Value;
        private Keycap(char value) => Value = (byte)value;

        /// <summary>
        /// RGI emoji keycap sequence の時はそれの1文字目、
        /// そうでないときは 0 (ヌル文字)を返す。
        /// </summary>
        public static Keycap Create(ReadOnlySpan<char> s)
        {
            if (s.Length < 3) return default;

            // combining enclosing keycap
            if (s[2] != 0x20E3) return default;

            // variation selector-16
            if (s[1] != 0xFE0F) return default;

            var c = s[0];
            return (c >= '0' && c <= '9') || c == '#' || c == '*' ? new Keycap(s[0]) : default;
        }

        public override string ToString() => ((char)Value).ToString();
    }
}
