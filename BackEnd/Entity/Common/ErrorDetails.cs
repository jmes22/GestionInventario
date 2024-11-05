using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Common
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }

        public ErrorDetails()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
