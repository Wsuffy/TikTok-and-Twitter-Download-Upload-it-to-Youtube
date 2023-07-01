using System;
using System.IO;
using System.Threading.Tasks;
using Application.Pages.VideoEditor.Contracts;
using Application.Windows.Main.Contracts;
using Downloader.Core;
using Newtonsoft.Json;
using User.Core;
using User.Core.Contracts;
using Video.Core;

namespace Application.Windows.Main;

public class MainWindowPresenter : IDisposable
{
    private readonly IMainWindowSignalBus _model;
    private readonly IVideoEditorModel _videoEditorModel;
    private readonly IVideoEditorSignalBus _videoEditorSignalBus;
    private readonly IUserManager _userManager;
    private readonly DownloaderManager _downloaderManager;

    public MainWindowPresenter(DownloaderManager downloaderManager, IMainWindowSignalBus model,
        IVideoEditorModel videoEditorModel, IVideoEditorSignalBus videoEditorSignalBus, IUserManager userManager)
    {
        _downloaderManager = downloaderManager;
        _model = model;
        _videoEditorModel = videoEditorModel;
        _videoEditorSignalBus = videoEditorSignalBus;
        _userManager = userManager;

        #region Subscribes

        _model.UserSecretChanged += SetUserSecret;
        _model.SavePathChanged += ChangePath;

        _videoEditorSignalBus.DownloadTikTokEvent += DownloadTikTokAsync;
        _videoEditorSignalBus.DownloadTwitterEvent += DownloadTwitterAsync;

        #endregion
    }

    private void ChangePath(string currentPath)
    {
        var json = JsonConvert.SerializeObject(currentPath);
        File.WriteAllText($"{Directory.GetCurrentDirectory()}/settings.json", json);
    }

    private async Task SetUserSecret(UserSecretEntity secretEntity)
    {
        await _userManager.UploadAsync(secretEntity);
    }

    private async Task<string> DownloadTikTokAsync(string url)
    {
        var id = await _downloaderManager.Download(ResourceType.TikTok, url);
        _videoEditorModel.SetVideoPath(VideoExtension.FindById(int.Parse(id))!);
        return id;
    }

    private async Task<string> DownloadTwitterAsync(string url)
    {
        var id = await _downloaderManager.Download(ResourceType.Twitter, url);
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