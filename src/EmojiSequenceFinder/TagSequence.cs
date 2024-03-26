using System;
using System.Text;

namespace RgiSequenceFinder
{
    /// <summary>
    /// Emoji tag sequence のタグ部分を相当する ASCII 文字列化したもの。
    /// 参考: http://unicode.org/reports/tr51/#def_std_emoji_tag_sequence_set
    ///
    /// 現状は国旗専用の仕様だけど、
    /// 将来他の用途でも使うかもしれないので「flag sequence」じゃなくて「tag sequence」になってる。
    /// </summary>
    /// <remarks>
    /// 絵文字の闇その2。
    ///
    /// 1F3F4-E0067-E0062-E0073-E0063-E0074-E007F であれば gbsct [ESC] の6文字。
    ///
    /// <see cref="Byte8.V7"/> にタグ長を入れてる。
    ///
    /// タグ数が <see cref="Byte8.MaxLength"/> - 1 以上のときは情報が切り捨てられるので注意
    /// (<see cref="EmojiSequence"/> 側で <see cref="EmojiSequenceType.MoreBufferRequired"/> 化してる)。
    ///
    /// 逆にタグ数が  <see cref="Byte8.MaxLength"/> - 1 より少ない時(というか、現状6しかあり得ない)、後ろの方(現状、末尾1文字)は0詰め。
    ///
    /// 先頭の文字(現状の RGI では 🏴 (1F3F4) 以外ありえない)は今、単に削除しちゃってる。
    /// 先頭文字をこの型に含めるかどうかは後々というか、実際のところあり得ないとは思うけども、旗以外の emoji tag sequence が追加されたらまた改めて考える。
    /// </remarks>
    public readonly struct TagSequence : IEquatable<TagSequence>
    {
        // 現状、emoji tag sequence のタグが6文字以上の RGI 絵文字はないんだけど、
        // どうせ alignment で8に揃えられたりするので8バイト取っとく。
        private readonly Byte8 _bytes;
        private TagSequence(Byte8 bytes, int count)
        {
            bytes.V7 = (byte)count;
            _bytes = bytes;
        }

        public ulong LongValue => _bytes.LongValue & 0x00FF_FFFF_FFFF_FFFF;
        public bool Equals(TagSequence other) => _bytes == other._bytes;
        public override bool Equals(object obj) => obj is TagSequence other && Equals(other);
        public override int GetHashCode() => _bytes.GetHashCode();
        public static bool operator ==(TagSequence x, TagSequence y) => x.Equals(y);
        public static bool operator !=(TagSequence x, TagSequence y) => !x.Equals(y);

        /// <summary>
        /// 🏴 始まりの emoji tag sequence かどうかを判定。
        /// そうなってないときは count == 0 を返す。
        /// </summary>
        /// <remarks>
        /// <see cref="IsFlag(ReadOnlySpan{char})"/> の仕様では表せない地域からクレーム入ったという話。
        /// ISO 3166-1 (ラテン文字2文字で表現する国コード)で表されてる「国または地域」の旗だけ用意したらそりゃ怒られるよね。
        /// ISO 3166-2 (ラテン文字4～5文字で表現する行政区画コード)で旗を出す。
        ///
        /// 🏴 (U+1F3F4、黒旗、UTF-16 で D83C DFF4)の後ろに「タグ文字」っていう特殊な記号を並べる仕様。
        /// タグ文字は E0000 の辺りに並んでる。
        /// 通常、0-9、a-z 相当の36文字 + cancel 文字(E007F)しか使わないと思うけど、
        /// 一応、E0000-E007F で判定しとく。
        /// high surrogate が DB40、
        /// low surrogate が DC00-DC7F。
        ///
        /// 一応原理上は「東京都の旗」(🏴JP13)とかも出せる。
        /// RGI に入ってるのはグレートブリテン島の3カントリーだけ。
        /// (というかこのカントリー旗のためにこの仕様が入った。
        /// サッカーリーグのチームとかがカントリーごとに分かれてるせいもあって、他の subdivision よりも圧倒的に需要が高いらしい。)
        ///
        /// タグ文字を使う仕様がこいつだけなので、これも先に判定してしまえば他の絵文字シーケンス処理から E0000 台の判定を消せる。
        /// </remarks>
        public static TagSequence FromFlagSequence(ReadOnlySpan<char> s)
        {
            if (s.Length < 2) return default;

            if (s[0] != 0xD83C) return default;
            if (s[1] != 0xDFF4) return default;

            var i = 0;
            Byte8 tags = default;
            var tagsSpan = tags.AsSpan();

            s = s.Slice(2);

            while (s.Length >= 2)
            {
                if (s[0] != 0xDB40) break;
                if (!isTagLowSurrogate(s[1])) break;

                if (i <= 8)
                {
                    tagsSpan[i] = (byte)(s[1] - 0xDC00);
                }
                ++i;
                s = s.Slice(2);
            }

            // 🏴 だけあって Tag が付いてないときと、🏴 もない時の区別は多分要らないと思う。
            // 1F3F4-200D-2620-FE0F (海賊旗)みたいな文字があるけど、それは ZWJ シーケンス判定の方で拾う。
            return new TagSequence(tags, i);

            bool isTagLowSurrogate(char c) => c >= (char)0xDC00 && c <= (char)0xDC7F;
        }

        /// <summary>
        /// 普通に "gbsct" みたいな文字列から E0067-E0062-E0073-E0063-E0074-E007F に相当する <see cref="TagSequence"/> を作る。
        /// (末尾に Cancel タグ(ESC 文字)を追加する処理もメソッド内でやってる。)
        /// </summary>
        public static TagSequence FromAscii(ReadOnlySpan<char> s)
        {
            Byte8 tags = default;
            var tagsSpan = tags.AsSpan();

            int i = 0;
            for (; i < s.Length && i < 6; i++)
            {
                tagsSpan[i] = (byte)s[i];
            }

            tagsSpan[i] = 0x7F;

            return new TagSequence(tags, i + 1);
        }

        public static string ToString(Byte8 tags)
        {
            if (tags.V0 == 0) return "";

            var sb = new StringBuilder();
            var span = tags.AsSpan();

            foreach (var c in span.Slice(0, tags.V7))
            {
                if (c == 0x7f || c == 0) break;
                sb.Append((char)c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// tag 長。
        /// </summary>
        public int Length => _bytes.V7;
    }
}
