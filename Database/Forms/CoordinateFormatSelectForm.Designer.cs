﻿namespace FAD3.Database.Forms
{
    partial class CoordinateFormatSelectForm
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
            this.groupRadio = new System.Windows.Forms.GroupBox();
            this.radioButtonUTM = new System.Windows.Forms.RadioButton();
            this.radioButtonDMS = new System.Windows.Forms.RadioButton();
            this.radioButtonDM = new System.Windows.Forms.RadioButton();
            this.radioButtonDD = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupRadio.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupRadio
            // 
            this.groupRadio.Controls.Add(this.radioButtonUTM);
            this.groupRadio.Controls.Add(this.radioButtonDMS);
            this.groupRadio.Controls.Add(this.radioButtonDM);
            this.groupRadio.Controls.Add(this.radioButtonDD);
            this.groupRadio.Location = new System.Drawing.Point(3, 10);
            this.groupRadio.Name = "groupRadio";
            this.groupRadio.Size = new System.Drawing.Size(147, 143);
            this.groupRadio.TabIndex = 0;
            this.groupRadio.TabStop = false;
            this.groupRadio.Text = "Select format";
            // 
            // radioButtonUTM
            // 
            this.radioButtonUTM.AutoSize = true;
            this.radioButtonUTM.Location = new System.Drawing.Point(9, 110);
            this.radioButtonUTM.Name = "radioButtonUTM";
            this.radioButtonUTM.Size = new System.Drawing.Size(85, 17);
            this.radioButtonUTM.TabIndex = 3;
            this.radioButtonUTM.TabStop = true;
            this.radioButtonUTM.Text = "UTM (Meter)";
            this.radioButtonUTM.UseVisualStyleBackColor = true;
            // 
            // radioButtonDMS
            // 
            this.radioButtonDMS.AutoSize = true;
            this.radioButtonDMS.Location = new System.Drawing.Point(9, 84);
            this.radioButtonDMS.Name = "radioButtonDMS";
            this.radioButtonDMS.Size = new System.Drawing.Size(135, 17);
            this.radioButtonDMS.TabIndex = 2;
            this.radioButtonDMS.TabStop = true;
            this.radioButtonDMS.Text = "Degree Minute Second";
            this.radioButtonDMS.UseVisualStyleBackColor = true;
            // 
            // radioButtonDM
            // 
            this.radioButtonDM.AutoSize = true;
            this.radioButtonDM.Location = new System.Drawing.Point(9, 58);
            this.radioButtonDM.Name = "radioButtonDM";
            this.radioButtonDM.Size = new System.Drawing.Size(95, 17);
            this.radioButtonDM.TabIndex = 1;
            this.radioButtonDM.TabStop = true;
            this.radioButtonDM.Text = "Degree Minute";
            this.radioButtonDM.UseVisualStyleBackColor = true;
            // 
            // radioButtonDD
            // 
            this.radioButtonDD.AutoSize = true;
            this.radioButtonDD.Location = new System.Drawing.Point(9, 32);
            this.radioButtonDD.Name = "radioButtonDD";
            this.radioButtonDD.Size = new System.Drawing.Size(101, 17);
            this.radioButtonDD.TabIndex = 0;
            this.radioButtonDD.TabStop = true;
            this.radioButtonDD.Text = "Degree Decimal";
            this.radioButtonDD.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(112, 163);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(34, 24);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // CoordinateFormatSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(153, 197);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupRadio);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CoordinateFormatSelectForm";
            this.ShowInTaskbar = false;
            this.Text = "Coordinate format";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.CoordinateFormatSelectForm_Load);
            this.groupRadio.ResumeLayout(false);
            this.groupRadio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupRadio;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.RadioButton radioButtonUTM;
        private System.Windows.Forms.RadioButton radioButtonDMS;
        private System.Windows.Forms.RadioButton radioButtonDM;
        private System.Windows.Forms.RadioButton radioButtonDD;
    }
}