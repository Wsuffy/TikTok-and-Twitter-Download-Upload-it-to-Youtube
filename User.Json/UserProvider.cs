using Newtonsoft.Json;
using User.Core;
using User.Core.Contracts;

namespace User.Json;

public class UserProvider : IUserProvider
{
    public UserSecretEntity ReadInfo()
    {
        var path = $"{Directory.GetCurrentDirectory()}/clientsecret.json";
        var str = File.ReadAllText(path);
        var entity = JsonConvert.DeserializeObject<UserSecretEntity>(str);
        return entity!;
    }

    public async Task<UserSecretEntity> ReadInfoAsync()
    {
        var path = $"{Directory.GetCurrentDirectory()}/clientsecret.json";
        var str = await File.ReadAllTextAsync(path);
        var entity = JsonConvert.DeserializeObject<UserSecretEntity>(str);
        return entity!;
    }
}