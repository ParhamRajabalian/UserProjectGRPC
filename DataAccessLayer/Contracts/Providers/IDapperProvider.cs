using System.Data;

namespace DataAccessLayer.Contracts.Providers;

public interface IDapperProvider
{
    Task<int> ExecuteAsync(string cmd, object? param, CommandType commandType, CancellationToken cancellationToken = default);
    Task<T> SingleAsync<T>(string cmd, object? param, CommandType commandType, CancellationToken cancellationToken = default);
    Task<T> ScalarAsync<T>(string cmd, object? param, CommandType commandType, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> SelectAsync<T>(string cmd, object? param, CommandType commandType, CancellationToken cancellationToken = default);
    Task<T> FirstAsync<T>(string cmd, object? param, CommandType commandType, CancellationToken cancellationToken = default);
}
