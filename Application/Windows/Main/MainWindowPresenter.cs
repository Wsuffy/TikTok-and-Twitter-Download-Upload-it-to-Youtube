using System;
using System.IO;
using System.Threading.Tasks;
using Application.Pages.VideoEditor.Contracts;
using Application.Windows.Main.Contracts;
using Downloader.Core;
using Newtonsoft.Json;
using Uploader.Core;

namespace Application.Windows.Main;

public class MainWindowPresenter : IDisposable
{
    private readonly IMainWindowSignalBus _model;
    private readonly IVideoEditorModel _videoEditorModel;
    private readonly IVideoEditorSignalBus _videoEditorSignalBus;
    private readonly DownloaderManager _downloaderManager;

    public MainWindowPresenter(DownloaderManager downloaderManager, IMainWindowSignalBus model, IVideoEditorModel videoEditorModel, IVideoEditorSignalBus videoEditorSignalBus)
    {
        _downloaderManager = downloaderManager;
        _model = model;
        _videoEditorModel = videoEditorModel;
        _videoEditorSignalBus = videoEditorSignalBus;

        #region Subscribes

        _model.UserSecretChanged += SetUserSecret;
        _model.SavePathChanged += ChangePath;

        _videoEditorSignalBus.DownloadTikTokEvent += DownloadTikTokAsync;
        _videoEditorSignalBus.DownloadTwitterEvent += DownloadTwitterAsync;

        #endregion
    }

    private static void ChangePath(string currentPath)
    {
        var json = JsonConvert.SerializeObject(currentPath);
        File.WriteAllText($"{Directory.GetCurrentDirectory()}/settings.json", json);
    }

    private static void SetUserSecret(UserSecretEntity secretEntity)
    {
        var jsonString = JsonConvert.SerializeObject(secretEntity);
        File.WriteAllText($"{Directory.GetCurrentDirectory()}/clientsecret.json", jsonString);
    }

    private async Task<string> DownloadTikTokAsync(string url)
    {
        var id =  await _downloaderManager.Download(ResourceType.TikTok, url);
        _videoEditorModel.SetVideoPath(VideoExtension.FindById(int.Parse(id))!);
        return id;
    }

    private async Task<string> DownloadTwitterAsync(string url)
    {
        var id =  await _downloaderManager.Download(ResourceType.Twitter, url);
        _videoEditorModel.SetVideoPath(VideoExtension.FindById(int.Parse(id))!);
        return id;
    }

    public void Dispose()
    {
        _model.UserSecretChanged -= SetUserSecret;
        _model.SavePathChanged -= ChangePath;
        
        _videoEditorSignalBus.DownloadTikTokEvent -= DownloadTikTokAsync;
        _videoEditorSignalBus.DownloadTwitterEvent -= DownloadTwitterAsync;
    }
}