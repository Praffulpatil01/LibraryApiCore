namespace LibraryApi.Dtos
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; } = null!;
        public T? Data { get; set; }

        public static ApiResponse<T> Success(int statusCode, string message, T? data)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                StatusMessage = message,
                Data = data
            };
        }

        public static ApiResponse<T> Fail(int statusCode, string message)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                StatusMessage = message,
                Data = default
            };
        }
    }
}
