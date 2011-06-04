using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.Varie.BackWorker_OLD
{
    public class WorkerResult<T>
    {
        public T Result { get; private set; }
        public Exception Error { get; private set; }
        public bool Cancelled { get; private set; }

        public WorkerResult(T result, Exception error, bool cancelled)
        {
            this.Result = result;
            this.Error = error;
            this.Cancelled = cancelled;
        }
    }
}
