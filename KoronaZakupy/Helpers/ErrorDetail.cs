using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Helpers
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }


        public ErrorDetails(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
