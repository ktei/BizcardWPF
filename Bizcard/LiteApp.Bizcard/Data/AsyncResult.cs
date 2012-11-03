using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteApp.Bizcard.Data
{
    public class AsyncResult<T>
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public Exception Error { get; set; }
    }
}
