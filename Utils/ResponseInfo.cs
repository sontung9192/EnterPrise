using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class ResponseInfo
    {
        public int Code { get; set; }
        public string ResponseMessage { get; set; }
        public object Data { get; set; }
    }
    public class ResponseCode
    {
        public string code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
