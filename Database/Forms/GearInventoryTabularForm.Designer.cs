namespace FAD3.Database.Forms
{
    partial class GearInventoryTabularForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Gear local names");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Count");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Months of fishing");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Peak season months");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Months of operation and season", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("CPUE historical trends");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("CPUE", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Catch composition");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Accessories");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Expenses");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Notes");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Fisher and vessel inventory", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode5,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Respondents");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Inventory Project", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13});
            this.treeInventory = new System.Windows.Forms.TreeView();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.listResults = new System.Windows.Forms.ListView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolBar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeInventory
            // 
            this.treeInventory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeInventory.HideSelection = false;
            this.treeInventory.Location = new System.Drawing.Point(0, 34);
            this.treeInventory.Margin = new System.Windows.Forms.Padding(4);
            this.treeInventory.Name = "treeInventory";
            treeNode1.Name = "nodeGear";
            treeNode1.Text = "Gear local names";
            treeNode2.Name = "nodeGearCount";
            treeNode2.Text = "Count";
            treeNode3.Name = "nodeMonths";
            treeNode3.Text = "Months of fishing";
            treeNode4.Name = "nodePeak";
            treeNode4.Text = "Peak season months";
            treeNode5.Name = "nodeGearOperation";
            treeNode5.Text = "Months of operation and season";
            treeNode6.Name = "nodeGearCPUEHistory";
            treeNode6.Text = "CPUE historical trends";
            treeNode7.Name = "nodeCPUE";
            treeNode7.Text = "CPUE";
            treeNode8.Name = "nodeCatchComp";
            treeNode8.Text = "Catch composition";
            treeNode9.Name = "nodeAccessories";
            treeNode9.Text = "Accessories";
            treeNode10.Name = "nodeExpenses";
            treeNode10.Text = "Expenses";
            treeNode11.Name = "nodeNotes";
            treeNode11.Text = "Notes";
            treeNode12.Name = "nodeFisherVessel";
            treeNode12.Text = "Fisher and vessel inventory";
            treeNode13.Name = "nodeRespondents";
            treeNode13.Text = "Respondents";
            treeNode14.Name = "nodeProject";
            treeNode14.Text = "Inventory Project";
            this.treeInventory.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode14});
            this.treeInventory.Size = new System.Drawing.Size(295, 498);
            this.treeInventory.TabIndex = 0;
            this.treeInventory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnNodeAfterSelect);
            // 
            // toolBar
            // 
            this.toolBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExport,
            this.tsbClose});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(976, 27);
            this.toolBar.TabIndex = 2;
            this.toolBar.Text = "toolStrip1";
            this.toolBar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnToolBarItemClicked);
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExport.Image = global::FAD3.Properties.Resources.ExportFile_16x;
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(29, 24);
            this.tsbExport.Text = "toolStripButton1";
            this.tsbExport.ToolTipText = "Export";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::FAD3.Properties.Resources.im_exit;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(29, 24);
            this.tsbClose.Text = "toolStripButton1";
            // 
            // listResults
            // 
            this.listResults.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listResults.FullRowSelect = true;
            this.listResults.HideSelection = false;
            this.listResults.Location = new System.Drawing.Point(301, 34);
            this.listResults.Margin = new System.Windows.Forms.Padding(4);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(673, 498);
            this.listResults.TabIndex = 3;
            this.listResults.UseCompatibleStateImageBehavior = false;
            this.listResults.View = System.Windows.Forms.View.Details;
            this.listResults.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnListMouseDown);
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(61, 4);
            this.contextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnMenuItemClicked);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgressBar,
            this.tsLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 532);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(976, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(294, 18);
            // 
            // tsLabel
            // 
            this.tsLabel.AutoSize = false;
            this.tsLabel.Margin = new System.Windows.Forms.Padding(4, 4, 0, 2);
            this.tsLabel.Name = "tsLabel";
            this.tsLabel.Size = new System.Drawing.Size(500, 20);
            this.tsLabel.Text = "tsLabel";
            this.tsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GearInventoryTabularForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 558);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listResults);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.treeInventory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GearInventoryTabularForm";
            this.Text = "GearInventoryTabular";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeInventory;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ListView listResults;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel tsLabel;
    }
}