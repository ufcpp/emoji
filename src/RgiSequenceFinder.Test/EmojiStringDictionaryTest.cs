using RgiSequenceFinder.TableGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RgiSequenceFinder.Test
{
    public class EmojiStringDictionaryTest : IAsyncLifetime
    {
        private string[] _rawData = null!;
        private EmojiDataRow[] _data = null!;
        public Task DisposeAsync() => Task.CompletedTask;
        public async Task InitializeAsync()
        {
            _rawData = await DataCache.GetRawData().ConfigureAwait(false);
            _data = await DataCache.GetData().ConfigureAwait(false);
        }

        [Fact]
        public void GetValue()
        {
            var categorized = new CategorizedEmoji(_data);

            var dics = new[]
            {
                ToDictionary(categorized.OtherNoSkin[0], 1),
                ToDictionary(categorized.OtherNoSkin[1], 2),
                ToDictionary(categorized.OtherNoSkin[2], 3),
                ToDictionary(categorized.OtherNoSkin[3], 4),
                ToDictionary(categorized.OtherOneSkin[0], 1),
                ToDictionary(categorized.OtherOneSkin[1], 2),
                ToDictionary(categorized.OtherTwoSkin[2], 3),
                ToDictionary(categorized.OtherVarTwoSkin[2], 3),
                // Unicode 13.1 になったら OtherTwoSkin[3], OtherVarTwoSkin[3] も必要なはず。
            };

            foreach (var x in _rawData)
            {
                var seq = GraphemeBreak.GetEmojiSequence(x);

                if (seq.Type == EmojiSequenceType.Other)
                {
                    // keycap, 国旗、skin tone を除けば、上記テーブルのどれか1つ限りに入ってるはず。
                    var count = 0;
                    foreach (var d in dics)
                    {
                        if (d.GetValue(x) is not null) count++;
                    }
                    Assert.Equal(1, count);
                }
            }
        }

        private static EmojiStringDictionary ToDictionary(List<(ushort[] emoji, int index)> list, int utf16Len)
        {
            var count = list.Count;
            var concat = new ushort[count * utf16Len];
            var indexes = new ushort[count];

            for (int i = 0; i < count; i++)
            {
                var (emoji, index) = list[i];
                emoji.AsSpan().CopyTo(concat.AsSpan(utf16Len * i));
                indexes[i] = (ushort)index;
            }

            var bits = (int)Math.Round(Math.Log2(count));

            // ビット数削るほど被り率上がるので、256/512 を境にビット数増やしてる。
            bits = bits <= 7 ? bits + 2 : bits + 1;
            var capacity = 1 << bits;

            return new((byte)utf16Len, capacity, concat, indexes);
        }

        [Fact]
        public void CapacityMustBePowerOf2()
        {
            foreach (var i in new[] { 1, 2, 4, 8, 16, 32, 64 })
            {
                _ = new EmojiStringDictionary(1, i, new ushort[i], new ushort[i]);
            }

            foreach (var i in new[] { 3, 5, 6, 7, 9, 10, 11, 12, 13, 14, 15, 17, 18, 19, 20, 21, 22, 31, 33, 63, 65, 127, 1000 })
            {
                var n = 2 << BitOperations.Log2((uint)i - 1);
                _ = new EmojiStringDictionary(1, n, new ushort[n], new ushort[n]);

                Assert.Throws<ArgumentException>(() =>
                {
                    _ = new EmojiStringDictionary(1, i, new ushort[i], new ushort[i]);
                });
            }
        }

        [Fact]
        public void CheckLength()
        {
            for (int count = 0; count < 100; count++)
            {
                for (int len = 1; len <= 4; len++)
                {
                    var capacity = 2 << BitOperations.Log2((uint)count - 1);
                    _ = new EmojiStringDictionary((byte)len, capacity, new ushort[len * count], new ushort[count]);

                    Assert.Throws<ArgumentException>(() =>
                    {
                        _ = new EmojiStringDictionary((byte)len, capacity, new ushort[len * count + 1], new ushort[count]);
                    });

                    Assert.Throws<ArgumentException>(() =>
                    {
                        _ = new EmojiStringDictionary((byte)len, capacity, new ushort[len * count], new ushort[count + 1]);
                    });
                }
            }
        }
    }
}
