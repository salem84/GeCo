using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using GeCo.Model;
using GeCo.Infrastructure.Workspace;
using System.Windows.Input;
using GeCo.Infrastructure;
using GeCo.BLL.Services;

namespace GeCo.ModuleOpzioni.ViewModels
{
    public class OpzioniVM : Workspace
    {
        #region PROPRIETA'

        protected override string containerName { get { return Names.MODULE_NAME; } }
        public override string IdWorkspace { get { return Names.VIEW_OPZIONI; } }

        public override string DisplayTabName
        {
            get
            {
                return "Gestione Parametri";
            }
        }

        

        private ObservableCollection<Parametro> _parametriLista;
        public ObservableCollection<Parametro> ParametriLista
        {
            get { return _parametriLista; }
            set
            {
                if (_parametriLista != value)
                {
                    _parametriLista = value;
                    RaisePropertyChanged("ParametriLista");
                }
            }
        }

        private Parametro _parametroSelezionato;
        public Parametro ParametroSelezionato
        {
            get { return _parametroSelezionato; }
            set
            {
                if (_parametroSelezionato != value)
                {
                    _parametroSelezionato = value;
                    RaisePropertyChanged("ParametroSelezionato");
                }
            }
        }

        
        #endregion


        #region COMMANDS
        
        
        public ICommand SalvaCommand { get; set; }
        public ICommand SalvaTuttoCommand { get; set; }

        #endregion

        #region SERVICES

        IAdminServices _adminServices;

        #endregion

        public OpzioniVM(IAdminServices adminServices)
        {
            Stato = "Opzioni";

            _adminServices = adminServices;

            SalvaCommand = new RelayCommand(SalvaParametro);
            SalvaTuttoCommand = new RelayCommand(SalvaTuttiParametri);
            
            //Carica i parametri
            ParametriLista = new ObservableCollection<Parametro>(_adminServices.GetParametri());
            
        }

        

        private void SalvaParametro()
        {
            if (ParametroSelezionato != null)
            {
                _adminServices.SalvaParametro(ParametroSelezionato);
                
            }

            
        }
        
        private void SalvaTuttiParametri()
        {
            foreach (var parametroMod in ParametriLista)
            {
                _adminServices.SalvaParametro(parametroMod);
            }
            
        }

    }
}
