using Dapper;
using DataAccessLayer.Contracts.Providers;
using DataAccessLayer.Entities;
using System.Data;

namespace DataAccessLayer.Oprations.Providers;
public class DapperProvider : IDapperProvider
{
    // Fields
    private readonly DbConnectionManager _connectionManager;


    // Ctor
    public DapperProvider(DbConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }


    // Implementations
    public async Task<int> ExecuteAsync(string cmd, object? param, CommandType commandType , CancellationToken cancellationToken = default)
    {
        var remainingIterations = 5;


        // Iterate for fallback connection strings
        while (remainingIterations-- > 0)
        {
            // Data type conversion
            if (param is IEnumerable<RemoteCommandParameter> castedParam2)
                param = ToDynamicParameters(castedParam2);

            // Direct connection
            using var _db = _connectionManager.GetConnection();
            return await _db.ExecuteAsync(new CommandDefinition(cmd, param, null, 60, commandType, CommandFlags.Buffered, cancellationToken));
        }

        throw new DataProviderException("No more remaining iterations!", null);
    }
    public async Task<T> SingleAsync<T>(string cmd, object? param, CommandType commandType , CancellationToken cancellationToken = default)
    {
        var remainingIterations = 5;

        // Iterate for fallback connection strings
        while (remainingIterations-- > 0)
        {
            // Data type conversion
            if (param is IEnumerable<RemoteCommandParameter> castedParam2)
                param = ToDynamicParameters(castedParam2);

            // Direct connection
            using var _db = _connectionManager.GetConnection();
            return await _db.QuerySingleOrDefaultAsync<T>(new CommandDefinition(cmd, param, null, 60, commandType, CommandFlags.Buffered, cancellationToken));
        }

        throw new DataProviderException("No more remaining iterations!", null);
    }
    public async Task<T> ScalarAsync<T>(string cmd, object? param, CommandType commandType , CancellationToken cancellationToken = default)
    {
        var remainingIterations = 5;

        // Iterate for fallback connection strings
        while (remainingIterations-- > 0)
        {
            // Data type conversion
            if (param is IEnumerable<RemoteCommandParameter> castedParam2)
                param = ToDynamicParameters(castedParam2);

            // Direct connection
            using var _db = _connectionManager.GetConnection();
            return await _db.ExecuteScalarAsync<T>(new CommandDefinition(cmd, param, null, 60, commandType, CommandFlags.Buffered, cancellationToken));
        }

        throw new DataProviderException("No more remaining iterations!", null);
    }
    public async Task<IEnumerable<T>> SelectAsync<T>(string cmd, object? param, CommandType commandType , CancellationToken cancellationToken = default)
    {
        var remainingIterations = 5;

        // Iterate for fallback connection strings
        while (remainingIterations-- > 0)
        {
            // Data type conversion
            if (param is IEnumerable<RemoteCommandParameter> castedParam2)
                param = ToDynamicParameters(castedParam2);

            // Direct connection
            using var _db = _connectionManager.GetConnection();
            return await _db.QueryAsync<T>(new CommandDefinition(cmd, param, null, 60, commandType, CommandFlags.Buffered, cancellationToken));
        }

        throw new DataProviderException("No more remaining iterations!", null);
    }
    public async Task<T> FirstAsync<T>(string cmd, object? param, CommandType commandType , CancellationToken cancellationToken = default)
    {
        var remainingIterations = 5;

        // Iterate for fallback connection strings
        while (remainingIterations-- > 0)
        {
            // Data type conversion
            if (param is IEnumerable<RemoteCommandParameter> castedParam2)
                param = ToDynamicParameters(castedParam2);

            // Direct connection
            using var _db = _connectionManager.GetConnection();
            return await _db.QueryFirstOrDefaultAsync<T>(new CommandDefinition(cmd, param, null, 60, commandType, CommandFlags.Buffered, cancellationToken));
        }

        throw new DataProviderException("No more remaining iterations!", null);
    }

    //private method
    private DynamicParameters? ToDynamicParameters(IEnumerable<RemoteCommandParameter>? parameters)
    {
        if (parameters is null)
            return null;

        DynamicParameters dynamicParameters = new();

        foreach (var par in parameters)
        {
            dynamicParameters.Add(par.Name, Convert.ChangeType(par.Value?.ToString(), Type.GetType(par.TypeName)));
        }

        return dynamicParameters;
    }
}
