namespace Talabat.Apis.Errors;

public class ApiResponse
{
    public string? Message { get; set; }
    public int StatusCode { get; set; }

    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }
    
    private string? GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => null
        };
    }
}