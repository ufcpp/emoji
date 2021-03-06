using System;

namespace RgiSequenceFinder
{
    /// <summary>
    /// 絵文字がらみ、結局最終的にはテーブルを引く以外の手段がないものの、
    /// 「将来、絵文字シーケンスとして追加するかもしれないものの判定のために、まず UAX #29 にそって grapheme cluster 分割してくれ」と言うことになってる。
    /// 参考: https://unicode.org/reports/tr29/
    ///
    /// これ、絵文字に限定すればここまで複雑な処理必要ないはずなので、簡易版みたいな判定をする。
    /// (サンスクリット、タイ文字、ハングル向けの処理不要＆ Extended_Pictographic の判定(辞書必須)をさぼれる。)
    ///
    /// 文字数を返すメソッドが多くなるけど、全部 UTF-16 code unit 数(C# の string.Length) で返す。
    /// (UTF-8 byte 数、rune 数、grapheme 数ではない。)
    /// </summary>
    /// <remarks>
    /// ちなみに、 .NET 5 以降なら <see cref="System.Globalization.StringInfo.GetTextElementEnumerator(string)"/> がちゃんと grapheme cluster 分割してくれる。
    /// (.NET Core 3.1 までは絵文字に対応してなかった。)
    ///
    /// Unicode 10.0 の時に grapheme cluster 分割のロジックを C# で書いたこともあり。
    /// https://github.com/ufcpp/GraphemeSplitter
    /// これ、prepend (サンスクリットで使う)だけ対応してなかったり。
    /// あと、GraphemeSplitter の方は <see cref="System.Globalization.StringInfo"/> 公式対応が入った今、保守を続けるつもりはない。
    /// </remarks>
    public class GraphemeBreak
    {
        /// <summary>
        /// <see cref="System.Globalization.StringInfo.GetNextTextElementLength"/> 的なことを絵文字シーケンス専用にしたもの。
        /// </summary>
        /// <returns>
        /// Emoji sequence (単独の絵文字含む)判定を受ける場合、その長さ(UTF-16 長)を返す。
        /// それ以外の文字(絵文字表示のロジック的には無視して素通しする文字)なら0。
        /// </returns>
        /// <remarks>
        /// 正規表現的に書くと
        /// Extended_Pictographic (Extend* ZWJ Extended_Pictographic)* Extend*
        /// な判定。
        ///
        /// <see cref="IsPictgraphicEstimate(Span{char})"/> と <see cref="IsExtendEstimate(Span{char})"/> のコメントも参照。
        /// Estimate (大体の予測)って名前が付いているのからお察しな通り、本来の grapheme cluster 分割よりもだいぶ荒い。
        /// 「この後 RGI 絵文字シーケンスの判定はどの道テーブルを引くしかないから正確な判定はそっちできるはず」という前提。
        /// </remarks>
        public static EmojiSequence GetEmojiSequence(ReadOnlySpan<char> s)
        {
            // パフォーマンス用。 empty 時に early return。
            if (s.Length == 0) return default;

            var key = Keycap.Create(s);
            if (key.Value != 0) return new EmojiSequence(key);

            // パフォーマンス用。keycap 以外に ASCII 出てこないので ASCII 用 fast path。
            if (s[0] < 0x80) return EmojiSequence.NotEmoji;

            // ここから下の判定に入らないこと荒く判定できるんで、それで先にはじいちゃう fast path。
            if (!CanBePictgraphic(s[0])) return EmojiSequence.NotEmoji;

            // RI 国旗。
            var r = RegionalIndicator.Create(s);
            if (r.Value != 0) return new EmojiSequence(r);

            // Tag 国旗。
            var tags = TagSequence.FromFlagSequence(s);
            if (tags.LongValue != 0) return new EmojiSequence(tags);

            var (count, zwjs) = IsZwjSequence(s);

            if (count == 0) return EmojiSequence.NotEmoji;

            // skin tone だけ別枠。
            // 本来、skin tone は IsZwjSequence に引っかからない仕様なんだけど、
            // IsPictgraphicEstimate が処理をちょっとさぼってるのでここに来る。
            if (count == 2)
            {
                var st = IsSkinTone(s);
                if (st > 0)
                {
                    return new EmojiSequence(st);
                }
            }

            return new EmojiSequence(count, zwjs);
        }

        /// <summary>
        /// skin tone の時に対応する <see cref="SkinTone"/> 値を、
        /// そうでないとき <see cref="SkinTone.None"/> を返す。
        /// </summary>
        /// <remarks>
        /// Fitzpatrick skin type も絵文字の闇。
        /// こいつだけ「他の絵文字の後ろに直接くっつく」特殊仕様。
        /// 後々の追加された絵文字シーケンスの場合は必ず前の絵文字との間に ZWJ を挟む仕様になってる。
        /// 男女の選択は ZWJ + ♂、♀ だし、髪型選択は ZWJ + 1F9B0-1F9B3。
        ///
        /// UAX #29 の Extend の判定にも使ってる。
        ///
        /// Extend の大半は U+0301 の ́  みたいに「他の文字にくっつけて表示する0幅文字」みたいなの。
        /// 絵文字と組み合わせて表示できるレンダリング システムほとんどないと思うし、もちろん RGI にそんな文字含まれない。
        ///
        /// 絵文字相手に使う Extend は実際のところ、skin tone か FE0F のどちらか。
        ///
        /// FE0F: 異体字セレクター16 (variation selector-16)
        /// 1F3FB-1F3FF: 肌色選択修飾子(emoji modifier Fitzpatrick、skin tone)
        ///
        /// skin tone は UTF-16 だと
        /// high surrogate が D83C、
        /// low surrogate が DFFB-DFFF。
        /// </remarks>
        public static SkinTone IsSkinTone(ReadOnlySpan<char> s)
        {
            if (s.Length < 2) return SkinTone.None;

            if (s[0] == 0xD83C && s[1] >= (char)0xDFFB && s[1] <= (char)0xDFFF)
            {
                return (SkinTone)(s[1] - 0xDFFB + 1);
            }

            return SkinTone.None;
        }

        /// <summary>
        /// UAX #29 の Extended_Pictographic をちょっと緩めに判定。
        /// </summary>
        /// <returns>
        /// 長さ(rune 単位で判定するものの、UTF-16 なので1か2があり得る)。
        /// Extended_Pictographic 判定を受けなかったものは0。
        /// </returns>
        /// <remarks>
        /// この後どうせ、RGI かどうかの判定はテーブル引きするので、
        /// Extended_Pictographic かどうかの判定はかなり「大は小を兼ねる」発想でやる。
        ///
        /// © (00A9、copyright)と ® (00AE、registered)だけかなり浮いた位置にいるのでこの2つだけ個別判定。
        /// 残りは 200E～32FF と 1F000 台全部 Extended_Pictographic 扱い。
        /// </remarks>
        public static int IsPictgraphicEstimate(ReadOnlySpan<char> s)
        {
            if (s.Length >= 2)
            {
                if ((s[0] | 0b11) == 0xD83F && char.IsLowSurrogate(s[1]))
                {
                    return 2;
                }
            }
            if (s.Length >= 1)
            {
                if (CanBePictgraphicBmp(s[0]))
                {
                    return 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// <see cref="GetEmojiSequence(ReadOnlySpan{char})"/> の ZWJ シーケンス判定ループに入る前に1文字限り見て early return するためのさらに荒い判定。
        /// 「サロゲートペアだから1文字後ろを読んで判定」みたいな処理が重たいので、サロゲートペア素通しな判定を1回やる。
        /// </summary>
        private static bool CanBePictgraphic(char c) => CanBePictgraphicBmp(c) || char.IsHighSurrogate(c);

        /// <summary>
        /// BMP 内で Extended_Pictographic 候補になる文字。
        /// </summary>
        private static bool CanBePictgraphicBmp(char c) => (c > (char)0x200D && c < (char)0x3300) || c == '©' || c == '®';

        /// <summary>
        /// <see cref="GetEmojiSequence(ReadOnlySpan{char})"/> の主要処理。
        /// 「keycap と国旗を除けばだいぶシンプルになる」前提の Emoji ZWJ sequence 判定。
        /// </summary>
        /// <remarks>
        /// 後々 skin variation を調べるために skin tone (0～2個)の情報も <see cref="ZwjSplitResult"/> に含めて返してる。
        /// </remarks>
        private static (int count, ZwjSplitResult zwjPositions) IsZwjSequence(ReadOnlySpan<char> s)
        {
            // ZWJ シーケンス。
            var count = 0;
            Byte8 zwjPositions = default;
            var zwjIndex = 0;
            var span = zwjPositions.AsSpan();
            var tone1 = SkinTone.None;
            SkinTone tone2;

            while (true)
            {
                tone2 = SkinTone.None;

                var pict = IsPictgraphicEstimate(s);

                if (pict == 0) break;

                count += pict;
                s = s.Slice(pict);

                while (true)
                {
                    var skinTone = IsSkinTone(s);
                    if (skinTone > 0)
                    {
                        if (zwjIndex == 0) tone1 = skinTone;
                        else tone2 = skinTone;

                        count += 2;
                        s = s.Slice(2);
                    }
                    else if (s.Length >= 1 && s[0] == 0xFE0F)
                    {
                        ++count;
                        s = s.Slice(1);
                    }
                    else break;
                }

                if (s.Length >= 1 && s[0] == (char)0x200D)
                {
                    if(zwjIndex < ZwjSplitResult.MaxLength)
                    {
                        span[zwjIndex] = (byte)count;
                        ++zwjIndex;
                    }

                    ++count;
                    s = s.Slice(1);
                }
                else break;
            }

            return (count, new ZwjSplitResult(zwjPositions, new SkinTonePair(tone1, tone2)));
        }
    }
}
