﻿using FAD3.Mapping.Classes;
using FAD3.Mapping.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using FAD3.Database.Classes;
using FAD3.GUI.Forms;
namespace FAD3.Database.Forms
{
    public partial class AllSpeciesForm : Form
    {
        private MainForm _parent;
        private Dictionary<string, string> _filters = new Dictionary<string, string>();
        private int _rowsImported;
        private static AllSpeciesForm _instance;
        private CatchLocalNamesForm _catchLocalNamesForm;
        private string _speciesName;

        public static AllSpeciesForm GetInstance(MainForm parent)
        {
            if (_instance == null) _instance = new AllSpeciesForm(parent);
            return _instance;
        }

        public MainForm parentForm
        {
            get { return _parent; }
        }

        public AllSpeciesForm(MainForm parent)
        {
            InitializeComponent();
            _parent = parent;
            Names.OnRowsImportedExported += OnNamesImportRows;
        }

        private void OnNamesImportRows(object sender, ImportRowsFromFileEventArgs e)
        {
            if (e.DataType == ExportImportDataType.SpeciesNames)
            {
                _rowsImported = e.RowsImported;
                if (e.IsComplete)
                {
                    lblListViewLabel.Invoke((MethodInvoker)delegate
                    {
                        lblListViewLabel.Text = $"Finished importing species names: {_rowsImported} names imported";
                    });
                }
                else
                {
                    lblListViewLabel.Invoke((MethodInvoker)delegate
                    {
                        lblListViewLabel.Text = $"Importing species names: {_rowsImported} names imported";
                    });
                }
            }
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

        private void OnForm_Load(object sender, EventArgs e)
        {
            global.LoadFormSettings(this, false);
            dropDownMenu.ItemClicked += OnDropDownMenuItemClicked;
            Text = "Database of species names of catch in the Philippines";
            lvTaxa.View = View.List;
            foreach (var item in CatchName.RetrieveTaxaDictionary())
            {
                lvTaxa.Items.Add(item.Key.ToString(), item.Value, null);
            }

            lvNames.With(o =>
                {
                    o.View = View.Details;
                    o.FullRowSelect = true;
                    o.Columns.Add("Row");
                    o.Columns.Add("Genus");
                    o.Columns.Add("Species");
                    o.Columns.Add("Taxa");
                    o.Columns.Add("In FishBase");
                    o.Columns.Add("Records");
                    o.Columns.Add("Notes");
                    o.HideSelection = false;
                });
            SizeColumns(lvNames);
            GetSpeciesNames();
        }

        private void OnDropDownMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            e.ClickedItem.Owner.Hide();
            switch (e.ClickedItem.Name)
            {
                case "menuViewSamplings":
                    var catchName = $"{lvNames.SelectedItems[0].SubItems[1].Text} {lvNames.SelectedItems[0].SubItems[2].Text}";
                    var gssf = GearSpeciesSamplingsForm.GetInstance(lvNames.SelectedItems[0].Name, catchName, this, OccurenceDataType.Species);
                    if (!gssf.Visible)
                    {
                        gssf.Show(this);
                    }
                    else
                    {
                        gssf.BringToFront();
                        gssf.setItemGuid_Name_Parent(lvNames.SelectedItems[0].Name, catchName, this);
                    }

                    break;

                case "menuMapOccurence":
                    var spName = lvNames.SelectedItems[0].SubItems[1].Text + " " + lvNames.SelectedItems[0].SubItems[2].Text;
                    OccurenceMapping mso = new OccurenceMapping(OccurenceDataType.Species, spName, global.MappingForm.MapControl)
                    {
                        MapLayersHandler = global.MappingForm.MapLayersHandler,
                        MapInteractionHandler = global.MappingForm.MapInterActionHandler
                    };
                    mso.RequestOccurenceInfo += OnRequestOccurenceInfo;
                    mso.MapOccurence();
                    break;

                case "menuAddNewName":
                    SpeciesNameForm snf = new SpeciesNameForm(this);
                    snf.ShowDialog(this);
                    break;

                case "menuEditName":
                    ShowNameDetail();
                    break;

                case "menuLocalNames":
                    ShowLocalNames(lvNames.SelectedItems[0].Name);
                    break;
            }
        }

        public void CatchhLocalNamesFormClosed()
        {
            _catchLocalNamesForm = null;
        }

        private void ShowLocalNames(string speciesGuid)
        {
            CatchLocalNamesForm clnf = CatchLocalNamesForm.GetInstance(speciesGuid, this);
            if (clnf.Visible)
            {
                clnf.BringToFront();
            }
            else
            {
                clnf.SpeciesName = _speciesName;
                clnf.Show(this);
                _catchLocalNamesForm = clnf;
            }
        }

        private void OnRequestOccurenceInfo(object sender, OccurenceMapEventArgs e)
        {
            var nameToMap = lvNames.SelectedItems[0].SubItems[1].Text + " " + lvNames.SelectedItems[0].SubItems[2].Text;
            var speciesGuid = lvNames.SelectedItems[0].Name;
            using (OccurenceMappingForm omf = new OccurenceMappingForm(e.OccurenceDataType, this))
            {
                omf.SpeciesToMap(nameToMap, speciesGuid);
                omf.ShowDialog(this);
                if (omf.DialogResult == DialogResult.OK)
                {
                    e.Aggregate = omf.Aggregate;
                    e.ExcludeOne = omf.ExcludeOne;
                    e.MapInSelectedTargetArea = omf.MapInSelectedTargetArea;
                    e.SelectedTargetAreaGuid = omf.SelectedTargetAreaGuid;
                    e.SamplingYears = omf.SamplingYears;
                    e.ItemToMapGuid = speciesGuid;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private async void GetSpeciesNames(Dictionary<string, string> filters = null, bool OnlyWithRecords = false)
        {
            lvNames.Visible = false;
            lvNames.Items.Clear();
            await FillListNamesAsync(filters, OnlyWithRecords);
            SizeColumns(lvNames, false);
            lvNames.Visible = true;
        }

        private Task<int> FillListNamesAsync(Dictionary<string, string> filters = null, bool OnlyWithRecords = false)
        {
            return Task.Run(() => FillListNames(filters, OnlyWithRecords));
        }

        private int FillListNames(Dictionary<string, string> filters = null, bool OnlyWithRecords = false)
        {
            //lvNames.Visible = false;
            //lvNames.Items.Clear();
            int n = 1;
            foreach (var item in Names.RetrieveScientificNames(filters, OnlyWithRecords))
            {
                var inFishBase = item.Value.inFishBase ? "x" : "";
                var recordCount = item.Value.catchCompositionRecordCount == 0 ? "" : item.Value.catchCompositionRecordCount.ToString();
                var lvi = new ListViewItem(new string[] { n.ToString(), item.Value.genus, item.Value.species, item.Value.taxaName, inFishBase, recordCount, item.Value.Notes });
                lvi.Name = item.Value.catchNameGuid;
                //AddItem(lvi);
                lvNames.Invoke((MethodInvoker)delegate
                {
                    //btnOk.Enabled = _enableOK;
                    lvNames.Items.Add(lvi);
                });
                n++;
            }
            //SizeColumns(lvNames, false);
            //lvNames.Visible = true;
            return n;
        }

        private delegate void AddItemCallback(object o);

        private void AddItem(object o)
        {
            if (lvNames.InvokeRequired)
            {
                AddItemCallback d = new AddItemCallback(AddItem);
                this.Invoke(d, new object[] { o });
            }
            else
            {
                lvNames.Items.Add(o as ListViewItem);
            }
        }

        private void OnButtonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnButton_Click(object sender, EventArgs e)
        {
            var o = (Button)sender;
            switch (o.Name)
            {
                case "buttonApply":

                    //process taxa select
                    var selectedTaxa = "";
                    foreach (ListViewItem item in lvTaxa.CheckedItems)
                    {
                        var taxaNo = int.Parse(item.Name);
                        selectedTaxa += $"{taxaNo.ToString()},";
                    }
                    selectedTaxa = selectedTaxa.Trim(',');

                    if (selectedTaxa.Length > 0)
                    {
                        if (_filters.Keys.Contains<string>("taxa"))
                            _filters["taxa"] = $" TaxaNo in ({selectedTaxa}) ";
                        else
                            _filters.Add("taxa", $" TaxaNo like ({selectedTaxa}) ");
                    }
                    else
                    {
                        _filters.Remove("taxa");
                    }

                    //process search textbox
                    if (txtSearch.Text.Length > 0)
                    {
                        if (_filters.Keys.Contains<string>("search"))
                            _filters["search"] = $" Genus ALIKE '{txtSearch.Text}%' ";
                        else
                            _filters.Add("search", $" Genus ALIKE '{txtSearch.Text}%' ");
                    }
                    else
                    {
                        _filters.Remove("search");
                    }
                    GetSpeciesNames(_filters, chkShowWithRecords.Checked);

                    break;

                case "buttonReset":
                    chkShowWithRecords.Checked = false;
                    GetSpeciesNames();
                    txtSearch.Text = "";
                    break;

                case "buttonSearch":

                    break;
            }
        }

        private void ShowNameDetail()
        {
            if (lvNames.SelectedItems.Count > 0)
            {
                lvNames.SelectedItems[0].With(o =>
                {
                    var genus = o.SubItems[1].Text;
                    var species = o.SubItems[2].Text;
                    var taxaName = o.SubItems[3].Text;
                    var nameGuid = o.Name;

                    using (SpeciesNameForm snf = new SpeciesNameForm(genus, species, nameGuid, taxaName, this))
                    {
                        snf.ShowDialog(this);
                        if (snf.DialogResult == DialogResult.OK)
                        {
                            if (snf.DeleteSuccess)
                            {
                                lvNames.Items.Remove(lvNames.SelectedItems[0]);
                            }
                            else
                            {
                                var lvi = lvNames.SelectedItems[0];
                                lvi.SubItems[1].Text = snf.Genus;
                                lvi.SubItems[2].Text = snf.Species;
                                lvi.SubItems[3].Text = snf.TaxaName;
                                if (snf.InFishBase)
                                {
                                    lvi.SubItems[4].Text = "x";
                                }
                                else
                                {
                                    lvi.SubItems[4].Text = "";
                                }
                                lvi.SubItems[6].Text = snf.Notes;
                            }
                        }
                    }
                });
            }
        }

        private void OnlvNames_DoubleClick(object sender, EventArgs e)
        {
            ShowNameDetail();
        }

        private void OnlvNames_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
            }
            else if (e.Button == MouseButtons.Right && lvNames.SelectedItems.Count > 0)
            {
                dropDownMenu.Items.Clear();
                var item = dropDownMenu.Items.Add("View samplings");
                item.Name = "menuViewSamplings";

                item = dropDownMenu.Items.Add("Map species occurence");
                item.Name = "menuMapOccurence";
                item.Enabled = global.MapIsOpen && lvNames.SelectedItems[0].SubItems[5].Text.Length > 0;

                item = dropDownMenu.Items.Add("Add new catch name");
                item.Name = "menuAddNewName";

                item = dropDownMenu.Items.Add("Edit catch name");
                item.Name = "menuEditName";

                item = dropDownMenu.Items.Add("Local names");
                item.Name = "menuLocalNames";

                dropDownMenu.Items.Add("-");

                ToolStripMenuItem subMenu = new ToolStripMenuItem();
                subMenu.Text = "Browse on WWW";

                CatchNameURLGenerator.CatchName = _speciesName;
                var urls = CatchNameURLGenerator.URLS;

                foreach (var url in urls)
                {
                    ToolStripMenuItem subItem = new ToolStripMenuItem();
                    subItem.Text = url.Key;
                    subItem.Tag = url.Value;
                    subMenu.DropDownItems.Add(subItem);
                }

                subMenu.DropDownItemClicked += OnSubMenuDropDownClick;

                dropDownMenu.Items.Add(subMenu);
            }
        }

        private void OnSubMenuDropDownClick(object sender, ToolStripItemClickedEventArgs e)
        {
            e.ClickedItem.OwnerItem.Owner.Hide();
            Process.Start(e.ClickedItem.Tag.ToString());
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            global.SaveFormSettings(this);
            _instance = null;
        }

        private async void OnToolBarItemClick(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "tbClose":
                    Close();
                    break;

                case "tbAdd":
                    SpeciesNameForm snf = new SpeciesNameForm(this);
                    snf.ShowDialog(this);
                    break;

                case "tbRemove":
                    break;

                case "tbEdit":
                    ShowNameDetail();
                    break;

                case "tbExport":
                    var count = 0;
                    var taxaCSV = "";
                    if (lvTaxa.CheckedItems.Count > 0)
                    {
                        foreach (ListViewItem checkedItem in lvTaxa.CheckedItems)
                        {
                            taxaCSV += ((Taxa)int.Parse(checkedItem.Name)).ToString() + ",";
                        }
                        taxaCSV.Trim(',');
                    }
                    using (ExportImportDialogForm edf = new ExportImportDialogForm(ExportImportDataType.SpeciesNames, ExportImportDeleteAction.ActionExport))
                    {
                        edf.TaxaCSV = taxaCSV;
                        edf.ShowDialog(this);
                        if (edf.DialogResult == DialogResult.OK)
                        {
                            ExportImportDataType result = edf.Selection;
                            if (result == ExportImportDataType.SpeciesNames)
                            {
                                FileDialogHelper.Title = "Provide filename for exported species name";
                                FileDialogHelper.DialogType = FileDialogType.FileSave;
                                FileDialogHelper.DataFileType = DataFileType.Text | DataFileType.XML | DataFileType.CSV;
                                FileDialogHelper.ShowDialog();
                                var fileName = FileDialogHelper.FileName;
                                if (fileName.Length > 0)
                                {
                                    switch (Path.GetExtension(fileName))
                                    {
                                        case ".xml":
                                        case ".XML":
                                            ProgessIndicatorForm pif = new ProgessIndicatorForm(url: "", fileName);
                                            pif.ExportImportDataType = ExportImportDataType.SpeciesNames;
                                            pif.ExportImportDeleteAction = ExportImportDeleteAction.ActionExport;
                                            pif.Show(this);
                                            int r = await Names.ExportSpeciesNamesAsync(fileName);
                                            break;

                                        case ".txt":
                                            break;

                                        case ".csv":
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    break;

                case "tbImport":
                    using (ExportImportDialogForm edf = new ExportImportDialogForm(ExportImportDataType.SpeciesNames, ExportImportDeleteAction.ActionImport))
                    {
                        edf.ShowDialog(this);
                        if (edf.DialogResult == DialogResult.OK)
                        {
                            ExportImportDataType result = edf.Selection;
                            if (result == ExportImportDataType.SpeciesNames)
                            {
                                FileDialogHelper.Title = "Provide filename for imported species name";
                                FileDialogHelper.DialogType = FileDialogType.FileOpen;
                                FileDialogHelper.DataFileType = DataFileType.Text | DataFileType.XML | DataFileType.CSV | DataFileType.HTML;
                                FileDialogHelper.ShowDialog();
                                var fileName = FileDialogHelper.FileName;
                                if (fileName.Length > 0)
                                {
                                    ProgessIndicatorForm pif;// = new ProgessIndicatorForm();
                                    switch (Path.GetExtension(fileName))
                                    {
                                        case ".htm":
                                        case ".html":
                                            using (HTMLTableSelectColumnsForm htmlColForm = new HTMLTableSelectColumnsForm(fileName, CatchNameDataType.CatchSpeciesName))
                                            {
                                                DialogResult dr = htmlColForm.ShowDialog(this);

                                                if (dr == DialogResult.OK)
                                                {
                                                    pif = new ProgessIndicatorForm(url: "", fileName);
                                                    pif.ExportImportDataType = ExportImportDataType.SpeciesNames;
                                                    pif.ExportImportDeleteAction = ExportImportDeleteAction.ActionImport;
                                                    pif.Show(this);
                                                    int r1 = await Names.ImportSpeciesNamesAsync(fileName, htmlColForm.SpeciesNameColumn);
                                                    lvNames.Visible = false;
                                                    lvNames.Items.Clear();
                                                    FillListNames();
                                                    SizeColumns(lvNames, false);
                                                    lvNames.Visible = true;
                                                    lblListViewLabel.Text = "List of species names";
                                                    //GetImportedRows(fileName, htmlColForm.SpeciesNameColumn);
                                                }
                                            }
                                            break;

                                        case ".xml":
                                            pif = new ProgessIndicatorForm(url: "", fileName);
                                            pif.ExportImportDataType = ExportImportDataType.SpeciesNames;
                                            pif.ExportImportDeleteAction = ExportImportDeleteAction.ActionImport;
                                            pif.Show(this);
                                            int r = await Names.ImportSpeciesNamesAsync(fileName, null);
                                            lvNames.Visible = false;
                                            lvNames.Items.Clear();
                                            FillListNames();
                                            SizeColumns(lvNames, false);
                                            lvNames.Visible = true;
                                            lblListViewLabel.Text = "List of species names";
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        private async void GetImportedRows(string fileName, int? speciesColumn)
        {
            int result = await Names.ImportSpeciesNamesAsync(fileName, speciesColumn);
            GetSpeciesNames();
            lblListViewLabel.Text = "List of species names";
            MessageBox.Show($"{_rowsImported} species names were saved to the database", "Import successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnlvNamesMouseClick(object sender, MouseEventArgs e)
        {
            _speciesName = $"{lvNames.SelectedItems[0].SubItems[1].Text} {lvNames.SelectedItems[0].SubItems[2].Text}";
            if (_catchLocalNamesForm != null)
            {
                _catchLocalNamesForm.SpeciesName = _speciesName;
                _catchLocalNamesForm.SpeciesGuid = lvNames.SelectedItems[0].Name;
            }
        }
    }
}