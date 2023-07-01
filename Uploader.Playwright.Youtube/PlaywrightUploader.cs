using Login.Core;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Uploader.Core;
using Uploader.Core.Contracts;
using User.Core;
using User.Core.Contracts;

namespace Uploader.Playwright.Youtube;

public class PlaywrightUploader : IUploader
{
    private readonly ILoginManager _loginManager;
    private readonly IUserProvider _userProvider;

    public PlaywrightUploader(ILoginManager loginManager, IUserProvider userProvider)
    {
        _loginManager = loginManager;
        _userProvider = userProvider;
    }

    public async Task Upload(Video.Core.Entites.Video videoInfo, string videoPath)
    {
        const string youtubeMain = "https://www.youtube.com/";
        var secret = await _userProvider.ReadInfoAsync();
        var youtubeStudioUrl = $"https://studio.youtube.com/channel/{secret!.ShortUrl}/videos/upload";
        try
        {
            // TODO Make Browser Connection Pool With Logined Account
            using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            await using var browser = await playwright.Firefox.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync(youtubeMain,
                new PageGotoOptions() { WaitUntil = WaitUntilState.DOMContentLoaded, Timeout = 0 });

            page = await _loginManager.LoginAsync(page, secret!.Login, secret!.Password);

            await page.GotoAsync(youtubeStudioUrl,
                new PageGotoOptions() { WaitUntil = WaitUntilState.DOMContentLoaded, Timeout = 0 });
            await page.Locator("//ytcp-button[@label='Create']").ClickAsync();
            await page.GetByText("Upload videos").First.ClickAsync();

            var fileChooserPromise = page.WaitForFileChooserAsync();
            await page.GetByText("Select files").ClickAsync();
            var fileChooser = await fileChooserPromise;
            await fileChooser.SetFilesAsync(videoPath,
                new FileChooserSetFilesOptions() { NoWaitAfter = false });
            await Task.Delay(3000);

            await UploadVideoInformationAsync(page, videoInfo);

            await page.Locator("//button[@id='step-badge-1']").ClickAsync();
            await page.Locator("//button[@id='step-badge-2']").ClickAsync();
            await page.Locator("//button[@id='step-badge-3']").ClickAsync();
            await page.Locator("//tp-yt-paper-radio-button[@name='PUBLIC']").ScrollIntoViewIfNeededAsync();
            await page.Locator("//tp-yt-paper-radio-button[@name='PUBLIC']").ClickAsync();
            await page.Locator("//ytcp-button[@id='done-button']").ClickAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    private async Task<IPage> UploadVideoInformationAsync(IPage page, Video.Core.Entites.Video videoInfo)
    {
        await page.Locator(
                "//div[@aria-label='Add a title that describes your video (type @ to mention a channel)']")
            .ScrollIntoViewIfNeededAsync();
        await page.Locator(
                "//div[@aria-label='Add a title that describes your video (type @ to mention a channel)']")
            .FillAsync(videoInfo.Snippet.Title);
        await page.Locator(
                "//div[@aria-label='Tell viewers about your video (type @ to mention a channel)']")
            .ScrollIntoViewIfNeededAsync();
        await page.Locator("//div[@aria-label='Tell viewers about your video (type @ to mention a channel)']")
            .FillAsync(videoInfo.Snippet.Description);

        await page.Locator("//tp-yt-paper-radio-button[@name='VIDEO_MADE_FOR_KIDS_MFK']")
            .ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions() { Timeout = 6000 });
        await page.Locator("//tp-yt-paper-radio-button[@name='VIDEO_MADE_FOR_KIDS_MFK']")
            .ClickAsync(new LocatorClickOptions() { Timeout = 6000 });


        return page;
    }
}