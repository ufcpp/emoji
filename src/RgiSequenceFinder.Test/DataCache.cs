using EmojiData;
using RgiSequenceFinder.TableGenerator;
using System.Text.Json;

namespace RgiSequenceFinder.Test;

internal class DataCache
{
    private static JsonDocument? _doc;
    private static EmojiDataRow[]? _data;
    private static string[]? _rawData;

    // lock 掛けずにキャッシュしてるせいか、最初1回だけ IOException 出てそう？
    // 1回限りなので気にせずやってる…

    private static async ValueTask<JsonDocument> GetDoc() => _doc ??= await Loader.LoadJsonDocAsync().ConfigureAwait(false);

    public static async ValueTask<EmojiDataRow[]> GetData() => _data ??= EmojiDataRow.Load(await GetDoc().ConfigureAwait(false)).ToArray();

    public static async ValueTask<string[]> GetRawData() => _rawData ??= EmojiDataRow.LoadAllStrings(await GetDoc().ConfigureAwait(false)).ToArray();
}
