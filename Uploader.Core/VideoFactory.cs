using AppDownloader;
using Google.Apis.YouTube.v3.Data;

namespace Uploader.Core
{
    public static class VideoFactory
    {
        public static Video CreateVideo(string tittle, string description,  bool isPrivate,
            bool isYoutubeShorts)
        {
            switch (isYoutubeShorts)
            {
                case true:
                    return new Video
                    {
                        Snippet = SnippetFactory.CreateVideoSnippet($"#shorts{tittle}", $"#shorts{description}"),
                        Status = VideoStatusCreating(isPrivate)
                    };
                case false:
                    return new Video
                    {
                        Snippet = SnippetFactory.CreateVideoSnippet(tittle, description),
                        Status = VideoStatusCreating(isPrivate)     
                    };
            }
        }

        private static VideoStatus VideoStatusCreating(bool isPrivate)
        {
            return new VideoStatus()
            {
                PrivacyStatus = PrivacyMapper.Map(isPrivate),
            };
        }
    }
}