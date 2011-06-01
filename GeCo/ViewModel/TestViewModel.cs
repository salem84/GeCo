using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GeCo.ViewModel
{
    public class TestViewModel : WorkspaceViewModel
    {

        private string _testo;
        public string Testo
        {
            get { return _testo; }
            set
            {
                if (_testo != value)
                {
                    _testo = value;
                    RaisePropertyChanged("Testo");
                }
            }
        }

        public TestViewModel()
        {

            //Faccio partire il worker passando il metodo

            //Progress settato automaticamente
            //
            Stato = "Avvio...";
            StartBackgroundAutoProgress(Fai);
        }

        protected void Fai()
        {
            for (int i = 0; i < 30; i++)
            {
                //ProgressPercent = (int)((float)i / (float)30 * 100);
                System.Threading.Thread.Sleep(300);
                if (i > 29)
                    CancelBackgroundWorker();
                if (IsCancellationPending)
                    return;
            }
        }

       
    }
}
