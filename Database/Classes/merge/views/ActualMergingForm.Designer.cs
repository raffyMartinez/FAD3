namespace FAD3.Database.Classes.merge.views
{
    partial class ActualMergingForm
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
            this.buttonMerge = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkProceed = new System.Windows.Forms.CheckBox();
            this.lvDestination = new System.Windows.Forms.ListView();
            this.lblList = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonViewConflict = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonResultsGraph = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonMerge
            // 
            this.buttonMerge.Location = new System.Drawing.Point(211, 79);
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.Size = new System.Drawing.Size(92, 33);
            this.buttonMerge.TabIndex = 1;
            this.buttonMerge.Text = "Merge";
            this.buttonMerge.UseVisualStyleBackColor = true;
            this.buttonMerge.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkProceed);
            this.groupBox1.Controls.Add(this.buttonMerge);
            this.groupBox1.Location = new System.Drawing.Point(22, 311);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 129);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Confirmation";
            // 
            // checkProceed
            // 
            this.checkProceed.Location = new System.Drawing.Point(6, 34);
            this.checkProceed.Name = "checkProceed";
            this.checkProceed.Size = new System.Drawing.Size(554, 33);
            this.checkProceed.TabIndex = 2;
            this.checkProceed.Text = "I want to merge data from source database to destination database";
            this.checkProceed.UseVisualStyleBackColor = true;
            this.checkProceed.CheckedChanged += new System.EventHandler(this.OnCheckBoxChanged);
            // 
            // lvDestination
            // 
            this.lvDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDestination.HideSelection = false;
            this.lvDestination.Location = new System.Drawing.Point(22, 67);
            this.lvDestination.Name = "lvDestination";
            this.lvDestination.Size = new System.Drawing.Size(588, 222);
            this.lvDestination.TabIndex = 7;
            this.lvDestination.UseCompatibleStateImageBehavior = false;
            this.lvDestination.View = System.Windows.Forms.View.Details;
            // 
            // lblList
            // 
            this.lblList.AutoSize = true;
            this.lblList.Location = new System.Drawing.Point(23, 41);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(236, 17);
            this.lblList.TabIndex = 6;
            this.lblList.Text = "Properties of destination target area";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(626, 69);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(108, 33);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // buttonViewConflict
            // 
            this.buttonViewConflict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonViewConflict.Enabled = false;
            this.buttonViewConflict.Location = new System.Drawing.Point(626, 120);
            this.buttonViewConflict.Name = "buttonViewConflict";
            this.buttonViewConflict.Size = new System.Drawing.Size(108, 33);
            this.buttonViewConflict.TabIndex = 9;
            this.buttonViewConflict.Text = "View conflict";
            this.buttonViewConflict.UseVisualStyleBackColor = true;
            this.buttonViewConflict.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgressBar,
            this.tsLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(751, 26);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(200, 18);
            // 
            // tsLabel
            // 
            this.tsLabel.Margin = new System.Windows.Forms.Padding(10, 4, 0, 2);
            this.tsLabel.Name = "tsLabel";
            this.tsLabel.Size = new System.Drawing.Size(117, 20);
            this.tsLabel.Text = "this is status text";
            // 
            // buttonResultsGraph
            // 
            this.buttonResultsGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResultsGraph.Location = new System.Drawing.Point(626, 170);
            this.buttonResultsGraph.Name = "buttonResultsGraph";
            this.buttonResultsGraph.Size = new System.Drawing.Size(108, 33);
            this.buttonResultsGraph.TabIndex = 11;
            this.buttonResultsGraph.Text = "View results";
            this.buttonResultsGraph.UseVisualStyleBackColor = true;
            this.buttonResultsGraph.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Location = new System.Drawing.Point(626, 218);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(108, 33);
            this.buttonExport.TabIndex = 12;
            this.buttonExport.Text = "Export results";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // ActualMergingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 470);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonResultsGraph);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonViewConflict);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.lvDestination);
            this.Controls.Add(this.lblList);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ActualMergingForm";
            this.Text = "ActualMergingForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMerge;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkProceed;
        private System.Windows.Forms.ListView lvDestination;
        private System.Windows.Forms.Label lblList;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonViewConflict;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel tsLabel;
        private System.Windows.Forms.Button buttonResultsGraph;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}