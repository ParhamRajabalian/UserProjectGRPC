using DataAccessLayer.Contracts;
using Newtonsoft.Json;

namespace DataAccessLayer.Oprations;

public class FileRepository<T> : IRepository<T> where T : class
{
    private readonly string _filePath;

    public FileRepository(DbConnectionManager connectionManager)
    {
        // دریافت مسیر فایل از DbConnectionManager
        _filePath = connectionManager.GetFilePath();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
            return Enumerable.Empty<T>();

        var jsonData = await File.ReadAllTextAsync(_filePath);
        return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonData) ?? Enumerable.Empty<T>();
    }

    public async Task<T?> GetByIdAsync(object id)
    {
        var data = await GetAllAsync();
        return data.FirstOrDefault(item => item.GetType().GetProperty("Id")?.GetValue(item)?.Equals(id) == true);
    }

    public async Task AddAsync(T entity)
    {
        var data = (await GetAllAsync()).ToList();
        data.Add(entity);

        var jsonData = JsonConvert.SerializeObject(data);
        await File.WriteAllTextAsync(_filePath, jsonData);
    }

    public async Task UpdateAsync(T entity)
    {
        var data = (await GetAllAsync()).ToList();
        var entityToUpdate = data.FirstOrDefault(e => e.GetType().GetProperty("Id")?.GetValue(e)?.Equals(entity.GetType().GetProperty("Id")?.GetValue(entity)) == true);

        if (entityToUpdate != null)
        {
            data.Remove(entityToUpdate);
            data.Add(entity);

            var jsonData = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(_filePath, jsonData);
        }
    }

    public async Task DeleteAsync(object id)
    {
        var data = (await GetAllAsync()).ToList();
        var entityToDelete = data.FirstOrDefault(e => e.GetType().GetProperty("Id")?.GetValue(e)?.Equals(id) == true);

        if (entityToDelete != null)
        {
            data.Remove(entityToDelete);

            var jsonData = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(_filePath, jsonData);
        }
    }
}
