using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Service
{
    public class ApiResponseService
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static ApiResponseService Success(string message, object data = null) =>
            new ApiResponseService { StatusCode = 200, Message = message, Data = data };

        public static ApiResponseService Error(int statusCode, string message, object data = null) =>
            new ApiResponseService { StatusCode = statusCode, Message = message };
    }
}