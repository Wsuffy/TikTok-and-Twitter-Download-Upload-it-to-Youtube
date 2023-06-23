using History.Core;

namespace Downloader.Core;

public class DownloaderManager
{
    private readonly IDownloader _tikTokDownloader;
    private readonly IDownloader _twitterDownloader;
    private readonly HistoryPool _historyPool;

    public DownloaderManager(IDownloader tikTokDownloader, IDownloader twitterDownloader, HistoryPool historyPool)
    {
        _tikTokDownloader = tikTokDownloader;
        _twitterDownloader = twitterDownloader;
        _historyPool = historyPool;
    }

    public async Task<string> Download(ResourceType type, string url)
    {
        var internalId = string.Empty;
        
        switch (type)
        {
            case ResourceType.TikTok:
                internalId = await _tikTokDownloader.Download(url);
                break;
            case ResourceType.Twitter:
                internalId = await _twitterDownloader.Download(url);
                break;
        }
        
        _historyPool.AddVideoHistory(internalId, url);
        return internalId;
    }
}