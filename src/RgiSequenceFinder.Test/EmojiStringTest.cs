using RgiSequenceFinder.TableGenerator;
using Xunit;

namespace RgiSequenceFinder.Test
{
    public class EmojiStringTest : IAsyncLifetime
    {
        private EmojiDataRow[] _data = null!;
        public Task DisposeAsync() => Task.CompletedTask;
        public async Task InitializeAsync() => _data = await DataCache.GetData().ConfigureAwait(false);

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

        [Fact]
        public void Equal()
        {
            foreach (var row in _data)
            {
                Assert.True(row.EmojiString.Equals(row.Utf16));
                Assert.True(EmojiString.Equals(row.EmojiString.Raw, row.Utf16));

                if (row.EmojiString.Raw.Length == 1)
                {
                    Assert.True(EmojiString.Equals(row.EmojiString.Raw[0], row.Utf16));
                }
            }
        }
    }
}
