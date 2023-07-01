namespace Uploader.Core.Contracts;

public interface IUploader
{
    Task Upload(Video.Core.Entites.Video videoInfo, string videoPath);
}