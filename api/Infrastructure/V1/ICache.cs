namespace DiffProject.Infrastructure.V1
{
    using System.Threading.Tasks;
    using DiffProject.Models.V1;

    public interface ICache
    {
        Task AddAsync(string key, Data data);
        Task<Data?> GetAsync(string key);
    }
}
