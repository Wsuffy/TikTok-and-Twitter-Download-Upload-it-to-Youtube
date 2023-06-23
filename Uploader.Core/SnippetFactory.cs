using Google.Apis.YouTube.v3.Data;

namespace Uploader.Core
{
    public static class SnippetFactory
    {
        public static VideoSnippet CreateVideoSnippet(string title, string description)
        {
            return new VideoSnippet
            {
                Title = title,
                Description = description,
                CategoryId = "22",
            };

        }
    }
}