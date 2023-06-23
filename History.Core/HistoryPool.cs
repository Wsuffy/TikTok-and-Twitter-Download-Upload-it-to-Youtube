using Newtonsoft.Json;

namespace History.Core;

public class HistoryPool
{
    private static readonly string DownloadedVideoHistoryPath = $"{Directory.GetCurrentDirectory()}/downloadedvideos.json";

    public void AddVideoHistory(string internalId, string url)
    {
        var json = File.ReadAllText(DownloadedVideoHistoryPath);
        List<DownloadedVideoEntity>? list = null;
        if (json.Length > 4)
            list = JsonConvert.DeserializeObject<List<DownloadedVideoEntity>>(json);
        else
            list = new List<DownloadedVideoEntity>();
        
        list!.Add(new DownloadedVideoEntity()
        {
            InternalId = internalId,
            DownloadedTime = DateTime.Now,
            Url = url
        });
        var updatedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
        File.WriteAllText(DownloadedVideoHistoryPath, updatedJson);
    }
}