﻿using System;
using System.Linq;

namespace RgiSequenceFinder
{
    /// <summary>
    /// RGI 絵文字シーケンスの辞書化、かなり条件を限定できるので専用のハッシュテーブルを作る。
    /// </summary>
    /// <remarks>
    /// 条件:
    /// - たかだか4000文字程度(Unicode 13.0 で3300文字)
    /// - 最長の文字数もわかってる(Unicode 13.0 で UTF-16 14文字)
    ///
    /// バケットを最初に固定長で取って、以後、resize 一切なし。
    /// GetHashCode の実装も「16文字以下」みたいな条件で計算してる。
    /// </remarks>
    class StringDictionary : System.Collections.IEnumerable
    {
        /// <summary>
        /// 辞書容量。
        /// RGI 絵文字シーケンスが3000～4000文字しかないので、それの倍程度の容量あれば十分。
        /// 13ビット(容量8192)にするか14ビット(容量16384)にするかはちょっと迷う。
        /// </summary>
        private const int Bits = 13;
        private const int Capacity = 1 << Bits;
        private const int Mask = Capacity - 1;

        private static ReadOnlySpan<byte> Primes => new byte[] { 83, 223, 227, 131, 53, 149, 227, 229, 7, 23, 5, 47, 59, 53, 3, 29 };
        private const int Skip = 655883; // 適当な大き目の素数

        private static int GetHashCode(ReadOnlySpan<char> s)
        {
            var primes = Primes;
            var hash = 0;
            for (int i = 0; i < s.Length && i < primes.Length; i++)
            {
                hash += s[i] * primes[i];
            }
            return hash;
        }

        internal struct Bucket
        {
            public string Key;
            public int Value;
            public bool HasValue => Key is not null;
        }

        private readonly Bucket[] _buckets = new Bucket[Capacity];

        /// <summary>
        /// 要素の追加。
        /// </summary>
        /// <remarks>
        /// 最初に想定している以上に追加すると永久ループする可能性があるので注意。
        /// (<see cref="CompactDictionary{TKey, TValue, TComparer}.CompactDictionary(int)"/>の引数で与えた数字の2倍を超えると可能性あり)
        /// </remarks>
        public void Add(string key, int value)
        {
            var hash = GetHashCode(key) & Mask;

            while (true)
            {
                ref var b = ref _buckets[hash];

                if (!b.HasValue)
                {
                    b.Key = key;
                    b.Value = value;
                    break;
                }

                hash = (hash + Skip) & Mask;
            }
        }

        /// <summary>
        /// 値の取得。
        /// キーが見つからなかったら false を返す。
        /// </summary>
        public bool TryGetValue(ReadOnlySpan<char> key, out int value)
        {
            var hash = GetHashCode(key) & Mask;

            while (true)
            {
                ref var b = ref _buckets[hash];

                if (!b.HasValue)
                {
                    value = 0;
                    return false;
                }
                else if (b.Key != null && key.SequenceEqual(b.Key))
                {
                    value = b.Value;
                    return true;
                }

                hash = (hash + Skip) & Mask;
            }
        }

        // コレクション初期化子を使いたいがためだけに IEnumerable で、実際に列挙はしないので空実装。
        // ぬるぽ上等。
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => null!;
    }
}
