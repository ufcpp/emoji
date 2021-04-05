using RgiSequenceFinder.TableGenerator;
using System.Threading.Tasks;
using Xunit;

namespace RgiSequenceFinder.Test
{
    public class EmojiStringTest : IAsyncLifetime
    {
        private EmojiDataRow[] _data = null!;

        public Task DisposeAsync() => Task.CompletedTask;

        public async Task InitializeAsync()
        {
            _data = await DataCache.GetData().ConfigureAwait(false);
        }

        [Fact]
        public void HashCode()
        {
            foreach (var row in _data)
            {
                var x = EmojiString.GetHashCode(row.Utf16);
                var y = row.EmojiString.GetHashCode();
                Assert.Equal(x, y);
            }
        }
    }
}
