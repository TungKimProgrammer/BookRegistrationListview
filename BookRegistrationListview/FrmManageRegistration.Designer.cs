namespace BookRegistrationListview
{
    partial class FrmManageRegistration
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
            this.lblRegistrationManagingTool = new System.Windows.Forms.Label();
            this.btnRemoveRegistration = new System.Windows.Forms.Button();
            this.btnRemoveRegisteredBook = new System.Windows.Forms.Button();
            this.grbRegistrations = new System.Windows.Forms.GroupBox();
            this.lviRegistrations = new System.Windows.Forms.ListView();
            this.grbCustomersAndBooks = new System.Windows.Forms.GroupBox();
            this.lviBooksAndRegDate = new System.Windows.Forms.ListView();
            this.lviCustomers = new System.Windows.Forms.ListView();
            this.grbRegistrations.SuspendLayout();
            this.grbCustomersAndBooks.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRegistrationManagingTool
            // 
            this.lblRegistrationManagingTool.AutoSize = true;
            this.lblRegistrationManagingTool.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.lblRegistrationManagingTool.Location = new System.Drawing.Point(114, 13);
            this.lblRegistrationManagingTool.Name = "lblRegistrationManagingTool";
            this.lblRegistrationManagingTool.Size = new System.Drawing.Size(395, 32);
            this.lblRegistrationManagingTool.TabIndex = 0;
            this.lblRegistrationManagingTool.Text = "Registration Managing Application";
            // 
            // btnRemoveRegistration
            // 
            this.btnRemoveRegistration.Location = new System.Drawing.Point(518, 22);
            this.btnRemoveRegistration.Name = "btnRemoveRegistration";
            this.btnRemoveRegistration.Size = new System.Drawing.Size(141, 23);
            this.btnRemoveRegistration.TabIndex = 4;
            this.btnRemoveRegistration.Text = "Remove &Registration";
            this.btnRemoveRegistration.UseVisualStyleBackColor = true;
            this.btnRemoveRegistration.Click += new System.EventHandler(this.btnRemoveRegistration_Click);
            // 
            // btnRemoveRegisteredBook
            // 
            this.btnRemoveRegisteredBook.Location = new System.Drawing.Point(498, 15);
            this.btnRemoveRegisteredBook.Name = "btnRemoveRegisteredBook";
            this.btnRemoveRegisteredBook.Size = new System.Drawing.Size(161, 23);
            this.btnRemoveRegisteredBook.TabIndex = 2;
            this.btnRemoveRegisteredBook.Text = "Remove Registered &Book";
            this.btnRemoveRegisteredBook.UseVisualStyleBackColor = true;
            this.btnRemoveRegisteredBook.Click += new System.EventHandler(this.btnRemoveRegisteredBook_Click);
            // 
            // grbRegistrations
            // 
            this.grbRegistrations.Controls.Add(this.lviRegistrations);
            this.grbRegistrations.Controls.Add(this.btnRemoveRegistration);
            this.grbRegistrations.Location = new System.Drawing.Point(12, 241);
            this.grbRegistrations.Name = "grbRegistrations";
            this.grbRegistrations.Size = new System.Drawing.Size(665, 361);
            this.grbRegistrations.TabIndex = 6;
            this.grbRegistrations.TabStop = false;
            this.grbRegistrations.Text = "Registrations";
            // 
            // lviRegistrations
            // 
            this.lviRegistrations.FullRowSelect = true;
            this.lviRegistrations.GridLines = true;
            this.lviRegistrations.Location = new System.Drawing.Point(6, 51);
            this.lviRegistrations.Name = "lviRegistrations";
            this.lviRegistrations.Size = new System.Drawing.Size(653, 304);
            this.lviRegistrations.TabIndex = 13;
            this.lviRegistrations.UseCompatibleStateImageBehavior = false;
            this.lviRegistrations.View = System.Windows.Forms.View.Details;
            this.lviRegistrations.SelectedIndexChanged += new System.EventHandler(this.lviRegistrations_SelectedIndexChanged);
            // 
            // grbCustomersAndBooks
            // 
            this.grbCustomersAndBooks.Controls.Add(this.lviBooksAndRegDate);
            this.grbCustomersAndBooks.Controls.Add(this.lviCustomers);
            this.grbCustomersAndBooks.Controls.Add(this.btnRemoveRegisteredBook);
            this.grbCustomersAndBooks.Location = new System.Drawing.Point(12, 58);
            this.grbCustomersAndBooks.Name = "grbCustomersAndBooks";
            this.grbCustomersAndBooks.Size = new System.Drawing.Size(665, 177);
            this.grbCustomersAndBooks.TabIndex = 7;
            this.grbCustomersAndBooks.TabStop = false;
            this.grbCustomersAndBooks.Text = "Customers and Registered Books";
            // 
            // lviBooksAndRegDate
            // 
            this.lviBooksAndRegDate.FullRowSelect = true;
            this.lviBooksAndRegDate.GridLines = true;
            this.lviBooksAndRegDate.Location = new System.Drawing.Point(270, 44);
            this.lviBooksAndRegDate.Name = "lviBooksAndRegDate";
            this.lviBooksAndRegDate.Size = new System.Drawing.Size(389, 127);
            this.lviBooksAndRegDate.TabIndex = 14;
            this.lviBooksAndRegDate.UseCompatibleStateImageBehavior = false;
            this.lviBooksAndRegDate.View = System.Windows.Forms.View.Details;
            this.lviBooksAndRegDate.SelectedIndexChanged += new System.EventHandler(this.lviBooksAndRegDate_SelectedIndexChanged);
            // 
            // lviCustomers
            // 
            this.lviCustomers.FullRowSelect = true;
            this.lviCustomers.GridLines = true;
            this.lviCustomers.Location = new System.Drawing.Point(6, 44);
            this.lviCustomers.Name = "lviCustomers";
            this.lviCustomers.Size = new System.Drawing.Size(253, 127);
            this.lviCustomers.TabIndex = 13;
            this.lviCustomers.UseCompatibleStateImageBehavior = false;
            this.lviCustomers.View = System.Windows.Forms.View.Details;
            this.lviCustomers.SelectedIndexChanged += new System.EventHandler(this.lviCustomers_SelectedIndexChanged);
            // 
            // FrmManageRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 613);
            this.Controls.Add(this.grbCustomersAndBooks);
            this.Controls.Add(this.grbRegistrations);
            this.Controls.Add(this.lblRegistrationManagingTool);
            this.Name = "FrmManageRegistration";
            this.Text = "Registration Manager";
            this.Load += new System.EventHandler(this.ManageRegistration_Load);
            this.grbRegistrations.ResumeLayout(false);
            this.grbCustomersAndBooks.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblRegistrationManagingTool;
        private Button btnRemoveRegistration;
        private Button btnRemoveRegisteredBook;
        private GroupBox grbRegistrations;
        private GroupBox grbCustomersAndBooks;
        private ListView lviRegistrations;
        private ListView lviBooksAndRegDate;
        private ListView lviCustomers;
    }
}