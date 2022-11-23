namespace BookRegistrationListview
{
    partial class FrmBookRegistration
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
            this.cbxCustomerName = new System.Windows.Forms.ComboBox();
            this.cbxBookTitle = new System.Windows.Forms.ComboBox();
            this.dtpRegistration = new System.Windows.Forms.DateTimePicker();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblBookRegistration = new System.Windows.Forms.Label();
            this.btnManageCustomer = new System.Windows.Forms.Button();
            this.btnManageBook = new System.Windows.Forms.Button();
            this.btnManageRegistration = new System.Windows.Forms.Button();
            this.lblErrMsg = new System.Windows.Forms.Label();
            this.lblCustomers = new System.Windows.Forms.Label();
            this.lblBooks = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxCustomerName
            // 
            this.cbxCustomerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCustomerName.FormattingEnabled = true;
            this.cbxCustomerName.Location = new System.Drawing.Point(38, 79);
            this.cbxCustomerName.Name = "cbxCustomerName";
            this.cbxCustomerName.Size = new System.Drawing.Size(200, 23);
            this.cbxCustomerName.TabIndex = 0;
            this.cbxCustomerName.Tag = "";
            this.cbxCustomerName.SelectedIndexChanged += new System.EventHandler(this.cbxCustomerName_SelectedIndexChanged);
            // 
            // cbxBookTitle
            // 
            this.cbxBookTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBookTitle.FormattingEnabled = true;
            this.cbxBookTitle.Location = new System.Drawing.Point(38, 135);
            this.cbxBookTitle.Name = "cbxBookTitle";
            this.cbxBookTitle.Size = new System.Drawing.Size(200, 23);
            this.cbxBookTitle.TabIndex = 1;
            this.cbxBookTitle.SelectedIndexChanged += new System.EventHandler(this.cbxBookTitle_SelectedIndexChanged);
            // 
            // dtpRegistration
            // 
            this.dtpRegistration.Location = new System.Drawing.Point(38, 191);
            this.dtpRegistration.Name = "dtpRegistration";
            this.dtpRegistration.Size = new System.Drawing.Size(200, 23);
            this.dtpRegistration.TabIndex = 2;
            this.dtpRegistration.ValueChanged += new System.EventHandler(this.dtpRegistration_ValueChanged);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(38, 253);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(200, 62);
            this.btnRegister.TabIndex = 3;
            this.btnRegister.Text = "&Register Book";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblBookRegistration
            // 
            this.lblBookRegistration.AutoSize = true;
            this.lblBookRegistration.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.lblBookRegistration.Location = new System.Drawing.Point(132, 22);
            this.lblBookRegistration.Name = "lblBookRegistration";
            this.lblBookRegistration.Size = new System.Drawing.Size(206, 32);
            this.lblBookRegistration.TabIndex = 0;
            this.lblBookRegistration.Text = "Book Registration";
            // 
            // btnManageCustomer
            // 
            this.btnManageCustomer.Location = new System.Drawing.Point(283, 79);
            this.btnManageCustomer.Name = "btnManageCustomer";
            this.btnManageCustomer.Size = new System.Drawing.Size(153, 61);
            this.btnManageCustomer.TabIndex = 4;
            this.btnManageCustomer.Text = "Manage &Customer";
            this.btnManageCustomer.UseVisualStyleBackColor = true;
            this.btnManageCustomer.Click += new System.EventHandler(this.btnManageCustomer_Click);
            // 
            // btnManageBook
            // 
            this.btnManageBook.Location = new System.Drawing.Point(283, 166);
            this.btnManageBook.Name = "btnManageBook";
            this.btnManageBook.Size = new System.Drawing.Size(153, 61);
            this.btnManageBook.TabIndex = 5;
            this.btnManageBook.Text = "Manage &Book";
            this.btnManageBook.UseVisualStyleBackColor = true;
            this.btnManageBook.Click += new System.EventHandler(this.btnManageBook_Click);
            // 
            // btnManageRegistration
            // 
            this.btnManageRegistration.Location = new System.Drawing.Point(283, 253);
            this.btnManageRegistration.Name = "btnManageRegistration";
            this.btnManageRegistration.Size = new System.Drawing.Size(153, 61);
            this.btnManageRegistration.TabIndex = 6;
            this.btnManageRegistration.Text = "Manage Registr&ation";
            this.btnManageRegistration.UseVisualStyleBackColor = true;
            this.btnManageRegistration.Click += new System.EventHandler(this.btnManageRegistration_Click);
            // 
            // lblErrMsg
            // 
            this.lblErrMsg.AutoSize = true;
            this.lblErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lblErrMsg.Location = new System.Drawing.Point(38, 224);
            this.lblErrMsg.Name = "lblErrMsg";
            this.lblErrMsg.Size = new System.Drawing.Size(0, 15);
            this.lblErrMsg.TabIndex = 8;
            // 
            // lblCustomers
            // 
            this.lblCustomers.AutoSize = true;
            this.lblCustomers.Location = new System.Drawing.Point(38, 61);
            this.lblCustomers.Name = "lblCustomers";
            this.lblCustomers.Size = new System.Drawing.Size(64, 15);
            this.lblCustomers.TabIndex = 9;
            this.lblCustomers.Text = "Customers";
            // 
            // lblBooks
            // 
            this.lblBooks.AutoSize = true;
            this.lblBooks.Location = new System.Drawing.Point(38, 117);
            this.lblBooks.Name = "lblBooks";
            this.lblBooks.Size = new System.Drawing.Size(39, 15);
            this.lblBooks.TabIndex = 10;
            this.lblBooks.Text = "Books";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(38, 173);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(31, 15);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "Date";
            // 
            // FrmBookRegistration
            // 
            this.AcceptButton = this.btnRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 354);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblBooks);
            this.Controls.Add(this.lblCustomers);
            this.Controls.Add(this.lblErrMsg);
            this.Controls.Add(this.btnManageRegistration);
            this.Controls.Add(this.btnManageBook);
            this.Controls.Add(this.btnManageCustomer);
            this.Controls.Add(this.lblBookRegistration);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.dtpRegistration);
            this.Controls.Add(this.cbxBookTitle);
            this.Controls.Add(this.cbxCustomerName);
            this.Name = "FrmBookRegistration";
            this.Text = "Book Registration";
            this.Load += new System.EventHandler(this.FrmBookRegistration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox cbxCustomerName;
        private ComboBox cbxBookTitle;
        private DateTimePicker dtpRegistration;
        private Button btnRegister;
        private Label lblBookRegistration;
        private Button btnManageCustomer;
        private Button btnManageBook;
        private Button btnCancel;
        private Button btnManageRegistration;
        private Label lblErrMsg;
        private Label lblCustomers;
        private Label lblBooks;
        private Label lblDate;
    }
}