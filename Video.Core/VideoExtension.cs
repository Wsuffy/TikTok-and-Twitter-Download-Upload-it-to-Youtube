using Newtonsoft.Json;

namespace Video.Core;

public static class VideoExtension
{
    public static string CalculateVideoId()
    {
        var d = new DirectoryInfo(GetSavePath());

        var files = d.GetFiles("*.mp4");
        if (files.Length == 0)
            return "1";

        var index = files.Select(x => x.Name.Replace(".mp4", "")).Select(int.Parse).Max();
        return (index + 1).ToString();
    }

    public static string? FindById(int id)
    {
        var d = new DirectoryInfo(GetSavePath());

        var files = d.GetFiles("*.mp4");
        if (files.Length == 0)
            return string.Empty;

        var path = files.FirstOrDefault(x => id.ToString() == x.Name.Replace(".mp4", ""));
        return path != null ? path.ToString() : throw new Exception($"{id} Doesnt Exist");
    }

    private static string GetSavePath()
    {
        var json = File
            .ReadAllText($"{Directory.GetCurrentDirectory()}/settings.json");
        return JsonConvert.DeserializeObject<string>(json)!;
    }
}