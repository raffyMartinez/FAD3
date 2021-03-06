﻿namespace FAD3.Database.Forms
{
    partial class FishingGroundForm
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
            this.tabFG = new System.Windows.Forms.TabControl();
            this.tabGrid25 = new System.Windows.Forms.TabPage();
            this.lblSubGrid = new System.Windows.Forms.Label();
            this.textBoxSubGrid = new System.Windows.Forms.TextBox();
            this.lvGrids = new System.Windows.Forms.ListView();
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRow = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxColumn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxGridNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxZone = new System.Windows.Forms.TextBox();
            this.tabText = new System.Windows.Forms.TabPage();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonGrids = new System.Windows.Forms.Button();
            this.tabPoints = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.colLatitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLontitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabFG.SuspendLayout();
            this.tabGrid25.SuspendLayout();
            this.tabPoints.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFG
            // 
            this.tabFG.Controls.Add(this.tabGrid25);
            this.tabFG.Controls.Add(this.tabPoints);
            this.tabFG.Controls.Add(this.tabText);
            this.tabFG.Location = new System.Drawing.Point(2, 38);
            this.tabFG.Margin = new System.Windows.Forms.Padding(4);
            this.tabFG.Name = "tabFG";
            this.tabFG.SelectedIndex = 0;
            this.tabFG.Size = new System.Drawing.Size(414, 198);
            this.tabFG.TabIndex = 0;
            // 
            // tabGrid25
            // 
            this.tabGrid25.Controls.Add(this.lblSubGrid);
            this.tabGrid25.Controls.Add(this.textBoxSubGrid);
            this.tabGrid25.Controls.Add(this.lvGrids);
            this.tabGrid25.Controls.Add(this.buttonRemoveAll);
            this.tabGrid25.Controls.Add(this.buttonRemove);
            this.tabGrid25.Controls.Add(this.buttonAdd);
            this.tabGrid25.Controls.Add(this.label4);
            this.tabGrid25.Controls.Add(this.textBoxRow);
            this.tabGrid25.Controls.Add(this.label3);
            this.tabGrid25.Controls.Add(this.textBoxColumn);
            this.tabGrid25.Controls.Add(this.label2);
            this.tabGrid25.Controls.Add(this.textBoxGridNo);
            this.tabGrid25.Controls.Add(this.label1);
            this.tabGrid25.Controls.Add(this.textBoxZone);
            this.tabGrid25.Location = new System.Drawing.Point(4, 24);
            this.tabGrid25.Margin = new System.Windows.Forms.Padding(4);
            this.tabGrid25.Name = "tabGrid25";
            this.tabGrid25.Padding = new System.Windows.Forms.Padding(4);
            this.tabGrid25.Size = new System.Drawing.Size(406, 170);
            this.tabGrid25.TabIndex = 0;
            this.tabGrid25.Text = "Grid 25";
            this.tabGrid25.UseVisualStyleBackColor = true;
            // 
            // lblSubGrid
            // 
            this.lblSubGrid.AutoSize = true;
            this.lblSubGrid.Location = new System.Drawing.Point(7, 128);
            this.lblSubGrid.Name = "lblSubGrid";
            this.lblSubGrid.Size = new System.Drawing.Size(54, 15);
            this.lblSubGrid.TabIndex = 13;
            this.lblSubGrid.Text = "Sub-grid";
            // 
            // textBoxSubGrid
            // 
            this.textBoxSubGrid.Location = new System.Drawing.Point(64, 125);
            this.textBoxSubGrid.Name = "textBoxSubGrid";
            this.textBoxSubGrid.Size = new System.Drawing.Size(75, 21);
            this.textBoxSubGrid.TabIndex = 12;
            this.textBoxSubGrid.Validating += new System.ComponentModel.CancelEventHandler(this.OntextBoxValidating);
            // 
            // lvGrids
            // 
            this.lvGrids.Location = new System.Drawing.Point(225, 21);
            this.lvGrids.Name = "lvGrids";
            this.lvGrids.Size = new System.Drawing.Size(100, 99);
            this.lvGrids.TabIndex = 11;
            this.lvGrids.UseCompatibleStateImageBehavior = false;
            this.lvGrids.DoubleClick += new System.EventHandler(this.lvGrids_DoubleClick);
            this.lvGrids.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvGrids_MouseDown);
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Location = new System.Drawing.Point(163, 87);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(30, 23);
            this.buttonRemoveAll.TabIndex = 10;
            this.buttonRemoveAll.Text = "<<";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Click += new System.EventHandler(this.OnbuttonGrid25_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(163, 60);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(30, 23);
            this.buttonRemove.TabIndex = 9;
            this.buttonRemove.Text = "<";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.OnbuttonGrid25_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(163, 31);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(30, 23);
            this.buttonAdd.TabIndex = 8;
            this.buttonAdd.Text = ">";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.OnbuttonGrid25_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Row";
            // 
            // textBoxRow
            // 
            this.textBoxRow.Location = new System.Drawing.Point(64, 98);
            this.textBoxRow.Name = "textBoxRow";
            this.textBoxRow.Size = new System.Drawing.Size(75, 21);
            this.textBoxRow.TabIndex = 6;
            this.textBoxRow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBox_KeyDown);
            this.textBoxRow.Validating += new System.ComponentModel.CancelEventHandler(this.OntextBoxValidating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Column";
            // 
            // textBoxColumn
            // 
            this.textBoxColumn.Location = new System.Drawing.Point(64, 72);
            this.textBoxColumn.Name = "textBoxColumn";
            this.textBoxColumn.Size = new System.Drawing.Size(75, 21);
            this.textBoxColumn.TabIndex = 4;
            this.textBoxColumn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBox_KeyDown);
            this.textBoxColumn.Validating += new System.ComponentModel.CancelEventHandler(this.OntextBoxValidating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Grid #";
            // 
            // textBoxGridNo
            // 
            this.textBoxGridNo.Location = new System.Drawing.Point(64, 46);
            this.textBoxGridNo.Name = "textBoxGridNo";
            this.textBoxGridNo.Size = new System.Drawing.Size(75, 21);
            this.textBoxGridNo.TabIndex = 2;
            this.textBoxGridNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextBox_KeyDown);
            this.textBoxGridNo.Validating += new System.ComponentModel.CancelEventHandler(this.OntextBoxValidating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Zone";
            // 
            // textBoxZone
            // 
            this.textBoxZone.Location = new System.Drawing.Point(64, 21);
            this.textBoxZone.Name = "textBoxZone";
            this.textBoxZone.Size = new System.Drawing.Size(75, 21);
            this.textBoxZone.TabIndex = 0;
            // 
            // tabText
            // 
            this.tabText.Location = new System.Drawing.Point(4, 24);
            this.tabText.Margin = new System.Windows.Forms.Padding(4);
            this.tabText.Name = "tabText";
            this.tabText.Padding = new System.Windows.Forms.Padding(4);
            this.tabText.Size = new System.Drawing.Size(341, 170);
            this.tabText.TabIndex = 1;
            this.tabText.Text = "Text";
            this.tabText.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(370, 243);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(39, 23);
            this.buttonOK.TabIndex = 9;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.Onbutton_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(309, 243);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(55, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.Onbutton_Click);
            // 
            // buttonGrids
            // 
            this.buttonGrids.CausesValidation = false;
            this.buttonGrids.Location = new System.Drawing.Point(251, 243);
            this.buttonGrids.Name = "buttonGrids";
            this.buttonGrids.Size = new System.Drawing.Size(52, 23);
            this.buttonGrids.TabIndex = 11;
            this.buttonGrids.Text = "Grids";
            this.buttonGrids.UseVisualStyleBackColor = true;
            this.buttonGrids.Click += new System.EventHandler(this.Onbutton_Click);
            // 
            // tabPoints
            // 
            this.tabPoints.Controls.Add(this.maskedTextBox2);
            this.tabPoints.Controls.Add(this.maskedTextBox1);
            this.tabPoints.Controls.Add(this.listView1);
            this.tabPoints.Controls.Add(this.button1);
            this.tabPoints.Controls.Add(this.button2);
            this.tabPoints.Controls.Add(this.button3);
            this.tabPoints.Controls.Add(this.label5);
            this.tabPoints.Controls.Add(this.label6);
            this.tabPoints.Location = new System.Drawing.Point(4, 24);
            this.tabPoints.Name = "tabPoints";
            this.tabPoints.Size = new System.Drawing.Size(406, 170);
            this.tabPoints.TabIndex = 2;
            this.tabPoints.Text = "Point coordinates";
            this.tabPoints.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Longitude";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Latitude";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(179, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "<<";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(179, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "<";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(179, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = ">";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLatitude,
            this.colLontitude});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(220, 22);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(179, 131);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(73, 21);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(100, 21);
            this.maskedTextBox1.TabIndex = 15;
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(73, 47);
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(100, 21);
            this.maskedTextBox2.TabIndex = 16;
            // 
            // colLatitude
            // 
            this.colLatitude.Text = "Latitude";
            this.colLatitude.Width = 81;
            // 
            // colLontitude
            // 
            this.colLontitude.Text = "Longitude";
            this.colLontitude.Width = 84;
            // 
            // FishingGroundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(417, 279);
            this.Controls.Add(this.buttonGrids);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabFG);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FishingGroundForm";
            this.ShowInTaskbar = false;
            this.Text = "Fishing ground";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.tabFG.ResumeLayout(false);
            this.tabGrid25.ResumeLayout(false);
            this.tabGrid25.PerformLayout();
            this.tabPoints.ResumeLayout(false);
            this.tabPoints.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabFG;
        private System.Windows.Forms.TabPage tabGrid25;
        private System.Windows.Forms.ListView lvGrids;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxColumn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxGridNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxZone;
        private System.Windows.Forms.TabPage tabText;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonGrids;
        private System.Windows.Forms.Label lblSubGrid;
        private System.Windows.Forms.TextBox textBoxSubGrid;
        private System.Windows.Forms.TabPage tabPoints;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colLatitude;
        private System.Windows.Forms.ColumnHeader colLontitude;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}