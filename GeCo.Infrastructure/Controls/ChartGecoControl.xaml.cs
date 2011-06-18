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

namespace GeCo.Infrastructure
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class ChartGecoControl : UserControl
    {
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


        public List<decimal> Values
        {
            get { return (List<decimal>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Valori.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", 
            typeof(List<decimal>), 
            typeof(ChartGecoControl), 
            new UIPropertyMetadata(null, new PropertyChangedCallback(OnValuesChanged)));




        public ChartGecoControl()
        {
            this.InitializeComponent();
            //var windowsForms = System.Windows.Forms.Integration.WindowsFormsHost();

            //panel.Children.Add()

            
            
            
        }

        private void CreateChart()
        {
            Chart chart1 = new Chart();

            ChartArea chartArea1 = new ChartArea();
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.Inclination = 50;

            // Add Chart Area to the Chart
            chart1.ChartAreas.Add(chartArea1);

            // Create a data series
            Series series1 = new Series();

            series1.Points.DataBindXY(Labels, Values);
            series1.ChartType = SeriesChartType.Pie;
            series1["PieLabelStyle"] = "Outside";

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

    }
}
