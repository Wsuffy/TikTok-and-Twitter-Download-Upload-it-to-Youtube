using System;
using System.Threading.Tasks;

namespace Application.Pages.VideoEditor.Contracts;

public interface IVideoEditorSignalBus
{
    Func<string, string, bool, bool, string, Task> SubmitEvent { get; set; }
    Func<string, Task> DownloadTwitterEvent { get; set; }
    Func<string, Task> DownloadTikTokEvent { get; set; }
}