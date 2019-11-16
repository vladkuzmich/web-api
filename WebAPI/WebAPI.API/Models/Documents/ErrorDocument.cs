using System.Text.Json;

namespace WebAPI.API.Models.Documents
{
    public class ErrorDocument
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
