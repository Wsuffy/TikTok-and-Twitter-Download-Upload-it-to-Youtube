namespace User.Core.Contracts;

public interface IUserManager
{
    void Upload(UserSecretEntity userSecretEntity);
    Task UploadAsync(UserSecretEntity userSecretEntity);
}