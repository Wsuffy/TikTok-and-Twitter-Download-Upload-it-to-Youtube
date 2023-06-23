using Google.Apis.YouTube.v3.Data;

namespace Uploader.Core;

public interface IUploader
{
    Task Upload(Video videoInfo, string videoPath);
}