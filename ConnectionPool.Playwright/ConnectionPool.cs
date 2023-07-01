using System.Collections.Concurrent;
using Microsoft.Playwright;

namespace ConnectionPool.Playwright;

internal static class ConnectionPool
{
    private static readonly ConcurrentStack<Tuple<IPage, IPlaywright>> Pool = new ConcurrentStack<Tuple<IPage, IPlaywright>>();

    public static void AddConnection(IPlaywright playwright, IPage page)
    {
        var tuple = new Tuple<IPage, IPlaywright>(page, playwright);
        Pool.Push(tuple);
    }

    public static Tuple<IPage, IPlaywright> TryGetConnection()
    {
        if (Pool.TryPop(out var connection))
            return connection;
        
        return null;
    }

    public static void ReturnConnection(IPage page, IPlaywright playwright)
    {
        var tuple = new Tuple<IPage, IPlaywright>(page, playwright);
        Pool.Push(tuple);
    }

    public static void Dispose()
    {
        foreach (var connect in Pool)
        {
            connect.Item2.Dispose();
        }
    }
}