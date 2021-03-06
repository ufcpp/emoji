using System.Text;
using System.Text.Json;

namespace EmojiData;

public class Loader
{
    const string RepositoryName = "iamcal/emoji-data";
    const string BranchName = "1ddc9ca67c1379c372b4ca39824659f71caa2825"; // 2022年3月。 Unicode 14.0。
    //const string BranchName = "675515762babff57a8d9c05f420806f1354203c3"; // 2022年1月。 Unicode 13.1。
    //const string BranchName = "56e5c532573edc8bdf8b16cb2e750e3cb383ad9d"; // 2021年3月。 Unicode 13.0。
    //const string BranchName = "cff32eea1d876e4c4a73c87ebc2fa218775de58f"; // 2020年1月。 Unicode 12.1。

    const string EmojiDataSourceUrl = "https://github.com/" + RepositoryName + "/raw/" + BranchName + "/emoji.json";
    const string CacheFileName = BranchName + ".json";

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
