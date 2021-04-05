using System;

namespace RgiSequenceFinder
{
    /// <summary>
    /// RGI 絵文字の最終判定用のテーブル。
    /// コード生成してる(このファイル自体は手書き。 partial をコード生成)。
    /// </summary>
    /// <remarks>
    /// 今、<see cref="GraphemeBreak"/> をテーブル生成用の SourceGenerator でも参照してるので、
    /// 相互依存状態になってて、「ソースコード生成結果がおかしくなるとコンパイルできなくなって生成しなおせない」みたいな状況になってる。
    /// <see cref="RgiTable"/> と <see cref="Finder"/> だけさらに独立させれば問題は解決するんだけど面倒でやってない。
    /// </remarks>
    internal partial class RgiTable
    {
        /// <summary>
        /// <see cref="Finder.Find(ReadOnlySpan{char}, Span{EmojiIndex})"/>
        ///
        /// <see cref="RgiTable"/> 自体を public にするのはいまいちかもと思って <see cref="Finder"/> をあとから分けた名残り。
        /// </summary>
        public static (int charRead, int indexWritten) Find(ReadOnlySpan<char> s, Span<EmojiIndex> indexes)
        {
            if (s.Length == 0) return (0, 0);

            var emoji = GraphemeBreak.GetEmojiSequence(s);

            switch (emoji.Type)
            {
                case EmojiSequenceType.Other:
                    {
                        var span = s.Slice(0, emoji.LengthInUtf16);
                        var i = FindOther(span, emoji.ZwjPositions);

                        if (i >= 0)
                        {
                            indexes[0] = i;
                            return (emoji.LengthInUtf16, 1);
                        }

                        // 素直に見つからなかったときの再検索
                        // - ZWJ で分割して再検索
                        // - FE0F(異体字セレクター16)を消してみて再検索
                        // - 1F3FB～1F3FF (肌色選択)を消してみて再検索 + 肌色自体の絵
                        var written = SplitZwjSequence(emoji.ZwjPositions, span, indexes);

                        return (emoji.LengthInUtf16, written);
                    }
                case EmojiSequenceType.Keycap:
                    {
                        var i = FindKeycap(emoji.Keycap);
                        if (i < 0) return (3, 0);
                        indexes[0] = i;
                        return (3, 1);
                    }
                case EmojiSequenceType.Flag:
                    {
                        var i = FindRegion(emoji.Region);

                        // ほんとは正確な仕様ではないものの、未対応の Flag sequence は ASCII 2文字に展開しちゃう。
                        //
                        // Unicode 的には「Regional Indicator の片割れは絵文字候補じゃない」扱いだし表示しなくていいと思う。
                        // が、ポリティカルな理由で意図的に国旗を表示しない某 OS は全ての Flag sequence をアルファベット2文字で表示してるし、
                        // たいていのプラットフォームは RI の片割れを「四角囲みのアルファベット」の絵で表示してる。
                        // 四角囲みのアルファベットは emoji-data.json にデータがないので、うちは ASCII 2文字に変換してしまうことに。

                        if (i < 0)
                        {
                            indexes[0] = new EmojiIndex(emoji.Region.First);
                            if (indexes.Length > 1) indexes[1] = new EmojiIndex(emoji.Region.Second);
                            return (4, 2);
                        }

                        if (i < 0) return (4, 0);

                        indexes[0] = i;
                        return (4, 1);
                    }
                case EmojiSequenceType.Tag:
                    {
                        var i = FindTag(emoji.Tags);

                        // 見つからなかった時、タグ文字を削って再検索する。
                        // 例えば 1F3F4 E006A E0070 E0031 E0033 E007F (原理的にはあり得る「東京都(JP13)の旗」)を 1F3F4 (🏴) だけにして返すみたいなの。
                        //
                        // http://unicode.org/reports/tr51/#DisplayInvalidEmojiTagSeqs 曰く、
                        // 推奨としては、「未サポートな旗」であることがわかるように黒旗に ? マークを重ねるか、黒旗 + ? で表示しろとのこと。
                        // ただ、タグ文字を解釈できないときには黒旗だけの表示も認めてそう。
                        //
                        // この実装では黒旗だけの表示をすることにする。
                        if (i < 0) i = _noSkin1Table.GetValue(s.Slice(0, 2)) ?? -1;

                        if (i < 0) return (emoji.LengthInUtf16, 0);

                        indexes[0] = i;
                        return (emoji.LengthInUtf16, 1);
                    }
                case EmojiSequenceType.SkinTone:
                    {
                        var i = FindSkinTone(emoji.SkinTone);
                        indexes[0] = i;
                        return (2, 1);
                    }
                default:
                case EmojiSequenceType.NotEmoji:
                case EmojiSequenceType.MoreBufferRequired:
                    // MoreBufferRequired の時は throw する？
                    return (emoji.LengthInUtf16, 0);
            }
        }

        /// <summary>
        /// RGI にない ZWJ sequence が来た時、ZWJ で分割してそれぞれ FindOther してみる。
        /// </summary>
        /// <returns><paramref name="indexes"/> に書き込んだ長さ。</returns>
        /// <remarks>
        /// さすがに <see cref="Find(ReadOnlySpan{char}, Span{int})"/> からの再起は要らないと思う。たぶん。
        /// FindOther しか見ないので、国旗 + ZWJ とかは受け付けない。
        /// </remarks>
        private static int SplitZwjSequence(ZwjSplitResult zwjPositions, ReadOnlySpan<char> s, Span<EmojiIndex> indexes)
        {
            var totalWritten = 0;
            var prevPos = 0;

            // ZWJ なしの単体。
            if(zwjPositions[0] == 0)
            {
                return ReduceExtends(s, indexes, false);
            }

            for (int j = 0; j < ZwjSplitResult.MaxLength; j++)
            {
                var pos = zwjPositions[j];
                if (pos == 0) break;

                var written = ReduceExtends(s.Slice(prevPos, pos - prevPos), indexes, true);
                totalWritten += written;
                indexes = indexes.Slice(written);
                prevPos = pos + 1;
            }

            totalWritten += ReduceExtends(s.Slice(prevPos), indexes, true);

            return totalWritten;
        }


        /// <summary>
        /// Extend (FE0F と skin tone) 削り。
        /// FE0F → ただ消す。
        /// skin tone → 基本絵文字 + 肌色四角に分解。
        /// </summary>
        /// <returns><paramref name="indexes"/> に書き込んだ長さ。</returns>
        private static int ReduceExtends(ReadOnlySpan<char> s, Span<EmojiIndex> indexes, bool allowCharFallback)
        {
            if (s.Length == 0) return 0;

            var firstChar = char.IsHighSurrogate(s[0]) ? 2 : 1;

            // 肌色。
            // skin tone よりも後ろに ZWJ を挟まず何かがくっついてることないはず。
            // この実装ではあっても無視。
            // 2個以上 skin tone が並んでるとかも無視。
            // 間に FEOF が挟まってる場合とかも未サポート。

            SkinTone tone = s.Length >= firstChar + 2
                ? GraphemeBreak.IsSkinTone(s.Slice(firstChar))
                : 0;

            // ZWJ 分割後が普通に skin tone も FE0F も付いてない絵文字なことは多々あるので再検索。

            if (_oneSkin1Table.GetValue(s) is ushort b)
            {
                // tone あり。
                indexes[0] = b + (byte)tone;
                return 1;
            }

            if (_noSkin1Table.GetValue(s) is ushort a)
            {
                if (tone == 0)
                {
                    // tone なし。
                    indexes[0] = a;
                    return 1;
                }
                else
                {
                    // tone なしのはずの文字に tone が付いてる。
                    indexes[0] = a;
                    if (indexes.Length > 1) indexes[1] = FindSkinTone(tone);
                    return 2;
                }
            }

            if (allowCharFallback)
            {
                // ZWJ sequence のときだけ文字素通ししたいので分岐。
                indexes[0] = GetChar(s);
                return 1;
            }
            else return 0;
        }

        /// <summary>
        /// 文字素通し <see cref="EmojiIndex"/> を返す。
        /// </summary>
        /// <remarks>
        /// ZWJ 分割後は非絵文字が混ざることがある。
        /// 一部の「単独だと絵文字扱いをうけないけども FE0F が付いてれば絵文字」な類の文字が、
        /// ZWJ Sequence 内では単独で出てきたりする。
        /// その場合、その文字を単体で返す。
        /// </remarks>
        private static EmojiIndex GetChar(ReadOnlySpan<char> s)
        {
            if (char.IsHighSurrogate(s[0]))
            {
                // 不正な UTF-16 の時どうしよう。例外の方がいい？
                if (s.Length < 2 || !char.IsLowSurrogate(s[1]))
                    return new EmojiIndex('\0');

                return new EmojiIndex(s[0], s[1]);
            }
            else
            {
                return new EmojiIndex(s[0]);
            }
        }

        private static int FindOther(ReadOnlySpan<char> s, ZwjSplitResult zwjs)
        {
            var len = zwjs.Length;
            var noTone = zwjs.SkinTones.Length == 0;

            if (len == 0)
            {
                if (_noSkin1Table.GetValue(s) is ushort a && noTone) return a;
                else if (_oneSkin1Table.GetValue(s) is ushort b) return b + oneOffset(zwjs.SkinTones);
            }
            else if (len == 1)
            {
                if (_noSkin2Table.GetValue(s) is ushort a && noTone) return a;
                else if (_oneSkin2Table.GetValue(s) is ushort b) return b + oneOffset(zwjs.SkinTones);
            }
            else if (len == 2)
            {
                if (_noSkin3Table.GetValue(s) is ushort a && noTone) return a;
                else if (_twoSkin3Table.GetValue(s) is ushort b) return b + twoOffset(zwjs.SkinTones);
                else if (_varTwoSkin3Table.GetValue(s) is ushort c) return c + varTwoOffset(zwjs.SkinTones);
            }
            else if (len == 3)
            {
                if (_noSkin4Table.GetValue(s) is ushort a && noTone) return a;
            }
            return -1;

            // emoji-data.json の並び的に、 skin_variations の並びは skin tone から機械的に決定できる。
            // ただ、3パターンある。

            // skin tone 1つ持ち
            int oneOffset(SkinTonePair tones) => (int)tones.Tone1;

            // skin tone 2つ持ち
            int twoOffset(SkinTonePair tones)
            {
                var (t1, t2) = tones;
                if (t1 == 0 || t2 == 0) return 0;
                return 5 * t1 + t2 - 5;
            }

            // 👫👬👭 用特殊処理
            int varTwoOffset(SkinTonePair tones)
            {
                var (t1, t2) = tones;
                if (t1 == 0 || t2 == 0) return 0;
                return t1 == t2
                    ? t1
                    : 4 * t1 + t2 - (t1 < t2 ? 1 : 0) + 1;
            }
        }
    }
}
