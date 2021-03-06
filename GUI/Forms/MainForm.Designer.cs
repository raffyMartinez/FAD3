﻿/*
 * Created by SharpDevelop.
 * User: Raffy
 * Date: 8/7/2016
 * Time: 1:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace FAD3.GUI.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuDropDown = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.labelErrorDetail = new System.Windows.Forms.Label();
            this.menuMenuBar = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripRecentlyOpened = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripRecentOpenedList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.resetReferenceNumbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenceNumberRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coordinateFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symbolFontsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showErrorMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.generateInlandDbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateGridMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZone50 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZone51 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLayoutTemplateOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.spatioTemporalMapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadSpatiotemporalDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBrowseLGUs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diagnosticMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusPanelDBPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPanelTargetArea = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPanelLandingSite = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPanelGearUsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.treeMain = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblErrorFormOpen = new System.Windows.Forms.Label();
            this.panelSamplingButtons = new System.Windows.Forms.Panel();
            this.buttonMap = new System.Windows.Forms.Button();
            this.buttonCatch = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.lvMain = new System.Windows.Forms.ListView();
            this.toolbar = new ToolStripExtensions.ToolStripEx();
            this.tsButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.tsButtonGear = new System.Windows.Forms.ToolStripButton();
            this.tsButtonFish = new System.Windows.Forms.ToolStripButton();
            this.tsButtonLN2SN = new System.Windows.Forms.ToolStripButton();
            this.tsButtonReport = new System.Windows.Forms.ToolStripButton();
            this.tsButtonMap = new System.Windows.Forms.ToolStripButton();
            this.tsButtonExit = new System.Windows.Forms.ToolStripButton();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMenuBar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tblLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelSamplingButtons.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuDropDown
            // 
            this.menuDropDown.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuDropDown.Name = "menuDropDown";
            this.menuDropDown.Size = new System.Drawing.Size(61, 4);
            this.menuDropDown.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnMenuDropDown_ItemClicked);
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.White;
            this.imageList16.Images.SetKeyName(0, "target_area");
            this.imageList16.Images.SetKeyName(1, "LandingSite");
            this.imageList16.Images.SetKeyName(2, "MonthGear");
            this.imageList16.Images.SetKeyName(3, "jigs");
            this.imageList16.Images.SetKeyName(4, "others");
            this.imageList16.Images.SetKeyName(5, "lines");
            this.imageList16.Images.SetKeyName(6, "impound");
            this.imageList16.Images.SetKeyName(7, "seines");
            this.imageList16.Images.SetKeyName(8, "traps");
            this.imageList16.Images.SetKeyName(9, "db");
            this.imageList16.Images.SetKeyName(10, "lev1");
            this.imageList16.Images.SetKeyName(11, "lev2");
            this.imageList16.Images.SetKeyName(12, "lev3");
            this.imageList16.Images.SetKeyName(13, "lev4");
            this.imageList16.Images.SetKeyName(14, "Level01");
            this.imageList16.Images.SetKeyName(15, "Level02");
            this.imageList16.Images.SetKeyName(16, "Level03");
            this.imageList16.Images.SetKeyName(17, "Level04");
            this.imageList16.Images.SetKeyName(18, "Level05");
            this.imageList16.Images.SetKeyName(19, "LayoutTransform");
            this.imageList16.Images.SetKeyName(20, "ListFolder");
            this.imageList16.Images.SetKeyName(21, "Actor_16xMD");
            this.imageList16.Images.SetKeyName(22, "propSmall");
            this.imageList16.Images.SetKeyName(23, "paddle");
            this.imageList16.Images.SetKeyName(24, "propMed");
            this.imageList16.Images.SetKeyName(25, "nets");
            this.imageList16.Images.SetKeyName(26, "label_add");
            // 
            // labelErrorDetail
            // 
            this.labelErrorDetail.BackColor = System.Drawing.SystemColors.Window;
            this.labelErrorDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelErrorDetail.Location = new System.Drawing.Point(321, 353);
            this.labelErrorDetail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelErrorDetail.Name = "labelErrorDetail";
            this.labelErrorDetail.Size = new System.Drawing.Size(379, 133);
            this.labelErrorDetail.TabIndex = 6;
            this.labelErrorDetail.Text = "label1";
            this.labelErrorDetail.Visible = false;
            // 
            // menuMenuBar
            // 
            this.menuMenuBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTools,
            this.menuHelp});
            this.menuMenuBar.Location = new System.Drawing.Point(0, 0);
            this.menuMenuBar.Name = "menuMenuBar";
            this.menuMenuBar.Size = new System.Drawing.Size(1293, 28);
            this.menuMenuBar.TabIndex = 1;
            this.menuMenuBar.Text = "menuStrip1";
            this.menuMenuBar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnmenuMenuBar_ItemClicked);
            this.menuMenuBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnToolbarMouseDown);
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFileNewMenuItem,
            this.toolStripFileOpen,
            this.toolStripRecentlyOpened,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(46, 24);
            this.menuFile.Text = "File";
            this.menuFile.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnMenuFile_DropDownItemClicked);
            // 
            // toolStripFileNewMenuItem
            // 
            this.toolStripFileNewMenuItem.Image = global::FAD3.Properties.Resources.VSO_NewFile_16x;
            this.toolStripFileNewMenuItem.Name = "toolStripFileNewMenuItem";
            this.toolStripFileNewMenuItem.Size = new System.Drawing.Size(203, 26);
            this.toolStripFileNewMenuItem.Tag = "new";
            this.toolStripFileNewMenuItem.Text = "New ...";
            // 
            // toolStripFileOpen
            // 
            this.toolStripFileOpen.Image = global::FAD3.Properties.Resources.OpenFileFromProject_16x;
            this.toolStripFileOpen.Name = "toolStripFileOpen";
            this.toolStripFileOpen.Size = new System.Drawing.Size(203, 26);
            this.toolStripFileOpen.Tag = "open";
            this.toolStripFileOpen.Text = "Open ...";
            // 
            // toolStripRecentlyOpened
            // 
            this.toolStripRecentlyOpened.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripRecentOpenedList});
            this.toolStripRecentlyOpened.Image = global::FAD3.Properties.Resources.History_16x;
            this.toolStripRecentlyOpened.Name = "toolStripRecentlyOpened";
            this.toolStripRecentlyOpened.Size = new System.Drawing.Size(203, 26);
            this.toolStripRecentlyOpened.Text = "Recently opened";
            // 
            // testToolStripRecentOpenedList
            // 
            this.testToolStripRecentOpenedList.Name = "testToolStripRecentOpenedList";
            this.testToolStripRecentOpenedList.Size = new System.Drawing.Size(116, 26);
            this.testToolStripRecentOpenedList.Text = "test";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::FAD3.Properties.Resources.Close_16x;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.exitToolStripMenuItem.Tag = "exit";
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // menuTools
            // 
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetReferenceNumbersToolStripMenuItem,
            this.referenceNumberRangeToolStripMenuItem,
            this.coordinateFormatToolStripMenuItem,
            this.symbolFontsToolStripMenuItem,
            this.showErrorMessagesToolStripMenuItem,
            this.mergeToolStripMenuItem,
            this.toolStripSeparator2,
            this.generateInlandDbToolStripMenuItem,
            this.generateGridMapToolStripMenuItem,
            this.spatioTemporalMapMenuItem,
            this.downloadSpatiotemporalDataToolStripMenuItem,
            this.menuItemBrowseLGUs});
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(58, 24);
            this.menuTools.Text = "Tools";
            this.menuTools.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnMenuTools_DropDownItemClicked);
            // 
            // resetReferenceNumbersToolStripMenuItem
            // 
            this.resetReferenceNumbersToolStripMenuItem.Name = "resetReferenceNumbersToolStripMenuItem";
            this.resetReferenceNumbersToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.resetReferenceNumbersToolStripMenuItem.Tag = "resetRefNos";
            this.resetReferenceNumbersToolStripMenuItem.Text = "Reset reference numbers";
            // 
            // referenceNumberRangeToolStripMenuItem
            // 
            this.referenceNumberRangeToolStripMenuItem.Name = "referenceNumberRangeToolStripMenuItem";
            this.referenceNumberRangeToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.referenceNumberRangeToolStripMenuItem.Tag = "refNoRange";
            this.referenceNumberRangeToolStripMenuItem.Text = "Reference number range";
            // 
            // coordinateFormatToolStripMenuItem
            // 
            this.coordinateFormatToolStripMenuItem.Name = "coordinateFormatToolStripMenuItem";
            this.coordinateFormatToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.coordinateFormatToolStripMenuItem.Tag = "coordFormat";
            this.coordinateFormatToolStripMenuItem.Text = "Coordinate format";
            // 
            // symbolFontsToolStripMenuItem
            // 
            this.symbolFontsToolStripMenuItem.Name = "symbolFontsToolStripMenuItem";
            this.symbolFontsToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.symbolFontsToolStripMenuItem.Tag = "symbolFonts";
            this.symbolFontsToolStripMenuItem.Text = "Symbol fonts";
            // 
            // showErrorMessagesToolStripMenuItem
            // 
            this.showErrorMessagesToolStripMenuItem.Name = "showErrorMessagesToolStripMenuItem";
            this.showErrorMessagesToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.showErrorMessagesToolStripMenuItem.Tag = "showError";
            this.showErrorMessagesToolStripMenuItem.Text = "Show error messages";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(304, 6);
            // 
            // generateInlandDbToolStripMenuItem
            // 
            this.generateInlandDbToolStripMenuItem.Name = "generateInlandDbToolStripMenuItem";
            this.generateInlandDbToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.generateInlandDbToolStripMenuItem.Tag = "createInland";
            this.generateInlandDbToolStripMenuItem.Text = "Create inland grid database";
            this.generateInlandDbToolStripMenuItem.ToolTipText = "Creates a database containing minor grids that are located inland";
            // 
            // generateGridMapToolStripMenuItem
            // 
            this.generateGridMapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemZone50,
            this.menuItemZone51,
            this.menuItemLayoutTemplateOpen});
            this.generateGridMapToolStripMenuItem.Name = "generateGridMapToolStripMenuItem";
            this.generateGridMapToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.generateGridMapToolStripMenuItem.Text = "Generate grid map";
            this.generateGridMapToolStripMenuItem.DropDownOpening += new System.EventHandler(this.OnDropDownOpening);
            this.generateGridMapToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnGenerateGridMapToolStripMenuItem_DropDownItemClicked);
            // 
            // menuItemZone50
            // 
            this.menuItemZone50.Name = "menuItemZone50";
            this.menuItemZone50.Size = new System.Drawing.Size(237, 26);
            this.menuItemZone50.Tag = "zone50";
            this.menuItemZone50.Text = "UTM zone 50";
            // 
            // menuItemZone51
            // 
            this.menuItemZone51.Name = "menuItemZone51";
            this.menuItemZone51.Size = new System.Drawing.Size(237, 26);
            this.menuItemZone51.Tag = "zone51";
            this.menuItemZone51.Text = "UTM zone 51";
            // 
            // menuItemLayoutTemplateOpen
            // 
            this.menuItemLayoutTemplateOpen.Name = "menuItemLayoutTemplateOpen";
            this.menuItemLayoutTemplateOpen.Size = new System.Drawing.Size(237, 26);
            this.menuItemLayoutTemplateOpen.Tag = "layoutTemplate";
            this.menuItemLayoutTemplateOpen.Text = "Open layout template";
            // 
            // spatioTemporalMapMenuItem
            // 
            this.spatioTemporalMapMenuItem.Enabled = false;
            this.spatioTemporalMapMenuItem.Name = "spatioTemporalMapMenuItem";
            this.spatioTemporalMapMenuItem.Size = new System.Drawing.Size(307, 26);
            this.spatioTemporalMapMenuItem.Tag = "spatio-temporal";
            this.spatioTemporalMapMenuItem.Text = "Spatio-temporal mapping";
            // 
            // downloadSpatiotemporalDataToolStripMenuItem
            // 
            this.downloadSpatiotemporalDataToolStripMenuItem.Enabled = false;
            this.downloadSpatiotemporalDataToolStripMenuItem.Name = "downloadSpatiotemporalDataToolStripMenuItem";
            this.downloadSpatiotemporalDataToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.downloadSpatiotemporalDataToolStripMenuItem.Tag = "downloadSpatioTemporal";
            this.downloadSpatiotemporalDataToolStripMenuItem.Text = "Download spatio-temporal data";
            // 
            // menuItemBrowseLGUs
            // 
            this.menuItemBrowseLGUs.Name = "menuItemBrowseLGUs";
            this.menuItemBrowseLGUs.Size = new System.Drawing.Size(307, 26);
            this.menuItemBrowseLGUs.Tag = "browseLGUs";
            this.menuItemBrowseLGUs.Text = "LGUs";
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.onlineManualToolStripMenuItem,
            this.diagnosticMenuItem});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(55, 24);
            this.menuHelp.Text = "Help";
            this.menuHelp.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnMenuHelp_DropDownItemClicked);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.aboutToolStripMenuItem.Tag = "about";
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // onlineManualToolStripMenuItem
            // 
            this.onlineManualToolStripMenuItem.Name = "onlineManualToolStripMenuItem";
            this.onlineManualToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
            this.onlineManualToolStripMenuItem.Tag = "onlineManual";
            this.onlineManualToolStripMenuItem.Text = "Online manual";
            // 
            // diagnosticMenuItem
            // 
            this.diagnosticMenuItem.Name = "diagnosticMenuItem";
            this.diagnosticMenuItem.Size = new System.Drawing.Size(261, 26);
            this.diagnosticMenuItem.Tag = "diagnostics";
            this.diagnosticMenuItem.Text = "Log database diagnostics";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusPanelDBPath,
            this.statusPanelTargetArea,
            this.statusPanelLandingSite,
            this.statusPanelGearUsed});
            this.statusStrip1.Location = new System.Drawing.Point(0, 665);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1293, 30);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusPanelDBPath
            // 
            this.statusPanelDBPath.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusPanelDBPath.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusPanelDBPath.DoubleClickEnabled = true;
            this.statusPanelDBPath.Name = "statusPanelDBPath";
            this.statusPanelDBPath.Size = new System.Drawing.Size(61, 24);
            this.statusPanelDBPath.Text = "DBPath";
            this.statusPanelDBPath.DoubleClick += new System.EventHandler(this.statusPanelDBPath_DoubleClick);
            // 
            // statusPanelTargetArea
            // 
            this.statusPanelTargetArea.AutoSize = false;
            this.statusPanelTargetArea.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusPanelTargetArea.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusPanelTargetArea.Name = "statusPanelTargetArea";
            this.statusPanelTargetArea.Size = new System.Drawing.Size(80, 24);
            this.statusPanelTargetArea.Text = "Target area";
            this.statusPanelTargetArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusPanelLandingSite
            // 
            this.statusPanelLandingSite.AutoSize = false;
            this.statusPanelLandingSite.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusPanelLandingSite.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusPanelLandingSite.Name = "statusPanelLandingSite";
            this.statusPanelLandingSite.Size = new System.Drawing.Size(4, 24);
            this.statusPanelLandingSite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusPanelGearUsed
            // 
            this.statusPanelGearUsed.AutoSize = false;
            this.statusPanelGearUsed.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusPanelGearUsed.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusPanelGearUsed.Name = "statusPanelGearUsed";
            this.statusPanelGearUsed.Size = new System.Drawing.Size(4, 24);
            this.statusPanelGearUsed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tblLayout
            // 
            this.tblLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblLayout.ColumnCount = 2;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 333F));
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout.Controls.Add(this.treeMain, 0, 0);
            this.tblLayout.Controls.Add(this.panel1, 1, 0);
            this.tblLayout.Location = new System.Drawing.Point(0, 71);
            this.tblLayout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 1;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 588F));
            this.tblLayout.Size = new System.Drawing.Size(1293, 588);
            this.tblLayout.TabIndex = 4;
            // 
            // treeMain
            // 
            this.treeMain.AllowDrop = true;
            this.treeMain.ContextMenuStrip = this.menuDropDown;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeMain.HideSelection = false;
            this.treeMain.ImageIndex = 0;
            this.treeMain.ImageList = this.imageList16;
            this.treeMain.Location = new System.Drawing.Point(4, 4);
            this.treeMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeMain.Name = "treeMain";
            this.treeMain.RightToLeftLayout = true;
            this.treeMain.SelectedImageIndex = 0;
            this.treeMain.Size = new System.Drawing.Size(325, 580);
            this.treeMain.TabIndex = 5;
            this.treeMain.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.OntreeMainAfterExpand);
            this.treeMain.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnItemDrag);
            this.treeMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeMainAfterSelect);
            this.treeMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.treeMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.treeMain.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.treeMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OntreeMain_MouseDown);
            this.treeMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OntreeMain_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.lblErrorFormOpen);
            this.panel1.Controls.Add(this.panelSamplingButtons);
            this.panel1.Controls.Add(this.labelErrorDetail);
            this.panel1.Controls.Add(this.lvMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(337, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(952, 580);
            this.panel1.TabIndex = 12;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(839, 33);
            this.lblTitle.TabIndex = 13;
            // 
            // lblErrorFormOpen
            // 
            this.lblErrorFormOpen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblErrorFormOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorFormOpen.Location = new System.Drawing.Point(4, 251);
            this.lblErrorFormOpen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblErrorFormOpen.Name = "lblErrorFormOpen";
            this.lblErrorFormOpen.Size = new System.Drawing.Size(940, 102);
            this.lblErrorFormOpen.TabIndex = 12;
            this.lblErrorFormOpen.Text = "label1";
            this.lblErrorFormOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblErrorFormOpen.Visible = false;
            // 
            // panelSamplingButtons
            // 
            this.panelSamplingButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSamplingButtons.BackColor = System.Drawing.SystemColors.Window;
            this.panelSamplingButtons.Controls.Add(this.buttonMap);
            this.panelSamplingButtons.Controls.Add(this.buttonCatch);
            this.panelSamplingButtons.Controls.Add(this.buttonOK);
            this.panelSamplingButtons.Location = new System.Drawing.Point(836, 37);
            this.panelSamplingButtons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSamplingButtons.Name = "panelSamplingButtons";
            this.panelSamplingButtons.Size = new System.Drawing.Size(93, 160);
            this.panelSamplingButtons.TabIndex = 11;
            this.panelSamplingButtons.Visible = false;
            // 
            // buttonMap
            // 
            this.buttonMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMap.Location = new System.Drawing.Point(9, 107);
            this.buttonMap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonMap.Name = "buttonMap";
            this.buttonMap.Size = new System.Drawing.Size(76, 37);
            this.buttonMap.TabIndex = 2;
            this.buttonMap.Text = "Map";
            this.buttonMap.UseVisualStyleBackColor = true;
            this.buttonMap.Click += new System.EventHandler(this.OnbuttonSamplingClick);
            // 
            // buttonCatch
            // 
            this.buttonCatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCatch.Location = new System.Drawing.Point(9, 63);
            this.buttonCatch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCatch.Name = "buttonCatch";
            this.buttonCatch.Size = new System.Drawing.Size(76, 37);
            this.buttonCatch.TabIndex = 1;
            this.buttonCatch.Text = "Catch";
            this.buttonCatch.UseVisualStyleBackColor = true;
            this.buttonCatch.Click += new System.EventHandler(this.OnbuttonSamplingClick);
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(9, 18);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 37);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.OnbuttonSamplingClick);
            // 
            // lvMain
            // 
            this.lvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvMain.ContextMenuStrip = this.menuDropDown;
            this.lvMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMain.HideSelection = false;
            this.lvMain.Location = new System.Drawing.Point(0, 37);
            this.lvMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvMain.MultiSelect = false;
            this.lvMain.Name = "lvMain";
            this.lvMain.ShowItemToolTips = true;
            this.lvMain.Size = new System.Drawing.Size(947, 543);
            this.lvMain.TabIndex = 11;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.DoubleClick += new System.EventHandler(this.OnListView_DoubleClick);
            this.lvMain.Leave += new System.EventHandler(this.OnListViewLeave);
            this.lvMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnListView_MouseClick);
            this.lvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnListView_MouseDown);
            // 
            // toolbar
            // 
            this.toolbar.ClickThrough = true;
            this.toolbar.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsButtonAbout,
            this.tsButtonGear,
            this.tsButtonFish,
            this.tsButtonLN2SN,
            this.tsButtonReport,
            this.tsButtonMap,
            this.tsButtonExit});
            this.toolbar.Location = new System.Drawing.Point(0, 28);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(1293, 35);
            this.toolbar.SuppressHighlighting = true;
            this.toolbar.TabIndex = 3;
            this.toolbar.Text = "toolStripEx1";
            this.toolbar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnToolbar_ItemClicked);
            // 
            // tsButtonAbout
            // 
            this.tsButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonAbout.Image = global::FAD3.Properties.Resources.help_browser;
            this.tsButtonAbout.ImageTransparentColor = System.Drawing.Color.White;
            this.tsButtonAbout.Name = "tsButtonAbout";
            this.tsButtonAbout.Size = new System.Drawing.Size(32, 32);
            this.tsButtonAbout.Text = "toolStripButton1";
            this.tsButtonAbout.ToolTipText = "About FAD3";
            // 
            // tsButtonGear
            // 
            this.tsButtonGear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonGear.Image = global::FAD3.Properties.Resources.imHook;
            this.tsButtonGear.ImageTransparentColor = System.Drawing.Color.White;
            this.tsButtonGear.Name = "tsButtonGear";
            this.tsButtonGear.Size = new System.Drawing.Size(32, 32);
            this.tsButtonGear.Text = "toolStripButton2";
            this.tsButtonGear.ToolTipText = "Fishing gears";
            // 
            // tsButtonFish
            // 
            this.tsButtonFish.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonFish.Image = global::FAD3.Properties.Resources.fish2;
            this.tsButtonFish.ImageTransparentColor = System.Drawing.Color.White;
            this.tsButtonFish.Name = "tsButtonFish";
            this.tsButtonFish.Size = new System.Drawing.Size(32, 32);
            this.tsButtonFish.Text = "toolStripButton3";
            this.tsButtonFish.ToolTipText = "Species";
            // 
            // tsButtonLN2SN
            // 
            this.tsButtonLN2SN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonLN2SN.Image = global::FAD3.Properties.Resources.fish_list;
            this.tsButtonLN2SN.ImageTransparentColor = System.Drawing.Color.White;
            this.tsButtonLN2SN.Name = "tsButtonLN2SN";
            this.tsButtonLN2SN.Size = new System.Drawing.Size(32, 32);
            this.tsButtonLN2SN.Text = "toolStripButton1";
            this.tsButtonLN2SN.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.tsButtonLN2SN.ToolTipText = "Local names to species names";
            // 
            // tsButtonReport
            // 
            this.tsButtonReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonReport.Image = global::FAD3.Properties.Resources.system_file_manager;
            this.tsButtonReport.ImageTransparentColor = System.Drawing.Color.White;
            this.tsButtonReport.Name = "tsButtonReport";
            this.tsButtonReport.Size = new System.Drawing.Size(32, 32);
            this.tsButtonReport.Text = "toolStripButton4";
            this.tsButtonReport.ToolTipText = "Reports";
            // 
            // tsButtonMap
            // 
            this.tsButtonMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonMap.Image = global::FAD3.Properties.Resources.internet_web_browser;
            this.tsButtonMap.ImageTransparentColor = System.Drawing.Color.White;
            this.tsButtonMap.Name = "tsButtonMap";
            this.tsButtonMap.Size = new System.Drawing.Size(32, 32);
            this.tsButtonMap.Text = "toolStripButton5";
            this.tsButtonMap.ToolTipText = "Map";
            // 
            // tsButtonExit
            // 
            this.tsButtonExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsButtonExit.Image = global::FAD3.Properties.Resources.im_exit;
            this.tsButtonExit.ImageTransparentColor = System.Drawing.Color.White;
            this.tsButtonExit.Name = "tsButtonExit";
            this.tsButtonExit.Size = new System.Drawing.Size(32, 32);
            this.tsButtonExit.Text = "toolStripButton6";
            this.tsButtonExit.ToolTipText = "Exit";
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.mergeToolStripMenuItem.Tag = "mergeDatabases";
            this.mergeToolStripMenuItem.Text = "Merge";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 695);
            this.Controls.Add(this.tblLayout);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuMenuBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMenuBar;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Fisheries Assessment Database";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.OnMainForm_Load);
            this.menuMenuBar.ResumeLayout(false);
            this.menuMenuBar.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tblLayout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelSamplingButtons.ResumeLayout(false);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripStatusLabel statusPanelGearUsed;
		private System.Windows.Forms.ToolStripStatusLabel statusPanelLandingSite;
		private System.Windows.Forms.ToolStripStatusLabel statusPanelTargetArea;
		private System.Windows.Forms.ToolStripStatusLabel statusPanelDBPath;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem toolStripFileOpen;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripFileNewMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.MenuStrip menuMenuBar;
        private System.Windows.Forms.ToolStripMenuItem toolStripRecentlyOpened;
        private System.Windows.Forms.ToolStripMenuItem testToolStripRecentOpenedList;
        private System.Windows.Forms.ImageList imageList16;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem resetReferenceNumbersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenceNumberRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coordinateFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symbolFontsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateGridMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemZone50;
        private System.Windows.Forms.ToolStripMenuItem menuItemZone51;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showErrorMessagesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuDropDown;
        private System.Windows.Forms.Label labelErrorDetail;
        private ToolStripExtensions.ToolStripEx toolbar;
        private System.Windows.Forms.ToolStripButton tsButtonAbout;
        private System.Windows.Forms.ToolStripButton tsButtonGear;
        private System.Windows.Forms.ToolStripButton tsButtonFish;
        private System.Windows.Forms.ToolStripButton tsButtonReport;
        private System.Windows.Forms.ToolStripButton tsButtonMap;
        private System.Windows.Forms.ToolStripButton tsButtonExit;
        private System.Windows.Forms.ToolStripMenuItem generateInlandDbToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem spatioTemporalMapMenuItem;
        private System.Windows.Forms.TableLayoutPanel tblLayout;
        private System.Windows.Forms.TreeView treeMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSamplingButtons;
        private System.Windows.Forms.Button buttonMap;
        private System.Windows.Forms.Button buttonCatch;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.Label lblErrorFormOpen;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolStripButton tsButtonLN2SN;
        private System.Windows.Forms.ToolStripMenuItem menuItemBrowseLGUs;
        private System.Windows.Forms.ToolStripMenuItem diagnosticMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadSpatiotemporalDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemLayoutTemplateOpen;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
    }
}
