using ConnectionPool.Core.Contracts;
using Login.Core;
using Microsoft.Playwright;

namespace ConnectionPool.Playwright;

public class ConnectionPoolManager : IConnectionPoolManager
{
    private const string YoutubeUrlMain = "https://www.youtube.com/";
    private readonly ILoginManager _loginManager;

    public ConnectionPoolManager(ILoginManager loginManager)
    {
        _loginManager = loginManager;
    }

    public async Task CreateConnectionAsync(string login, string password)
    {
        var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        var browser = await playwright.Firefox.LaunchAsync();
        var page = await browser.NewPageAsync();
        await page.GotoAsync(YoutubeUrlMain,
            new PageGotoOptions() { WaitUntil = WaitUntilState.DOMContentLoaded, Timeout = 0 });
        await _loginManager.LoginAsync(page, login, password);
        ConnectionPool.AddConnection(playwright, page);
    }
}