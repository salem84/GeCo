using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GeCo.Varie.BackWorker_OLD
{
    public class DoWorkArg<T>
    {
        public T Arg { get; private set; }
        public Action<int,object> ReportProgress;
        public Action CancelAsync;

        public DoWorkArg(T Arg, Action<int,object> reportProgress, Action cancelAsync)
        {
            this.Arg = Arg;
            this.ReportProgress = reportProgress;
            this.CancelAsync = cancelAsync;
        }
    }
}
