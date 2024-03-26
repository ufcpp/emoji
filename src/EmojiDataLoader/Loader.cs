using System.Text;
using System.Text.Json;

namespace EmojiData;

public class Loader
{
    public static IReadOnlyList<(string branchName, string version)> Branches => _branches;

    private static readonly (string branchName, string version)[] _branches =
    [
        ("a8174c74675355c8c6a9564516b2e961fe7257ef", "15.1"), // 2024年2月。
        ("0669bda2b9348984eeb4ce66215819bf61c35811", "15.0"), // 2023年5月。
        ("1ddc9ca67c1379c372b4ca39824659f71caa2825", "14.0"), // 2022年3月。
        ("675515762babff57a8d9c05f420806f1354203c3", "13.1"), // 2022年1月。
        ("56e5c532573edc8bdf8b16cb2e750e3cb383ad9d", "13.0"), // 2021年3月。
        ("cff32eea1d876e4c4a73c87ebc2fa218775de58f", "12.1"), // 2020年1月。
    ];

    const string RepositoryName = "iamcal/emoji-data";

    private static string GetEmojiDataSourceUrl(string branchName) => $"https://github.com/{RepositoryName}/raw/{branchName}/emoji.json";
    private static string GetCacheFileName(string branchName) => $"{branchName}.json";

    public static async ValueTask<byte[]> LoadBytesAsync(string? branchName = null)
    {
        branchName ??= _branches[0].branchName;
        var cacheFileName = GetCacheFileName(branchName);

        if (File.Exists(cacheFileName))
        {
            System.Diagnostics.Debug.WriteLine("read from cache");

            return await File.ReadAllBytesAsync(cacheFileName);
        }

        System.Diagnostics.Debug.WriteLine("read from github");

        var c = new HttpClient();
        var res = await c.GetAsync(GetEmojiDataSourceUrl(branchName));
        var json = await res.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync(cacheFileName, json);
        return json;
    }

    public static async ValueTask<string> LoadStringAsync(string? branchName = null)
    {
        var bytes = await LoadBytesAsync(branchName);
        return Encoding.UTF8.GetString(bytes);
    }

    public static async ValueTask<JsonDocument> LoadJsonDocAsync(string? branchName = null)
    {
        var bytes = await LoadBytesAsync(branchName);
        return JsonDocument.Parse(bytes);
    }
}
