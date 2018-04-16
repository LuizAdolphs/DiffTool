
namespace DiffProject.Infrastructure.V1
{
    using System.Threading.Tasks;

    public interface IEncodeDecodeStrategy
    {
        Task<string> Encode(string source);
        Task<string> Decode(string encoded);
    }
}
