namespace VoucherPrintingApp
{
    partial class Welcome
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
            btn_Ok = new Button();
            btn_Close = new Button();
            groupBox1 = new GroupBox();
            lbl_Address = new Label();
            cmb_Company = new ComboBox();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_Ok
            // 
            btn_Ok.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_Ok.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btn_Ok.Location = new Point(413, 214);
            btn_Ok.Name = "btn_Ok";
            btn_Ok.Size = new Size(129, 46);
            btn_Ok.TabIndex = 0;
            btn_Ok.Text = "OK";
            btn_Ok.UseVisualStyleBackColor = true;
            btn_Ok.Click += btn_Ok_Click;
            // 
            // btn_Close
            // 
            btn_Close.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_Close.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btn_Close.Location = new Point(559, 214);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(129, 46);
            btn_Close.TabIndex = 1;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = true;
            btn_Close.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lbl_Address);
            groupBox1.Controls.Add(cmb_Company);
            groupBox1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.Location = new Point(32, 31);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(656, 165);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Select Company";
            // 
            // lbl_Address
            // 
            lbl_Address.AutoSize = true;
            lbl_Address.Location = new Point(19, 91);
            lbl_Address.Name = "lbl_Address";
            lbl_Address.Size = new Size(54, 21);
            lbl_Address.TabIndex = 1;
            lbl_Address.Text = "label2";
            // 
            // cmb_Company
            // 
            cmb_Company.FormattingEnabled = true;
            cmb_Company.Location = new Point(19, 45);
            cmb_Company.Name = "cmb_Company";
            cmb_Company.Size = new Size(623, 29);
            cmb_Company.TabIndex = 0;
            cmb_Company.SelectedIndexChanged += cmb_Company_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(300, 10);
            label1.Name = "label1";
            label1.Size = new Size(131, 21);
            label1.TabIndex = 3;
            label1.Text = "Voucher Printing";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(516, 269);
            label3.Name = "label3";
            label3.Size = new Size(172, 15);
            label3.TabIndex = 2;
            label3.Text = "Software By Easypack Solutions";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(621, 287);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 4;
            label4.Text = "0777217731";
            // 
            // Welcome
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(722, 305);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(btn_Close);
            Controls.Add(btn_Ok);
            Name = "Welcome";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Welcome";
            Load += Welcome_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_Ok;
        private Button btn_Close;
        private GroupBox groupBox1;
        private ComboBox cmb_Company;
        private Label label1;
        private Label lbl_Address;
        private Label label3;
        private Label label4;
    }
}