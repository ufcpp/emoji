﻿using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmojiData
{
    public class Loader
    {
        //todo: master/main じゃなくて、特定のコミットから取らないとある日突然変わるので困るはず。
        // あと、違う URL (違うコミット)から取ったときはローカルストレージのキャッシュ無効化しないと。
        const string EmojiDataSourceUrl = "https://github.com/iamcal/emoji-data/raw/master/emoji.json";
        const string CacheFileName = "emoji.json";

        public static async ValueTask<byte[]> LoadBytesAsync()
        {
            if (File.Exists(CacheFileName))
            {
                System.Diagnostics.Debug.WriteLine("read from cache");

                return await File.ReadAllBytesAsync(CacheFileName);
            }

            System.Diagnostics.Debug.WriteLine("read from github");

            var c = new HttpClient();
            var res = await c.GetAsync(EmojiDataSourceUrl);
            var json = await res.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(CacheFileName, json);
            return json;
        }

        public static async ValueTask<string> LoadStringAsync()
        {
            var bytes = await LoadBytesAsync();
            return Encoding.UTF8.GetString(bytes);
        }

        public static async ValueTask<JsonDocument> LoadJsonDocAsync()
        {
            var bytes = await LoadBytesAsync();
            return JsonDocument.Parse(bytes);
        }
    }
}
