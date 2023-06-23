using System;
using System.IO;
using Application.App;
using Application.Pages;
using Application.Pages.VideoEditor;
using Application.Pages.VideoEditor.Contracts;
using Application.Windows.Main;
using Application.Windows.Main.Contracts;
using Downloader.Core;
using Downloader.TikTok;
using Downloader.Twitter;
using History.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Uploader.Core;
using Uploader.Playwright.Youtube;
using Uploader.YouTube;


namespace Application;

public class Program : System.Windows.Application
{
    [STAThread]
    public static void Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                #region Windows And Pages

                services.AddSingleton<AppMain>();
                services.AddSingleton<MainWindowView>();
                services.AddSingleton<MainWindowModel>();
                services.AddSingleton<IMainWindowSignalBus>(s => s.GetRequiredService<MainWindowModel>());
                services.AddSingleton<IMainWindowModel>(s => s.GetRequiredService<MainWindowModel>());
                services.AddSingleton<MainWindowPresenter>();
                
                services.AddSingleton<VideoEditorView>();
                services.AddSingleton<VideoEditorModel>();
                services.AddSingleton<IVideoEditorModel>(s => s.GetRequiredService<VideoEditorModel>());
                services.AddSingleton<IVideoEditorSignalBus>(s => s.GetRequiredService<VideoEditorModel>());
                services.AddSingleton<VideoEditorPresenter>();

                #endregion

                services.AddSingleton<TikTokDownloader>();
                services.AddSingleton<TwitterDownloader>();
                services.AddSingleton<HistoryPool>();
                services.AddSingleton<DownloaderManager>(x =>
                    new DownloaderManager(x.GetRequiredService<TikTokDownloader>(),
                        x.GetRequiredService<TwitterDownloader>(),
                        x.GetRequiredService<HistoryPool>()));
                services.AddSingleton<IUploader, PlaywrightUploader>();
                services.AddSingleton<PlaywrightUploader>();
            });

        if (!File.Exists($"{Directory.GetCurrentDirectory()}/downloadedvideos.json"))
            File.Create($"{Directory.GetCurrentDirectory()}/downloadedvideos.json");
        if (!File.Exists($"{Directory.GetCurrentDirectory()}/settings.json"))
            File.Create($"{Directory.GetCurrentDirectory()}/settings.json");
        if (!File.Exists($"{Directory.GetCurrentDirectory()}/clientsecret.json"))
            File.Create($"{Directory.GetCurrentDirectory()}/clientsecret.json");
        var build = host.Build();
        var app = build.Services.GetService<AppMain>();

        #region Non Lazy Injection

        var mainWindowPresenter = build.Services.GetService<MainWindowPresenter>();
        var videoEditorPresenter = build.Services.GetService<VideoEditorPresenter>();

        #endregion
        /*build.Services.GetService<PlaywrightUploader>().Upload(VideoFactory.CreateVideo("Funny Video", "Decription Test", new []{"tag1,tag2,tag3,tag4"}, false,
            false),@"D:\GitProjects\AppDownloader\Application\Videos\1.mp4");*/
        app?.Run();
        
    }
}