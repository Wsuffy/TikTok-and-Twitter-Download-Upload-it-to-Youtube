using System;
using System.Threading.Tasks;
using Application.Pages.VideoEditor.Contracts;
using Uploader.Core;
using Uploader.Core.Contracts;
using Video.Core;

namespace Application.Pages.VideoEditor;

public class VideoEditorPresenter : IDisposable
{
    private readonly IVideoEditorSignalBus _videoEditorSignalBus;
    private readonly IUploader _uploader;
    
    public VideoEditorPresenter(IVideoEditorSignalBus videoEditorSignalBus, IUploader uploader)
    {
        _videoEditorSignalBus = videoEditorSignalBus;
        _uploader = uploader;

        #region Subcribes

        _videoEditorSignalBus.SubmitEvent += Submit;

        #endregion
    }

    private async Task Submit(string tittle, string description, bool isYouTubeShort, bool isPrivateVideo, string path)
    {
        var video = VideoFactory.CreateVideo(tittle, description, isPrivateVideo, isYouTubeShort);
        await _uploader.Upload(video, path);
    }

    public void Dispose()
    {
        _videoEditorSignalBus.SubmitEvent -= Submit;
    }
}