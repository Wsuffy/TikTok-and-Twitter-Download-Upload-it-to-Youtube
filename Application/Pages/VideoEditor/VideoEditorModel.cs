using System;
using System.Threading.Tasks;
using Application.Pages.VideoEditor.Contracts;

namespace Application.Pages.VideoEditor;

public class VideoEditorModel : IVideoEditorModel, IVideoEditorSignalBus
{
    public Action<bool> YouTubeShortChanged { get; set; }
    public Action<bool> PrivateVideoChanged { get; set; }
    public Func<string, string, bool, bool, string, Task> SubmitEvent { get; set; }
    public Func<string, Task> DownloadTwitterEvent { get; set; }
    public Func<string, Task> DownloadTikTokEvent { get; set; }


    public bool IsYoutubeShort
    {
        get => _isYoutubeShort;
        set
        {
            _isYoutubeShort = value;
            YouTubeShortChanged?.Invoke(value);
        }
    }

    private bool _isYoutubeShort = false;

    public bool IsPrivateVideo
    {
        get => _isPrivateVideo;
        set
        {
            _isPrivateVideo = value;
            PrivateVideoChanged?.Invoke(value);
        }
    }

    private bool _isPrivateVideo = false;

    private string _videoPath;

    public void ChangePrivateVideoStatus()
    {
        IsPrivateVideo = !IsPrivateVideo;
    }

    public void ChangeYouTubeShortVideoStatus()
    {
        IsYoutubeShort = !IsYoutubeShort;
    }

    public void SetVideoPath(string path)
    {
        _videoPath = path;
    }
    
    public void Submit(string tittle, string description)
    {
        SubmitEvent?.Invoke(tittle, description, _isYoutubeShort, _isPrivateVideo, _videoPath);
    }
}