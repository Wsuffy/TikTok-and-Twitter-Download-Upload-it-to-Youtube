namespace Video.Core
{
    public static class VideoFactory
    {
        public static Entites.Video CreateVideo(string tittle, string description,  bool isPrivate,
            bool isYoutubeShorts)
        {
            switch (isYoutubeShorts)
            {
                case true:
                    return new Entites.Video
                    {
                        Snippet = SnippetFactory.CreateVideoSnippet($"#shorts{tittle}", $"#shorts{description}"),
                    };
                case false:
                    return new Entites.Video
                    {
                        Snippet = SnippetFactory.CreateVideoSnippet(tittle, description),
                    };
            }
        }
    }
}