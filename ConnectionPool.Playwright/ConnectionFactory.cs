using ConnectionPool.Core.Contracts;

namespace ConnectionPool.Playwright;

public class ConnectionFactory : IConnectionFactory
{
    public IConnection Create()
    {
        var connection = ConnectionPool.TryGetConnection();
        return new PlaywrightConnection(connection.Item1, connection.Item2);
    }
}