namespace FAD3.Database.Classes.merge.views
{
    partial class MergeDbForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonMerge = new System.Windows.Forms.Button();
            this.listViewAOIs = new System.Windows.Forms.ListView();
            this.lblSelectAOI = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ProgressBarMergeDB = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusLabelMerge = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect.Location = new System.Drawing.Point(604, 78);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(184, 35);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "Select database to merge";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(604, 184);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(184, 35);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // buttonMerge
            // 
            this.buttonMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMerge.Location = new System.Drawing.Point(604, 130);
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.Size = new System.Drawing.Size(184, 35);
            this.buttonMerge.TabIndex = 3;
            this.buttonMerge.Text = "Merge to destination";
            this.buttonMerge.UseVisualStyleBackColor = true;
            this.buttonMerge.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // listViewAOIs
            // 
            this.listViewAOIs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAOIs.HideSelection = false;
            this.listViewAOIs.Location = new System.Drawing.Point(12, 78);
            this.listViewAOIs.Name = "listViewAOIs";
            this.listViewAOIs.Size = new System.Drawing.Size(554, 334);
            this.listViewAOIs.TabIndex = 4;
            this.listViewAOIs.UseCompatibleStateImageBehavior = false;
            // 
            // lblSelectAOI
            // 
            this.lblSelectAOI.AutoSize = true;
            this.lblSelectAOI.Location = new System.Drawing.Point(12, 49);
            this.lblSelectAOI.Name = "lblSelectAOI";
            this.lblSelectAOI.Size = new System.Drawing.Size(201, 17);
            this.lblSelectAOI.TabIndex = 5;
            this.lblSelectAOI.Text = "Select target area to merge to ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBarMergeDB,
            this.StatusLabelMerge});
            this.statusStrip1.Location = new System.Drawing.Point(0, 415);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 26);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ProgressBarMergeDB
            // 
            this.ProgressBarMergeDB.Name = "ProgressBarMergeDB";
            this.ProgressBarMergeDB.Size = new System.Drawing.Size(200, 18);
            // 
            // StatusLabelMerge
            // 
            this.StatusLabelMerge.AutoSize = false;
            this.StatusLabelMerge.Margin = new System.Windows.Forms.Padding(6, 4, 0, 2);
            this.StatusLabelMerge.Name = "StatusLabelMerge";
            this.StatusLabelMerge.Size = new System.Drawing.Size(400, 20);
            this.StatusLabelMerge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MergeDbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 441);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblSelectAOI);
            this.Controls.Add(this.listViewAOIs);
            this.Controls.Add(this.buttonMerge);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MergeDbForm";
            this.Text = "MergeDbForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonMerge;
        private System.Windows.Forms.ListView listViewAOIs;
        private System.Windows.Forms.Label lblSelectAOI;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar ProgressBarMergeDB;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabelMerge;
    }
}