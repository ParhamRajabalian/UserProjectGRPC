namespace DataAccessLayer.Entities;
public sealed class OptionConnectionManager
{
    public const string Name = "Connections";
    public string? SQLConnection { get; set; }
    public string? FilePathConnection { get; set; }
    public string? DataBaseType { get; set; }
    public string? GetConnectionString(string connectionName)
    {
        return connectionName switch
        {
            "SQL" => SQLConnection,
            "File" => FilePathConnection,
            _ => throw new InvalidOperationException("Unsupported connection name.")
        };
    }
}
