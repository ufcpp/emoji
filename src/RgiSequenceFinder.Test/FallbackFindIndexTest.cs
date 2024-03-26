using System.Text;
using Xunit;

namespace RgiSequenceFinder.Test;

/// <summary>
/// 過剰に FE0F がついてるとか、過剰に ZWJ でつながってるとか、RGI にないパターンで肌色選択が掛かってるのとかを分割したり余計な分を削ったりのテスト。
/// </summary>
public class FallbackFindIndexTest : IAsyncLifetime
{
    private string[] _data = null!;
    public Task DisposeAsync() => Task.CompletedTask;
    public async Task InitializeAsync() => _data = await DataCache.GetRawData().ConfigureAwait(false);

    [Theory]
    [InlineData("✨")] // 2728, Sparkles
    [InlineData("❌")] // 274C, Cross Mark
    [InlineData("⬛")] // 2B1B, Black Large Square
    [InlineData("🐈")] // 1F408, Cat
    public void 末尾FE0Fを削る(string emoji)
    {
        Span<EmojiIndex> indexes = stackalloc EmojiIndex[1];

        // 単独で RGI になってるものをあえて選んでるはずなのを一応確認。
        var (len, indexWritten) = RgiTable.Find(emoji, indexes);

        Assert.Equal(1, indexWritten);
        Assert.Equal(emoji.Length, len);

        // 元データと照会。
        var indexFromData = _data.TakeWhile(x => x != emoji).Count();
        Assert.Equal(indexFromData, indexes[0]);

        // FE0F 足してみる。
        var fe0fAdded = emoji + "\uFE0F";
        (len, indexWritten) = RgiTable.Find(fe0fAdded, indexes);

        Assert.Equal(1, indexWritten);
        Assert.Equal(fe0fAdded.Length, len);
        Assert.Equal(indexFromData, indexes[0]);
    }

    [Fact]
    public void 未サポート旗Region()
    {
        Span<EmojiIndex> indexes = stackalloc EmojiIndex[2];
        Span<char> fallbackChars = stackalloc char[1];

        // 未サポート旗、ASCII の A～Z 2文字に fallback するように作った。
        var (len, indexWritten) = RgiTable.Find("\U0001F1E6\U0001F1E7", indexes);
        Assert.Equal(2, indexWritten);

        Assert.Equal(1, indexes[0].WriteUtf16(fallbackChars));
        Assert.Equal('A', fallbackChars[0]);
        Assert.Equal(1, indexes[1].WriteUtf16(fallbackChars));
        Assert.Equal('B', fallbackChars[0]);

        // 別途 EmojiIndex の単体テストを書けという感じはするけども…
        Assert.Equal(new Rune('A').Value, (int)indexes[0].Rune);
        Assert.Equal(new Rune('B').Value, (int)indexes[1].Rune);
    }

    [Fact]
    public void 未サポート旗Tag()
    {
        Span<EmojiIndex> indexes = stackalloc EmojiIndex[1];

        // 未サポート旗、黒旗だけの絵文字に fallback するように作った。
        var (len, indexWritten) = RgiTable.Find("\U0001F3F4", indexes);
        var fallbackIndex = indexes[0];

        // gbsct 🏴󠁧󠁢󠁳󠁣󠁴󠁿
        // iOS とかではちゃんとスコットランド旗が出る。
        // Windows でも Twitter とかにコピペするとスコットランド旗画像に指し変わる。
        var supported = "\U0001F3F4\U000E0067\U000E0062\U000E0073\U000E0063\U000E0074\U000E007F";

        // この場合は普通に対応するインデックスが取れる。
        (len, indexWritten) = RgiTable.Find(supported, indexes);
        Assert.Equal(1, indexWritten);
        Assert.Equal(supported.Length, len);
        Assert.NotEqual(fallbackIndex, indexes[0]);

        // jp13 (東京都) 🏴󠁪󠁰󠀱󠀳󠁿
        // Emoji tag sequence の仕様上、1F3F4 (黒旗)の後ろに ISO 3166-2 (行政区画コード)に相当するタグ文字が並んでるとその区の旗という扱いになる。
        // 原理的にいくらでもサポートできる旗を増やせるというだけで RGI に入ってるのは gbeng, gbsct, gbwls だけ。
        // (でも、Emojipedia には並んでたりする。 https://emojipedia.org/flag-for-tokyo-jp13/)
        var unsupported = "\U0001F3F4\U000E006A\U000E0070\U000E0031\U000E0033\U000E007F";

        (len, indexWritten) = RgiTable.Find(unsupported, indexes);
        Assert.Equal(1, indexWritten);
        Assert.Equal(unsupported.Length, len);
        Assert.Equal(fallbackIndex, indexes[0]);
    }

    [Fact]
    public void 未サポート肌色修飾()
    {
        // 肌色が関係ない基本絵文字、動物とかを適当に選んだもの。
        var emojis = new[]
        {
                "♈", "♉", "♊", "♋", "♌", "♍", "♎", "♏", "♐", "♑", "♒", "♓",
                "🐭", "🐮", "🐯", "🐰", "🐲", "🐍", "🐴", "🐏", "🐵", "🐔", "🐶", "🐗",
            };

        var skinTones = new[] { "🏻", "🏼", "🏽", "🏾", "🏿", };

        // 基本絵文字と skin tone を交互に並べる。
        string concat()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < emojis.Length; i++)
            {
                sb.Append(emojis[i]);
                sb.Append(skinTones[i % skinTones.Length]);
            }

            return sb.ToString();
        }

        static HashSet<int> toIndex(string[] strings)
        {
            Span<EmojiIndex> indexes = stackalloc EmojiIndex[1];
            var set = new HashSet<int>();
            foreach (var st in strings)
            {
                RgiTable.Find(st, indexes);
                set.Add(indexes[0].Index);
            }
            return set;
        }

        var s = concat().AsSpan();
        var skinToneIndexes = toIndex(skinTones);
        Span<EmojiIndex> indexes = stackalloc EmojiIndex[2];

        while (true)
        {
            // RGI に含まれていないので、基本絵文字と skin tone の2文字分返ってくる。
            var (len, indexWritten) = RgiTable.Find(s, indexes);
            Assert.Equal(2, indexWritten);
            Assert.Contains(indexes[1].Index, skinToneIndexes);

            s = s[len..];
            if (s.Length == 0) break;
        }
    }

    private static int FindAll(ReadOnlySpan<char> s, Span<EmojiIndex> indexes)
    {
        int totalWritten = 0;
        while (true)
        {
            var (read, written) = RgiTable.Find(s, indexes);

            totalWritten += written;
            s = s[read..];
            indexes = indexes[written..];

            if (s.Length == 0) break;
        }
        return totalWritten;
    }

    [Fact]
    public void 未サポートZWJシーケンス()
    {
        Span<EmojiIndex> indexes = stackalloc EmojiIndex[2];

        RgiTable.Find("🐱", indexes);
        var catIndex = indexes[0];

        // 🐱‍👤🐱‍🏍🐱‍💻🐱‍🐉🐱‍👓🐱‍🚀
        // Windows オリジナルキャラの忍者猫。
        // Microsoft 内で使ってたマスコットだったらしい。
        // 1F431 200D の後ろにそれぞれ 1F464, 1F3CD, 1F4BB, 1F409, 1F453, 1F680
        // 当然 RGI には入ってないのでちょうどいいので未サポート ZWJ sequence のテストデータに使う。
        //
        // 🐱‍🏍 は 🏍 (1F3CD)が「FE0F がついてるときだけ絵文字扱い」の文字なので、FE0F の扱いによっては別枠にしないとダメ。
        // 現状は FE0F を単に無視して絵文字判定してるので混ぜて大丈夫。
        var ninjaCats = new[] { "🐱‍👤", "🐱‍🏍", "🐱‍💻", "🐱‍🐉", "🐱‍👓", "🐱‍🚀" };

        foreach (var cat in ninjaCats)
        {
            var (read, written) = RgiTable.Find(cat, indexes);

            Assert.Equal(5, read);
            Assert.Equal(2, written);
            Assert.Equal(catIndex, indexes[0]);
        }

        // 未対応 ZWJ sequence は、ZWJ を消し去ったのと同じ結果を産むはず。
        Span<EmojiIndex> indexes1 = stackalloc EmojiIndex[12];
        var concat = string.Concat(ninjaCats);
        var written1 = FindAll(concat, indexes1);
        Assert.Equal(ninjaCats.Length * 2, written1);

        Span<EmojiIndex> indexes2 = stackalloc EmojiIndex[12];
        var zwjRemoved = concat.Replace("\u200D", "");
        var written2 = FindAll(zwjRemoved, indexes2);
        Assert.Equal(ninjaCats.Length * 2, written2);

        Assert.True(indexes1.SequenceEqual(indexes2));
    }

    [Fact]
    public void コーナーケース()
    {
        string[] emojis = [
            // ZWJが3つのケース
            // Family: Adult, Adult, Child, Child
            // 1F9D1, 200D, 1F9D1, 200D, 1F9D2, 200D, 1F9D2
            "🧑‍🧑‍🧒‍🧒",
            // Skin Tone 1つ、ZWJ 2つのケース。
            // Woman in Motorized Wheelchair Facing Right: Medium-Dark Skin Tone
            // 1F469, 1F3FE, 200D, 1F9BC, 200D, 27A1, FE0F
            "👩🏾‍🦼‍➡️",
            // Skin Tone 2つ、ZWJ 3つのケース
            // kiss: person, person, medium-light skin tone, medium skin tone
            // 1F9D1, 1F3FC, 200D, 2764, FE0F, 200D, 1F48B, 200D, 1F9D1, 1F3FD
            "🧑🏼‍❤️‍💋‍🧑🏽",
        ];

        var indexes = (stackalloc EmojiIndex[12]);
        foreach (var s in emojis)
        {
            var (read, written) = RgiTable.Find(s, indexes);
            Assert.Equal(1, written);
        }
    }

    [Fact]
    public void 未サポートZWJ肌色シーケンス()
    {
        // 👩🏻‍👩🏿‍👧🏼‍👧🏾
        // 1F469 1F3FB 200D 1F469 1F3FF 200D 1F467 1F3FC 200D 1F467 1F3FE
        // 肌色違いの4人家族。
        // Windows はむっちゃ頑張って指定した肌色で家族くっつけてレンダリングする。
        //
        // 一方で、 RGI 的には Unicode 12.0 以降、カップル絵文字までは肌色の組み合わせ(5×5)に対応したけど、さすがに3人以上の家族絵文字は適用外。
        // この場合、👩🏻👩🏿👧🏼👧🏾 (ZWJ を除去したもの)と同じ結果を生んでほしい。
        //
        // Unicode 15.1 で4人家族に対応したけど、さすがに肌色のバリエーションまでは未対応。

        var family = "👩🏻‍👩🏿‍👧🏼‍👧🏾";

        // 未対応 ZWJ sequence は、ZWJ を消し去ったのと同じ結果を産むはず。
        Span<EmojiIndex> indexes1 = stackalloc EmojiIndex[12];
        var written1 = FindAll(family, indexes1);
        Assert.Equal(4, written1);

        Span<EmojiIndex> indexes2 = stackalloc EmojiIndex[12];
        var zwjRemoved = family.Replace("\u200D", "");
        var written2 = FindAll(zwjRemoved, indexes2);
        Assert.Equal(4, written2);

        Assert.True(indexes1.SequenceEqual(indexes2));
    }
}
