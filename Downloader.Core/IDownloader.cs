namespace Downloader.Core;

public interface IDownloader
{
    Task<string> Download(string url);
}