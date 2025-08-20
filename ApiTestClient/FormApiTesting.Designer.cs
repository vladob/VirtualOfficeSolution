
using System.Drawing;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiTestClient
{
    partial class FormApiTesting
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblAPIkey = new Label();
            textBoxApiKey = new TextBox();
            lblFromDate = new Label();
            lblToDate = new Label();
            btnFetchData = new Button();
            dataGridViewResult = new DataGridView();
            fromDatePicker = new DateTimePicker();
            toDatePicker = new DateTimePicker();
            comboBoxCompany = new ComboBox();
            btnGetFilenames = new Button();
            lblDocumentsCount = new Label();
            txtPath = new TextBox();
            label1 = new Label();
            txtSummary = new TextBox();
            btnPath = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).BeginInit();
            SuspendLayout();
            // 
            // lblAPIkey
            // 
            lblAPIkey.AutoSize = true;
            lblAPIkey.Location = new Point(10, 7);
            lblAPIkey.Name = "lblAPIkey";
            lblAPIkey.Size = new Size(80, 15);
            lblAPIkey.TabIndex = 0;
            lblAPIkey.Text = "API key input:";
            // 
            // textBoxApiKey
            // 
            textBoxApiKey.Location = new Point(99, 4);
            textBoxApiKey.Margin = new Padding(3, 2, 3, 2);
            textBoxApiKey.Name = "textBoxApiKey";
            textBoxApiKey.ReadOnly = true;
            textBoxApiKey.Size = new Size(474, 23);
            textBoxApiKey.TabIndex = 1;
            textBoxApiKey.Text = "ebt5bhbh98c-2a4ta3-4ucq83-9ovrb4-fb99l4aqbr-6bbqbdb";
            // 
            // lblFromDate
            // 
            lblFromDate.AutoSize = true;
            lblFromDate.Location = new Point(12, 90);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new Size(64, 15);
            lblFromDate.TabIndex = 2;
            lblFromDate.Text = "From date:";
            // 
            // lblToDate
            // 
            lblToDate.AutoSize = true;
            lblToDate.Location = new Point(235, 90);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new Size(48, 15);
            lblToDate.TabIndex = 4;
            lblToDate.Text = "To date:";
            lblToDate.Visible = false;
            // 
            // btnFetchData
            // 
            btnFetchData.Enabled = false;
            btnFetchData.Location = new Point(579, 32);
            btnFetchData.Margin = new Padding(3, 2, 3, 2);
            btnFetchData.Name = "btnFetchData";
            btnFetchData.Size = new Size(101, 23);
            btnFetchData.TabIndex = 6;
            btnFetchData.Text = "Fetch Data";
            btnFetchData.UseVisualStyleBackColor = true;
            btnFetchData.Click += BtnFetchData_Click;
            // 
            // dataGridViewResult
            // 
            dataGridViewResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResult.Location = new Point(10, 124);
            dataGridViewResult.Margin = new Padding(3, 2, 3, 2);
            dataGridViewResult.Name = "dataGridViewResult";
            dataGridViewResult.RowHeadersWidth = 51;
            dataGridViewResult.Size = new Size(430, 353);
            dataGridViewResult.TabIndex = 7;
            // 
            // fromDatePicker
            // 
            fromDatePicker.Format = DateTimePickerFormat.Short;
            fromDatePicker.Location = new Point(100, 86);
            fromDatePicker.Margin = new Padding(3, 2, 3, 2);
            fromDatePicker.Name = "fromDatePicker";
            fromDatePicker.Size = new Size(130, 23);
            fromDatePicker.TabIndex = 8;
            fromDatePicker.Value = new DateTime(2024, 11, 14, 0, 0, 0, 0);
            // 
            // toDatePicker
            // 
            toDatePicker.Format = DateTimePickerFormat.Short;
            toDatePicker.Location = new Point(310, 86);
            toDatePicker.Margin = new Padding(3, 2, 3, 2);
            toDatePicker.Name = "toDatePicker";
            toDatePicker.Size = new Size(130, 23);
            toDatePicker.TabIndex = 9;
            toDatePicker.Value = new DateTime(2024, 11, 19, 10, 46, 30, 592);
            toDatePicker.Visible = false;
            // 
            // comboBoxCompany
            // 
            comboBoxCompany.FormattingEnabled = true;
            comboBoxCompany.Location = new Point(12, 60);
            comboBoxCompany.Margin = new Padding(3, 2, 3, 2);
            comboBoxCompany.Name = "comboBoxCompany";
            comboBoxCompany.Size = new Size(218, 23);
            comboBoxCompany.TabIndex = 10;
            comboBoxCompany.SelectedIndexChanged += comboBoxCompany_SelectedIndexChanged;
            // 
            // btnGetFilenames
            // 
            btnGetFilenames.Enabled = false;
            btnGetFilenames.Location = new Point(579, 5);
            btnGetFilenames.Margin = new Padding(3, 2, 3, 2);
            btnGetFilenames.Name = "btnGetFilenames";
            btnGetFilenames.Size = new Size(101, 22);
            btnGetFilenames.TabIndex = 11;
            btnGetFilenames.Text = "Get Filenames";
            btnGetFilenames.UseVisualStyleBackColor = true;
            btnGetFilenames.Click += btnGetFilenames_Click;
            // 
            // lblDocumentsCount
            // 
            lblDocumentsCount.AutoSize = true;
            lblDocumentsCount.Location = new Point(242, 64);
            lblDocumentsCount.Name = "lblDocumentsCount";
            lblDocumentsCount.Size = new Size(123, 15);
            lblDocumentsCount.TabIndex = 13;
            lblDocumentsCount.Text = "Documents fetched: 0";
            // 
            // txtPath
            // 
            txtPath.Location = new Point(99, 32);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(438, 23);
            txtPath.TabIndex = 14;
            txtPath.Text = "\\\\srv2019uct\\dokumenty\\Import bločkov";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 35);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 15;
            label1.Text = "XML path:";
            // 
            // txtSummary
            // 
            txtSummary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            txtSummary.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txtSummary.Location = new Point(450, 126);
            txtSummary.Multiline = true;
            txtSummary.Name = "txtSummary";
            txtSummary.Size = new Size(261, 351);
            txtSummary.TabIndex = 16;
            txtSummary.WordWrap = false;
            // 
            // btnPath
            // 
            btnPath.Location = new Point(543, 32);
            btnPath.Name = "btnPath";
            btnPath.Size = new Size(30, 23);
            btnPath.TabIndex = 17;
            btnPath.Text = "...";
            btnPath.UseVisualStyleBackColor = true;
            btnPath.Click += btnPath_Click;
            // 
            // FormApiTesting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(723, 488);
            Controls.Add(btnPath);
            Controls.Add(txtSummary);
            Controls.Add(label1);
            Controls.Add(txtPath);
            Controls.Add(lblDocumentsCount);
            Controls.Add(btnGetFilenames);
            Controls.Add(comboBoxCompany);
            Controls.Add(toDatePicker);
            Controls.Add(fromDatePicker);
            Controls.Add(dataGridViewResult);
            Controls.Add(btnFetchData);
            Controls.Add(lblToDate);
            Controls.Add(lblFromDate);
            Controls.Add(textBoxApiKey);
            Controls.Add(lblAPIkey);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormApiTesting";
            Text = "API testing";
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAPIkey;
        private TextBox textBoxApiKey;
        private Label lblFromDate;
        private Label lblToDate;
        private Button btnFetchData;
        private DataGridView dataGridViewResult;
        private DateTimePicker fromDatePicker;
        private DateTimePicker toDatePicker;
        private ComboBox comboBoxCompany;
        private Button btnGetFilenames;
        private Label lblDocumentsCount;
        private TextBox txtPath;
        private Label label1;
        private TextBox txtSummary;
        private Button btnPath;
    }
}
