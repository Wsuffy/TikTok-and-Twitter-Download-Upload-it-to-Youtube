using Microsoft.Playwright;

namespace ConnectionPool.Core.Contracts;

public interface IConnection : IDisposable
{
    IPage Page { get; set;  }
}