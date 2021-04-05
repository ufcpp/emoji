using RgiSequenceFinder.TableGenerator.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgiSequenceFinder.TableGenerator.Experimental
{
    /// <summary>
    /// <see cref="GroupedEmojis.Singlulars"/>, <see cref="GroupedEmojis.Plurals"/> の動作検証。
    /// </summary>
    class SingularEmoji
    {
        public static void CollisionCount()
        {
            var cat = new CategorizedEmoji(Cache.Data);

            CollisionCount(cat.OtherNoSkin[0].Select(t => t.emoji).ToArray());
            CollisionCount(cat.OtherNoSkin[1].Select(t => t.emoji).ToArray());
            CollisionCount(cat.OtherNoSkin[2].Select(t => t.emoji).ToArray());
            CollisionCount(cat.OtherNoSkin[3].Select(t => t.emoji).ToArray());
            CollisionCount(cat.OtherOneSkin[0].Select(t => t.emoji).ToArray());
            CollisionCount(cat.OtherOneSkin[1].Select(t => t.emoji).ToArray());

            // この2種はそれぞれ1個、3個しかないし下手にハッシュテーブル使うより線形探索の方がいいかも。
            // (Unicode 13.1 でそれぞれ3個、9個になるかも。それでもまあ。)
            CollisionCount(cat.OtherTwoSkin[2].Select(t => t.emoji).ToArray());
            CollisionCount(cat.OtherVarTwoSkin[2].Select(t => t.emoji).ToArray());
        }

        private static void CollisionCount(IEnumerable<ushort[]> list)
        {
            var count = list.Count();
            var bits = (int)Math.Round(Math.Log2(count));

            // ビット数削るほど被り率上がるので、256/512 を境にビット数増やしてる。
            bits = bits <= 7 ? bits + 2 : bits + 1;
            var capacity = 1 << bits;
            var mask = capacity - 1;

            // 元々、下位桁に被りがあんまりないので単純に mod をハッシュ値にしても大して被らないみたい。
            // これでハッシュ値衝突率1割ないくらいになる。
            var groups = list.Select(x => new EmojiString(x).GetHashCode() & mask).GroupBy(x => x);
            var hashDistinct = groups.Count();
            var max = groups.Max(g => g.Count());
            var ave = groups.Average(g => (double)g.Count());

            Console.WriteLine($"{bits,2} ({capacity,4}) {hashDistinct,3}/{count,3} max: {max}, ave: {ave}");
        }
    }
}
