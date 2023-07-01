using ConnectionPool.Core.Contracts;
using Microsoft.Playwright;

namespace ConnectionPool.Playwright;

public class PlaywrightConnection : IConnection
{
    public IPage Page { get; set; }
    private readonly IPlaywright _playwright;

    public PlaywrightConnection(IPage page, IPlaywright playwright)
    {
        Page = page;
        _playwright = playwright;
    }

    public void Dispose()
    {
        ConnectionPool.ReturnConnection(Page, _playwright);
    }
}