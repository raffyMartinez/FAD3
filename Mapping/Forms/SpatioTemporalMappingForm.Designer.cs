namespace FAD3.Mapping.Forms
{
    partial class SpatioTemporalMappingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpatioTemporalMappingForm));
            this.tabMap = new System.Windows.Forms.TabControl();
            this.tabStart = new System.Windows.Forms.TabPage();
            this.btnBinning = new System.Windows.Forms.Button();
            this.cboLastData = new System.Windows.Forms.ComboBox();
            this.cboFirstData = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.cboValue = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTemporal = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLatitude = new System.Windows.Forms.ComboBox();
            this.cboLongitude = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tabMetadata = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.txtMetadata = new System.Windows.Forms.TextBox();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.txtDatasetNumberOfTimePeriods = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDatasetMin = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtDatasetMax = new System.Windows.Forms.TextBox();
            this.lblMax = new System.Windows.Forms.Label();
            this.txtDatasetNumberValues = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDatasetUniqueCount = new System.Windows.Forms.TextBox();
            this.txtInlandPoints = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRows = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabCategorize = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.cboClassificationScheme = new System.Windows.Forms.ComboBox();
            this.groupBoxSummary = new System.Windows.Forms.GroupBox();
            this.txtSelectedNumberOfPeriods = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtSelectedUnique = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSelectedMaximum = new System.Windows.Forms.TextBox();
            this.txtSelectedMinimum = new System.Windows.Forms.TextBox();
            this.txtSelectedValuesCount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnShowGridPoints = new System.Windows.Forms.Button();
            this.btnShowGridPolygons = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnColorScheme = new System.Windows.Forms.Button();
            this.dgCategories = new System.Windows.Forms.DataGridView();
            this.dgcolCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcolCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCategorize = new System.Windows.Forms.Button();
            this.txtCategoryCount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.icbColorScheme = new FAD3.Mapping.UserControls.ImageCombo();
            this.tabMapping = new System.Windows.Forms.TabPage();
            this.chkViewTimeSeriesChart = new System.Windows.Forms.CheckBox();
            this.lblParameter = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.graphSheet = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnDown = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnUp = new System.Windows.Forms.Button();
            this.dgSheetSummary = new System.Windows.Forms.DataGridView();
            this.colVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMappedSheet = new System.Windows.Forms.Label();
            this.listSelectedTimePeriods = new System.Windows.Forms.ListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabMap.SuspendLayout();
            this.tabStart.SuspendLayout();
            this.tabMetadata.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tabCategorize.SuspendLayout();
            this.groupBoxSummary.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCategories)).BeginInit();
            this.tabMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSheetSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMap
            // 
            this.tabMap.Controls.Add(this.tabStart);
            this.tabMap.Controls.Add(this.tabMetadata);
            this.tabMap.Controls.Add(this.tabSummary);
            this.tabMap.Controls.Add(this.tabCategorize);
            this.tabMap.Controls.Add(this.tabMapping);
            this.tabMap.Location = new System.Drawing.Point(3, 47);
            this.tabMap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabMap.Name = "tabMap";
            this.tabMap.SelectedIndex = 0;
            this.tabMap.Size = new System.Drawing.Size(659, 601);
            this.tabMap.TabIndex = 0;
            this.tabMap.SelectedIndexChanged += new System.EventHandler(this.OnTabMapIndexChanged);
            // 
            // tabStart
            // 
            this.tabStart.Controls.Add(this.btnBinning);
            this.tabStart.Controls.Add(this.cboLastData);
            this.tabStart.Controls.Add(this.cboFirstData);
            this.tabStart.Controls.Add(this.label6);
            this.tabStart.Controls.Add(this.label7);
            this.tabStart.Controls.Add(this.btnReadFile);
            this.tabStart.Controls.Add(this.cboValue);
            this.tabStart.Controls.Add(this.label4);
            this.tabStart.Controls.Add(this.cboTemporal);
            this.tabStart.Controls.Add(this.label3);
            this.tabStart.Controls.Add(this.cboLatitude);
            this.tabStart.Controls.Add(this.cboLongitude);
            this.tabStart.Controls.Add(this.label2);
            this.tabStart.Controls.Add(this.label1);
            this.tabStart.Controls.Add(this.txtFile);
            this.tabStart.Controls.Add(this.btnOpen);
            this.tabStart.Location = new System.Drawing.Point(4, 25);
            this.tabStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabStart.Name = "tabStart";
            this.tabStart.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabStart.Size = new System.Drawing.Size(651, 572);
            this.tabStart.TabIndex = 0;
            this.tabStart.Text = "Start";
            this.tabStart.UseVisualStyleBackColor = true;
            // 
            // btnBinning
            // 
            this.btnBinning.Enabled = false;
            this.btnBinning.Location = new System.Drawing.Point(301, 438);
            this.btnBinning.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBinning.Name = "btnBinning";
            this.btnBinning.Size = new System.Drawing.Size(84, 31);
            this.btnBinning.TabIndex = 36;
            this.btnBinning.Text = "Binning";
            this.btnBinning.UseVisualStyleBackColor = true;
            this.btnBinning.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // cboLastData
            // 
            this.cboLastData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLastData.Enabled = false;
            this.cboLastData.FormattingEnabled = true;
            this.cboLastData.Location = new System.Drawing.Point(245, 393);
            this.cboLastData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboLastData.Name = "cboLastData";
            this.cboLastData.Size = new System.Drawing.Size(247, 24);
            this.cboLastData.TabIndex = 33;
            this.cboLastData.SelectedIndexChanged += new System.EventHandler(this.OnComboSelectionIndexChanged);
            // 
            // cboFirstData
            // 
            this.cboFirstData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFirstData.Enabled = false;
            this.cboFirstData.FormattingEnabled = true;
            this.cboFirstData.Location = new System.Drawing.Point(247, 348);
            this.cboFirstData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboFirstData.Name = "cboFirstData";
            this.cboFirstData.Size = new System.Drawing.Size(247, 24);
            this.cboFirstData.TabIndex = 32;
            this.cboFirstData.SelectedIndexChanged += new System.EventHandler(this.OnComboSelectionIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 396);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 17);
            this.label6.TabIndex = 29;
            this.label6.Text = "End time period";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 352);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "Start time period";
            // 
            // btnReadFile
            // 
            this.btnReadFile.Enabled = false;
            this.btnReadFile.Location = new System.Drawing.Point(301, 270);
            this.btnReadFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(84, 31);
            this.btnReadFile.TabIndex = 27;
            this.btnReadFile.Text = "Read file";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // cboValue
            // 
            this.cboValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboValue.Enabled = false;
            this.cboValue.FormattingEnabled = true;
            this.cboValue.Location = new System.Drawing.Point(245, 220);
            this.cboValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboValue.Name = "cboValue";
            this.cboValue.Size = new System.Drawing.Size(245, 24);
            this.cboValue.TabIndex = 26;
            this.cboValue.Tag = "dc";
            this.cboValue.SelectedIndexChanged += new System.EventHandler(this.OnComboSelectionIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 224);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 25;
            this.label4.Text = "Values column";
            // 
            // cboTemporal
            // 
            this.cboTemporal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTemporal.Enabled = false;
            this.cboTemporal.FormattingEnabled = true;
            this.cboTemporal.Location = new System.Drawing.Point(245, 171);
            this.cboTemporal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboTemporal.Name = "cboTemporal";
            this.cboTemporal.Size = new System.Drawing.Size(245, 24);
            this.cboTemporal.TabIndex = 24;
            this.cboTemporal.Tag = "dc";
            this.cboTemporal.SelectedIndexChanged += new System.EventHandler(this.OnComboSelectionIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 175);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Temporal column";
            // 
            // cboLatitude
            // 
            this.cboLatitude.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLatitude.Enabled = false;
            this.cboLatitude.FormattingEnabled = true;
            this.cboLatitude.Location = new System.Drawing.Point(245, 124);
            this.cboLatitude.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboLatitude.Name = "cboLatitude";
            this.cboLatitude.Size = new System.Drawing.Size(245, 24);
            this.cboLatitude.TabIndex = 22;
            this.cboLatitude.Tag = "dc";
            this.cboLatitude.SelectedIndexChanged += new System.EventHandler(this.OnComboSelectionIndexChanged);
            // 
            // cboLongitude
            // 
            this.cboLongitude.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLongitude.Enabled = false;
            this.cboLongitude.FormattingEnabled = true;
            this.cboLongitude.Location = new System.Drawing.Point(245, 79);
            this.cboLongitude.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboLongitude.Name = "cboLongitude";
            this.cboLongitude.Size = new System.Drawing.Size(245, 24);
            this.cboLongitude.TabIndex = 21;
            this.cboLongitude.Tag = "dc";
            this.cboLongitude.SelectedIndexChanged += new System.EventHandler(this.OnComboSelectionIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 128);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Latitude column";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Longitude column";
            // 
            // txtFile
            // 
            this.txtFile.BackColor = System.Drawing.SystemColors.Window;
            this.txtFile.Location = new System.Drawing.Point(88, 26);
            this.txtFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(477, 22);
            this.txtFile.TabIndex = 18;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(13, 22);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(67, 31);
            this.btnOpen.TabIndex = 17;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // tabMetadata
            // 
            this.tabMetadata.Controls.Add(this.label21);
            this.tabMetadata.Controls.Add(this.txtMetadata);
            this.tabMetadata.Location = new System.Drawing.Point(4, 25);
            this.tabMetadata.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabMetadata.Name = "tabMetadata";
            this.tabMetadata.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabMetadata.Size = new System.Drawing.Size(651, 572);
            this.tabMetadata.TabIndex = 5;
            this.tabMetadata.Text = "Metadata";
            this.tabMetadata.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 20);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(67, 17);
            this.label21.TabIndex = 1;
            this.label21.Text = "Metadata";
            // 
            // txtMetadata
            // 
            this.txtMetadata.Location = new System.Drawing.Point(8, 39);
            this.txtMetadata.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMetadata.Multiline = true;
            this.txtMetadata.Name = "txtMetadata";
            this.txtMetadata.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMetadata.Size = new System.Drawing.Size(588, 507);
            this.txtMetadata.TabIndex = 0;
            this.txtMetadata.Text = "Metadata is only available in NCCVS file type";
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.txtDatasetNumberOfTimePeriods);
            this.tabSummary.Controls.Add(this.label19);
            this.tabSummary.Controls.Add(this.txtDatasetMin);
            this.tabSummary.Controls.Add(this.label20);
            this.tabSummary.Controls.Add(this.txtDatasetMax);
            this.tabSummary.Controls.Add(this.lblMax);
            this.tabSummary.Controls.Add(this.txtDatasetNumberValues);
            this.tabSummary.Controls.Add(this.label17);
            this.tabSummary.Controls.Add(this.label16);
            this.tabSummary.Controls.Add(this.txtDatasetUniqueCount);
            this.tabSummary.Controls.Add(this.txtInlandPoints);
            this.tabSummary.Controls.Add(this.label14);
            this.tabSummary.Controls.Add(this.txtRows);
            this.tabSummary.Controls.Add(this.label5);
            this.tabSummary.Location = new System.Drawing.Point(4, 25);
            this.tabSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabSummary.Size = new System.Drawing.Size(651, 572);
            this.tabSummary.TabIndex = 6;
            this.tabSummary.Text = "Dataset Summary";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // txtDatasetNumberOfTimePeriods
            // 
            this.txtDatasetNumberOfTimePeriods.BackColor = System.Drawing.SystemColors.Window;
            this.txtDatasetNumberOfTimePeriods.Location = new System.Drawing.Point(261, 62);
            this.txtDatasetNumberOfTimePeriods.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDatasetNumberOfTimePeriods.Name = "txtDatasetNumberOfTimePeriods";
            this.txtDatasetNumberOfTimePeriods.ReadOnly = true;
            this.txtDatasetNumberOfTimePeriods.Size = new System.Drawing.Size(245, 22);
            this.txtDatasetNumberOfTimePeriods.TabIndex = 58;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(59, 65);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(155, 17);
            this.label19.TabIndex = 57;
            this.label19.Text = "Number of time periods";
            // 
            // txtDatasetMin
            // 
            this.txtDatasetMin.Location = new System.Drawing.Point(261, 409);
            this.txtDatasetMin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDatasetMin.Name = "txtDatasetMin";
            this.txtDatasetMin.Size = new System.Drawing.Size(245, 22);
            this.txtDatasetMin.TabIndex = 56;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(59, 412);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 17);
            this.label20.TabIndex = 55;
            this.label20.Text = "Minumum";
            // 
            // txtDatasetMax
            // 
            this.txtDatasetMax.Location = new System.Drawing.Point(261, 351);
            this.txtDatasetMax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDatasetMax.Name = "txtDatasetMax";
            this.txtDatasetMax.Size = new System.Drawing.Size(245, 22);
            this.txtDatasetMax.TabIndex = 54;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(59, 354);
            this.lblMax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(66, 17);
            this.lblMax.TabIndex = 53;
            this.lblMax.Text = "Maximum";
            // 
            // txtDatasetNumberValues
            // 
            this.txtDatasetNumberValues.Location = new System.Drawing.Point(261, 239);
            this.txtDatasetNumberValues.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDatasetNumberValues.Name = "txtDatasetNumberValues";
            this.txtDatasetNumberValues.Size = new System.Drawing.Size(245, 22);
            this.txtDatasetNumberValues.TabIndex = 52;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(59, 242);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 17);
            this.label17.TabIndex = 51;
            this.label17.Text = "Number of values";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(59, 300);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(166, 17);
            this.label16.TabIndex = 44;
            this.label16.Text = "Number of unique values";
            // 
            // txtDatasetUniqueCount
            // 
            this.txtDatasetUniqueCount.Location = new System.Drawing.Point(261, 297);
            this.txtDatasetUniqueCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDatasetUniqueCount.Name = "txtDatasetUniqueCount";
            this.txtDatasetUniqueCount.Size = new System.Drawing.Size(245, 22);
            this.txtDatasetUniqueCount.TabIndex = 43;
            // 
            // txtInlandPoints
            // 
            this.txtInlandPoints.BackColor = System.Drawing.SystemColors.Window;
            this.txtInlandPoints.Location = new System.Drawing.Point(261, 177);
            this.txtInlandPoints.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtInlandPoints.Name = "txtInlandPoints";
            this.txtInlandPoints.ReadOnly = true;
            this.txtInlandPoints.Size = new System.Drawing.Size(245, 22);
            this.txtInlandPoints.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(59, 181);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 17);
            this.label14.TabIndex = 41;
            this.label14.Text = "Inland points";
            // 
            // txtRows
            // 
            this.txtRows.BackColor = System.Drawing.SystemColors.Window;
            this.txtRows.Location = new System.Drawing.Point(261, 119);
            this.txtRows.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRows.Name = "txtRows";
            this.txtRows.ReadOnly = true;
            this.txtRows.Size = new System.Drawing.Size(245, 22);
            this.txtRows.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 123);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 17);
            this.label5.TabIndex = 39;
            this.label5.Text = "Number of point coordinates";
            // 
            // tabCategorize
            // 
            this.tabCategorize.Controls.Add(this.label8);
            this.tabCategorize.Controls.Add(this.cboClassificationScheme);
            this.tabCategorize.Controls.Add(this.groupBoxSummary);
            this.tabCategorize.Controls.Add(this.groupBox2);
            this.tabCategorize.Controls.Add(this.label13);
            this.tabCategorize.Controls.Add(this.btnColorScheme);
            this.tabCategorize.Controls.Add(this.dgCategories);
            this.tabCategorize.Controls.Add(this.btnCategorize);
            this.tabCategorize.Controls.Add(this.txtCategoryCount);
            this.tabCategorize.Controls.Add(this.label10);
            this.tabCategorize.Controls.Add(this.icbColorScheme);
            this.tabCategorize.Location = new System.Drawing.Point(4, 25);
            this.tabCategorize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabCategorize.Name = "tabCategorize";
            this.tabCategorize.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabCategorize.Size = new System.Drawing.Size(651, 572);
            this.tabCategorize.TabIndex = 1;
            this.tabCategorize.Text = "Categorize";
            this.tabCategorize.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 49);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 17);
            this.label8.TabIndex = 41;
            this.label8.Text = "Classification scheme";
            // 
            // cboClassificationScheme
            // 
            this.cboClassificationScheme.FormattingEnabled = true;
            this.cboClassificationScheme.Location = new System.Drawing.Point(155, 46);
            this.cboClassificationScheme.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboClassificationScheme.Name = "cboClassificationScheme";
            this.cboClassificationScheme.Size = new System.Drawing.Size(192, 24);
            this.cboClassificationScheme.TabIndex = 40;
            // 
            // groupBoxSummary
            // 
            this.groupBoxSummary.Controls.Add(this.txtSelectedNumberOfPeriods);
            this.groupBoxSummary.Controls.Add(this.label22);
            this.groupBoxSummary.Controls.Add(this.label18);
            this.groupBoxSummary.Controls.Add(this.txtSelectedUnique);
            this.groupBoxSummary.Controls.Add(this.label12);
            this.groupBoxSummary.Controls.Add(this.label11);
            this.groupBoxSummary.Controls.Add(this.txtSelectedMaximum);
            this.groupBoxSummary.Controls.Add(this.txtSelectedMinimum);
            this.groupBoxSummary.Controls.Add(this.txtSelectedValuesCount);
            this.groupBoxSummary.Controls.Add(this.label9);
            this.groupBoxSummary.Location = new System.Drawing.Point(8, 368);
            this.groupBoxSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxSummary.Name = "groupBoxSummary";
            this.groupBoxSummary.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxSummary.Size = new System.Drawing.Size(264, 192);
            this.groupBoxSummary.TabIndex = 39;
            this.groupBoxSummary.TabStop = false;
            this.groupBoxSummary.Text = "Summary of selected time periods";
            // 
            // txtSelectedNumberOfPeriods
            // 
            this.txtSelectedNumberOfPeriods.Location = new System.Drawing.Point(145, 31);
            this.txtSelectedNumberOfPeriods.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSelectedNumberOfPeriods.Name = "txtSelectedNumberOfPeriods";
            this.txtSelectedNumberOfPeriods.Size = new System.Drawing.Size(81, 22);
            this.txtSelectedNumberOfPeriods.TabIndex = 60;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(17, 34);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(125, 17);
            this.label22.TabIndex = 59;
            this.label22.Text = "Number of periods";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 164);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(98, 17);
            this.label18.TabIndex = 58;
            this.label18.Text = "Unique values";
            // 
            // txtSelectedUnique
            // 
            this.txtSelectedUnique.Location = new System.Drawing.Point(145, 160);
            this.txtSelectedUnique.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSelectedUnique.Name = "txtSelectedUnique";
            this.txtSelectedUnique.Size = new System.Drawing.Size(81, 22);
            this.txtSelectedUnique.TabIndex = 57;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 132);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 17);
            this.label12.TabIndex = 56;
            this.label12.Text = "Maximum";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 100);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 17);
            this.label11.TabIndex = 55;
            this.label11.Text = "Minimum";
            // 
            // txtSelectedMaximum
            // 
            this.txtSelectedMaximum.Location = new System.Drawing.Point(145, 128);
            this.txtSelectedMaximum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSelectedMaximum.Name = "txtSelectedMaximum";
            this.txtSelectedMaximum.Size = new System.Drawing.Size(81, 22);
            this.txtSelectedMaximum.TabIndex = 54;
            // 
            // txtSelectedMinimum
            // 
            this.txtSelectedMinimum.Location = new System.Drawing.Point(145, 96);
            this.txtSelectedMinimum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSelectedMinimum.Name = "txtSelectedMinimum";
            this.txtSelectedMinimum.Size = new System.Drawing.Size(81, 22);
            this.txtSelectedMinimum.TabIndex = 53;
            // 
            // txtSelectedValuesCount
            // 
            this.txtSelectedValuesCount.Location = new System.Drawing.Point(145, 64);
            this.txtSelectedValuesCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSelectedValuesCount.Name = "txtSelectedValuesCount";
            this.txtSelectedValuesCount.Size = new System.Drawing.Size(81, 22);
            this.txtSelectedValuesCount.TabIndex = 52;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 68);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 17);
            this.label9.TabIndex = 51;
            this.label9.Text = "Number of values";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnShowGridPoints);
            this.groupBox2.Controls.Add(this.btnShowGridPolygons);
            this.groupBox2.Location = new System.Drawing.Point(315, 369);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(267, 124);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Show on map";
            // 
            // btnShowGridPoints
            // 
            this.btnShowGridPoints.Location = new System.Drawing.Point(91, 25);
            this.btnShowGridPoints.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnShowGridPoints.Name = "btnShowGridPoints";
            this.btnShowGridPoints.Size = new System.Drawing.Size(97, 31);
            this.btnShowGridPoints.TabIndex = 13;
            this.btnShowGridPoints.Text = "Data points";
            this.btnShowGridPoints.UseVisualStyleBackColor = true;
            this.btnShowGridPoints.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnShowGridPolygons
            // 
            this.btnShowGridPolygons.Location = new System.Drawing.Point(91, 76);
            this.btnShowGridPolygons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnShowGridPolygons.Name = "btnShowGridPolygons";
            this.btnShowGridPolygons.Size = new System.Drawing.Size(97, 31);
            this.btnShowGridPolygons.TabIndex = 26;
            this.btnShowGridPolygons.Text = "Grid";
            this.btnShowGridPolygons.UseVisualStyleBackColor = true;
            this.btnShowGridPolygons.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 10);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 17);
            this.label13.TabIndex = 37;
            this.label13.Text = "Color schemes";
            // 
            // btnColorScheme
            // 
            this.btnColorScheme.Location = new System.Drawing.Point(469, 7);
            this.btnColorScheme.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnColorScheme.Name = "btnColorScheme";
            this.btnColorScheme.Size = new System.Drawing.Size(120, 31);
            this.btnColorScheme.TabIndex = 35;
            this.btnColorScheme.Text = "Choose colors";
            this.btnColorScheme.UseVisualStyleBackColor = true;
            this.btnColorScheme.Visible = false;
            // 
            // dgCategories
            // 
            this.dgCategories.AllowUserToAddRows = false;
            this.dgCategories.AllowUserToDeleteRows = false;
            this.dgCategories.AllowUserToResizeColumns = false;
            this.dgCategories.AllowUserToResizeRows = false;
            this.dgCategories.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCategories.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcolCategory,
            this.dgcolCount,
            this.dgCol});
            this.dgCategories.GridColor = System.Drawing.SystemColors.Window;
            this.dgCategories.Location = new System.Drawing.Point(7, 122);
            this.dgCategories.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgCategories.Name = "dgCategories";
            this.dgCategories.ReadOnly = true;
            this.dgCategories.RowHeadersVisible = false;
            this.dgCategories.RowHeadersWidth = 51;
            this.dgCategories.Size = new System.Drawing.Size(585, 239);
            this.dgCategories.TabIndex = 34;
            this.dgCategories.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellDblClick);
            // 
            // dgcolCategory
            // 
            this.dgcolCategory.HeaderText = "Category";
            this.dgcolCategory.MinimumWidth = 6;
            this.dgcolCategory.Name = "dgcolCategory";
            this.dgcolCategory.ReadOnly = true;
            this.dgcolCategory.Width = 125;
            // 
            // dgcolCount
            // 
            this.dgcolCount.HeaderText = "Count";
            this.dgcolCount.MinimumWidth = 6;
            this.dgcolCount.Name = "dgcolCount";
            this.dgcolCount.ReadOnly = true;
            this.dgcolCount.Width = 75;
            // 
            // dgCol
            // 
            this.dgCol.HeaderText = "Symbol";
            this.dgCol.MinimumWidth = 6;
            this.dgCol.Name = "dgCol";
            this.dgCol.ReadOnly = true;
            this.dgCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgCol.Width = 75;
            // 
            // btnCategorize
            // 
            this.btnCategorize.Location = new System.Drawing.Point(363, 80);
            this.btnCategorize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCategorize.Name = "btnCategorize";
            this.btnCategorize.Size = new System.Drawing.Size(120, 31);
            this.btnCategorize.TabIndex = 33;
            this.btnCategorize.Text = "Categorize";
            this.btnCategorize.UseVisualStyleBackColor = true;
            this.btnCategorize.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // txtCategoryCount
            // 
            this.txtCategoryCount.Location = new System.Drawing.Point(155, 84);
            this.txtCategoryCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCategoryCount.Name = "txtCategoryCount";
            this.txtCategoryCount.Size = new System.Drawing.Size(193, 22);
            this.txtCategoryCount.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 87);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 17);
            this.label10.TabIndex = 31;
            this.label10.Text = "Number of categories";
            // 
            // icbColorScheme
            // 
            this.icbColorScheme.Color1 = System.Drawing.Color.Gray;
            this.icbColorScheme.Color2 = System.Drawing.Color.Gray;
            this.icbColorScheme.ColorSchemes = null;
            this.icbColorScheme.ComboStyle = FAD3.Mapping.UserControls.ImageComboStyle.ColorSchemeGraduated;
            this.icbColorScheme.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.icbColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.icbColorScheme.FormattingEnabled = true;
            this.icbColorScheme.Location = new System.Drawing.Point(155, 7);
            this.icbColorScheme.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.icbColorScheme.Name = "icbColorScheme";
            this.icbColorScheme.OutlineColor = System.Drawing.Color.Black;
            this.icbColorScheme.Size = new System.Drawing.Size(193, 23);
            this.icbColorScheme.TabIndex = 36;
            // 
            // tabMapping
            // 
            this.tabMapping.Controls.Add(this.chkViewTimeSeriesChart);
            this.tabMapping.Controls.Add(this.lblParameter);
            this.tabMapping.Controls.Add(this.btnExport);
            this.tabMapping.Controls.Add(this.graphSheet);
            this.tabMapping.Controls.Add(this.btnDown);
            this.tabMapping.Controls.Add(this.btnUp);
            this.tabMapping.Controls.Add(this.dgSheetSummary);
            this.tabMapping.Controls.Add(this.lblMappedSheet);
            this.tabMapping.Controls.Add(this.listSelectedTimePeriods);
            this.tabMapping.Controls.Add(this.label15);
            this.tabMapping.Location = new System.Drawing.Point(4, 25);
            this.tabMapping.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabMapping.Name = "tabMapping";
            this.tabMapping.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabMapping.Size = new System.Drawing.Size(651, 572);
            this.tabMapping.TabIndex = 2;
            this.tabMapping.Text = "Mapping";
            this.tabMapping.UseVisualStyleBackColor = true;
            // 
            // chkViewTimeSeriesChart
            // 
            this.chkViewTimeSeriesChart.AutoSize = true;
            this.chkViewTimeSeriesChart.Checked = true;
            this.chkViewTimeSeriesChart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkViewTimeSeriesChart.Location = new System.Drawing.Point(23, 534);
            this.chkViewTimeSeriesChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkViewTimeSeriesChart.Name = "chkViewTimeSeriesChart";
            this.chkViewTimeSeriesChart.Size = new System.Drawing.Size(105, 21);
            this.chkViewTimeSeriesChart.TabIndex = 34;
            this.chkViewTimeSeriesChart.Text = "View results";
            this.chkViewTimeSeriesChart.UseVisualStyleBackColor = true;
            // 
            // lblParameter
            // 
            this.lblParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParameter.Location = new System.Drawing.Point(253, 534);
            this.lblParameter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParameter.Name = "lblParameter";
            this.lblParameter.Size = new System.Drawing.Size(339, 22);
            this.lblParameter.TabIndex = 33;
            this.lblParameter.Text = "Mapped parameter";
            this.lblParameter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(23, 496);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(161, 31);
            this.btnExport.TabIndex = 29;
            this.btnExport.Text = "Export to time series";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // graphSheet
            // 
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.Name = "ChartArea1";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 94F;
            chartArea2.Position.Width = 100F;
            this.graphSheet.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.graphSheet.Legends.Add(legend2);
            this.graphSheet.Location = new System.Drawing.Point(241, 374);
            this.graphSheet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.graphSheet.Name = "graphSheet";
            series2.ChartArea = "ChartArea1";
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.graphSheet.Series.Add(series2);
            this.graphSheet.Size = new System.Drawing.Size(399, 129);
            this.graphSheet.TabIndex = 32;
            this.graphSheet.Text = "chart1";
            // 
            // btnDown
            // 
            this.btnDown.ImageKey = "go-down.png";
            this.btnDown.ImageList = this.imageList1;
            this.btnDown.Location = new System.Drawing.Point(205, 181);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(40, 37);
            this.btnDown.TabIndex = 31;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "go-down.png");
            this.imageList1.Images.SetKeyName(1, "go-up.png");
            this.imageList1.Images.SetKeyName(2, "system-file-manager.png");
            this.imageList1.Images.SetKeyName(3, "Folder_16x.png");
            this.imageList1.Images.SetKeyName(4, "WebFile_16x.png");
            this.imageList1.Images.SetKeyName(5, "im_exit.bmp");
            // 
            // btnUp
            // 
            this.btnUp.ImageKey = "go-up.png";
            this.btnUp.ImageList = this.imageList1;
            this.btnUp.Location = new System.Drawing.Point(205, 126);
            this.btnUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(40, 37);
            this.btnUp.TabIndex = 30;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // dgSheetSummary
            // 
            this.dgSheetSummary.AllowUserToAddRows = false;
            this.dgSheetSummary.AllowUserToDeleteRows = false;
            this.dgSheetSummary.AllowUserToResizeColumns = false;
            this.dgSheetSummary.AllowUserToResizeRows = false;
            this.dgSheetSummary.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgSheetSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSheetSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVisible,
            this.colCategory,
            this.colSymbol,
            this.colCount,
            this.colPercent});
            this.dgSheetSummary.Location = new System.Drawing.Point(253, 31);
            this.dgSheetSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgSheetSummary.Name = "dgSheetSummary";
            this.dgSheetSummary.ReadOnly = true;
            this.dgSheetSummary.RowHeadersVisible = false;
            this.dgSheetSummary.RowHeadersWidth = 51;
            this.dgSheetSummary.Size = new System.Drawing.Size(384, 336);
            this.dgSheetSummary.TabIndex = 28;
            this.dgSheetSummary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnGridSummaryCellClick);
            // 
            // colVisible
            // 
            this.colVisible.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colVisible.Frozen = true;
            this.colVisible.HeaderText = "";
            this.colVisible.MinimumWidth = 25;
            this.colVisible.Name = "colVisible";
            this.colVisible.ReadOnly = true;
            this.colVisible.Width = 25;
            // 
            // colCategory
            // 
            this.colCategory.Frozen = true;
            this.colCategory.HeaderText = "Category";
            this.colCategory.MinimumWidth = 6;
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            this.colCategory.Width = 125;
            // 
            // colSymbol
            // 
            this.colSymbol.Frozen = true;
            this.colSymbol.HeaderText = "Symbol";
            this.colSymbol.MinimumWidth = 6;
            this.colSymbol.Name = "colSymbol";
            this.colSymbol.ReadOnly = true;
            this.colSymbol.Width = 50;
            // 
            // colCount
            // 
            this.colCount.Frozen = true;
            this.colCount.HeaderText = "n";
            this.colCount.MinimumWidth = 6;
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            this.colCount.Width = 50;
            // 
            // colPercent
            // 
            this.colPercent.Frozen = true;
            this.colPercent.HeaderText = "%";
            this.colPercent.MinimumWidth = 6;
            this.colPercent.Name = "colPercent";
            this.colPercent.ReadOnly = true;
            this.colPercent.Width = 50;
            // 
            // lblMappedSheet
            // 
            this.lblMappedSheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMappedSheet.Location = new System.Drawing.Point(256, 507);
            this.lblMappedSheet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMappedSheet.Name = "lblMappedSheet";
            this.lblMappedSheet.Size = new System.Drawing.Size(339, 22);
            this.lblMappedSheet.TabIndex = 27;
            this.lblMappedSheet.Text = "Mapped sheet";
            this.lblMappedSheet.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // listSelectedTimePeriods
            // 
            this.listSelectedTimePeriods.FormattingEnabled = true;
            this.listSelectedTimePeriods.ItemHeight = 16;
            this.listSelectedTimePeriods.Location = new System.Drawing.Point(7, 31);
            this.listSelectedTimePeriods.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listSelectedTimePeriods.Name = "listSelectedTimePeriods";
            this.listSelectedTimePeriods.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listSelectedTimePeriods.Size = new System.Drawing.Size(189, 452);
            this.listSelectedTimePeriods.TabIndex = 26;
            this.listSelectedTimePeriods.Click += new System.EventHandler(this.OnSelectedSheetsClick);
            this.listSelectedTimePeriods.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListBoxKeyDown);
            this.listSelectedTimePeriods.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnListBoxKeyUp);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 11);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 17);
            this.label15.TabIndex = 25;
            this.label15.Text = "Time periods";
            // 
            // btnOk
            // 
            this.btnOk.ImageKey = "im_exit.bmp";
            this.btnOk.ImageList = this.imageList1;
            this.btnOk.Location = new System.Drawing.Point(613, 655);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(40, 37);
            this.btnOk.TabIndex = 28;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(4, 665);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(457, 22);
            this.lblStatus.TabIndex = 29;
            // 
            // SpatioTemporalMappingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 698);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SpatioTemporalMappingForm";
            this.Text = "SpatioTemporalHelperForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.tabMap.ResumeLayout(false);
            this.tabStart.ResumeLayout(false);
            this.tabStart.PerformLayout();
            this.tabMetadata.ResumeLayout(false);
            this.tabMetadata.PerformLayout();
            this.tabSummary.ResumeLayout(false);
            this.tabSummary.PerformLayout();
            this.tabCategorize.ResumeLayout(false);
            this.tabCategorize.PerformLayout();
            this.groupBoxSummary.ResumeLayout(false);
            this.groupBoxSummary.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCategories)).EndInit();
            this.tabMapping.ResumeLayout(false);
            this.tabMapping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSheetSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMap;
        private System.Windows.Forms.TabPage tabStart;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TabPage tabCategorize;
        private System.Windows.Forms.ComboBox cboValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTemporal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboLatitude;
        private System.Windows.Forms.ComboBox cboLongitude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.ComboBox cboLastData;
        private System.Windows.Forms.ComboBox cboFirstData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabMapping;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboClassificationScheme;
        private System.Windows.Forms.GroupBox groupBoxSummary;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnShowGridPoints;
        private System.Windows.Forms.Button btnShowGridPolygons;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnColorScheme;
        private System.Windows.Forms.DataGridView dgCategories;
        private System.Windows.Forms.Button btnCategorize;
        private System.Windows.Forms.TextBox txtCategoryCount;
        private System.Windows.Forms.Label label10;
        private UserControls.ImageCombo icbColorScheme;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataVisualization.Charting.Chart graphSheet;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.DataGridView dgSheetSummary;
        private System.Windows.Forms.Label lblMappedSheet;
        private System.Windows.Forms.ListBox listSelectedTimePeriods;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblParameter;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabPage tabMetadata;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtMetadata;
        private System.Windows.Forms.Button btnBinning;
        private System.Windows.Forms.TabPage tabSummary;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtDatasetUniqueCount;
        private System.Windows.Forms.TextBox txtInlandPoints;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRows;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDatasetNumberValues;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSelectedMaximum;
        private System.Windows.Forms.TextBox txtSelectedMinimum;
        private System.Windows.Forms.TextBox txtSelectedValuesCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtSelectedUnique;
        private System.Windows.Forms.TextBox txtDatasetMin;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtDatasetMax;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.TextBox txtDatasetNumberOfTimePeriods;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtSelectedNumberOfPeriods;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox chkViewTimeSeriesChart;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcolCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcolCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgCol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colVisible;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPercent;
    }
}