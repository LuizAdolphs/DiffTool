namespace DiffProject.Infrastructure.V1
{
	using System;
	using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;

    public class RawRequestBodyInputFormatter : InputFormatter
    {
        IEncodeDecodeStrategy _encodeDecodeStrategy;

        static string _applicationJson = "application/json";
        static string _textPlain = "text/plain";

        public RawRequestBodyInputFormatter(IEncodeDecodeStrategy encodeDecodeStrategy)
        {
            this._encodeDecodeStrategy = encodeDecodeStrategy;

            SupportedMediaTypes.Add(new MediaTypeHeaderValue(_applicationJson));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(_textPlain));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {

            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;

            if (string.IsNullOrEmpty(contentType) || 
                contentType.Contains(_applicationJson) ||
                contentType.Contains(_textPlain))
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var content = await reader.ReadToEndAsync();

                    var decoded = await _encodeDecodeStrategy.Decode(content);

                    if(contentType.Contains(_applicationJson))
                        return await InputFormatterResult.SuccessAsync(JsonConvert.DeserializeObject(decoded, context.ModelType));

                    return await InputFormatterResult.SuccessAsync(decoded);
                }
            }

            return await InputFormatterResult.FailureAsync();
        }

		public override bool CanRead(InputFormatterContext context)
		{
            if (context == null) throw new ArgumentNullException(nameof(context));

            var contentType = context.HttpContext.Request.ContentType;

            if (string.IsNullOrEmpty(contentType) || 
                contentType.Contains(_applicationJson) ||
                contentType.Contains(_textPlain))
                return true;

            return false;
		}
	}
}
