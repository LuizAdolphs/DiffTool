namespace DiffProject.Infrastructure.V1
{
	using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class Md5HashStrategy : IHashStrategy
    {

        public async Task<string> GetHashAsync(string source)
        {
            return await Task.Run(() =>
            {
                if (String.IsNullOrEmpty(source))
                    return string.Empty;

                using (var md5 = MD5.Create())
                {
                    var result = md5.ComputeHash(Encoding.ASCII.GetBytes(source));
                    return Encoding.ASCII.GetString(result);
                }
            });
        }
    }
}
