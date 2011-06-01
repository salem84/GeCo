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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeCo;
using GeCo.DAL;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace GeCo.View
{
    /// <summary>
    /// Interaction logic for Competenze.xaml
    /// </summary>
    public partial class CompetenzeView_OLD : UserControl
    {
        //protected ObservableCollection<Competenza> CompetenzeOC { get; set; }
        private PavimentalDb _context = new PavimentalDb();

        public CompetenzeView_OLD()
        {
            InitializeComponent();
        }

        

        protected ObservableCollection<Competenza> GetCompetenze()
        {
            
            ObservableCollection<Competenza> CompetenzeOC = new ObservableCollection<Competenza>(
                from c in _context.Competenze
                select c);
                

            return CompetenzeOC;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            /*foreach (var competenza in _context.Competenze.Local.ToList())
            {
                if (competenza. == null)
                {
                    _context.Products.Remove(product);
                }
            }*/

            _context.SaveChanges();
            // Refresh the grids so the database generated values show up.
            this.competenzaDataGrid.Items.Refresh();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource competenzeViewSource = ((CollectionViewSource)(this.FindResource("competenzeViewSource")));
            
            _context.Competenze.Load();

            competenzeViewSource.Source = _context.Competenze.Local;

        }

        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //    //todo da collegare
        //    this._context.Dispose();
        //}

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Competenza competenza = competenzaDataGrid.SelectedItem as Competenza;
            _context.Competenze.Remove(competenza);
            _context.SaveChanges();
        }
    }
}
