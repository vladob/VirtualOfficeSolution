
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
            dataGridViewCompanies = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCompanies).BeginInit();
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
            lblFromDate.Location = new Point(17, 60);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new Size(64, 15);
            lblFromDate.TabIndex = 2;
            lblFromDate.Text = "From date:";
            // 
            // lblToDate
            // 
            lblToDate.AutoSize = true;
            lblToDate.Location = new Point(240, 60);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new Size(48, 15);
            lblToDate.TabIndex = 4;
            lblToDate.Text = "To date:";
            // 
            // btnFetchData
            // 
            btnFetchData.Enabled = false;
            btnFetchData.Location = new Point(472, 28);
            btnFetchData.Margin = new Padding(3, 2, 3, 2);
            btnFetchData.Name = "btnFetchData";
            btnFetchData.Size = new Size(82, 22);
            btnFetchData.TabIndex = 6;
            btnFetchData.Text = "Fetch Data";
            btnFetchData.UseVisualStyleBackColor = true;
            btnFetchData.Click += BtnFetchData_Click;
            // 
            // dataGridViewResult
            // 
            dataGridViewResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResult.Location = new Point(10, 81);
            dataGridViewResult.Margin = new Padding(3, 2, 3, 2);
            dataGridViewResult.Name = "dataGridViewResult";
            dataGridViewResult.RowHeadersWidth = 51;
            dataGridViewResult.Size = new Size(240, 245);
            dataGridViewResult.TabIndex = 7;
            // 
            // fromDatePicker
            // 
            fromDatePicker.Format = DateTimePickerFormat.Short;
            fromDatePicker.Location = new Point(105, 56);
            fromDatePicker.Margin = new Padding(3, 2, 3, 2);
            fromDatePicker.Name = "fromDatePicker";
            fromDatePicker.Size = new Size(130, 23);
            fromDatePicker.TabIndex = 8;
            fromDatePicker.Value = new DateTime(2024, 11, 14, 0, 0, 0, 0);
            // 
            // toDatePicker
            // 
            toDatePicker.Format = DateTimePickerFormat.Short;
            toDatePicker.Location = new Point(315, 56);
            toDatePicker.Margin = new Padding(3, 2, 3, 2);
            toDatePicker.Name = "toDatePicker";
            toDatePicker.Size = new Size(130, 23);
            toDatePicker.TabIndex = 9;
            toDatePicker.Value = new DateTime(2024, 11, 19, 10, 46, 30, 592);
            // 
            // comboBoxCompany
            // 
            comboBoxCompany.FormattingEnabled = true;
            comboBoxCompany.Location = new Point(24, 30);
            comboBoxCompany.Margin = new Padding(3, 2, 3, 2);
            comboBoxCompany.Name = "comboBoxCompany";
            comboBoxCompany.Size = new Size(211, 23);
            comboBoxCompany.TabIndex = 10;
            comboBoxCompany.SelectedIndexChanged += comboBoxCompany_SelectedIndexChanged;
            // 
            // btnGetFilenames
            // 
            btnGetFilenames.Location = new Point(560, 28);
            btnGetFilenames.Margin = new Padding(3, 2, 3, 2);
            btnGetFilenames.Name = "btnGetFilenames";
            btnGetFilenames.Size = new Size(101, 22);
            btnGetFilenames.TabIndex = 11;
            btnGetFilenames.Text = "Get Filenames";
            btnGetFilenames.UseVisualStyleBackColor = true;
            btnGetFilenames.Click += btnGetFilenames_Click;
            // 
            // dataGridViewCompanies
            // 
            dataGridViewCompanies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCompanies.Location = new Point(270, 87);
            dataGridViewCompanies.Name = "dataGridViewCompanies";
            dataGridViewCompanies.Size = new Size(486, 239);
            dataGridViewCompanies.TabIndex = 12;
            // 
            // FormApiTesting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(768, 337);
            Controls.Add(dataGridViewCompanies);
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
            ((System.ComponentModel.ISupportInitialize)dataGridViewCompanies).EndInit();
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
        private DataGridView dataGridViewCompanies;
    }
}
