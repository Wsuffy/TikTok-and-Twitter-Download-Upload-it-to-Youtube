using System.Reflection;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Uploader.Core;

namespace Uploader.YouTube
{
    public class VideoUploader : IUploader
    {
        [Obsolete("Obsolete")]
        public async Task Upload(Video videoInfo, string videoPath)
        {
            UserCredential credential;
            await using (var stream =
                         new FileStream($"{Directory.GetCurrentDirectory()}/clientsecret.json",
                             FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.YoutubeUpload },
                    "user",
                    CancellationToken.None
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            await using (var fileStream = new FileStream(videoPath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(videoInfo,
                    "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;
                await videosInsertRequest.UploadAsync();
                void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
                {
                    switch (progress.Status)
                    {
                        case UploadStatus.Uploading:
                            Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                            break;

                        case UploadStatus.Failed:
                            Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                            break;
                    }
                }

                void videosInsertRequest_ResponseReceived(Video video)
                {
                    Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id);
                }
            }
        }
    }
}