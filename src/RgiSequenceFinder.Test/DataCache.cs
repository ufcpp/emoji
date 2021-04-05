using EmojiData;
using RgiSequenceFinder.TableGenerator;
using System.Linq;
using System.Threading.Tasks;

namespace RgiSequenceFinder.Test
{
    internal class DataCache
    {
        private static EmojiDataRow[]? _data;

        public static async ValueTask<EmojiDataRow[]> GetData() => _data ??= EmojiDataRow.Load(await Loader.LoadJsonDocAsync().ConfigureAwait(false)).ToArray();
    }
}
