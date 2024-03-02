using System;
using System.Collections.Generic;
using System.Text;

namespace BookingSolution.ViewModels.Common
{
    public class ApiResult<T>
    {
        public static ApiResult<T> Success(T data)
        {
            return new ApiResult<T> { IsSuccessed = true, Token = data };
        }
        public bool IsSuccessed { get; set; }

        public string Message { get; set; }

        public T Token { get; set; }
    }
}