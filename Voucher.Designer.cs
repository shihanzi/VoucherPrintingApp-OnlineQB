namespace VoucherPrintingApp
{
    partial class Voucher
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
            dgv_Voucher = new DataGridView();
            lbl_VoucherPrint = new Label();
            btn_Print = new Button();
            btn_Close = new Button();
            ((System.ComponentModel.ISupportInitialize)dgv_Voucher).BeginInit();
            SuspendLayout();
            // 
            // dgv_Voucher
            // 
            dgv_Voucher.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_Voucher.Location = new Point(24, 49);
            dgv_Voucher.Name = "dgv_Voucher";
            dgv_Voucher.RowTemplate.Height = 25;
            dgv_Voucher.Size = new Size(891, 437);
            dgv_Voucher.TabIndex = 0;
            // 
            // lbl_VoucherPrint
            // 
            lbl_VoucherPrint.AutoSize = true;
            lbl_VoucherPrint.Location = new Point(419, 19);
            lbl_VoucherPrint.Name = "lbl_VoucherPrint";
            lbl_VoucherPrint.Size = new Size(95, 15);
            lbl_VoucherPrint.TabIndex = 1;
            lbl_VoucherPrint.Text = "Voucher Printing";
            // 
            // btn_Print
            // 
            btn_Print.Location = new Point(583, 502);
            btn_Print.Name = "btn_Print";
            btn_Print.Size = new Size(75, 23);
            btn_Print.TabIndex = 2;
            btn_Print.Text = "Print";
            btn_Print.UseVisualStyleBackColor = true;
            btn_Print.Click += btn_Print_Click;
            // 
            // btn_Close
            // 
            btn_Close.Location = new Point(688, 502);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(75, 23);
            btn_Close.TabIndex = 3;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = true;
            btn_Close.Click += btn_Close_Click;
            // 
            // Voucher
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(946, 540);
            Controls.Add(btn_Close);
            Controls.Add(btn_Print);
            Controls.Add(lbl_VoucherPrint);
            Controls.Add(dgv_Voucher);
            Name = "Voucher";
            Text = "Voucher";
            ((System.ComponentModel.ISupportInitialize)dgv_Voucher).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgv_Voucher;
        private Label lbl_VoucherPrint;
        private Button btn_Print;
        private Button btn_Close;
    }
}