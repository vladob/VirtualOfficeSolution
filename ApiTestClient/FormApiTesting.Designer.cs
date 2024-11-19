
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
            lblFromDate.Location = new Point(12, 42);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new Size(80, 20);
            lblFromDate.TabIndex = 2;
            lblFromDate.Text = "From date:";
            // 
            // lblToDate
            // 
            lblToDate.AutoSize = true;
            lblToDate.Location = new Point(272, 42);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new Size(62, 20);
            lblToDate.TabIndex = 4;
            lblToDate.Text = "To date:";
            // 
            // btnFetchData
            // 
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
            dataGridViewResult.Location = new Point(12, 72);
            dataGridViewResult.Name = "dataGridViewResult";
            dataGridViewResult.RowHeadersWidth = 51;
            dataGridViewResult.Size = new Size(642, 355);
            dataGridViewResult.TabIndex = 7;
            // 
            // fromDatePicker
            // 
            fromDatePicker.Format = DateTimePickerFormat.Short;
            fromDatePicker.Location = new Point(113, 37);
            fromDatePicker.Name = "fromDatePicker";
            fromDatePicker.Size = new Size(148, 27);
            fromDatePicker.TabIndex = 8;
            fromDatePicker.Value = new DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // toDatePicker
            // 
            toDatePicker.Format = DateTimePickerFormat.Short;
            toDatePicker.Location = new Point(358, 37);
            toDatePicker.Name = "toDatePicker";
            toDatePicker.Size = new Size(148, 27);
            toDatePicker.TabIndex = 9;
            toDatePicker.Value = DateTime.Now;
            // 
            // FormApiTesting
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(665, 437);
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
    }
}
