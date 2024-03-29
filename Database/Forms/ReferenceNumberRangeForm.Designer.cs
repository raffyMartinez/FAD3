﻿namespace FAD3.Database.Forms
{
    partial class ReferenceNumberRangeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMinVal = new System.Windows.Forms.TextBox();
            this.textBoxMaxVal = new System.Windows.Forms.TextBox();
            this.buttonRefNo = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Set the minimum and maximum values for reference nos. \r\nThis should be used when " +
    "2 or more computers are used to \r\nencode data separately. ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimum value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Maximum value";
            // 
            // textBoxMinVal
            // 
            this.textBoxMinVal.Location = new System.Drawing.Point(136, 86);
            this.textBoxMinVal.Name = "textBoxMinVal";
            this.textBoxMinVal.Size = new System.Drawing.Size(117, 21);
            this.textBoxMinVal.TabIndex = 3;
            this.textBoxMinVal.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateTextBox);
            // 
            // textBoxMaxVal
            // 
            this.textBoxMaxVal.Location = new System.Drawing.Point(136, 127);
            this.textBoxMaxVal.Name = "textBoxMaxVal";
            this.textBoxMaxVal.Size = new System.Drawing.Size(117, 21);
            this.textBoxMaxVal.TabIndex = 4;
            this.textBoxMaxVal.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateTextBox);
            // 
            // buttonRefNo
            // 
            this.buttonRefNo.Location = new System.Drawing.Point(269, 172);
            this.buttonRefNo.Name = "buttonRefNo";
            this.buttonRefNo.Size = new System.Drawing.Size(49, 23);
            this.buttonRefNo.TabIndex = 5;
            this.buttonRefNo.Text = "OK";
            this.buttonRefNo.UseVisualStyleBackColor = true;
            this.buttonRefNo.Click += new System.EventHandler(this.ButtonClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.Location = new System.Drawing.Point(210, 172);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(53, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonClick);
            // 
            // buttonReset
            // 
            this.buttonReset.CausesValidation = false;
            this.buttonReset.Location = new System.Drawing.Point(151, 172);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(53, 23);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonClick);
            // 
            // ReferenceNumberRangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 212);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonRefNo);
            this.Controls.Add(this.textBoxMaxVal);
            this.Controls.Add(this.textBoxMinVal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReferenceNumberRangeForm";
            this.ShowInTaskbar = false;
            this.Text = "Reference number range";
            this.Load += new System.EventHandler(this.frmRefNoRange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMinVal;
        private System.Windows.Forms.TextBox textBoxMaxVal;
        private System.Windows.Forms.Button buttonRefNo;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonReset;
    }
}