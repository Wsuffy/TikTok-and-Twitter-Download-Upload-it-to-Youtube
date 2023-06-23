using Google.Apis.YouTube.v3.Data;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Uploader.Core;

namespace Uploader.Playwright.Youtube;

public class PlaywrightUploader : IUploader
{
    
    public async Task Upload(Video videoInfo, string videoPath)
    {
        const string youtubeMain = "https://www.youtube.com/";
        string path = $"{Directory.GetCurrentDirectory()}/clientsecret.json";

        var json = await File.ReadAllTextAsync(path);
        var secret = JsonConvert.DeserializeObject<UserSecretEntity>(json);
        var youtubeStudioUrl = $"https://studio.youtube.com/channel/{secret!.ShortUrl}/videos";
        try
        {
            using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            await using var browser = await playwright.Firefox.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync(youtubeMain);

            await page.GetByText("Sign in").First.ClickAsync();
            await page.Locator("//input[@type='email']").FillAsync(secret!.Login);
            await page.Locator(
                    "//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc LQeN7 qIypjc TrZEUc lw1w4b']")
                .ClickAsync();
            await page.Locator("//input[@type='password']").First.FillAsync(secret!.Password);
            await page.Locator(
                    "//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc LQeN7 qIypjc TrZEUc lw1w4b']")
                .ClickAsync();
            await page.WaitForURLAsync(youtubeMain);
            await page.GotoAsync(youtubeStudioUrl);
            await page.WaitForTimeoutAsync(1000);
            await page.Locator("//ytcp-button[@label='Create']").ClickAsync();
            await page.GetByText("Upload videos").First.ClickAsync();

            var fileChooserPromise = page.WaitForFileChooserAsync();
            await page.GetByText("Select files").ClickAsync();
            var fileChooser = await fileChooserPromise;
            await fileChooser.SetFilesAsync(videoPath,new FileChooserSetFilesOptions(){NoWaitAfter = false,Timeout = 4000});

            var locator = page.Locator(
                "//div[@aria-label='Add a title that describes your video (type @ to mention a channel)']");
            await locator.WaitForAsync(new LocatorWaitForOptions(){Timeout = 6000});
            await page.Locator(
                "//div[@aria-label='Add a title that describes your video (type @ to mention a channel)']").FillAsync(videoInfo.Snippet.Title);
            await page.Locator("//div[@aria-label='Tell viewers about your video (type @ to mention a channel)']")
                .FillAsync(videoInfo.Snippet.Description);

            await page.Locator("//tp-yt-paper-radio-button[@name='VIDEO_MADE_FOR_KIDS_MFK']")
                .ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions(){Timeout = 1000});
            await page.Locator("//tp-yt-paper-radio-button[@name='VIDEO_MADE_FOR_KIDS_MFK']").ClickAsync(new LocatorClickOptions(){Timeout = 1000});

            await page.Locator("//button[@id='step-badge-1']").ClickAsync();
            await page.Locator("//button[@id='step-badge-2']").ClickAsync();
            await page.Locator("//button[@id='step-badge-3']").ClickAsync();
            await page.Locator("//tp-yt-paper-radio-button[@name='PUBLIC']").ClickAsync();
            await page.Locator("//ytcp-button[@id='done-button']").ClickAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}