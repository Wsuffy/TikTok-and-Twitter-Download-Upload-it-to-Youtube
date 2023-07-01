namespace ConnectionPool.Core.Contracts;

public interface IConnectionFactory
{
    IConnection Create();
}