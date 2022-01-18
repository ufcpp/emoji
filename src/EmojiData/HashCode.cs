using System.Text;

namespace EmojiData
{
    /// <summary>
    /// 「絵文字シーケンスになりそうな文字列」のパターンは意外と少ないので、
    /// ハッシュ値が被ることほとんどないんじゃないかという前提で、
    /// 「元の文字列を持たない、ハッシュ値だけを定数で持ってハッシュ値だけでテーブルを引く」でいけるかもしれないというのの調査。
    ///
    /// 結果:
    /// 文字列のハッシュ値、結構ちゃんとしたハッシュアルゴリズムでも4・5文字長くらいから被り結構出てくる。
    /// RGI に限ってハッシュ値被りがないくらいならいけそうだけど、他の絵文字シーケンスになりうる任意の文字列とも被らないようにするのは無理そう。
    ///
    /// RGI に限っても、たかだか3300種類を32ビットハッシュにするのでも被りのなく作るの結構大変そう。
    /// 今現在被りがなくても、Unicode のバージョンアップのたびに確認が必要だし、被りが見つかったらハッシュ値計算に使う定数を調整するとかが必要。
    /// </summary>
    class HashCode
    {
        /// <summary>
        /// ハッシュ値計算をどうするか。
        /// </summary>
        /// <remarks>
        /// .NET 5.0 時点ではクラス名からすると「Marvin hash」ってのを使ってるっぽい。
        ///
        /// .NET Framework 4.7 時代は djb2 (線形合同法的なの)の亜種っぽかった。
        ///
        /// TextMeshPro も djb2 っぽく、net47 のものよりももっと簡素。
        /// 「まあ絵文字のパターンはそんなに多くない」前提があるのでそこまでこだわる必要はないんじゃないかと言うことで、簡素なやつから試す。
        /// </remarks>
        private static int GetSimpleHashCode(ReadOnlySpan<char> s)
        {
#if false
            // 標準ライブラリの。
            // セキュリティ上の理由で毎度乱数を含めてプロセス起動のたびに別の値を返すようにしてあるので、
            // 今回みたいな事前計算用途では使えないけど比較のために試すだけ試す。
            return string.GetHashCode(s);
#endif

#if false
            // TextMeshPro のやつ。
            // サロゲートペアに対して結構な率で衝突してそう。
            int hashCode = 5381;
            for (int i = 0; i < s.Length; i++)
                hashCode = ((hashCode << 5) + hashCode) ^ s[i];
            return hashCode;
#else

            // net47 時代の実装。
            // 奇数と偶数で処理分けてるの、サロゲートペア対策かも。
            int hash1 = 5381;
            int hash2 = hash1;

            while (s.Length > 0)
            {
                var c = s[0];
                hash1 = ((hash1 << 5) + hash1) ^ c;
                if (s.Length <= 1)
                    break;
                c = s[1];
                hash2 = ((hash2 << 5) + hash2) ^ c;
                s = s[2..];
            }

            return hash1 + (hash2 * 1566083941);
#endif
        }

#if true
        private static readonly Rune[] _emojiBaseCandidates =
            Enumerable.Range(0x200E, 0x3300 - 0x200D)
            .Concat(Enumerable.Range(0x1F000, 0x1000))
            .Select(x => new Rune(x))
            .ToArray();

        private static readonly Rune[] _skinTones =
            Enumerable.Range(0x1F3FB, 5)
            .Select(x => new Rune(x))
            .ToArray();

        private const char _zwj = '\u200D';
        private const char _fe0f = '\uFE0F';
#else
        private static readonly Rune[] _emojiBaseCandidates =
            Enumerable.Range(0x202C, 0x3300 - 0x202C)
            .Concat(Enumerable.Range(0x4000, 0x1000))
            .Select(x => new Rune(x))
            .ToArray();

        private static readonly Rune[] _skinTones = new[] { 65423, 65437, 65447, 65449, 65479   }.Select(x => new Rune(x)).ToArray();

        private const char _zwj = (char)65497;
        private const char _fe0f = (char)65519;
#endif

        // 「単独で絵文字の候補 + 肌色 or FE0F」は最大で UTF-16 5文字。
        private static readonly char[] _buffer1 = new char[5];

        // まず ZWJ ないタイプの候補。
        // 「単独で絵文字の候補 + 肌色 or FE0F」
        // この時点で15万パターンくらい
        private static IEnumerable<ReadOnlyMemory<char>> EnumerateNonZwjCandidates()
        {
            foreach (var c in _emojiBaseCandidates)
            {
                var w = c.EncodeToUtf16(_buffer1);
                yield return _buffer1.AsMemory(0, w);

#if false
                foreach (var st in _skinTones)
                {
                    // ベース + 肌色
                    var len = st.EncodeToUtf16(_buffer1.AsSpan(w));
                    yield return _buffer1.AsMemory(0, w + len);

                    // ベース + 肌色 + FE0F
                    _buffer1[w + len] = _fe0f;
                    yield return _buffer1.AsMemory(0, w + len + 1);
                }

                // ベース + FE0F
                _buffer1[w] = _fe0f;
                yield return _buffer1.AsMemory(0, w + 1);

                foreach (var st in _skinTones)
                {
                    // ベース + FE0F + 肌色
                    var len = st.EncodeToUtf16(_buffer1.AsSpan(w + 1));
                    yield return _buffer1.AsMemory(0, w + len + 1);
                }
#endif
            }
        }

        // 普通はどんなに長くても 30 char くらいのシーケンスしかないけど一応多めに取ってる。
        private static readonly char[] _buffer2 = new char[128];

        private static IEnumerable<ReadOnlyMemory<char>> EnumerateEmojiSequenceCandidates()
        {
            foreach (var c in EnumerateNonZwjCandidates())
            {
                yield return c;
            }

            foreach (var c1 in EnumerateNonZwjCandidates())
            {
                var s = _buffer2.AsMemory();
                c1.CopyTo(s);
                s = s[c1.Length..];

                s.Span[0] = _zwj;
                s = s[1..];

                foreach (var c2 in EnumerateNonZwjCandidates())
                {
                    c2.CopyTo(s);
                    yield return _buffer2.AsMemory(0, c1.Length + 1 + c2.Length);
                }
            }
        }

        public static void CheckHashCollision()
        {
            var ss = new HashSet<string>();
            var hashcodes = new HashSet<int>();

            foreach (var s in EnumerateEmojiSequenceCandidates())
            {
                if (ss.Contains(s.ToString())) throw new Exception();
                ss.Add(s.ToString());

                var hash = GetSimpleHashCode(s.Span);

                if (hashcodes.Contains(hash)) throw new Exception();

                hashcodes.Add(hash);
            }
        }
    }
}
