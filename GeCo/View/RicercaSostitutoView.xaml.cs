using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GeCo;
using System.Data.Entity;
using System.Collections;
using System.ComponentModel;
using GeCo.ViewModel;

namespace GeCo.View
{
    /// <summary>
    /// Interaction logic for RicercaSostituto.xaml
    /// </summary>
    public partial class RicercaSostitutoView : UserControl
    {
        

        public RicercaSostitutoView()
        {
            BindingErrorListener.Listen(m => MessageBox.Show(m));
            //RicercaSostitutoViewModel vm = new RicercaSostitutoViewModel();
            //DataContext = vm;
            InitializeComponent();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
            

            //context.Livelli.Load();
            //cmbLivelli.ItemsSource = context.Livelli.Local;
            
            
            /*cmbLivelli.ItemsSource = context.Livelli
                //.Include(l => l.Conoscenze.Select(c => c.Competenza))
                //.Include(l => l.Conoscenze.Select(c => c.LivelloConoscenza))
                .AsEnumerable();*/

            
            //grid.DataContext = this;

        //}


        

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
            
            
            //int id = (cmbLivelli.SelectedItem as Livelli).Id;
            //Livelli livelloS = db.Livelli
            //    .Include(l => l.Conoscenze.Select(c => c.Competenza))
            //    .Include(l => l.Conoscenze.Select(c => c.LivelloConoscenza))
            //    .Where(l=> l.Id == id).Single();

            //int indiceTotaleSelezionato = livelloS.IndiceTotale;

            ///*var res = (from livello in db.Livelli
            //          select new
            //          {
            //              Livello = livello,
            //              IndiceSomiglianza = indiceTotaleSelezionato / livello.IndiceTotale * 100
            //          }).OrderBy(l => l.IndiceSomiglianza) ;*/

            ////var res1 = db.Livelli
            ////    .Include(l => l.Conoscenze.Select(c => c.Competenza))
            ////    .Include(l => l.Conoscenze.Select(c => c.LivelloConoscenza))
            ////    .Select(l => new
            ////    {
            ////        livello = l,
            ////        indice = indiceTotaleSelezionato / l.IndiceTotale * 100
            ////    });

            ////IEnumerable<Livelli> tuttiLivelli = GetTuttiLivelli();

            //IEnumerable livelliConIndice = CalcolaIndiciEOrdina(Livelli);

            //lbLivelli.ItemsSource = livelliConIndice;
            
        //}

        
    }

    
}
