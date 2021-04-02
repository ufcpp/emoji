using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RgiSequenceFinder.TableGenerator.Experimental
{
    /// <summary>
    /// いろんなプラットフォームでの絵文字のレンダリングを見てるに、U+FE0F の有無によって emoji/text style の切り替えするのがばかばかしくなってきてる。
    ///
    /// しいて言うなら text 表示できた方がいいかもしれないと思えるのは、
    /// ©® (00A9, 00AE) … latin-1 くらいはさすがに。
    /// ‼⁉™㊗㊙ (203C, 2049, 2122, 3297, 3299) … たまにこれの text 表示見かけることがある。
    /// ♟(265F) … この周辺にチェスのコマが色々あるけど、黒ポーンだけが FE0F を付けて絵文字になりえる。
    /// くらい。
    ///
    /// なので今、FE0F を単純に無視することを考えてる。
    /// そうなったとき、keycap と国旗を除くと、
    ///
    /// - ZWJ で split 後、skin tone と FE0F を削ったら1文字しか残らないはず
    /// - その文字は A9, AE, 200E～3300, 1F000 台 という範囲しかないはず
    /// - 1F000 台の文字を 1000 台とかに振り替えてしまえばサロゲートペアも消えて、0～3300の範囲の数値1個になるはず
    ///
    /// みたいなことできそうな感じがしてる。
    /// FE0F, 200D, サロゲートペアの処理が要らなくなるのでテーブルをコンパクトにできそう。
    ///
    /// その辺りの確認。
    /// </summary>
    class Compaction
    {
        /// <summary>
        /// keycaps、国旗、skin tone variation を除くと、
        /// ZWJ での分割結果は、
        /// - 1文字
        /// - 1文字 + FE0F
        /// のどれかになってるはず。
        ///
        /// その1文字は、BMP の時は2000～3300あたり、
        /// SMP のときは 1F000 台のはず。
        ///
        /// 1F000 台を 1000 台に振り替えとかすれば、被りのない 0～3300 辺りのユニークな値を得られるはず。
        ///
        /// この前提に当てはまってないパターンの文字列が来た時には 0 を返す。
        /// </summary>
        private static char Convert(ReadOnlySpan<char> s)
        {
            if (s.Length == 1)
            {
                return s[0];
            }
            else if (s.Length == 2)
            {
                if (char.IsHighSurrogate(s[0]) && char.IsLowSurrogate(s[1]))
                {
                    Rune.DecodeFromUtf16(s, out var r, out _);
                    return (char)(r.Value - 0x1E000); // 1F000台を1000台に振り替え。
                }
                else if (s[1] == '\uFE0F')
                {
                    return s[0];
                }
            }
            else if (s.Length == 3)
            {
                if (char.IsHighSurrogate(s[0]) && char.IsLowSurrogate(s[1]) && s[2] == '\uFE0F')
                {
                    Rune.DecodeFromUtf16(s, out var r, out _);
                    return (char)(r.Value - 0x1E000); // 1F000台を1000台に振り替え。
                }
            }

            return '\0';
        }

        public static void CheckConversion()
        {
            var list = Data.RgiEmojiSequenceList;
            Span<char> buffer = stackalloc char[16];
            var convertedSequences = new List<string>();

            foreach (var s in list)
            {
                var emoji = GraphemeBreak.GetEmojiSequence(s);

                // keycaps とか国旗とかは別枠。
                if (emoji.Type != EmojiSequenceType.Other) continue;

                // skin tone variation も別枠。
                if (emoji.ZwjPositions.SkinTones.Length > 0) continue;

                var split = s.Split('\u200D');

                var i = 0;
                foreach (var part in split)
                {
                    var c = Convert(part);

                    // 想定パターン以外が来てないか確認。
                    Debug.Assert(c != '\0');

                    buffer[i] = c;
                    ++i;
                }

                convertedSequences.Add(new string(buffer[..i]));
            }

            // Convert で作った文字列、ユニークなはず。
            var set = new HashSet<string>();
            var index = 0;
            foreach (var s in convertedSequences)
            {
                if (set.Contains(s))
                {
                    Debug.Assert(false);
                }
                set.Add(s);
                ++index;
            }

            // 文字列長毎にグループ化した方が辞書作りが楽かもしれないのでそれぞれの内容確認。
            foreach (var g in convertedSequences.GroupBy(x => x.Length).OrderBy(x => x.Key))
            {
                Console.WriteLine((g.Key, g.Count()));
            }
        }
    }
}
