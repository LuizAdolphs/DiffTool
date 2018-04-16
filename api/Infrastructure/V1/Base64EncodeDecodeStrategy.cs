namespace DiffProject.Infrastructure.V1
{
	using System;
    using System.Text;
    using System.Threading.Tasks;

    public class Base64EncodeDecodeStrategy : IEncodeDecodeStrategy
    {
        public async Task<string> Decode(string encoded)
        {
            return await Task.Run(() =>
            {
                byte[] decodedBytes = Convert.FromBase64String(encoded);
                return Encoding.UTF8.GetString(decodedBytes);
            });
		}

        public async Task<string> Encode(string source)
        {
            return await Task.Run(() =>
            {
                byte[] encodedBytes = Encoding.UTF8.GetBytes(source);
                return Convert.ToBase64String(encodedBytes);
            });
		}
    }
}
