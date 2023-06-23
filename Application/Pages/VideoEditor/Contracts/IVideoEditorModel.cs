namespace Application.Pages.VideoEditor.Contracts;

public interface IVideoEditorModel
{
    void ChangePrivateVideoStatus();
    void SetVideoPath(string path);
    void Submit(string tittle, string description);
}