namespace Uploader.Core.Contracts;

public interface IUploader
{
    Task UploadAsync(Video.Core.Entites.Video videoInfo, string videoPath);
}