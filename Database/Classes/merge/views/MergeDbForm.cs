using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using FAD3.Database.Classes.merge;
using FAD3.GUI.Forms;
namespace FAD3.Database.Classes.merge.views
{
    public partial class MergeDbForm : Form
    {
        private static MergeDbForm _instance;
        private MergeDBHelper _mergeDBHelper;
        public MergeDbForm()
        {
            InitializeComponent();
            Load += OnFormLoad;
            FormClosed += OnFormClosed;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            global.SaveFormSettings(this);
            _instance = null;
            Load -= OnFormLoad;
            FormClosed -= OnFormClosed;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            global.LoadFormSettings(this);
            Text = "Merge fish catch monitoring target areas";
            listViewAOIs.Columns.Clear();
            listViewAOIs.FullRowSelect = true;
            listViewAOIs.View = View.Details;
            listViewAOIs.CheckBoxes = true;
            listViewAOIs.Columns.Add("Target area");
            listViewAOIs.Columns.Add("# of landing sites");
            listViewAOIs.Columns.Add("# of sampling");
            listViewAOIs.Columns.Add("Date of first sampling");
            listViewAOIs.Columns.Add("Ref# of first sampling");
            listViewAOIs.Columns.Add("Date of last sampling");
            listViewAOIs.Columns.Add("Ref# of last sampling");
            listViewAOIs.Columns.Add("Ref# range");
            SizeColumns(listViewAOIs);
            lblSelectAOI.Text = $"Select target areas to merge to {global.mainForm.TargetArea.TargetAreaName}";

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
        public static MergeDbForm GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MergeDbForm();
            }
            return _instance;
        }
        private bool SelectFileToMerge()
        {
            bool success = false;
            openFileDialog.Title = "Select database file to merge";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Filter = "Access database file (*.mdb)|*.mdb|Other files(*.*)|*.*";
            openFileDialog.FileName = "";
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr != DialogResult.Cancel)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    if (openFileDialog.FileName != global.MDBPath)
                    {
                        MergeDataBases.OtherDatabaseFileName = openFileDialog.FileName;
                        success = true;
                    }
                    else
                    {
                        MessageBox.Show("Selected database must be different from the current database", "Merging database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        success = false;
                    }
                }
            }
            else
            {
                success = false;
            }
            return success;
        }


        private async void OnButtonClick(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "buttonSelect":
                    if (SelectFileToMerge())
                    {
                        _mergeDBHelper = new MergeDBHelper(52);
                        _mergeDBHelper.OnMergeDBTable += OnReadingTableToMerge;
                        _mergeDBHelper.OnMergeDBTableDone += OnReadingMergeDone;
                        await MergeDataBases.Setup(_mergeDBHelper);
                        listViewAOIs.Visible = false;
                        listViewAOIs.Items.Clear();
                        int counter = 0;
                        try
                        {
                            
                            foreach (AOI aoi in MergeDataBases.Source.AOIViewModel.AOICollection)
                            {
                                var lvi = listViewAOIs.Items.Add(aoi.AOIName);
                                lvi.Name = aoi.AOIGuid;
                                lvi.SubItems.Add(MergeDataBases.Source.LandingSiteViewModel.CountByAOI(aoi).ToString("N0"));
                                lvi.SubItems.Add(MergeDataBases.Source.SamplingViewModel.CountByAOI(aoi).ToString("N0"));
                                if (MergeDataBases.Source.SamplingViewModel.Count > 0)
                                {
                                    var samplingFirst = MergeDataBases.Source.SamplingViewModel.GetEarliestSampling(aoi);
                                    var samplingLast = MergeDataBases.Source.SamplingViewModel.GetLatestSampling(aoi);
                                    if (samplingFirst != null)
                                    {
                                        lvi.SubItems.Add(samplingFirst.DateTimeSampled.ToString("MMM-dd-yyyy"));
                                        lvi.SubItems.Add(samplingFirst.ReferenceNumber.ToString());
                                        lvi.SubItems.Add(samplingLast.DateTimeSampled.ToString("MMM-dd-yyyy"));
                                        lvi.SubItems.Add(samplingLast.ReferenceNumber.ToString());
                                        lvi.SubItems.Add($"{MergeDataBases.Source.SamplingViewModel.SerialNumberMinima(aoi).ToString()} - {MergeDataBases.Source.SamplingViewModel.SerialNumberMaxima(aoi).ToString("N0")}");
                                    }
                                }
                                counter++;
                            }
                        }
                        catch(Exception ex)
                        {
                            Logger.Log(ex);
                        }
                        SizeColumns(listViewAOIs,false);
                        listViewAOIs.Visible = true;
                        if (counter == 0)
                        {
                            MessageBox.Show("There are no target areas in this database", "Merging database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    break;
                case "buttonCancel":
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;
                case "buttonMerge":
                    if (global.mainForm.TreeLevel == "target_area" && global.mainForm.TargetArea!=null)
                    {
                        if (listViewAOIs.CheckedItems.Count == 1)
                        {
                            var amf = ActualMergingForm.GetInstance(MergeDataBases.Source.AOIViewModel.GetAOI(listViewAOIs.CheckedItems[0].Name));
                            if (amf.Visible)
                            {
                                amf.BringToFront();
                            }
                            else
                            {
                                amf.Show(this);
                            }
                        }
                        else
                        {
                            if (listViewAOIs.Items.Count > 0)
                            {
                                MessageBox.Show("Please check one target area from the list", "Merging database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Please select a database file for merging", "Merging database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a target area", "Merging database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void OnReadingMergeDone(object sender, MergeDBEventArgs e)
        {
            if (ProgressBarMergeDB.GetCurrentParent().InvokeRequired)
            {

                ProgressBarMergeDB.GetCurrentParent().Invoke(new MethodInvoker(delegate
                {
                    ProgressBarMergeDB.Maximum = e.TableCount;
                    ProgressBarMergeDB.Value = 0;
                    StatusLabelMerge.Text = $"Finished reading tables for merging";

                }));

            }
        }

        private void OnReadingTableToMerge(object sender, MergeDBEventArgs e)
        {
            if (ProgressBarMergeDB.GetCurrentParent().InvokeRequired)
            {

                ProgressBarMergeDB.GetCurrentParent().Invoke(new MethodInvoker(delegate
                {
                    ProgressBarMergeDB.Maximum = e.TableCount;
                    ProgressBarMergeDB.Value = e.RunningCount;
                    StatusLabelMerge.Text = $"Reading table to merge {e.RunningCount} of {e.TableCount}: {e.Location}\\{e.CurrentTableRead}";
                    
                }));

            }
        }
    }
}
