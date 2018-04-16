
namespace DiffProject.Infrastructure.V1
{
	using System.Threading.Tasks;

    public interface IHashStrategy
    {
        Task<string> GetHashAsync(string source);
    }
}
