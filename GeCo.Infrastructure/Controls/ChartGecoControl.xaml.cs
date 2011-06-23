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
using System.Windows.Forms.DataVisualization.Charting;


namespace GeCo.Infrastructure.Controls
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class ChartGecoControl : UserControl
    {


        public List<string> Legends
        {
            get { return (List<string>)GetValue(LegendsProperty); }
            set { SetValue(LegendsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Legends.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LegendsProperty =
            DependencyProperty.Register("Legends", 
            typeof(List<string>), 
            typeof(ChartGecoControl), 
            new UIPropertyMetadata(null));

        



        //Lista di Label
        public List<string> Labels
        {
            get { return (List<string>)GetValue(LabelsProperty); }
            set { SetValue(LabelsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Labels.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelsProperty =
            DependencyProperty.Register("Labels", 
            typeof(List<string>), 
            typeof(ChartGecoControl), 
            new UIPropertyMetadata(null, new PropertyChangedCallback(OnLabelsChanged)));



        //Lista di Valori
        public List<double> Values
        {
            get { return (List<double>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Valori.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", 
            typeof(List<double>), 
            typeof(ChartGecoControl), 
            new UIPropertyMetadata(null, new PropertyChangedCallback(OnValuesChanged)));

        

        public List<System.Drawing.Color> PaletteCustomColors
        {
            get { return (List<System.Drawing.Color>)GetValue(PaletteCustomColorsProperty); }
            set { SetValue(PaletteCustomColorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PaletteColors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaletteCustomColorsProperty =
            DependencyProperty.Register("PaletteCustomColors", typeof(List<System.Drawing.Color>), typeof(ChartGecoControl), new UIPropertyMetadata(null));

        


        public ChartGecoControl()
        {
            this.InitializeComponent();
            //var windowsForms = System.Windows.Forms.Integration.WindowsFormsHost();

            //panel.Children.Add()

            
            
            
        }

        private void CreateChart()
        {
            Chart chart1 = new Chart();

            //Se ho impostato una palette di colori
            if (PaletteCustomColors != null)
            {
                chart1.PaletteCustomColors = PaletteCustomColors.ToArray();
                chart1.Palette = ChartColorPalette.None;
            }
            //chart1.BorderSkin.SkinStyle = BorderSkinStyle.Sunken;

            
            //Titolo
            /*Title title1 = new Title();
            title1.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold);
            title1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            title1.Name = "Title1";
            title1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            title1.ShadowOffset = 3;
            title1.Text = "Grafico Punteggi Totali";
            chart1.Titles.Add(title1);*/

            ChartArea chartArea1 = new ChartArea();
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.Inclination = 50;

            /*chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(165)))), ((int)(((byte)(191)))), ((int)(((byte)(228)))));
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));*/

            chart1.ChartAreas.Add(chartArea1);
            
            // serie dati 
            Series series1 = new Series();

            series1.Points.DataBindXY(Labels,Values);
            series1.IsValueShownAsLabel = false;
            //series1.Label = "Y = #VALY\nX = #VALX";
            series1.Label = "#VALY %";
            series1.ChartType = SeriesChartType.Pie;
            series1["PieLabelStyle"] = "Inside";
            series1.BorderColor = System.Drawing.Color.Black;
            series1.BorderWidth = 1;
            series1.BorderDashStyle = ChartDashStyle.Solid;

            Legend legend1 = new Legend();
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.LegendStyle = LegendStyle.Table;
            legend1.Enabled = true;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            legend1.IsTextAutoFit = false;
            legend1.Name = "Default";

            LegendCellColumn firstColumn = new LegendCellColumn();
            firstColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
            //firstColumn.HeaderText = "Color";
            firstColumn.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
            legend1.CellColumns.Add(firstColumn);

            LegendCellColumn secondColumn = new LegendCellColumn();
            secondColumn.ColumnType = LegendCellColumnType.Text;
            //secondColumn.HeaderText = "Name";
            secondColumn.Text = "#VALX";
            secondColumn.HeaderBackColor = System.Drawing.Color.WhiteSmoke;
            legend1.CellColumns.Add(secondColumn);


            chart1.Legends.Add(legend1);

            // Add series to the chart
            chart1.Series.Add(series1);

            
            

            
            

            // Set chart control location
            //chart1.Location = new System.Drawing.Point(16, 48);

            // Set Chart control size
            //chart1.Size = new System.Drawing.Size(360, 260);
            windowsFormsHost1.Child = chart1;
        }

        private static void OnLabelsChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as ChartGecoControl;
            if (control.Values != null && control.Labels != null)
                control.CreateChart();
        }

        private static void OnValuesChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as ChartGecoControl;
            if (control.Values != null && control.Labels != null)
                control.CreateChart();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Visibility = System.Windows.Visibility.Hidden;
        }

        
    }
}
