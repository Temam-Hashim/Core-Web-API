using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Service
{
    public class ApiResponseService
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

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
