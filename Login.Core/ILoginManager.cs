using Microsoft.Playwright;

namespace Login.Core;

public interface ILoginManager
{
    Task<IPage> LoginAsync(IPage currentPage, string login, string password);
}