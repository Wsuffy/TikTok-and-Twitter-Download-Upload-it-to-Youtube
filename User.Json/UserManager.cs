using Newtonsoft.Json;
using User.Core;
using User.Core.Contracts;

namespace User.Json;

public class UserManager : IUserManager
{
    public void Upload(UserSecretEntity userSecretEntity)
    {
        var jsonString = JsonConvert.SerializeObject(userSecretEntity);
        File.WriteAllText($"{Directory.GetCurrentDirectory()}/clientsecret.json", jsonString);
    }

    public async Task UploadAsync(UserSecretEntity userSecretEntity)
    {
        var jsonString = JsonConvert.SerializeObject(userSecretEntity);
        await File.WriteAllTextAsync($"{Directory.GetCurrentDirectory()}/clientsecret.json", jsonString);
    }
}