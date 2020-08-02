using ClosedXML.Excel;
using FAD3.Database.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using FAD3.Database.Classes.gearinventory;
namespace FAD3.Database.Forms
{
    public partial class GearInventoryTabularForm : Form
    {
        private DateTime _startOpen;
        private TraceListener _listener;
        private Dictionary<string, string> _columnDataType = new Dictionary<string, string>();
        private static GearInventoryTabularForm _instance;
        private string _inventoryGuid;
        private FishingGearInventory _inventory;
        private Dictionary<string, (string month, string type)> _monthsFishing = new Dictionary<string, (string month, string type)>();

        private bool _isExportingInventory = false;
        private bool _isGettingInventoryFromdb = false;
        private InventoryReadHelper _readHelper;
        private InventoryViewModel _inventoryViewModel;

        public bool ShowProjectColumn { get; set; }
        public string InventoryProjectName { get; set; }

        public static GearInventoryTabularForm GetInstance(FishingGearInventory inventory, string inventoryGuid)
        {
            if (_instance == null) return new GearInventoryTabularForm(inventory, inventoryGuid);
            return _instance;
        }

        public GearInventoryTabularForm(FishingGearInventory inventory, string inventoryGuid)
        {
            _startOpen = DateTime.Now;
            Logger.Log($"Constructing {this.ToString()}");
            InitializeComponent();
            _inventory = inventory;
            _inventoryGuid = inventoryGuid;
            ShowProjectColumn = true;
            _listener = new DelimitedListTraceListener($@"{Application.StartupPath}\fad.log");
            Debug.Listeners.Add(_listener);
            Debug.AutoFlush = true;
        }

        private void OnNodeAfterSelect(object sender, TreeViewEventArgs e)
        {
            tsLabel.Text = "";
            if (_isExportingInventory)
            {
                tsLabel.Text = $"Exporting {e.Node.Text}";
                statusStrip1.Refresh();
            }
            switch (e.Node.Name)
            {
                case "nodeProject":
                    ShowProject();
                    break;

                case "nodeFisherVessel":
                    FillHeaderRowsEx(showVesselCounts:true);
                    break;

                case "nodeGear":
                    ShowGearLocalNames();
                    break;

                case "nodeGearCount":
                    ShowGearCountsEx();
                    break;

                case "nodeGearOperation":
                    ShowGearDaysInUseEx();
                    break;

                case "nodeMonths":
                    ShowMonthsFishingEx();
                    break;

                case "nodePeak":
                    ShowPeakMonthsFishingEx();
                    break;

                case "nodeCPUE":
                    //ShowCPUE();
                    ShowGearCPUE();
                    break;

                case "nodeGearCPUEHistory":
                    ShowCPUEHistory();
                    break;

                case "nodeCatchComp":
                    ShowCatchCompositionEx();
                    break;

                case "nodeAccessories":
                    ShowFishingAccessoriesEx();
                    break;

                case "nodeExpenses":
                    ShowExpenses();
                    break;

                case "nodeNotes":
                    ShowNotesEx();
                    break;

                case "nodeRespondents":
                    FillHeaderRowsEx(showRespondents:true);
                    break;
            }

        }

        private void ShowCPUEHistory()
        {
            FillHeaderRows(showCPUETrend: true);
            SizeColumns(listResults, false);
        }

        private void ShowNotesEx()
        {
            FillHeaderRows(showNotes: true);
            SizeColumns(listResults, false);
        }

        private void ShowFishingAccessoriesEx()
        {
            FillHeaderRows(showFishingAccessories: true);
            SizeColumns(listResults, false);
        }

        private void ShowCatchCompositionEx()
        {
            FillHeaderRows(showCatchComposition: true);
            SizeColumns(listResults, false);
        }


        private void ShowExpenses()
        {
            FillHeaderRows(showExpenses: true);
            SizeColumns(listResults, false);
        }

        private void AddColumnDataType(ColumnHeader col, string type)
        {
            if (!_columnDataType.ContainsKey(col.Text))
            {
                _columnDataType.Add(col.Text, type)
;
            }
        }

        private void FillHeaderRowsEx(bool showVesselCounts=false, bool showRespondents=false)
        {
            var col = new ColumnHeader();
            listResults.Visible = false;
            listResults.Clear();
            if (ShowProjectColumn)
            {
                listResults.Columns.Add("Project");
            }
            listResults.Columns.Add("Province");
            listResults.Columns.Add("LGU");
            listResults.Columns.Add("Barangay");
            listResults.Columns.Add("Sitio");
            listResults.Columns.Add("Enumerator");
            col = listResults.Columns.Add("Date surveyed");
            AddColumnDataType(col, "date");

            if(showVesselCounts)
            {
                col = listResults.Columns.Add("Number of fishers");
                AddColumnDataType(col, "int");
                col = listResults.Columns.Add("Number of commercial vessels");
                AddColumnDataType(col, "int");
                col = listResults.Columns.Add("Number of municipal motorized vessels");
                AddColumnDataType(col, "int");
                col = listResults.Columns.Add("Number of municipal non motorized vessels");
                AddColumnDataType(col, "int");

                
            }

            if(showRespondents)
            {
                listResults.Columns.Add("Respondent");
            }

            SizeColumns(listResults);

            tsProgressBar.Maximum = _inventoryViewModel.InventoryCollection.Count;

            int counter = 0;
            foreach(var item in InventoryEntities.InventoryViewModel.InventoryCollection)
            {
                
                tsProgressBar.Value = ++counter;

                var lvi = listResults.Items.Add(_inventoryViewModel.InventoryProject.Name);
                lvi.SubItems.Add(item.Location.Province);
                lvi.SubItems.Add(item.Location.Municipality);
                lvi.SubItems.Add(item.Location.Barangay);
                var sitio = item.Location.Sitio ;
                if (sitio.Length > 0)
                {
                    lvi.SubItems.Add(sitio);
                }
                else
                {
                    lvi.SubItems.Add("Entire barangay");
                }
                lvi.SubItems.Add(item.Enumerator);
                lvi.SubItems.Add(string.Format("{0:MMM-dd-yyyy}", item.DateEnumerated));

                if (showVesselCounts)
                {
                    lvi.SubItems.Add(item.NumberOfFishers.ToString());
                    lvi.SubItems.Add(item.NumberCommercial.ToString());
                    lvi.SubItems.Add(item.NumberMunicipalMotorized.ToString());
                    lvi.SubItems.Add(item.NumberMunicipalNonMotorized.ToString());
                }

                if(showRespondents)
                {
                    int rowCount = 0;
                    foreach (var responder in (item.Respondents))
                    {
                        if (rowCount == 0)
                        {
                            lvi.SubItems.Add(responder);
                        }
                        else
                        {
                            for (int n = 0; n < 7; n++)
                            {
                                if (n == 0)
                                {
                                    lvi = listResults.Items.Add("");
                                }
                                else
                                {
                                    lvi.SubItems.Add("");
                                }
                            }
                            lvi.SubItems.Add(responder);
                        }
                        rowCount++;
                    }
                }
            }
            SizeColumns(listResults, false);
            tsProgressBar.Value = 0;
        }

        private void FillHeaderRows(bool showExpenses = false, bool showCPUETrend = false, bool showCPUE = false,
            bool showCounts = false, bool showMonthsFishing = false, bool showPeakFishingMonths = false, bool showDaysInsUse = false,
            bool showCatchComposition = false, bool showFishingAccessories = false, bool showNotes = false)
        {
            var col = new ColumnHeader();
            listResults.Visible = false;
            listResults.Clear();
            if (ShowProjectColumn)
            {
                listResults.Columns.Add("Project");
            }
            listResults.Columns.Add("Province");
            listResults.Columns.Add("LGU");
            listResults.Columns.Add("Barangay");
            listResults.Columns.Add("Sitio");
            listResults.Columns.Add("Enumerator");
            col = listResults.Columns.Add("Date surveyed");
            AddColumnDataType(col, "date");


            listResults.Columns.Add("Gear class");
            listResults.Columns.Add("Gear variation");
            listResults.Columns.Add("Local names");



            if (showExpenses)
            {
                listResults.Columns.Add("Expense item");
                col = listResults.Columns.Add("Cost");
                AddColumnDataType(col, "double");
                listResults.Columns.Add("Source");
                listResults.Columns.Add("Notes");

            }

            if (showCPUETrend)
            {
                listResults.Columns.Add("Decade");
                listResults.Columns.Add("Year");
                col = listResults.Columns.Add("CPUE");
                AddColumnDataType(col, "int");
                listResults.Columns.Add("Unit");
                listResults.Columns.Add("Notes");

            }

            if (showCPUE)
            {
                listResults.Columns.Add("Maximum CPUE");
                listResults.Columns.Add("Minimum CPUE");
                listResults.Columns.Add("Average CPUE");
                listResults.Columns.Add("Upper CPUE mode");
                listResults.Columns.Add("Lower CPUE mode");
                listResults.Columns.Add("CPUE mode");
                listResults.Columns.Add("Unit");
                listResults.Columns.Add("Equivalent kg");
            }

            if (showCounts)
            {
                listResults.Columns.Add("Count in commercial vessels");
                listResults.Columns.Add("Count in motorized municipal vessels");
                listResults.Columns.Add("Count in non-motorized municipal vessels");
                listResults.Columns.Add("Count in no vessels");
                listResults.Columns.Add("Total number of gears");
            }

            if (showMonthsFishing || showPeakFishingMonths)
            {
                listResults.Columns.Add("Jan");
                listResults.Columns.Add("Feb");
                listResults.Columns.Add("Mar");
                listResults.Columns.Add("Apr");
                listResults.Columns.Add("May");
                listResults.Columns.Add("Jun");
                listResults.Columns.Add("Jul");
                listResults.Columns.Add("Aug");
                listResults.Columns.Add("Sep");
                listResults.Columns.Add("Oct");
                listResults.Columns.Add("Nov");
                listResults.Columns.Add("Dec");
            }

            if (showDaysInsUse)
            {
                listResults.Columns.Add("Number of days used per month");
            }

            if (showCatchComposition)
            {
                listResults.Columns.Add("Composition of dominant catch");
                listResults.Columns.Add("Composition of non-dominant catch");
                listResults.Columns.Add("Percentage of dominance");
            }

            if (showFishingAccessories)
            {
                listResults.Columns.Add("Accessories used in fishing");
            }

            if (showNotes)
            {
                listResults.Columns.Add("Notes");
            }

            SizeColumns(listResults);

            ListViewItem lvi = new ListViewItem();
            tsProgressBar.Maximum = _inventoryViewModel.InventoryCollection.Count;
            

            int counter = 0;
            foreach (var item in _inventoryViewModel.InventoryCollection)
            {
                tsProgressBar.Value = ++counter;

                foreach (var gear in item.GearInventories)
                {
                    
                    if (ShowProjectColumn)
                    {
                        lvi = listResults.Items.Add(gear.BrgyGearInventoryGuid, _inventoryViewModel.InventoryProject.Name, null);
                        lvi.SubItems.Add(item.Location.Province);
                    }
                    else
                    {
                        lvi = listResults.Items.Add(gear.BrgyGearInventoryGuid, item.Location.Province, null);
                    }
                    lvi.SubItems.Add(item.Location.Municipality);
                    lvi.SubItems.Add(item.Location.Barangay);
                    var sitio = item.Location.Sitio;
                    if (sitio.Length > 0)
                    {
                        lvi.SubItems.Add(sitio);
                    }
                    else
                    {
                        lvi.SubItems.Add("Entire barangay");
                    }
                    lvi.SubItems.Add(item.Enumerator);
                    lvi.SubItems.Add(string.Format("{0:MMM-dd-yyyy}", item.DateEnumerated));


                    lvi.SubItems.Add(gear.GearClass);
                    lvi.SubItems.Add(gear.GearName);
                    lvi.SubItems.Add(gear.GearLocalNames);


                    if (showExpenses)
                    {
                        var costLine = 0;
                        foreach (var expense in gear.Expenses)
                        {
                            if (costLine > 0)
                            {
                                for (int n = 1; n < 11; n++)
                                {
                                    if (n == 1)
                                    {
                                        lvi = listResults.Items.Add("");
                                    }
                                    else
                                    {
                                        lvi.SubItems.Add("");
                                    }
                                }
                            }
                            lvi.SubItems.Add(expense.ExpenseItem);
                            lvi.SubItems.Add(expense.Cost.ToString());
                            lvi.SubItems.Add(expense.Source);
                            lvi.SubItems.Add(expense.Notes);
                            costLine++;
                        }
                    }

                    if (showCPUETrend)
                    {
                        var cpueLine = 0;
                        foreach(var cpue in gear.CPUEHistories)
                        {
                            if(cpueLine>0)
                            {
                                for (int n = 1; n < 11; n++)
                                {
                                    if (n == 1)
                                    {
                                        lvi = listResults.Items.Add("");
                                    }
                                    else
                                    {
                                        lvi.SubItems.Add("");
                                    }
                                }
                            }

                            var sDecade = cpue.Decade.ToString();
                            if (sDecade.Length > 0)
                            {
                                sDecade += "s";
                            }
                            lvi.SubItems.Add(sDecade);
                            lvi.SubItems.Add(cpue.HistoryYear.ToString());
                            lvi.SubItems.Add(cpue.CPUE.ToString());
                            lvi.SubItems.Add(cpue.CPUEUnit);
                            lvi.SubItems.Add(cpue.Notes);

                            cpueLine++;
                        }
                    }

                    if (showCPUE)
                    {
                        lvi.SubItems.Add(gear.MaxCPUE.ToString());
                        lvi.SubItems.Add(gear.MinCPUE.ToString());
                        lvi.SubItems.Add(gear.AverageCPUE.ToString());
                        lvi.SubItems.Add(gear.ModeUpper.ToString());
                        lvi.SubItems.Add(gear.ModeLower.ToString());
                        lvi.SubItems.Add(gear.Mode.ToString());
                        lvi.SubItems.Add(gear.CPUEUnit.ToString());
                        lvi.SubItems.Add(gear.EquivalentKg.ToString());

                    }

                    if (showCounts)
                    {
                        lvi.SubItems.Add(gear.CountCommercial.ToString());
                        lvi.SubItems.Add(gear.CountMunicipalMotorized.ToString());
                        lvi.SubItems.Add(gear.CountMunicipalNonMotorized.ToString());
                        lvi.SubItems.Add(gear.CountNoBoat.ToString());
                        lvi.SubItems.Add(gear.CountTotal.ToString());
                    }

                    if (showMonthsFishing || showPeakFishingMonths)
                    {

                        if (showMonthsFishing)
                        {
                            foreach (var m in gear.ArrFishingMonths)
                            {
                                lvi.SubItems.Add(m);
                            }
                        }
                        else
                        {
                            foreach (var m in gear.ArrFishingMonthsPeak)
                            {
                                lvi.SubItems.Add(m);
                            }
                        }
                    }





                    if (showDaysInsUse)
                    {
                        lvi.SubItems.Add(gear.NumberDaysUsedPerMonth.ToString());
                    }

                    if (showCatchComposition)
                    {
                        lvi.SubItems.Add(gear.CatchCompositionDominant);
                        lvi.SubItems.Add(gear.CatchComposition);
                        lvi.SubItems.Add(gear.DominantCatchPercent.ToString());

                    }

                    if (showFishingAccessories)
                    {
                        lvi.SubItems.Add(gear.GearAccessories);
                    }

                    if (showNotes)
                    {
                        lvi.SubItems.Add(gear.Notes);
                    }
                }
            }
            tsProgressBar.Value = 0;
        }




        private void OnRecordRead(object sender, InventoryReadEventArg e)
        {

            if (tsProgressBar.GetCurrentParent().InvokeRequired)
            {

                tsProgressBar.GetCurrentParent().Invoke(new MethodInvoker(delegate
                {
                    tsProgressBar.Maximum = e.Records;
                    tsProgressBar.Value = e.CurrentRecord;
                    if (_isGettingInventoryFromdb)
                    {
                        tsLabel.Text = $"Retrieving inventory record {e.CurrentRecord} of {e.Records} from {e.CurrentLocation}";
                    }
                }));

            }

        }

        private async void OnFormLoad(object sender, EventArgs e)
        {
            _isGettingInventoryFromdb = true;
            global.LoadFormSettings(this);
            treeInventory.SelectedNode = treeInventory.Nodes["treeInventory"];
            await GetInventoryViewModel();
            ShowProject();
            Text = "Inventory of fishers, fishing vessels and gears tables";

        }

        public async Task GetInventoryViewModel()
        {
            _readHelper = new InventoryReadHelper();
            _readHelper.OnInventoryRecordRead += OnRecordRead;
            _inventoryViewModel = await Task.Run(() => new InventoryViewModel(_inventoryGuid, _readHelper));
            tsLabel.Text = "Finished retrieving inventory records!";
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
            global.SaveFormSettings(this);
            foreach (TraceListener listener in Debug.Listeners)
            {
                listener.Flush();
                listener.Close();
            }
            _listener = null;
        }

        /// <summary>
        /// Sizes all columns so that it fits the widest column content or the column header content
        /// </summary>
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
                lv.Visible = !init;
            }
            catch { lv.Visible = true; }

        }

        private void ShowProject()
        {
            Debug.WriteLine($"{GetType().Name} {MethodBase.GetCurrentMethod().Name} {DateTime.Now.ToString()}");
            var col = new ColumnHeader();
            listResults.Clear();
            listResults.Columns.Add("Project name");
            col = listResults.Columns.Add("Date implemented");
            AddColumnDataType(col, "date");
            SizeColumns(listResults);
            var result = _inventory.GetInventory(_inventoryGuid);
            var lvi = listResults.Items.Add(result.inventoryName);
            lvi.SubItems.Add(string.Format("{0:MMM-dd-yyyy}", result.dateImplemented));
            SizeColumns(listResults, false);
        }

        private void ShowMonthsFishingEx()
        {
            FillHeaderRows(showMonthsFishing: true);
            SizeColumns(listResults, false);
        }

        private void ShowPeakMonthsFishingEx()
        {
            FillHeaderRows(showPeakFishingMonths: true);
            SizeColumns(listResults, false);
        }
        private void ShowGearCountsEx()
        {
            FillHeaderRows(showCounts: true);
            SizeColumns(listResults, false);
        }

        private void ShowGearCPUE()
        {
            Debug.WriteLine($"{GetType().Name} {MethodBase.GetCurrentMethod().Name} {DateTime.Now.ToString()}");
            FillHeaderRows(showCPUE: true);
            SizeColumns(listResults, false);
        }
        private void ShowGearLocalNames()
        {
            Debug.WriteLine($"{GetType().Name} {MethodBase.GetCurrentMethod().Name} {DateTime.Now.ToString()}");
            FillHeaderRows();
            SizeColumns(listResults, false);
        }


        private void ShowGearDaysInUseEx()
        {
            FillHeaderRows(showDaysInsUse: true);
            SizeColumns(listResults, false);
        }





        private DataTable ListViewToDataTable(ListView lv, string tableName)
        {
            Debug.WriteLine($"{GetType().Name} {MethodBase.GetCurrentMethod().Name} {DateTime.Now.ToString()}");
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            foreach (ColumnHeader ch in lv.Columns)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = ch.Text;
                if (_columnDataType.ContainsKey(col.ColumnName))
                {
                    switch (_columnDataType[col.ColumnName])
                    {
                        case "date":
                            col.DataType = typeof(DateTime);
                            break;

                        case "int":
                            col.DataType = typeof(int);
                            break;

                        case "double":
                            col.DataType = typeof(double);
                            break;
                    }
                }
                else
                {
                    col.DataType = typeof(string);
                }
                dt.Columns.Add(col);
            }

            foreach (ListViewItem lvi in lv.Items)
            {
                var col = 0;
                DataRow row = dt.NewRow();
                foreach (var subItem in lvi.SubItems)
                {
                    if (lvi.SubItems[col].Text.Length > 0)
                    {
                        row[lv.Columns[col].Text] = lvi.SubItems[col].Text;
                    }
                    col++;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        private void PrintNodesRecursive(TreeNode nd, XLWorkbook wb)
        {
            TreeViewEventArgs e = new TreeViewEventArgs(nd);
            OnNodeAfterSelect(null, e);
            try
            {
                var wks = wb.Worksheets.Add(ListViewToDataTable(listResults, nd.Text));
                wks.Name = nd.Text;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "GearInventoryTabularForm", "PrintNodesRecursive");
            }
            foreach (TreeNode subNode in nd.Nodes)
            {
                PrintNodesRecursive(subNode, wb);
            }
        }

        private void LogExport(string logText)
        {
            string filepath = Application.StartupPath + "\\export.log";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine($"{logText} | Date:{DateTime.Now.ToString()}");
            }
        }
        private void ExportInventoryXL(string fileName)
        {
            _isGettingInventoryFromdb = false;
            _isExportingInventory = true;
            try
            {
                var wb = new XLWorkbook();
                LogExport($"file: {global.MDBPath}");
                foreach (TreeNode nd in treeInventory.Nodes)
                {
                    PrintNodesRecursive(nd, wb);

                }
                wb.SaveAs(fileName);
                treeInventory.SelectedNode = treeInventory.Nodes["nodeProject"];
                ShowProject();
                MessageBox.Show($"Inventory data successfully saved to {fileName}");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "GearInventoryTabularForm", "ExportInventoryXL");
            }
        }

        private void ExportInventoryXML(string fileName)
        {
            ;
        }

        private void OnToolBarItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "tsbExport":
                    FileDialogHelper.Title = "Provide filename for exporting fisher, vessel and fishing gear inventory";
                    FileDialogHelper.DialogType = FileDialogType.FileSave;
                    FileDialogHelper.DataFileType = DataFileType.Text | DataFileType.XML | DataFileType.CSV | DataFileType.Excel;
                    FileDialogHelper.FileName = InventoryProjectName;
                    FileDialogHelper.ShowDialog();

                    var fileName = FileDialogHelper.FileName;

                    if (fileName.Length > 0)
                    {
                        switch (Path.GetExtension(fileName))
                        {
                            case ".txt":

                                break;

                            case ".XML":
                            case ".xml":
                                ExportInventoryXML(fileName);
                                break;

                            case ".xlsx":
                                ExportInventoryXL(fileName);
                                break;
                        }
                    }
                    break;

                case "tsbClose":
                    Close();
                    break;
            }
        }

        private void OnListMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Items.Clear();
                var tsi = contextMenu.Items.Add("Copy text");
                tsi.Name = "menuCopyText";

                contextMenu.Show(Cursor.Position);
            }
        }

        private void OnMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "menuCopyText":

                    StringBuilder copyText = new StringBuilder();
                    string col = "";
                    foreach (ColumnHeader c in listResults.Columns)
                    {
                        col += $"{c.Text}\t";
                    }
                    copyText.Append($"{col.TrimEnd()}\r\n");
                    foreach (ListViewItem item in listResults.Items)
                    {
                        copyText.Append(item.Text);
                        for (int n = 1; n < item.SubItems.Count; n++)
                        {
                            copyText.Append($"\t{item.SubItems[n]?.Text}");
                        }
                        copyText.Append("\r\n");
                    }
                    Clipboard.SetText(copyText.ToString());

                    break;
            }
        }
    }
}