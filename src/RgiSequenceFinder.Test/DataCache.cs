using EmojiData;
using RgiSequenceFinder.TableGenerator;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RgiSequenceFinder.Test
{
    internal class DataCache
    {
        private static JsonDocument? _doc;
        private static EmojiDataRow[]? _data;
        private static string[]? _rawData;

        private static async ValueTask<JsonDocument> GetDoc() => _doc ??= await Loader.LoadJsonDocAsync().ConfigureAwait(false);

        public static async ValueTask<EmojiDataRow[]> GetData() => _data ??= EmojiDataRow.Load(await GetDoc().ConfigureAwait(false)).ToArray();

        public static async ValueTask<string[]> GetRawData() => _rawData ??= EmojiDataRow.LoadAllStrings(await GetDoc().ConfigureAwait(false)).ToArray();
    }
}
