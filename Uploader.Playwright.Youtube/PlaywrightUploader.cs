using ConnectionPool.Core.Contracts;
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
    private readonly IUserProvider _userProvider;
    private readonly IConnectionFactory _connectionFactory;

    public PlaywrightUploader(IUserProvider userProvider, IConnectionFactory connectionFactory)
    {
        _userProvider = userProvider;
        _connectionFactory = connectionFactory;
    }

    public async Task UploadAsync(Video.Core.Entites.Video videoInfo, string videoPath)
    {
        var secret = await _userProvider.ReadInfoAsync();
        var youtubeStudioUrl = $"https://studio.youtube.com/channel/{secret!.ShortUrl}/videos/upload";
        try
        {
            using var connection = _connectionFactory.Create();
            await connection.Page.GotoAsync(youtubeStudioUrl,
                new PageGotoOptions() { WaitUntil = WaitUntilState.DOMContentLoaded, Timeout = 0 });
            await connection.Page.Locator("//ytcp-button[@label='Create']").ClickAsync();
            await connection.Page.GetByText("Upload videos").First.ClickAsync();

            var fileChooserPromise = connection.Page.WaitForFileChooserAsync();
            await connection.Page.GetByText("Select files").ScrollIntoViewIfNeededAsync();
            await connection.Page.GetByText("Select files").ClickAsync();
            var fileChooser = await fileChooserPromise;
            await fileChooser.SetFilesAsync(videoPath,
                new FileChooserSetFilesOptions() { NoWaitAfter = false, Timeout = 6000 });

            await UploadVideoInformationAsync(connection.Page, videoInfo);

            await connection.Page.Locator("//button[@id='step-badge-1']").ClickAsync();
            await connection.Page.Locator("//button[@id='step-badge-2']").ClickAsync();
            await connection.Page.Locator("//button[@id='step-badge-3']").ClickAsync();
            await connection.Page.Locator("//tp-yt-paper-radio-button[@name='PUBLIC']").ScrollIntoViewIfNeededAsync();
            await connection.Page.Locator("//tp-yt-paper-radio-button[@name='PUBLIC']").ClickAsync();
            await connection.Page.Locator("//ytcp-button[@id='done-button']").ClickAsync();
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
            .ScrollIntoViewIfNeededAsync(new LocatorScrollIntoViewIfNeededOptions { Timeout = 0 });
        await page.Locator("//tp-yt-paper-radio-button[@name='VIDEO_MADE_FOR_KIDS_MFK']")
            .ClickAsync(new LocatorClickOptions() { Timeout = 0 });


        return page;
    }
}