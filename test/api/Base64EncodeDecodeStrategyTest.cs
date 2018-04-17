namespace api
{
    using System;
    using Xunit;
    using DiffProject.Infrastructure.V1;

    public class Base64EncodeDecodeStrategyTest
    {

        private IEncodeDecodeStrategy _encodeDecodeStrategy = new Base64EncodeDecodeStrategy();

        [Theory]
        [InlineData("Lazy", "TGF6eQ==")]
        [InlineData("Brown", "QnJvd24=")]
        [InlineData("Fox", "Rm94")]
        public void TestDecodeToBase64(string expected, string encoded)
        {
            var result = _encodeDecodeStrategy.Decode(encoded).Result;

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("TGF6eQ==", "Lazy")]
        [InlineData("QnJvd24=", "Brown")]
        [InlineData("Rm94", "Fox")]
        public void TestEncodeFromBase64(string expected, string decoded)
        {
            var result = _encodeDecodeStrategy.Encode(decoded).Result;

            Assert.Equal(expected, result);
        }
    }
}
