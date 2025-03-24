
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
            dataGridViewResult.Size = new Size(748, 248);
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
            comboBoxCompany.Items.AddRange(new object[] { "55380166/4BE Group s. r. o.", "47551224/AgroGaran s.r.o.", "51212595/BigByt s.r.o.", "50635212/Brány Benedek s.r.o.", "45534802/CONSULTING A&T", "36206075/CONSULTING, s.r.o.", "47444525/CORSO REAL s.r.o.", "55638384/Davina&Simonic, s.r.o.", "52345386/DUDISTAV GROUP s.r.o.", "51947803/EDSTREX s.r.o.", "50836471/EMM STAV s.r.o.", "36590045/Farma Komaničan s.r.o.", "50096168/Group ML, s.r.o.", "51250055/Hokejový klub Dukla Ingema Michalovce", "50914871/Humbol s.r.o.", "42104629/Ing. Slávka Molčanyiová, PhD.", "30295068/Ján Brecko", "51762803/Jazdecký klub Klokočina o.z.", "50180321/JOPAS", "44726732/KobraI s.r.o.", "52736491/MN invest s.r.o.", "50540483/paint horses, s.r.o.", "53753666/PALWOOD s.r.o.", "50960652/R Strechy s.r.o.", "52811417/RSD SK, s.r.o.", "50222805/RTmont s.r.o.", "47504170/SAMA-PL s.r.o.", "52761754/SATOgroup s.r.o.", "47449683/SCHOOL EDUCATION, s.r.o.", "36568872/SLOVKARTON", "51454467/STAVFEX s.r.o.", "45696829/STAVIMPEX LOGISTIK, s.r.o.", "51077485/SZILBER s.r.o.", "46440500/TIM BER HOUSE s.r.o.", "31719741/V a V s.r.p.", "46102116/VID s.r.o.", "50200453/Wellness štúdio s.r.o", "54280923/Wood & House s. r. o.", "54369827/ŽEN&MAS s. r. o." });
            comboBoxCompany.Location = new Point(24, 31);
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
            btnGetFilenames.Size = new Size(82, 22);
            btnGetFilenames.TabIndex = 11;
            btnGetFilenames.Text = "Get Filenames";
            btnGetFilenames.UseVisualStyleBackColor = true;
            btnGetFilenames.Click += btnGetFilenames_Click;
            // 
            // FormApiTesting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(768, 337);
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
    }
}
