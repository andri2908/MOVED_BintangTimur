﻿namespace AlphaSoft
{
    partial class dataReturPenjualanForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dataReturPenjualanForm));
            this.label4 = new System.Windows.Forms.Label();
            this.invoiceInfoLabel = new System.Windows.Forms.Label();
            this.noReturTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rsDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.invoiceInfoTextBox = new System.Windows.Forms.TextBox();
            this.invoiceDateLabel = new System.Windows.Forms.Label();
            this.invoiceDateTextBox = new System.Windows.Forms.TextBox();
            this.invoiceTotalLabelValue = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.detailReturDataGridView = new System.Windows.Forms.DataGridView();
            this.invoiceTotalLabel = new System.Windows.Forms.Label();
            this.invoiceSignLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChangePrinterButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailReturDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(142, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = ":";
            // 
            // invoiceInfoLabel
            // 
            this.invoiceInfoLabel.AutoSize = true;
            this.invoiceInfoLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoiceInfoLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.invoiceInfoLabel.Location = new System.Drawing.Point(6, 49);
            this.invoiceInfoLabel.Name = "invoiceInfoLabel";
            this.invoiceInfoLabel.Size = new System.Drawing.Size(118, 18);
            this.invoiceInfoLabel.TabIndex = 19;
            this.invoiceInfoLabel.Text = "NO INVOICE";
            // 
            // noReturTextBox
            // 
            this.noReturTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.noReturTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noReturTextBox.Location = new System.Drawing.Point(162, 13);
            this.noReturTextBox.Name = "noReturTextBox";
            this.noReturTextBox.ReadOnly = true;
            this.noReturTextBox.Size = new System.Drawing.Size(225, 27);
            this.noReturTextBox.TabIndex = 16;
            this.noReturTextBox.TextChanged += new System.EventHandler(this.noReturTextBox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(491, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(160, 18);
            this.label11.TabIndex = 20;
            this.label11.Text = "TANGGAL RETUR";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Location = new System.Drawing.Point(6, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 18);
            this.label12.TabIndex = 29;
            this.label12.Text = "NO RETUR";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(142, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 18);
            this.label6.TabIndex = 12;
            this.label6.Text = ":";
            // 
            // rsDateTimePicker
            // 
            this.rsDateTimePicker.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rsDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.rsDateTimePicker.Location = new System.Drawing.Point(696, 13);
            this.rsDateTimePicker.Name = "rsDateTimePicker";
            this.rsDateTimePicker.Size = new System.Drawing.Size(178, 27);
            this.rsDateTimePicker.TabIndex = 55;
            // 
            // invoiceInfoTextBox
            // 
            this.invoiceInfoTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoiceInfoTextBox.Location = new System.Drawing.Point(162, 46);
            this.invoiceInfoTextBox.Name = "invoiceInfoTextBox";
            this.invoiceInfoTextBox.ReadOnly = true;
            this.invoiceInfoTextBox.Size = new System.Drawing.Size(225, 27);
            this.invoiceInfoTextBox.TabIndex = 16;
            // 
            // invoiceDateLabel
            // 
            this.invoiceDateLabel.AutoSize = true;
            this.invoiceDateLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoiceDateLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.invoiceDateLabel.Location = new System.Drawing.Point(491, 49);
            this.invoiceDateLabel.Name = "invoiceDateLabel";
            this.invoiceDateLabel.Size = new System.Drawing.Size(176, 18);
            this.invoiceDateLabel.TabIndex = 20;
            this.invoiceDateLabel.Text = "TANGGAL INVOICE\r\n";
            // 
            // invoiceDateTextBox
            // 
            this.invoiceDateTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoiceDateTextBox.Location = new System.Drawing.Point(696, 46);
            this.invoiceDateTextBox.Name = "invoiceDateTextBox";
            this.invoiceDateTextBox.ReadOnly = true;
            this.invoiceDateTextBox.Size = new System.Drawing.Size(178, 27);
            this.invoiceDateTextBox.TabIndex = 21;
            // 
            // invoiceTotalLabelValue
            // 
            this.invoiceTotalLabelValue.AutoSize = true;
            this.invoiceTotalLabelValue.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoiceTotalLabelValue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.invoiceTotalLabelValue.Location = new System.Drawing.Point(162, 84);
            this.invoiceTotalLabelValue.Name = "invoiceTotalLabelValue";
            this.invoiceTotalLabelValue.Size = new System.Drawing.Size(83, 29);
            this.invoiceTotalLabelValue.TabIndex = 49;
            this.invoiceTotalLabelValue.Text = "Rp. 0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.errorLabel);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(891, 29);
            this.panel1.TabIndex = 50;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.White;
            this.errorLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(3, 6);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(23, 18);
            this.errorLabel.TabIndex = 55;
            this.errorLabel.Text = "   ";
            // 
            // detailReturDataGridView
            // 
            this.detailReturDataGridView.AllowUserToAddRows = false;
            this.detailReturDataGridView.AllowUserToDeleteRows = false;
            this.detailReturDataGridView.BackgroundColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailReturDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.detailReturDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.detailReturDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.detailReturDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.detailReturDataGridView.Location = new System.Drawing.Point(2, 208);
            this.detailReturDataGridView.MultiSelect = false;
            this.detailReturDataGridView.Name = "detailReturDataGridView";
            this.detailReturDataGridView.RowHeadersVisible = false;
            this.detailReturDataGridView.Size = new System.Drawing.Size(888, 402);
            this.detailReturDataGridView.TabIndex = 51;
            this.detailReturDataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.detailReturDataGridView_CellBeginEdit);
            this.detailReturDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailReturDataGridView_CellEndEdit);
            this.detailReturDataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailReturDataGridView_CellEnter);
            this.detailReturDataGridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailReturDataGridView_CellValidated);
            this.detailReturDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.detailReturDataGridView_CellValueChanged);
            this.detailReturDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.detailReturDataGridView_CurrentCellDirtyStateChanged);
            this.detailReturDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.detailReturDataGridView_RowsAdded);
            // 
            // invoiceTotalLabel
            // 
            this.invoiceTotalLabel.AutoSize = true;
            this.invoiceTotalLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoiceTotalLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.invoiceTotalLabel.Location = new System.Drawing.Point(6, 93);
            this.invoiceTotalLabel.Name = "invoiceTotalLabel";
            this.invoiceTotalLabel.Size = new System.Drawing.Size(68, 18);
            this.invoiceTotalLabel.TabIndex = 51;
            this.invoiceTotalLabel.Text = "TOTAL";
            // 
            // invoiceSignLabel
            // 
            this.invoiceSignLabel.AutoSize = true;
            this.invoiceSignLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invoiceSignLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.invoiceSignLabel.Location = new System.Drawing.Point(142, 93);
            this.invoiceSignLabel.Name = "invoiceSignLabel";
            this.invoiceSignLabel.Size = new System.Drawing.Size(14, 18);
            this.invoiceSignLabel.TabIndex = 52;
            this.invoiceSignLabel.Text = ":";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(142, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 18);
            this.label8.TabIndex = 54;
            this.label8.Text = ":";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(6, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 18);
            this.label7.TabIndex = 53;
            this.label7.Text = "TOTAL RETUR";
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.totalLabel.Location = new System.Drawing.Point(162, 122);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(83, 29);
            this.totalLabel.TabIndex = 55;
            this.totalLabel.Text = "Rp. 0";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.saveButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(267, 616);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(122, 37);
            this.saveButton.TabIndex = 52;
            this.saveButton.Text = "SAVE ";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(407, 616);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 37);
            this.button1.TabIndex = 53;
            this.button1.Text = "PRINT LAST RECEIPT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.ChangePrinterButton);
            this.groupBox1.Controls.Add(this.totalLabel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.invoiceTotalLabelValue);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.invoiceDateTextBox);
            this.groupBox1.Controls.Add(this.invoiceSignLabel);
            this.groupBox1.Controls.Add(this.invoiceTotalLabel);
            this.groupBox1.Controls.Add(this.rsDateTimePicker);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.invoiceDateLabel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.invoiceInfoTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.noReturTextBox);
            this.groupBox1.Controls.Add(this.invoiceInfoLabel);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(2, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(891, 165);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            // 
            // ChangePrinterButton
            // 
            this.ChangePrinterButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangePrinterButton.ForeColor = System.Drawing.Color.Black;
            this.ChangePrinterButton.Location = new System.Drawing.Point(494, 115);
            this.ChangePrinterButton.Name = "ChangePrinterButton";
            this.ChangePrinterButton.Size = new System.Drawing.Size(158, 34);
            this.ChangePrinterButton.TabIndex = 54;
            this.ChangePrinterButton.Text = "SET PRINTER";
            this.ChangePrinterButton.UseVisualStyleBackColor = true;
            this.ChangePrinterButton.Visible = false;
            this.ChangePrinterButton.Click += new System.EventHandler(this.ChangePrinterButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(676, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 18);
            this.label2.TabIndex = 31;
            this.label2.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(676, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 18);
            this.label1.TabIndex = 30;
            this.label1.Text = ":";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(671, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 18);
            this.label5.TabIndex = 61;
            this.label5.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(490, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 18);
            this.label3.TabIndex = 60;
            this.label3.Text = "UKURAN KERTAS";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1/2 KUARTO",
            "KUARTO"});
            this.comboBox1.Location = new System.Drawing.Point(691, 125);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(183, 26);
            this.comboBox1.TabIndex = 59;
            this.comboBox1.Text = "1/2 KUARTO";
            // 
            // dataReturPenjualanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(894, 661);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.detailReturDataGridView);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "dataReturPenjualanForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DATA RETUR PENJUALAN";
            this.Activated += new System.EventHandler(this.dataReturPenjualanForm_Activated);
            this.Deactivate += new System.EventHandler(this.dataReturPenjualanForm_Deactivate);
            this.Load += new System.EventHandler(this.dataReturPenjualanForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailReturDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label invoiceInfoLabel;
        private System.Windows.Forms.TextBox noReturTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox invoiceInfoTextBox;
        private System.Windows.Forms.Label invoiceDateLabel;
        private System.Windows.Forms.TextBox invoiceDateTextBox;
        private System.Windows.Forms.Label invoiceTotalLabelValue;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView detailReturDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label invoiceTotalLabel;
        private System.Windows.Forms.Label invoiceSignLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.DateTimePicker rsDateTimePicker;
        private System.Windows.Forms.Label errorLabel;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ChangePrinterButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}