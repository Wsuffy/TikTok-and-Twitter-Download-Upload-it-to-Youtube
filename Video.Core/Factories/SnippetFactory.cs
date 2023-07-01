using Video.Core.Entites;

namespace Video.Core
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