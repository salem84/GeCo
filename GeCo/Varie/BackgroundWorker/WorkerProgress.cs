using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeCo.Varie.BackWorker_OLD
{
    public class WorkerProgress<Tus>
    {
        public int ProgressPercentage { get; private set; }
        public Tus UserState { get; private set; }

        public WorkerProgress(int progressPercentage, Tus userState)
        {
            this.ProgressPercentage = progressPercentage;
            this.UserState = userState;
        }
    }
}
