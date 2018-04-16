namespace DiffProject.Infrastructure.V1
{
	using System;
	using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    public class RawRequestBodyInputFormatter : InputFormatter
    {
        public RawRequestBodyInputFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;


            if (string.IsNullOrEmpty(contentType) || contentType.Contains("text/plain"))
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var content = await reader.ReadToEndAsync();
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }

            if (contentType.Contains("application/octet-stream"))
            {
                using (var ms = new MemoryStream())
                {
                    await request.Body.CopyToAsync(ms);
                    var content = ms.ToArray();
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }

            return await InputFormatterResult.FailureAsync();
        }

		public override bool CanRead(InputFormatterContext context)
		{
            if (context == null) throw new ArgumentNullException(nameof(context));

            var contentType = context.HttpContext.Request.ContentType;

            if (string.IsNullOrEmpty(contentType) || 
                contentType.Contains("text/plain") ||
                contentType.Contains("application/octet-stream"))
                return true;

            return false;
		}
	}
}
