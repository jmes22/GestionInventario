using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }
        public string Error { get; }
        public int StatusCode { get; }

        protected Result(bool isSuccess, T data, string error, int statusCode)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T data, int statusCode = 200)
        {
            return new Result<T>(true, data, null, statusCode);
        }

        public static Result<T> Failure(string error, int statusCode = 400)
        {
            return new Result<T>(false, default, error, statusCode);
        }
    }
}
