namespace DataAccessLayer.Oprations.Providers;

public class DataProviderException : Exception
{
    public DataProviderException() : base()
    {

    }

    public DataProviderException(string? message, Exception? innerException) : base(message, innerException)
    {

    }
}
