using DataAccessLayer.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Data;

namespace DataAccessLayer;
public class DbConnectionManager
{
    private readonly OptionConnectionManager _connections;

    public DbConnectionManager(IOptions<OptionConnectionManager> connections)
    {
        _connections = connections.Value ?? throw new ArgumentNullException(nameof(connections));
    }

    // GET connection 
    public IDbConnection GetConnection()
    {
        var dbType = _connections.DataBaseType;
        var connectionString = _connections.GetConnectionString(dbType ?? throw new InvalidOperationException("Database type is not configured"));

        return dbType switch
        {
            "SQL" => new SqlConnection(connectionString),
            "Other" => new SqliteConnection(connectionString),
            _ => throw new InvalidOperationException($"Unsupported database type: {dbType}")
        };
    }

    public string GetFilePath()
    {
        if (_connections.DataBaseType == "File")
        {
            var pathFile = _connections.GetConnectionString("File")
               ?? throw new InvalidOperationException("File path is not configured.");
            var baseDirectory = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(baseDirectory, pathFile);
            return fullPath;
        }

        throw new InvalidOperationException("Current database type does not support file storage.");
    }
}
