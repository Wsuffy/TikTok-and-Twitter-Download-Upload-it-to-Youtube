namespace User.Core.Contracts;

public interface IUserProvider
{
    UserSecretEntity ReadInfo();
    Task<UserSecretEntity> ReadInfoAsync();
}