using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace WebAPI.API.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var sb = new StringBuilder();

            using var stringWriter = new StringWriter(sb);
            using (var csvWriter = new CsvWriter(stringWriter))
            {
                csvWriter.WriteRecords((IEnumerable<object>)context.Object);
            }

            return context.HttpContext.Response.WriteAsync(stringWriter.ToString(), selectedEncoding);
        }
    }
}
