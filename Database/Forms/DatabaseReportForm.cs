using FAD3.Database.Classes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FAD3.Database.Classes.merge;
namespace FAD3.Database.Forms
{
    public partial class DatabaseReportForm : Form
    {
        private MergeDBHelper _mergeDBHelper;
        private static DatabaseReportForm _instance;
        private string _treeLevel;
        private TargetArea _targetArea;
        private Dictionary<string, string> _sampledYears;
        private string _topic;
        private string _topicDescription;

        public TargetArea TargetArea
        {
            get { return _targetArea; }
            set
            {
                _targetArea = value;
                ShowSampledYears();
                Text = $"Reports: {_targetArea.TargetAreaName}";
                ReportGeneratorClass.TargetArea = TargetArea;
            }
        }

        public static DatabaseReportForm GetInstance()
        {
            if (_instance != null) return _instance;
            return null;
        }

        public static DatabaseReportForm GetInstance(string treeLevel, TargetArea targetArea)
        {
            if (_instance == null) _instance = new DatabaseReportForm(treeLevel, targetArea);
            return _instance;
        }

        public DatabaseReportForm(string treeLevel, TargetArea targetArea)
        {
            InitializeComponent();
            _treeLevel = treeLevel;
            _targetArea = targetArea;
            _mergeDBHelper = new MergeDBHelper(26);
            _mergeDBHelper.OnMergeDBTable += OnMergeDBTable;
            _mergeDBHelper.OnMergeDBTableDone += OnMergeTableDone;
            MergeDataBases.SetUpForReporting(_mergeDBHelper);
            tsLabel.Text = "";
        }

        private void OnMergeTableDone(object sender, MergeDBEventArgs e)
        {
            if (tsProgressBar.GetCurrentParent().InvokeRequired)
            {

                tsProgressBar.GetCurrentParent().Invoke(new MethodInvoker(delegate
                {
                    tsProgressBar.Maximum = e.TableCount;
                    tsProgressBar.Value = 0;
                    tsLabel.Text = $"Finished loading {_mergeDBHelper.TotalTables} tables";

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
                    tsLabel.Text = $"Loading table {e.RunningCount} of {e.TableCount}: {e.CurrentTableRead}";

                }));

            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            global.LoadFormSettings(this);
            var ndRoot = tvTopics.Nodes.Add("root", "Topics");
            ndRoot.Nodes.Add("nodeEffort", "Effort");
            ndRoot.Nodes.Add("nodeCatch", "Catch composition");
            //ndRoot.Nodes.Add("nodeGearSpecs", "Specifications of fishing gears");
            Text = $"Reports: {_targetArea.TargetAreaName}";

            lvYears.View = View.Details;
            lvYears.Columns.Add("Year");
            lvYears.Columns.Add("Samples");
            lvYears.FullRowSelect = true;

            lvReports.View = View.Details;
            lvReports.Columns.Add("Row");
            lvReports.Columns.Add("Report title");
            lvReports.FullRowSelect = true;

            SizeColumns(lvYears);
            SizeColumns(lvReports);
            ShowSampledYears();
            ReportGeneratorClass.TargetArea = TargetArea;
        }

        /// <summary>
        /// Sizes all columns so that it fits the widest column content or the column header content
        /// </summary>
        private void SizeColumns(ListView lv, bool init = true)
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
                    c.Width = c.Width > (int)c.Tag ? c.Width : (int)c.Tag;
                }
            }
        }

        private void ShowSampledYears()
        {
            lvYears.Items.Clear();
            _sampledYears = _targetArea.ListYearsWithSamplingCount();
            if (_sampledYears.Count > 0)
            {
                foreach (var item in _sampledYears)
                {
                    var lvi = lvYears.Items.Add(item.Key, $"{item.Key}", null);
                    lvi.SubItems.Add(item.Value);
                }
                SizeColumns(lvYears, false);
            }
            else
            {
                SizeColumns(lvYears);
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            _instance = null;
            global.SaveFormSettings(this);
        }

        private async void OnToolbarItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "tsbClose":
                    Close();
                    break;

                case "tsbExcel":
                    if (lvReports.SelectedItems.Count > 0
                        && TargetArea != null
                        && Years().Count>0)
                    {
                        string fn = "";
                        string defaultFN = lvReports.SelectedItems[0].SubItems[1].Text;
                        if (GetExcelFile(defaultFN, out fn))
                        {
                            string topic = lvReports.SelectedItems[0].Name;
                            ReportGeneratorClass.TargetArea = TargetArea;
                            ReportGeneratorClass.Topic = topic;
                            ReportGeneratorClass.Years = Years();
                            string sheetName="";
                            switch(topic)
                            {
                                case "len_freq":
                                    sheetName = "Length frequency";
                                    break;
                                case "effort":
                                    sheetName = "Fishing effort";
                                    break;
                                case "fishing_expense_items":
                                    sheetName = "Expense items";
                                    break;
                                case "fishing_expense":
                                    sheetName = "Cost of fishing";
                                    break;
                                case "gear_specs":
                                    sheetName = "Fishing gear specs";
                                    break;
                                case "catch":
                                    sheetName = "Catch composition";
                                    break;

                            }
                            tsProgressBar.Style = ProgressBarStyle.Marquee;
                            tsProgressBar.MarqueeAnimationSpeed = 100;
                            tsLabel.Text = $"Generating report for {sheetName}";
                            await ReportGeneratorClass.GenerateAsync();
                            tsLabel.Text = $"Finished generating report for {sheetName}";
                            tsProgressBar.Style = ProgressBarStyle.Continuous;
                            tsProgressBar.Value = 0;

                            if (ReportGeneratorClass.ExportToExcel(fn, sheetName))
                            {
                                MessageBox.Show("Finished exporting data to MS Excel","Export to Excel",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Exporting to Excel was not successful", "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show
                            (
                                "Please select a target area, topic and one or more years",
                                "More information required for exporting",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                    }
                    break;
            }
        }

        private bool GetExcelFile(string defaultFilename,  out string fileName, bool toExcel=true)
        {
            fileName = "";
            FileDialogHelper.Title = "Provide filename for exporting fisher, vessel and fishing gear inventory";
            FileDialogHelper.DialogType = FileDialogType.FileSave;
            FileDialogHelper.DataFileType = DataFileType.Text | DataFileType.XML | DataFileType.CSV | DataFileType.Excel;
            if(toExcel)
            {
                FileDialogHelper.FilterIndex = 3;
            }
            FileDialogHelper.FileName = fileName;
            FileDialogHelper.ShowDialog();

            fileName = FileDialogHelper.FileName;
            return fileName.Length > 0;
        }
        private void OnTreeNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            lvReports.Items.Clear();
            switch (e.Node.Name)
            {
                case "nodeEffort":
                    int row = lvReports.Items.Count + 1;
                    var lvi = lvReports.Items.Add("effort", row.ToString(), null);
                    _topicDescription = $"Effort data of {TargetArea.TargetAreaName} target area.";
                    lvi.SubItems.Add(_topicDescription);
                    _topic = "effort";

                    row = lvReports.Items.Count + 1;
                    lvi = lvReports.Items.Add("gear_specs", row.ToString(), null);
                    _topicDescription = $"Specifications of fishing gears in {TargetArea.TargetAreaName} target area.";
                    lvi.SubItems.Add(_topicDescription);
                    _topic = "gear_specs";

                    row = lvReports.Items.Count + 1;
                    lvi = lvReports.Items.Add("fishing_expense", row.ToString(), null);
                    _topicDescription = $"Cost of fishing operation in {TargetArea.TargetAreaName} target area.";
                    lvi.SubItems.Add(_topicDescription);
                    _topic = "fishing_expense";

                    row = lvReports.Items.Count + 1;
                    lvi = lvReports.Items.Add("fishing_expense_items", row.ToString(), null);
                    _topicDescription = $"Fishing expense items in {TargetArea.TargetAreaName} target area.";
                    lvi.SubItems.Add(_topicDescription);
                    _topic = "fishing_expense_items";
                    break;

                case "nodeCatch":
                    row = lvReports.Items.Count + 1;
                    lvi = lvReports.Items.Add("catch", row.ToString(), null);
                    _topicDescription = $"Catch composition data of {TargetArea.TargetAreaName} target area.";
                    lvi.SubItems.Add(_topicDescription);
                    _topic = "catch";

                    row = lvReports.Items.Count + 1;
                    lvi = lvReports.Items.Add("len_freq", row.ToString(), null);
                    _topicDescription = $"Length frequency data of {TargetArea.TargetAreaName} target area.";
                    lvi.SubItems.Add(_topicDescription);
                    _topic = "len_freq";
                    break;

            }
            if (lvReports.Items.Count > 0)
            {
                SizeColumns(lvReports, false);
            }
        }

        private List<int>Years()
        {
            List<int> years = new List<int>();
            foreach (ListViewItem lvi in lvYears.Items)
            {
                if (lvi.Checked)
                {
                    years.Add(int.Parse(lvi.Text));
                }
            }
            return years;
        }
        private void OnListDoubleClick(object sender, EventArgs e)
        {
            List<int> years = new List<int>();
            foreach (ListViewItem lvi in lvYears.Items)
            {
                if (lvi.Checked)
                {
                    years.Add(int.Parse(lvi.Text));
                }
            }
            if (years.Count > 0)
            {
                _topic = ((ListView)sender).SelectedItems[0].Name;
                ReportTableForm rtf = ReportTableForm.GetInstance(TargetArea, _topic, years);
                rtf.TopicDescription = ((ListView)sender).SelectedItems[0].SubItems[1].Text;
                if (rtf.Visible)
                {
                    rtf.BringToFront();
                }
                else
                {
                    rtf.Show(this);
                    //await rtf.Report();
                    rtf.Showreport();
                }
            }
            else
            {
                MessageBox.Show("Please select one or more years", "No year selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}