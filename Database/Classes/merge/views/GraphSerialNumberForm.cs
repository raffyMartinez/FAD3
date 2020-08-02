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
    public partial class GraphSerialNumberForm : Form
    {
        private static GraphSerialNumberForm _instance;
        private AOI _source;
        private AOI _destination;
        public GraphSerialNumberForm(AOI source, AOI destination)
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

        public Dictionary<int,int> GetNumbers(bool useSource)
        {
            var numbers = new Dictionary<int, int>();
            if(useSource)
            {
                foreach(var s in MergeDataBases.Source.SamplingViewModel.SamplingCollection
                    .Where(t=>t.AOI.AOIGuid==_source.AOIGuid))
                {
                    if(numbers.ContainsKey(s.ReferenceNumber.SerialNumber))
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
        public void RefreshChart()
        {
            var sourceNumbers = GetNumbers(true);
            var destinationNumbers = GetNumbers(false);
            
            var seriesSource = new Series("Source");
            var seriesDestination = new Series("Destination");
            seriesSource.ChartType = SeriesChartType.StackedColumn;
            seriesDestination.ChartType = SeriesChartType.StackedColumn;

            int maxXValue = sourceNumbers.Keys.Max() > destinationNumbers.Keys.Max() ? sourceNumbers.Keys.Max() : destinationNumbers.Keys.Max();
            for (int x=1;x <= maxXValue; x++ )
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


        }

        public static GraphSerialNumberForm GetInstance(AOI source, AOI destination)
        {
            if (_instance == null) _instance = new GraphSerialNumberForm(source,destination);
            return _instance;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Close();
        }
    }
}
