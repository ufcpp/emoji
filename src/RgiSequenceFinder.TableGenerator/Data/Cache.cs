using EmojiData;
using System.Text.Json;

namespace RgiSequenceFinder.TableGenerator.Data;

/// <summary>
/// Console App だし async 汚染を避けて <see cref="ValueTask{TResult}.Result"/> を呼んじゃってるキャッシュデータ。
/// </summary>
internal class Cache
{
    private static JsonDocument? _doc;
#pragma warning disable CA2012 // Use ValueTasks correctly
    private static JsonDocument Doc => _doc ??= Loader.LoadJsonDocAsync().GetAwaiter().GetResult();
#pragma warning restore CA2012

    private static EmojiDataRow[]? _data;
    public static EmojiDataRow[] Data => _data ??= EmojiDataRow.Load(Doc).ToArray();

    private static string[]? _raw;
    public static string[] RawData => _raw ??= EmojiDataRow.LoadAllStrings(Doc).ToArray();
}
