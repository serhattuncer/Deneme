namespace Dastone.HttpRequest
{
    public class ResponseWrapper<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public ResponseWrapper(bool isSuccess, T data, string errorMessage, int statusCode)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}
