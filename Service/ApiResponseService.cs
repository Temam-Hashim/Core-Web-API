using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Service
{
    public class ApiResponseService
    {
        public required int  StatusCode { get; set; }
        public required string Message { get; set; }
        public object Data { get; set; } = null;

        // Generic Success Response
        public static IActionResult Success(string message, object data = null, int statusCode = 200)
        {
            return new ObjectResult(new ApiResponseService
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            })
            {
                StatusCode = statusCode
            };
        }

        // Generic Error Response
        public static IActionResult Error(int statusCode, string message, object data = null)
        {
            return new ObjectResult(new ApiResponseService
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            })
            {
                StatusCode = statusCode
            };
        }
    }
}
