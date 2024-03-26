using System.Text;
using Xunit;

namespace RgiSequenceFinder.Test;

public class FindIndexTest : IAsyncLifetime
{
    private string[] _data = null!;
    public Task DisposeAsync() => Task.CompletedTask;
    public async Task InitializeAsync() => _data = await DataCache.GetRawData().ConfigureAwait(false);

    [Fact]
    public void Rgi絵文字シーケンス自体のインデックスは必ず見つかる()
    {
        Span<EmojiIndex> indexes = stackalloc EmojiIndex[1];

        var data = _data;

        for (int i = 0; i < data.Length; i++)
        {
            var s = data[i];
            var (len, indexWritten) = RgiTable.Find(s, indexes);

            Assert.Equal(1, indexWritten);
            Assert.Equal(s.Length, len);
            Assert.Equal(i, indexes[0]);
        }
    }

    [Fact]
    public void Rgi絵文字シーケンスの前後にASCII文字挟んでみる()
    {
        Span<EmojiIndex> indexes = stackalloc EmojiIndex[1];

        var data = _data;

        for (int i = 0; i < data.Length; i++)
        {
            var s = data[i];
            var s2 = "a" + s + "a";

            var (len, indexWritten) = RgiTable.Find(s2, indexes);

            Assert.Equal(1, len);
            Assert.Equal(0, indexWritten);

            (len, indexWritten) = RgiTable.Find(s2.AsSpan(1), indexes);

            Assert.Equal(1, indexWritten);
            Assert.Equal(s.Length, len);
            Assert.Equal(i, indexes[0]);

            (len, indexWritten) = RgiTable.Find(s2.AsSpan(1 + len), indexes);

            Assert.Equal(1, len);
            Assert.Equal(0, indexWritten);
        }
    }

    [Fact]
    public void 全RGI絵文字をConcat()
    {
        // 間に何も挟まずに Concat するバージョンも欲しいものの… 肌色セレクターが邪魔。
        // 1F3FB～1F3FF は単体で RgiTable.Find に含まれているものの、GraphemeBreak 的には1個前の絵文字にくっついちゃう。
        Span<EmojiIndex> indexes = stackalloc EmojiIndex[1];

        var data = _data;
        var sb = new StringBuilder();

        foreach (var s in data)
        {
            sb.Append(s);
            sb.Append('a');
        }

        var cat = sb.ToString().AsSpan();

        var odd = false;
        var i = 0;

        while (true)
        {
            if (cat.Length == 0) break;

            var (len, _) = RgiTable.Find(cat, indexes);

            if (odd)
            {
                odd = false;
                Assert.Equal(1, len);
                Assert.Equal('a', cat[0]);
            }
            else
            {
                odd = true;
                Assert.True(data[i].AsSpan().SequenceEqual(cat[..len]));
                ++i;
            }

            cat = cat[len..];
        }
    }

    [Fact]
    public void 全RGI絵文字をConcatしたものをReplaceにかける()
    {
        var data = _data;
        var sb = new StringBuilder();

        foreach (var s in data)
        {
            sb.Append(s);
            sb.Append('a');
        }

        var cat = sb.ToString().AsSpan();

        var buffer = new char[cat.Length];
        var written = Finder.Replace(cat, buffer, ReplaceMode.None);

        foreach (var c in buffer[..written])
        {
            Assert.True(c is 'a' or (>= '\uE000' and < '\uF900'));
        }

        // ゼロ幅スペース埋め版
        written = Finder.Replace(cat, buffer, ReplaceMode.FillsZwsp);

        foreach (var c in buffer[..written])
        {
            Assert.True(c is 'a' or (>= '\uE000' and < '\uF900') or '\u200B');
        }
        Assert.Equal(cat.Length, written);

        // Remove
        written = Finder.Replace(cat, buffer, ReplaceMode.Remove);

        foreach (var c in buffer[..written])
        {
            Assert.True(c is 'a');
        }
    }
}
