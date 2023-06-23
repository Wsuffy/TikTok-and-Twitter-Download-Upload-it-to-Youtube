namespace History.Core;

public class DownloadedVideoEntity
{
    public string InternalId { get; set; }
    public DateTime DownloadedTime { get; set; }
    public string Url { get; set; }
}