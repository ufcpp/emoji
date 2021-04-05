﻿using EmojiData;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RgiSequenceFinder.TableGenerator.Data
{
    /// <summary>
    /// Console App だし async 汚染を避けて <see cref="ValueTask{TResult}.Result"/> を呼んじゃってるキャッシュデータ。
    /// </summary>
    internal class Cache
    {
        private static JsonDocument? _doc;
        private static JsonDocument Doc => _doc ??= Loader.LoadJsonDocAsync().GetAwaiter().GetResult();

        private static EmojiDataRow[]? _data;
        public static EmojiDataRow[] Data => _data ??= EmojiDataRow.Load(Doc).ToArray();
    }
}
