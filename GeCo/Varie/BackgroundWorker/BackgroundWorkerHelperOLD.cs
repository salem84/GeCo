using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GeCo.Varie.BackWorker_OLD
{
    public static class BackgroundWorkerHelper<Tin,Tout,Tus>
    {
        public static void DoWork(
        Tin inArg,
        Func<DoWorkArg<Tin>, Tout> doWork,
        Action<WorkerResult<Tout>> workerCompleted,
        Action<WorkerProgress<Tus>> progressChanged)
        {
            BackgroundWorker bw = new BackgroundWorker();
            
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += (sender, args) =>
            {
                if (doWork != null)
                {
                    //Action reportProgress = new Action(bw.ReportProgress);
                    Action<int,object> reportProgress = new Action<int,object>(bw.ReportProgress);
                    Action cancelAsync = new Action(bw.CancelAsync);
                    args.Result = doWork(new DoWorkArg<Tin>((Tin)args.Argument, reportProgress, cancelAsync));
                }
            };
            bw.RunWorkerCompleted += (sender, args) =>
            {
                if (workerCompleted != null)
                {
                    workerCompleted(new WorkerResult<Tout>((Tout)args.Result, args.Error, args.Cancelled));
                }
            };
            bw.ProgressChanged += (sender, args) =>
            {
                if (progressChanged != null)
                {
                    progressChanged(new WorkerProgress<Tus>(args.ProgressPercentage, (Tus)args.UserState));
                }
            };
            bw.RunWorkerAsync(inArg);
        }
    }
}

