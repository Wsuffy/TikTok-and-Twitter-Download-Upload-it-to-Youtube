using AppDownloader;
using Downloader.Core;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Uploader.Core;

namespace Downloader.Twitter;

public class TwitterDownloader : IDownloader
{
    public async Task<string> Download(string url)
    {
        var downloadService = "https://www.downloadtwittervideo.com/";
        var internalId = VideoExtension.CalculateVideoId();
        var json = await File
            .ReadAllTextAsync($"{Directory.GetCurrentDirectory()}/settings.json");
        var defaultPath =
            JsonConvert.DeserializeObject<string>(json);
        var path = $"{defaultPath}\\{internalId}.mp4";

        try
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            var downloadPromise = page.WaitForDownloadAsync();
            await page.GotoAsync(downloadService);
            await page.Locator("//input[@id='url']").FillAsync(url);
            await page.Locator("//div[@id='DownloadMP4HD']").ClickAsync();
            var download = await downloadPromise;
            await download.SaveAsAsync(path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return internalId;
    }
}