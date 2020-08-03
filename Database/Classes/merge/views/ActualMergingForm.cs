using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAD3.Database.Classes.merge.views
{
    public partial class ActualMergingForm : Form
    {
        private static ActualMergingForm _instance;
        public AOI SourceAOI { get; private set; }
        public AOI DestinationAOI { get; private set; }

        private MergeDBHelper _mergeDBHelper;
        public ActualMergingForm(AOI source)
        {
            InitializeComponent();
            SourceAOI = source;

        }
        public static ActualMergingForm GetInstance(AOI source)
        {
            if (_instance == null) _instance = new ActualMergingForm(source);
            return _instance;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            global.LoadFormSettings(this);
            Text = $"Merging data from {SourceAOI.AOIName} to {global.mainForm.TargetArea.TargetAreaName}";
            checkProceed.Checked = false;
            checkProceed.Text = $"I want to merge data from {SourceAOI.AOIName} to {global.mainForm.TargetArea.TargetAreaName}";
            buttonMerge.Enabled = false;

            DestinationAOI = MergeDataBases.Destination.AOIViewModel.GetAOI(global.mainForm.TargetArea.TargetAreaGuid);
            DestinationTargetAreaQuickView dtaqv = new DestinationTargetAreaQuickView
            {
                Name = DestinationAOI.AOIName,
                NumberLandingSites = MergeDataBases.Destination.LandingSiteViewModel.CountByAOI(DestinationAOI),
                NumberSamplings = MergeDataBases.Destination.SamplingViewModel.CountByAOI(DestinationAOI)
            };

            if (dtaqv.NumberSamplings > 0)
            {
                dtaqv.FirstSamplingDate = MergeDataBases.Destination.SamplingViewModel.GetEarliestSampling(DestinationAOI).DateTimeSampled;
                dtaqv.LastSamplingDate = MergeDataBases.Destination.SamplingViewModel.GetLatestSampling(DestinationAOI).DateTimeSampled;
                dtaqv.FirstRefNo = MergeDataBases.Destination.SamplingViewModel.GetEarliestSampling(DestinationAOI).ReferenceNumber.ToString();
                dtaqv.LastRefNo = MergeDataBases.Destination.SamplingViewModel.GetLatestSampling(DestinationAOI).ReferenceNumber.ToString();
                dtaqv.SerialNumberMin = MergeDataBases.Destination.SamplingViewModel.SerialNumberMinima(DestinationAOI);
                dtaqv.SerialNumerMax = MergeDataBases.Destination.SamplingViewModel.SerialNumberMaxima(DestinationAOI);
            }


            lvDestination.Columns.Add("Property");
            lvDestination.Columns.Add("Value");
            var lvi = lvDestination.Items.Add("Name");
            lvi.SubItems.Add(dtaqv.Name);
            lvi = lvDestination.Items.Add("Number of landing sites");
            lvi.SubItems.Add(dtaqv.NumberLandingSites.ToString());
            lvi = lvDestination.Items.Add("Number of samplings");
            lvi.SubItems.Add(dtaqv.NumberSamplings.ToString("N0"));

            if (dtaqv.NumberSamplings > 0)
            {
                lvi = lvDestination.Items.Add("First sampling date");
                lvi.SubItems.Add(dtaqv.FirstSamplingDate.ToString("MMM-dd-yyyy"));
                lvi = lvDestination.Items.Add("First Ref#");
                lvi.SubItems.Add(dtaqv.FirstRefNo);
                lvi = lvDestination.Items.Add("Last sampling date");
                lvi.SubItems.Add(dtaqv.LastSamplingDate.ToString("MMM-dd-yyyy"));
                lvi = lvDestination.Items.Add("Last Ref#");
                lvi.SubItems.Add(dtaqv.LastRefNo);
                lvi = lvDestination.Items.Add("Serial number range");
                lvi.SubItems.Add($"{dtaqv.SerialNumberMin} - {dtaqv.SerialNumerMax}");
                lvi = lvDestination.Items.Add("Potential conflict with ref numbers");
                lvi.SubItems.Add(HasConflictWithRefNos()?"Yes":"No");
            }

            SizeColumns(lvDestination, false);
        }

        private bool HasConflictWithRefNos()
        {
            int sourceSamplingCount = MergeDataBases.Source.SamplingViewModel.SamplingCountByAOI(SourceAOI);
            int destinationSamplingCount = MergeDataBases.Destination.SamplingViewModel.SamplingCountByAOI(DestinationAOI);
            if (sourceSamplingCount > 0 || destinationSamplingCount > 0)
            {
                if (sourceSamplingCount > 0 && destinationSamplingCount > 0)
                {
                    var sourceSerialNumberRange = MergeDataBases.Source.SamplingViewModel.SerialNumberRange(SourceAOI);
                    var destinationSerialNumberRange = MergeDataBases.Destination.SamplingViewModel.SerialNumberRange(DestinationAOI);

                    var sourceRange = new RangeObject
                    {
                        MinDate = MergeDataBases.Source.SamplingViewModel.GetEarliestSampling(SourceAOI).DateTimeSampled,
                        MaxDate = MergeDataBases.Source.SamplingViewModel.GetLatestSampling(SourceAOI).DateTimeSampled,
                        MinNumber = sourceSerialNumberRange.Min,
                        MaxNumber = sourceSerialNumberRange.Max
                    };

                    var destinationRange = new RangeObject
                    {
                        MinDate = MergeDataBases.Destination.SamplingViewModel.GetEarliestSampling(DestinationAOI).DateTimeSampled,
                        MaxDate = MergeDataBases.Destination.SamplingViewModel.GetLatestSampling(DestinationAOI).DateTimeSampled,
                        MinNumber = destinationSerialNumberRange.Min,
                        MaxNumber = destinationSerialNumberRange.Max
                    };

                    bool hasConflict= sourceRange.IsOverlapping(destinationRange, true) && sourceRange.IsOverlapping(destinationRange, false) && (SourceAOI.Code == DestinationAOI.Code);
                    buttonViewConflict.Enabled = hasConflict;
                    return hasConflict;
                }
                else
                {
                    return false;
                }


            }
            else
            {
                return false;
            }

        }


        

        
        private void SizeColumns(ListView lv, bool init = true)
        {
            try
            {
                foreach (ColumnHeader c in lv.Columns)
                {
                    if (init)
                    {
                        c.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                        c.Tag = c.Width;
                    }
                    else
                    {
                        c.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        if (c.Tag != null)
                        {
                            c.Width = c.Width > (int)c.Tag ? c.Width : (int)c.Tag;
                        }
                    }
                }
            }
            catch { lv.Visible = true; }

        }
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            global.SaveFormSettings(this);
            _instance = null;
        }


        private async void OnButtonClick(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "buttonResultsGraph":
                    var gsnf = MergeGraphForm.GetInstance(SourceAOI, DestinationAOI);
                    if (gsnf.Visible)
                    {
                        gsnf.BringToFront();
                    }
                    else
                    {
                        gsnf.Show(this);
                    }
                    gsnf.ShowCounts();
                    break;
                case "buttonMerge":
                    if (checkProceed.Checked)
                    {

                        MergeDataBases.SourceAOI = SourceAOI;
                        MergeDataBases.DestinationAOI = DestinationAOI;
                        _mergeDBHelper = new MergeDBHelper(26);
                        _mergeDBHelper.OnMergeDBTable += OnMergeDBTable;
                        _mergeDBHelper.OnMergeDBTableDone += OnMergeDBTableDone;
                        if ( await MergeDataBases.Merge(_mergeDBHelper, true))
                        {
                            MessageBox.Show("Finished merging target area", "Merging database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    break;
                case "buttonCancel":
                    Close();
                    break;
                case "buttonViewConflict":
                     gsnf = MergeGraphForm.GetInstance(SourceAOI,DestinationAOI);
                    if(gsnf.Visible)
                    {
                        gsnf.BringToFront();
                    }
                    else
                    {
                        gsnf.Show(this);
                    }
                    gsnf.ShowRefSerialNumbers();
                    break;
            }
        }

        private void OnMergeDBTableDone(object sender, MergeDBEventArgs e)
        {
            if (tsProgressBar.GetCurrentParent().InvokeRequired)
            {

                tsProgressBar.GetCurrentParent().Invoke(new MethodInvoker(delegate
                {
                    tsProgressBar.Maximum = e.TableCount;
                    tsProgressBar.Value = 0;
                    tsLabel.Text = $"Finished merging {e.TableCount} tables";

                }));

            }
        }

        private void OnMergeDBTable(object sender, MergeDBEventArgs e)
        {
            if (tsProgressBar.GetCurrentParent().InvokeRequired)
            {

                tsProgressBar.GetCurrentParent().Invoke(new MethodInvoker(delegate
                {
                    tsProgressBar.Maximum = e.TableCount;
                    tsProgressBar.Value = e.RunningCount;
                    tsLabel.Text = $"Finished merging table {e.RunningCount} of {e.TableCount}: {e.CurrentTableRead}";

                }));

            }
        }

        private void OnCheckBoxChanged(object sender, EventArgs e)
        {
            switch (((CheckBox)sender).Name)
            {
                case "checkProceed":
                    buttonMerge.Enabled = checkProceed.Checked;
                    break;
            }
        }
    }
}
