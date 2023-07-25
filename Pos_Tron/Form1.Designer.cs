namespace Pos_Tron
{
    partial class Form1
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
            this.PanelOrder = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TotalPrice = new System.Windows.Forms.TextBox();
            this.panelChotDon = new System.Windows.Forms.Panel();
            this.labelDaBan = new System.Windows.Forms.Label();
            this.btnChotDon = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.totalSold = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.PanelOrder.SuspendLayout();
            this.panelChotDon.SuspendLayout();
            this.PanelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelOrder
            // 
            this.PanelOrder.AutoScroll = true;
            this.PanelOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelOrder.Controls.Add(this.btnClear);
            this.PanelOrder.Controls.Add(this.label1);
            this.PanelOrder.Location = new System.Drawing.Point(647, 109);
            this.PanelOrder.Name = "PanelOrder";
            this.PanelOrder.Size = new System.Drawing.Size(454, 289);
            this.PanelOrder.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(333, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(99, 33);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(176, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 36);
            this.label1.TabIndex = 17;
            this.label1.Text = "Order";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(643, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 36);
            this.label2.TabIndex = 21;
            this.label2.Text = "Tổng cộng";
            // 
            // TotalPrice
            // 
            this.TotalPrice.Location = new System.Drawing.Point(923, 413);
            this.TotalPrice.Name = "TotalPrice";
            this.TotalPrice.ReadOnly = true;
            this.TotalPrice.Size = new System.Drawing.Size(178, 32);
            this.TotalPrice.TabIndex = 21;
            this.TotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelChotDon
            // 
            this.panelChotDon.AutoScroll = true;
            this.panelChotDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChotDon.Controls.Add(this.labelDaBan);
            this.panelChotDon.Location = new System.Drawing.Point(649, 511);
            this.panelChotDon.Name = "panelChotDon";
            this.panelChotDon.Size = new System.Drawing.Size(277, 246);
            this.panelChotDon.TabIndex = 22;
            // 
            // labelDaBan
            // 
            this.labelDaBan.AutoSize = true;
            this.labelDaBan.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDaBan.Location = new System.Drawing.Point(87, 9);
            this.labelDaBan.Name = "labelDaBan";
            this.labelDaBan.Size = new System.Drawing.Size(114, 36);
            this.labelDaBan.TabIndex = 24;
            this.labelDaBan.Text = "Đã bán";
            // 
            // btnChotDon
            // 
            this.btnChotDon.Location = new System.Drawing.Point(998, 451);
            this.btnChotDon.Name = "btnChotDon";
            this.btnChotDon.Size = new System.Drawing.Size(104, 32);
            this.btnChotDon.TabIndex = 23;
            this.btnChotDon.Text = "Chốt đơn";
            this.btnChotDon.UseVisualStyleBackColor = true;
            this.btnChotDon.Click += new System.EventHandler(this.btnChotDon_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(991, 672);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 36);
            this.label3.TabIndex = 25;
            this.label3.Text = "Tổng ly";
            // 
            // totalSold
            // 
            this.totalSold.Location = new System.Drawing.Point(997, 725);
            this.totalSold.Name = "totalSold";
            this.totalSold.ReadOnly = true;
            this.totalSold.Size = new System.Drawing.Size(104, 32);
            this.totalSold.TabIndex = 26;
            this.totalSold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(252, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 36);
            this.label4.TabIndex = 7;
            this.label4.Text = "Menu";
            // 
            // PanelMenu
            // 
            this.PanelMenu.AutoScroll = true;
            this.PanelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelMenu.Controls.Add(this.label4);
            this.PanelMenu.Location = new System.Drawing.Point(10, 109);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(631, 648);
            this.PanelMenu.TabIndex = 1;
            // 
            // panelLogo
            // 
            this.panelLogo.Location = new System.Drawing.Point(391, 12);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(400, 80);
            this.panelLogo.TabIndex = 27;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 882);
            this.Controls.Add(this.panelLogo);
            this.Controls.Add(this.totalSold);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnChotDon);
            this.Controls.Add(this.panelChotDon);
            this.Controls.Add(this.TotalPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PanelOrder);
            this.Controls.Add(this.PanelMenu);
            this.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Form1";
            this.Text = "POS Tròn";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.PanelOrder.ResumeLayout(false);
            this.PanelOrder.PerformLayout();
            this.panelChotDon.ResumeLayout(false);
            this.panelChotDon.PerformLayout();
            this.PanelMenu.ResumeLayout(false);
            this.PanelMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel PanelOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TotalPrice;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panelChotDon;
        private System.Windows.Forms.Button btnChotDon;
        private System.Windows.Forms.Label labelDaBan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox totalSold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.Panel panelLogo;
    }
}

