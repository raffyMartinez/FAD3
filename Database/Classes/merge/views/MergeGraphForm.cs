using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FAD3.Database.Classes.merge.views
{
    public partial class MergeGraphForm : Form
    {
        private static MergeGraphForm _instance;
        private AOI _source;
        private AOI _destination;
        public MergeGraphForm(AOI source, AOI destination)
        {
            InitializeComponent();
            _source = source;
            _destination = destination;
            Load += OnFormLoad;
            FormClosing += OnFormClosing;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            global.SaveFormSettings(this);
            _instance = null;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            global.LoadFormSettings(this);
        }

        public Dictionary<int, int> GetNumbers(bool useSource)
        {
            var numbers = new Dictionary<int, int>();
            if (useSource)
            {
                foreach (var s in MergeDataBases.Source.SamplingViewModel.SamplingCollection
                    .Where(t => t.AOI.AOIGuid == _source.AOIGuid))
                {
                    if (numbers.ContainsKey(s.ReferenceNumber.SerialNumber))
                    {
                        numbers[s.ReferenceNumber.SerialNumber]++;
                    }
                    else
                    {
                        numbers.Add(s.ReferenceNumber.SerialNumber, 1);
                    }
                }
            }
            else
            {
                foreach (var s in MergeDataBases.Destination.SamplingViewModel.SamplingCollection
                     .Where(t => t.AOI.AOIGuid == _destination.AOIGuid))
                {
                    if (numbers.ContainsKey(s.ReferenceNumber.SerialNumber))
                    {
                        numbers[s.ReferenceNumber.SerialNumber]++;
                    }
                    else
                    {
                        numbers.Add(s.ReferenceNumber.SerialNumber, 1);
                    }
                }
            }

            return numbers;
        }


        public void ShowCounts()
        {

            chart1.Legends[0].Docking = Docking.Bottom;
            chart1.Legends[0].Alignment = StringAlignment.Center;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            //chart1.ChartAreas[0].AxisY.Maximum = sourceNumbers.Values.Max() + destinationNumbers.Values.Max() + 5;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            chart1.ChartAreas[0].AxisX.LabelStyle = new LabelStyle { Interval = 1, Angle = 90, TruncatedLabels = true };   
            chart1.ChartAreas[0].Name = "area1";

            var seriesSource = new Series("Source");
            var seriesDestinationBefore = new Series("Destination before");
            var seriesDestinationAfter = new Series("Destination after");

            seriesSource.ChartType = SeriesChartType.Column;
            seriesDestinationBefore.ChartType = SeriesChartType.Column;
            seriesDestinationAfter.ChartType = SeriesChartType.Column;

            FillSeries(seriesSource);
            FillSeries(seriesDestinationBefore);
            FillSeries(seriesDestinationAfter);

            chart1.Series.Add(seriesSource);
            chart1.Series.Add(seriesDestinationBefore);
            chart1.Series.Add(seriesDestinationAfter);

            seriesSource.YValueType = ChartValueType.Int32;
            seriesDestinationBefore.YValueType = ChartValueType.Int32;
            seriesDestinationAfter.YValueType = ChartValueType.Int32;

            seriesSource.ChartArea = "area1";
            seriesDestinationBefore.ChartArea = "area1";
            seriesDestinationAfter.ChartArea = "area1";

            Text = "Comparing record counts of source and destination target areas (before and after merging)";
        }

        private void FillSeries(Series series)
        {
            Dictionary<string, int> sourceOfCounts = new Dictionary<string, int>();
            switch (series.Name)
            {
                case "Source":
                    sourceOfCounts = MergeDataBases.SourceCounts; ;
                    break;
                case "Destination before":
                    sourceOfCounts = MergeDataBases.DestinationBeforeCounts;
                    break;
                case "Destination after":
                    sourceOfCounts = MergeDataBases.DestinationAfterCounts;
                    break;

            }

            foreach(var item in sourceOfCounts)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
             

        }
        public void ShowRefSerialNumbers()
        {
            var sourceNumbers = GetNumbers(true);
            var destinationNumbers = GetNumbers(false);

            var seriesSource = new Series("Source");
            var seriesDestination = new Series("Destination");
            seriesSource.ChartType = SeriesChartType.StackedColumn;
            seriesDestination.ChartType = SeriesChartType.StackedColumn;

            int maxXValue = sourceNumbers.Keys.Max() > destinationNumbers.Keys.Max() ? sourceNumbers.Keys.Max() : destinationNumbers.Keys.Max();
            for (int x = 1; x <= maxXValue; x++)
            {
                DataPoint dp = new DataPoint(x, sourceNumbers.ContainsKey(x) ? sourceNumbers[x] : 0);
                Console.WriteLine($"x:{dp.XValue} y:{dp.YValues[0]}");
                seriesSource.Points.Add(dp);
            }

            for (int x = 1; x <= maxXValue; x++)
            {
                DataPoint dp = new DataPoint(x, destinationNumbers.ContainsKey(x) ? destinationNumbers[x] : 0);
                Console.WriteLine($"x:{dp.XValue} y:{dp.YValues[0]}");
                seriesDestination.Points.Add(dp);
            }
            chart1.Legends[0].Docking = Docking.Bottom;
            chart1.Legends[0].Alignment = StringAlignment.Center;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = sourceNumbers.Values.Max() + destinationNumbers.Values.Max() + 5;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            chart1.ChartAreas[0].Name = "area1";


            seriesSource.YValueType = ChartValueType.Int32;
            seriesDestination.YValueType = ChartValueType.Int32;
            chart1.Series.Add(seriesSource);
            seriesSource.ChartArea = "area1";
            chart1.Series.Add(seriesDestination);
            seriesDestination.ChartArea = "area1";

            Text = "Range of serial numbers used by Reference numbers in source and destination target areas";
        }

        public static MergeGraphForm GetInstance(AOI source, AOI destination)
        {
            if (_instance == null) _instance = new MergeGraphForm(source, destination);
            return _instance;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}
