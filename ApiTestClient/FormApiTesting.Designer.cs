
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
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).BeginInit();
            SuspendLayout();
            // 
            // lblAPIkey
            // 
            lblAPIkey.AutoSize = true;
            lblAPIkey.Location = new Point(12, 9);
            lblAPIkey.Name = "lblAPIkey";
            lblAPIkey.Size = new Size(98, 20);
            lblAPIkey.TabIndex = 0;
            lblAPIkey.Text = "API key input:";
            // 
            // textBoxApiKey
            // 
            textBoxApiKey.Location = new Point(113, 6);
            textBoxApiKey.Name = "textBoxApiKey";
            textBoxApiKey.ReadOnly = true;
            textBoxApiKey.Size = new Size(541, 27);
            textBoxApiKey.TabIndex = 1;
            textBoxApiKey.Text = "ebt5bhbh98c-2a4ta3-4ucq83-9ovrb4-fb99l4aqbr-6bbqbdb";
            // 
            // lblFromDate
            // 
            lblFromDate.AutoSize = true;
            lblFromDate.Location = new Point(19, 80);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new Size(80, 20);
            lblFromDate.TabIndex = 2;
            lblFromDate.Text = "From date:";
            // 
            // lblToDate
            // 
            lblToDate.AutoSize = true;
            lblToDate.Location = new Point(274, 80);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new Size(62, 20);
            lblToDate.TabIndex = 4;
            lblToDate.Text = "To date:";
            // 
            // btnFetchData
            // 
            btnFetchData.Enabled = false;
            btnFetchData.Location = new Point(539, 37);
            btnFetchData.Name = "btnFetchData";
            btnFetchData.Size = new Size(94, 29);
            btnFetchData.TabIndex = 6;
            btnFetchData.Text = "Fetch Data";
            btnFetchData.UseVisualStyleBackColor = true;
            btnFetchData.Click += BtnFetchData_Click;
            // 
            // dataGridViewResult
            // 
            dataGridViewResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResult.Location = new Point(12, 108);
            dataGridViewResult.Name = "dataGridViewResult";
            dataGridViewResult.RowHeadersWidth = 51;
            dataGridViewResult.Size = new Size(855, 331);
            dataGridViewResult.TabIndex = 7;
            // 
            // fromDatePicker
            // 
            fromDatePicker.Format = DateTimePickerFormat.Short;
            fromDatePicker.Location = new Point(120, 75);
            fromDatePicker.Name = "fromDatePicker";
            fromDatePicker.Size = new Size(148, 27);
            fromDatePicker.TabIndex = 8;
            fromDatePicker.Value = new DateTime(2024, 11, 14, 0, 0, 0, 0);
            // 
            // toDatePicker
            // 
            toDatePicker.Format = DateTimePickerFormat.Short;
            toDatePicker.Location = new Point(360, 75);
            toDatePicker.Name = "toDatePicker";
            toDatePicker.Size = new Size(148, 27);
            toDatePicker.TabIndex = 9;
            toDatePicker.Value = new DateTime(2024, 11, 19, 10, 46, 30, 592);
            // 
            // comboBoxCompany
            // 
            comboBoxCompany.FormattingEnabled = true;
            comboBoxCompany.Items.AddRange(new object[] { 
                "36206075/CONSULTING, s.r.o.", 
                "47444525/CORSO REAL s.r.o.", 
                "52345386/DUDISTAV GROUP s.r.o.", 
                "52736491/MN invest s.r.o.", 
                "47449683/SCHOOL EDUCATION, s.r.o.", 
                "36590045/Farma Komaničan s.r.o.", 
                "51250055/Hokejový klub Dukla Ingema Michalovce", 
                "30295068/Ján Brecko", 
                "46440500/TIM BER HOUSE s.r.o." , 
                "45696829/STAVIMPEX LOGISTIK, s.r.o.", 
                "50222805/RTmont s.r.o.",
                "50635212/Brány Benedek s.r.o.",
                "51762803/Jazdecký klub Klokočina o.z.",
                "52345386/DUDISTAV GROUP s.r.o.",
                "50540483/paint horses, s.r.o.",
                "31719741/V a V s.r.p.",
                "54280923/Wood & House s. r. o.",
                "42104629/Ing. Slávka Molčanyiová, PhD.",
                "45534802/CONSULTING A&T",
                "55380166/4BE Group s. r. o.",
                "51212595/BigByt s.r.o.",
                "51454467/STAVFEX s.r.o.",
                "45696829/STAVIMPEX LOGISTIK, s.r.o.",
                "50914871/Humbol s.r.o.",
                "53753666/PALWOOD s.r.o.",
                "54369827/ŽEN&MAS s. r. o.",
                "50836471/EMM STAV s.r.o.",
                "50200453/Wellness štúdio s.r.o",
                "50096168/Group ML, s.r.o.",
                "52811417/RSD SK, s.r.o.",
                "46102116/VID s.r.o.",
                "36590045/FarmaKomanican",
                "50960652/R Strechy s.r.o.",
                "51947803/EDSTREX s.r.o."
            });
            comboBoxCompany.Location = new Point(27, 41);
            comboBoxCompany.Name = "comboBoxCompany";
            comboBoxCompany.Size = new Size(241, 28);
            comboBoxCompany.TabIndex = 10;
            comboBoxCompany.SelectedIndexChanged += comboBoxCompany_SelectedIndexChanged;
            // 
            // FormApiTesting
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 449);
            Controls.Add(comboBoxCompany);
            Controls.Add(toDatePicker);
            Controls.Add(fromDatePicker);
            Controls.Add(dataGridViewResult);
            Controls.Add(btnFetchData);
            Controls.Add(lblToDate);
            Controls.Add(lblFromDate);
            Controls.Add(textBoxApiKey);
            Controls.Add(lblAPIkey);
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
    }
}
