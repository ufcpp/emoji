using RgiSequenceFinder.TableGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        if (d.TryGetValue(x, out _)) count++;
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
                var r = list[i];
                r.emoji.AsSpan().CopyTo(concat.AsSpan(utf16Len * i));
                indexes[i] = (ushort)r.index;
            }

            var bits = (int)Math.Round(Math.Log2(count));

            // ビット数削るほど被り率上がるので、256/512 を境にビット数増やしてる。
            bits = bits <= 7 ? bits + 2 : bits + 1;
            var capacity = 1 << bits;

            return new((byte)utf16Len, capacity, concat, indexes);
        }
    }
}
