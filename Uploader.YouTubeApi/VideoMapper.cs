using Google.Apis.YouTube.v3.Data;

namespace Uploader.YouTubeApi;

public static class VideoMapper
{
    public static Google.Apis.YouTube.v3.Data.Video Map(Video.Core.Entites.Video video)
    {
        return new Google.Apis.YouTube.v3.Data.Video()
        {
            Snippet = new VideoSnippet()
            {
                Title = video.Snippet.Title,
                Description = video.Snippet.Description,
                CategoryId = video.Snippet.CategoryId,
            }
        };
    }
}