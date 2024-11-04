using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Common
{
    public class ResultList<T>
    {
        public bool IsSuccess { get; }
        public IEnumerable<T> Data { get; }
        public string Error { get; }
        public int StatusCode { get; }
        public int TotalRecords { get; }

        protected ResultList(bool isSuccess, IEnumerable<T> data, string error, int statusCode, int totalRecords)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
            StatusCode = statusCode;
            TotalRecords = totalRecords;
        }

        public static ResultList<T> Success(IEnumerable<T> data, int totalRecords, int statusCode = 200)
        {
            return new ResultList<T>(true, data, null, statusCode, totalRecords);
        }

        public static ResultList<T> Failure(string error, int statusCode = 400)
        {
            return new ResultList<T>(false, default, error, statusCode, 0);
        }
    }
}
