using System;

namespace RgiSequenceFinder
{
    /// <summary>
    /// Regional Indicator 2文字を単純な数値化したもの。
    /// </summary>
    /// <remarks>
    /// 絵文字の闇その1。
    ///
    /// 国旗は Regional Indicator っていう A～Z に相当する特殊記号26文字があって、それ2個(固定長)の組み合わせで表現されてる。
    /// 原理上26×26が上限(これよりも複雑な国旗は emoji tag sequence っていう別の仕様を使って表現することになってる)。
    ///
    /// 結局、A-Z を 0～25 扱いして、first * 26 + second + 1 の 1～676 の値を ushort で持つ作りにした。
    /// +1 してるのは「emoji flag sequence じゃなかったときに 0 を返す」仕様にするため。
    /// </remarks>
    public readonly struct RegionalIndicator
    {
        public readonly ushort Value;

        public RegionalIndicator(char firstLowSurrogate, char secondLowSurrogate)
        {
            const int RegionalIndicatorSymbolA = 0xDDE6;

            var first = firstLowSurrogate - RegionalIndicatorSymbolA;
            var second = secondLowSurrogate - RegionalIndicatorSymbolA;

            Value = (ushort)(first * NumLatinAlphabet + second + 1);
        }

        private const int NumLatinAlphabet = 26;

        public char First => (char)('A' + (Value - 1) / NumLatinAlphabet);
        public char Second => (char)('A' + (Value - 1) % NumLatinAlphabet);

        /// <summary>
        /// 「国旗じゃなかった」判定結果に 0, 0 の値を使う。
        /// </summary>
        public static readonly RegionalIndicator Invalid = default;

        /// <summary>
        /// Emoji flag sequence (RGI に限らない)を判定。
        /// 参考: http://unicode.org/reports/tr51/#def_std_emoji_flag_sequence_set
        /// </summary>
        /// <returns>
        /// 国旗絵文字が存在したら国コードに対応する数値(<see cref="RegionalIndicator"/>)を、
        /// なければ default (0, 0)を返す。
        ///
        /// 2文字固定(UTF-16 だと4文字固定)なので、文字列長は返さなくていいはず。
        /// (仕様変更は不可能なレベルだと思うので将来固定長でなくなる心配は多分要らない。)
        /// 戻り値を int とか short にしちゃうと他のメソッドの「grapheme cluster 長を返す」って仕様と混ざるのが怖いので専用の型を作った。
        /// </returns>
        /// <remarks>
        /// 他の grapheme 判定と違って「特定の範囲の文字が2文字並んでいるときに区切る」っていう特殊仕様。
        /// (奇数個並んでると絵文字にならないという嫌な仕様。
        /// 他は正規表現でいうところの * (0個以上)判定。)
        ///
        /// 他の仕様と完全に独立だし、「必ず2文字で区切る」って処理がかなり変なのでこれも先に判定。
        ///
        /// Regional Indicator っていう 1F1E6-1F1FF の26文字を使う。
        /// UTF-16 の場合、
        /// high surrogate が D83C 固定で、
        /// low surrogate が DDE6-DDFF。
        /// </remarks>
        public static RegionalIndicator Create(ReadOnlySpan<char> s)
        {
            if (s.Length < 4) return Invalid;

            if (s[0] != 0xD83C || s[2] != 0xD83C) return Invalid;

            if (!isRegionalIndicatorLowSurrogate(s[1])) return Invalid;
            if (!isRegionalIndicatorLowSurrogate(s[3])) return Invalid;

            return new RegionalIndicator(s[1], s[3]);

            bool isRegionalIndicatorLowSurrogate(char c) => c >= (char)0xDDE6 && c <= (char)0xDDFF;
        }

        public override string ToString()
        {
            return $"{First}{Second}";
        }
    }
}
