using Microsoft.Playwright;

namespace ConnectionPool.Core.Contracts;

public interface IConnectionPoolManager
{
    Task CreateConnectionAsync(string login, string password);
}