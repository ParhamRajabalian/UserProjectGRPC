using DataAccessLayer.Contracts;
using DataAccessLayer.Entities;
using DataAccessLayer.Oprations.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace DataAccessLayer.Oprations;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IOptions<OptionConnectionManager> _connectionOptions;
    private readonly IServiceProvider _serviceProvider;

    public RepositoryFactory(IOptions<OptionConnectionManager> connectionOptions, IServiceProvider serviceProvider)
    {
        _connectionOptions = connectionOptions;
        _serviceProvider = serviceProvider;
    }

    public IRepository<T> CreateRepository<T>() where T : class
    {

        if (_connectionOptions.Value.DataBaseType == "SQL")
        {
            return _serviceProvider.GetRequiredService<DapperRepository<T>>();
        }
        else if (_connectionOptions.Value.DataBaseType == "File")
        {
            return _serviceProvider.GetRequiredService<FileRepository<T>>();
        }

        throw new InvalidOperationException("Unsupported database type");

    }

}
