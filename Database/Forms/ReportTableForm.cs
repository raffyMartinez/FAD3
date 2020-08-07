using FAD3.Database.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Text;
using FAD3.Database.Classes.merge;
using System.Threading.Tasks;

namespace FAD3.Database.Forms
{
    public partial class ReportTableForm : Form
    {
        private static ReportTableForm _instance;

        public TargetArea TargetArea { get; set; }
        public string Topic { get; set; }
        public List<int> Years { get; set; }
        public string TopicDescription { get; set; }
        private DataSet _dataSet;


        public bool Showreport()
        {
            var samplingGUID = "";
            lvTable.Visible = false;
            ReportGeneratorClass.TargetArea = TargetArea;
            ReportGeneratorClass.Topic = Topic;
            ReportGeneratorClass.Years = Years;
            ReportGeneratorClass.Generate();
            _dataSet = ReportGeneratorClass.DataSet;
            lvTable.Columns.Clear();
            var ch = lvTable.Columns.Add("Row");
            foreach (DataColumn col in _dataSet.Tables[0].Columns)
            {
                ch = lvTable.Columns.Add(col.ColumnName);
                switch (col.DataType.Name)
                {
                    case "Double":
                    case "Int32":
                    case "DateTime":
                    case "Decimal":
                        ch.TextAlign = HorizontalAlignment.Right;
                        break;
                }
            }
            lvTable.Columns.Add("");
            SizeColumns(lvTable);

            bool done = false;
            double weightCatch = 0;
            double? weightSample = null;
            double weightSpecies = 0;
            bool fromTotal = false;
            int? countSpecies = null;
            double? subSampleWeight = null;
            int? subSampleCount = null;
            foreach (DataRow dr in _dataSet.Tables[0].Rows)
            {
                var colName = "";
                ListViewItem lvi = new ListViewItem();
                for (int n = 0; n < _dataSet.Tables[0].Columns.Count; n++)
                {
                    if (n == 0)
                    {
                        lvi = lvTable.Items.Add((lvTable.Items.Count + 1).ToString());
                    }

                    switch (dr[n].GetType().Name)
                    {
                        case "FishingVessel":
                            lvi.SubItems.Add (((Classes.merge.FishingVessel)dr[n]).ToString());
                            break;
                        case "DateTime":
                            DateTime dt = DateTime.Parse(dr[n].ToString());
                            lvi.SubItems.Add(string.Format("{0:MMM-dd-yyyy HH:mm}", dt));
                            break;
                        case "Double":
                            lvi.SubItems.Add(((double)dr[n]).ToString("N2"));
                            break;

                        case "Decimal":
                            lvi.SubItems.Add(((Decimal)dr[n]).ToString("N2"));
                            break;

                        default:
                            switch(_dataSet.Tables[0].Columns[n].ColumnName)
                            {
                                case "RowID":
                                    samplingGUID = dr[n].ToString();
                                    lvi.SubItems.Add(samplingGUID);
                                    break;
                                case "FishingGround":
                                    lvi.SubItems.Add ( $"{dr[n].ToString()}, {new AdditionalFishingGroundsMerged(samplingGUID, MergeDataBases.Destination).MergedFishingGrounds}".Trim(new Char[] { ' ',','}));
                                    break;
                                default:
                                    lvi.SubItems.Add(dr[n].ToString());
                                    break;
                            }
                            break;
                    }

                    colName = lvTable.Columns[n].Text;

                    switch (Topic)
                    {
                        case "catch":
                            if (colName == "fromTotal1")
                            {
                                countSpecies = null;
                                subSampleCount = null;
                                subSampleWeight = null;
                                weightSample = null;
                                weightCatch = (double)dr["catchTotalWt"];
                                if (double.TryParse(dr["catchSampleWt"].ToString(), out double wtSample))
                                {
                                    weightSample = wtSample;
                                }
                                weightSpecies = (double)dr["catchWt"];
                                fromTotal = (bool)dr["fromTotal"];

                                if (int.TryParse(dr["catchCt"].ToString(), out int count))
                                {
                                    countSpecies = count;
                                }
                                if (countSpecies == null)
                                {
                                    subSampleWeight = (double)dr["catchSubSampleWt"];
                                    subSampleCount = (int)dr["catchSubSampleCt"];
                                }
                                done = true;
                            }
                            if (done)
                            {
                                if (fromTotal)
                                {
                                    lvi.SubItems[n + 1].Text = weightSpecies.ToString("N2");
                                    if (countSpecies == null)
                                    {
                                        if (subSampleWeight > 0 && subSampleCount>0)
                                        { 
                                            lvi.SubItems.Add(((int)((weightSpecies / subSampleWeight) * subSampleCount)).ToString());
                                        }
                                        else
                                        {
                                            lvi.SubItems.Add("");
                                        }
                                    }
                                    else
                                    {
                                        lvi.SubItems.Add(countSpecies.ToString());
                                    }
                                }
                                else if (countSpecies == null)
                                {
                                    lvi.SubItems[n + 1].Text = weightSpecies.ToString("N2");
                                    lvi.SubItems.Add
                                        (
                                        subSampleWeight>0 ? 
                                        ((int)((weightSpecies / subSampleWeight) * subSampleCount)).ToString() : ""
                                        );


                                }
                                else
                                {
                                    if (weightSample != null)
                                    {
                                        lvi.SubItems[n + 1].Text = (weightSpecies * (double)(weightCatch / weightSample)).ToString("N2");
                                        lvi.SubItems.Add(((int)(countSpecies * (weightCatch / weightSample))).ToString());
                                    }
                                    else
                                    {
                                        lvi.SubItems[n + 1].Text = "";
                                        lvi.SubItems.Add("");
                                    }
                                }
                                done = false;
                            }
                            break;
                    }
                }
            }

            switch(Topic)
            {
                case "catch":
                    //lvTable.Columns.RemoveAt(lvTable.Columns.Count-1);
                    break;
            }
            SizeColumns(lvTable, false);
            lvTable.Visible = true;
            return true;
        }

        public static ReportTableForm GetInstance(TargetArea targetArea, string topic, List<int> years)
        {
            if (_instance == null) return new ReportTableForm(targetArea, topic, years);
            return _instance;
        }

        public ReportTableForm(TargetArea targetArea, string topic, List<int> years)
        {
            InitializeComponent();
            TargetArea = targetArea;
            Topic = topic;
            Years = years;
        }
        public  async Task<bool> Report()
        {
            return await Task.Run(() => Showreport());
        }
        private async void OnFormLoad(object sender, EventArgs e)
        {
            global.LoadFormSettings(this);
            lvTable.View = View.Details;
            lvTable.FullRowSelect = true;
            Text = TopicDescription;
            //await Report();
            //Showreport();
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
                    if (c.Tag != null)
                    {
                        c.Width = c.Width > (int)c.Tag ? c.Width : (int)c.Tag;
                    }
                }
            }
        }

        private void ReportTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instance = null;
            global.SaveFormSettings(this);
        }

        private void CopyText()
        {
            StringBuilder copyText = new StringBuilder();
            string col = "";
            foreach (ColumnHeader c in lvTable.Columns)
            {
                col += $"{c.Text}\t";
            }
            copyText.Append($"{col.TrimEnd()}\r\n");
            foreach (ListViewItem item in lvTable.Items)
            {
                copyText.Append(item.Text);
                for (int n = 1; n < item.SubItems.Count; n++)
                {
                    copyText.Append($"\t{item.SubItems[n]?.Text}");
                }
                copyText.Append("\r\n");
            }
            Clipboard.SetText(copyText.ToString());
        }

        private void OnMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "menuitemCopyText":
                    CopyText();
                    break;

                case "menuItemClose":
                    Close();
                    break;
            }
        }

        private void OnToolbarItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "btnExportToExcel":
                    break;

                case "btnCopyText":
                    CopyText();
                    break;

                case "btnClose":
                    Close();
                    break;
            }
        }
    }
}