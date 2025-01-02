using DataAccessLayer.Contracts;
using DataAccessLayer.Contracts.Providers;
using System.Data;

namespace DataAccessLayer.Oprations
{
    public class DapperRepository<T> : IRepository<T> where T : class
    {
        private readonly IDapperProvider _dapper;

        public DapperRepository(IDapperProvider dapperRepository)
        {
            _dapper = dapperRepository;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var cmd = $"SELECT * FROM {typeof(T).Name}s";
            return await _dapper.SelectAsync<T>(cmd, null, CommandType.Text);
        }
        public async Task<T?> GetByIdAsync(object id)
        {
            var cmd = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
            var param = new { Id = id };
            return await _dapper.SingleAsync<T>(cmd, param, CommandType.Text);
        }

        public async Task AddAsync(T entity)
        {
            var cmd = $"INSERT INTO {typeof(T).Name}s ({string.Join(", ", typeof(T).GetProperties().Select(p => p.Name))}) " +
                      $"VALUES ({string.Join(", ", typeof(T).GetProperties().Select(p => "@" + p.Name))})";
            var param = entity;
            await _dapper.ExecuteAsync(cmd, param, CommandType.Text);
        }

        public async Task UpdateAsync(T entity)
        {
            var cmd = $"UPDATE {typeof(T).Name}s SET " +
                      $"{string.Join(", ", typeof(T).GetProperties().Select(p => $"{p.Name} = @{p.Name}"))} " +
                      $"WHERE Id = @Id";
            var param = entity;
            await _dapper.ExecuteAsync(cmd, param, CommandType.Text);
        }

        public async Task DeleteAsync(object id)
        {
            var cmd = $"DELETE FROM {typeof(T).Name}s WHERE Id = @Id";
            var param = new { Id = id };
            await _dapper.ExecuteAsync(cmd, param, CommandType.Text);
        }
    }
}
