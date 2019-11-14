namespace WebAPI.Business.Contracts.Models
{
    public class OperationResult
    {
        public OperationResult(bool isSuccess, string message = null)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
