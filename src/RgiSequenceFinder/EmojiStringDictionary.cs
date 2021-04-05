using System;

namespace RgiSequenceFinder
{
    /// <summary>
    /// RGI 絵文字シーケンスの辞書化、かなり条件を限定できるので専用のハッシュテーブルを作る。
    /// </summary>
    /// <remarks>
    /// 条件:
    /// - たかだか4000文字程度(Unicode 13.0 で3300文字)
    /// - 半分以上が <see cref="EmojiString"/> 化すると1文字になる
    /// - <see cref="EmojiString"/> 化後の文字数の分布大体わかってて、最大でも4文字
    ///
    /// なので、
    /// - 文字列リテラルは全部連結したものを1個だけ渡す
    ///   - 文字数が同じもの同士をグループ化してからこの辞書を作る
    /// - バケットを最初に固定長で取って、以後、resize 一切なし
    /// - GetHashCode の実装も「一定文字以下」みたいな条件で計算する
    /// </remarks>
    readonly struct EmojiStringDictionary
    {
        private const int Skip = 655883; // 適当な大き目の素数

        internal struct Bucket
        {
            public ushort KeyStart;
            public ushort Index; // Index == 0 は来ない(Keycap #️⃣ に使ってるけど、Keycap は別管理してる)ので これの == 0 で空判定。
            public bool HasValue => Index != 0;
        }

        /// <summary>
        /// キーになる文字列を全部 concat したもの。
        /// キー1つ1つは固定長(長さは一律 <see cref="_utf16Length"/> になるように事前に GroupBy しておく想定)。
        ///
        /// それぞれのキーが長さ1の時は <see cref="Bucket.KeyStart"/> に直接文字を入れてしまう(このフィールドは null)。
        /// </summary>
        private readonly ushort[] _concatinatedString;

        /// <summary>
        /// キーの文字列の UTF-16 長さ。
        /// </summary>
        private readonly int _utf16Length;

        /// <summary>
        /// _buckets[index % _buckets - 1] を index &amp; _mask で代用するために、
        /// _buckets.Length は2のべきという前提で _buckets.Length - 1 を取っておく。
        /// </summary>
        private readonly int _mask;

        /// <summary>
        /// 長さ常に2のべきの想定。
        /// new Bucket[capacity] で作るので capacity が2のべきでないとダメ。
        /// </summary>
        private readonly Bucket[] _buckets;

        public EmojiStringDictionary(byte utf16Length, int capacity, ushort[] concatinatedString, ReadOnlySpan<ushort> indexes)
        {
            //todo: capacity が2のべき乗になってるか確認
            //todo: concatinatedString.Length * utf16Length == indexex.Length のはず
            //todo: capacity >= indexex.Length のはず

            _utf16Length = utf16Length;
            _buckets = new Bucket[capacity];
            _mask = capacity - 1;

            if(utf16Length == 1)
            {
                _concatinatedString = null;

                for (int i = 0; i < indexes.Length; i++)
                {
                    Add(concatinatedString[i], indexes[i]);
                }
            }
            else
            {
                _concatinatedString = concatinatedString;

                var span = concatinatedString.AsSpan();
                ushort total = 0;
                for (int i = 0; i < indexes.Length; i++)
                {
                    Add(span, total, utf16Length, indexes[i]);
                    total += utf16Length;
                }
            }
        }

        /// <summary>
        /// 要素の追加。
        /// </summary>
        private void Add(ReadOnlySpan<ushort> s, ushort keyStart, ushort keyLength, ushort index)
        {
            var mask = _mask;
            var key = s.Slice(keyStart, keyLength);
            var hash = new EmojiString(key).GetHashCode() & mask;
            var buckets = _buckets;

            while (true)
            {
                ref var b = ref buckets[hash];

                if (!b.HasValue)
                {
                    b.KeyStart = keyStart;
                    b.Index = index;
                    break;
                }

                hash = (hash + Skip) & mask;
            }
        }

        /// <summary>
        /// 要素の追加。
        /// 1絵文字 = 1符号点のやつは <see cref="Bucket.KeyStart"/> に直接文字を格納する。
        /// </summary>
        private void Add(ushort key, ushort index)
        {
            var mask = _mask;
            var hash = key & mask;
            var buckets = _buckets;

            while (true)
            {
                ref var b = ref buckets[hash];

                if (!b.HasValue)
                {
                    b.KeyStart = key;
                    b.Index = index;
                    break;
                }

                hash = (hash + Skip) & mask;
            }
        }

        /// <summary>
        /// 値の取得。
        /// キーが見つからなかったら false を返す。
        /// </summary>
        public bool TryGetValue(ReadOnlySpan<char> key, out ushort index)
            => _concatinatedString == null ? TryGetValueSingular(key, out index) : TryGetValuePlural(key, out index);

        public bool TryGetValueSingular(ReadOnlySpan<char> key, out ushort index)
        {
            var mask = _mask;
            var hash = EmojiString.GetHashCode(key) & mask;
            var buckets = _buckets;

            while (true)
            {
                var b = buckets[hash];

                if (!b.HasValue)
                {
                    index = default;
                    return false;
                }
                else
                {
                    if (EmojiString.Equals(b.KeyStart, key))
                    {
                        index = b.Index;
                        return true;
                    }
                }

                hash = (hash + Skip) & mask;
            }
        }

        public bool TryGetValuePlural(ReadOnlySpan<char> key, out ushort index)
        {
            var mask = _mask;
            var hash = EmojiString.GetHashCode(key) & mask;
            var buckets = _buckets;
            var keyLength = _utf16Length;
            var s = _concatinatedString;

            while (true)
            {
                var b = buckets[hash];

                if (!b.HasValue)
                {
                    index = default;
                    return false;
                }
                else
                {
                    if (EmojiString.Equals(s.AsSpan(b.KeyStart, keyLength), key)) // この行しか差がないんだけど、ループの内側の分岐を避けた結果。
                    {
                        index = b.Index;
                        return true;
                    }
                }

                hash = (hash + Skip) & mask;
            }
        }
    }
}
