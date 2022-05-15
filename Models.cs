using System;
using System.Collections.Generic;
using System.Text;

namespace PlumTest
{
    public class Models
    {
        public class Request
        {
            public string url { get; set; }
            public string provider { get; set; }
        }

        public class Response
        {
            public string url { get; set; }
            public string link { get; set; }
        }
    }
}
