namespace DIP
{
    partial class DIPSample
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBtoGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.亮度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上下翻轉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.左右翻轉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.旋轉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.度ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.直方圖等化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任意大小縮放ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.鄰近ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.內插ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任意角度的旋轉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.濾波器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高通濾波器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobel濾波器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prewitt濾波器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.低通濾波器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.平均濾波器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高斯濾波器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.影像銳化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otuss分割ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.說明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stStripLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 409);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(876, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stStripLabel
            // 
            this.stStripLabel.Name = "stStripLabel";
            this.stStripLabel.Size = new System.Drawing.Size(158, 19);
            this.stStripLabel.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.iPToolStripMenuItem,
            this.說明ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(876, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // iPToolStripMenuItem
            // 
            this.iPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rGBtoGrayToolStripMenuItem,
            this.rotateToolStripMenuItem,
            this.亮度ToolStripMenuItem,
            this.上下翻轉ToolStripMenuItem,
            this.左右翻轉ToolStripMenuItem,
            this.旋轉ToolStripMenuItem,
            this.直方圖等化ToolStripMenuItem,
            this.任意大小縮放ToolStripMenuItem,
            this.任意角度的旋轉ToolStripMenuItem,
            this.濾波器ToolStripMenuItem,
            this.otuss分割ToolStripMenuItem});
            this.iPToolStripMenuItem.Name = "iPToolStripMenuItem";
            this.iPToolStripMenuItem.Size = new System.Drawing.Size(36, 24);
            this.iPToolStripMenuItem.Text = "&IP";
            // 
            // rGBtoGrayToolStripMenuItem
            // 
            this.rGBtoGrayToolStripMenuItem.Name = "rGBtoGrayToolStripMenuItem";
            this.rGBtoGrayToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.rGBtoGrayToolStripMenuItem.Text = "負片";
            this.rGBtoGrayToolStripMenuItem.Click += new System.EventHandler(this.rGBtoGrayToolStripMenuItem_Click);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.rotateToolStripMenuItem.Text = "對比";
            this.rotateToolStripMenuItem.Click += new System.EventHandler(this.rotateToolStripMenuItem_Click);
            // 
            // 亮度ToolStripMenuItem
            // 
            this.亮度ToolStripMenuItem.Name = "亮度ToolStripMenuItem";
            this.亮度ToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.亮度ToolStripMenuItem.Text = "亮度";
            this.亮度ToolStripMenuItem.Click += new System.EventHandler(this.亮度ToolStripMenuItem_Click);
            // 
            // 上下翻轉ToolStripMenuItem
            // 
            this.上下翻轉ToolStripMenuItem.Name = "上下翻轉ToolStripMenuItem";
            this.上下翻轉ToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.上下翻轉ToolStripMenuItem.Text = "上下翻轉";
            this.上下翻轉ToolStripMenuItem.Click += new System.EventHandler(this.上下翻轉ToolStripMenuItem_Click);
            // 
            // 左右翻轉ToolStripMenuItem
            // 
            this.左右翻轉ToolStripMenuItem.Name = "左右翻轉ToolStripMenuItem";
            this.左右翻轉ToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.左右翻轉ToolStripMenuItem.Text = "左右翻轉";
            this.左右翻轉ToolStripMenuItem.Click += new System.EventHandler(this.左右翻轉ToolStripMenuItem_Click);
            // 
            // 旋轉ToolStripMenuItem
            // 
            this.旋轉ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.度ToolStripMenuItem,
            this.度ToolStripMenuItem1});
            this.旋轉ToolStripMenuItem.Name = "旋轉ToolStripMenuItem";
            this.旋轉ToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.旋轉ToolStripMenuItem.Text = "旋轉";
            // 
            // 度ToolStripMenuItem
            // 
            this.度ToolStripMenuItem.Name = "度ToolStripMenuItem";
            this.度ToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.度ToolStripMenuItem.Text = "90度";
            this.度ToolStripMenuItem.Click += new System.EventHandler(this.度ToolStripMenuItem_Click);
            // 
            // 度ToolStripMenuItem1
            // 
            this.度ToolStripMenuItem1.Name = "度ToolStripMenuItem1";
            this.度ToolStripMenuItem1.Size = new System.Drawing.Size(134, 26);
            this.度ToolStripMenuItem1.Text = "270度";
            this.度ToolStripMenuItem1.Click += new System.EventHandler(this.度ToolStripMenuItem1_Click);
            // 
            // 直方圖等化ToolStripMenuItem
            // 
            this.直方圖等化ToolStripMenuItem.Name = "直方圖等化ToolStripMenuItem";
            this.直方圖等化ToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.直方圖等化ToolStripMenuItem.Text = "直方圖等化";
            this.直方圖等化ToolStripMenuItem.Click += new System.EventHandler(this.直方圖等化ToolStripMenuItem_Click);
            // 
            // 任意大小縮放ToolStripMenuItem
            // 
            this.任意大小縮放ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.鄰近ToolStripMenuItem,
            this.內插ToolStripMenuItem});
            this.任意大小縮放ToolStripMenuItem.Name = "任意大小縮放ToolStripMenuItem";
            this.任意大小縮放ToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.任意大小縮放ToolStripMenuItem.Text = "任意大小縮放";
            // 
            // 鄰近ToolStripMenuItem
            // 
            this.鄰近ToolStripMenuItem.Name = "鄰近ToolStripMenuItem";
            this.鄰近ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.鄰近ToolStripMenuItem.Text = "鄰近";
            this.鄰近ToolStripMenuItem.Click += new System.EventHandler(this.鄰近ToolStripMenuItem_Click);
            // 
            // 內插ToolStripMenuItem
            // 
            this.內插ToolStripMenuItem.Name = "內插ToolStripMenuItem";
            this.內插ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.內插ToolStripMenuItem.Text = "內插";
            this.內插ToolStripMenuItem.Click += new System.EventHandler(this.內插ToolStripMenuItem_Click);
            // 
            // 任意角度的旋轉ToolStripMenuItem
            // 
            this.任意角度的旋轉ToolStripMenuItem.Name = "任意角度的旋轉ToolStripMenuItem";
            this.任意角度的旋轉ToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.任意角度的旋轉ToolStripMenuItem.Text = "任意角度的旋轉";
            this.任意角度的旋轉ToolStripMenuItem.Click += new System.EventHandler(this.任意角度的旋轉ToolStripMenuItem_Click);
            // 
            // 濾波器ToolStripMenuItem
            // 
            this.濾波器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.高通濾波器ToolStripMenuItem,
            this.低通濾波器ToolStripMenuItem,
            this.影像銳化ToolStripMenuItem});
            this.濾波器ToolStripMenuItem.Name = "濾波器ToolStripMenuItem";
            this.濾波器ToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.濾波器ToolStripMenuItem.Text = "濾波器";
            this.濾波器ToolStripMenuItem.Click += new System.EventHandler(this.濾波器ToolStripMenuItem_Click);
            // 
            // 高通濾波器ToolStripMenuItem
            // 
            this.高通濾波器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sobel濾波器ToolStripMenuItem,
            this.prewitt濾波器ToolStripMenuItem});
            this.高通濾波器ToolStripMenuItem.Name = "高通濾波器ToolStripMenuItem";
            this.高通濾波器ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.高通濾波器ToolStripMenuItem.Text = "高通濾波器";
            // 
            // sobel濾波器ToolStripMenuItem
            // 
            this.sobel濾波器ToolStripMenuItem.Name = "sobel濾波器ToolStripMenuItem";
            this.sobel濾波器ToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.sobel濾波器ToolStripMenuItem.Text = "Sobel濾波器";
            this.sobel濾波器ToolStripMenuItem.Click += new System.EventHandler(this.sobel濾波器ToolStripMenuItem_Click);
            // 
            // prewitt濾波器ToolStripMenuItem
            // 
            this.prewitt濾波器ToolStripMenuItem.Name = "prewitt濾波器ToolStripMenuItem";
            this.prewitt濾波器ToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.prewitt濾波器ToolStripMenuItem.Text = "Prewitt濾波器";
            this.prewitt濾波器ToolStripMenuItem.Click += new System.EventHandler(this.prewitt濾波器ToolStripMenuItem_Click);
            // 
            // 低通濾波器ToolStripMenuItem
            // 
            this.低通濾波器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.平均濾波器ToolStripMenuItem,
            this.高斯濾波器ToolStripMenuItem});
            this.低通濾波器ToolStripMenuItem.Name = "低通濾波器ToolStripMenuItem";
            this.低通濾波器ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.低通濾波器ToolStripMenuItem.Text = "低通濾波器";
            // 
            // 平均濾波器ToolStripMenuItem
            // 
            this.平均濾波器ToolStripMenuItem.Name = "平均濾波器ToolStripMenuItem";
            this.平均濾波器ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.平均濾波器ToolStripMenuItem.Text = "平均濾波器";
            this.平均濾波器ToolStripMenuItem.Click += new System.EventHandler(this.平均濾波器ToolStripMenuItem_Click);
            // 
            // 高斯濾波器ToolStripMenuItem
            // 
            this.高斯濾波器ToolStripMenuItem.Name = "高斯濾波器ToolStripMenuItem";
            this.高斯濾波器ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.高斯濾波器ToolStripMenuItem.Text = "高斯濾波器";
            this.高斯濾波器ToolStripMenuItem.Click += new System.EventHandler(this.高斯濾波器ToolStripMenuItem_Click);
            // 
            // 影像銳化ToolStripMenuItem
            // 
            this.影像銳化ToolStripMenuItem.Name = "影像銳化ToolStripMenuItem";
            this.影像銳化ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.影像銳化ToolStripMenuItem.Text = "影像銳化";
            this.影像銳化ToolStripMenuItem.Click += new System.EventHandler(this.影像銳化ToolStripMenuItem_Click);
            // 
            // otuss分割ToolStripMenuItem
            // 
            this.otuss分割ToolStripMenuItem.Name = "otuss分割ToolStripMenuItem";
            this.otuss分割ToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.otuss分割ToolStripMenuItem.Text = "Otus\'s 分割";
            this.otuss分割ToolStripMenuItem.Click += new System.EventHandler(this.otuss分割ToolStripMenuItem_Click);
            // 
            // oFileDlg
            // 
            this.oFileDlg.FileName = "openFileDialog1";
            // 
            // 說明ToolStripMenuItem
            // 
            this.說明ToolStripMenuItem.Name = "說明ToolStripMenuItem";
            this.說明ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.說明ToolStripMenuItem.Text = "說明";
            this.說明ToolStripMenuItem.Click += new System.EventHandler(this.說明ToolStripMenuItem_Click);
            // 
            // DIPSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 434);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DIPSample";
            this.Text = "數位影像處理";
            this.Load += new System.EventHandler(this.DIPSample_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stStripLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog oFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem rGBtoGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 亮度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上下翻轉ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 左右翻轉ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 旋轉ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 度ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 任意大小縮放ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 鄰近ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 內插ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 任意角度的旋轉ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直方圖等化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 濾波器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高通濾波器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobel濾波器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prewitt濾波器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 低通濾波器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 平均濾波器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高斯濾波器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 影像銳化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otuss分割ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 說明ToolStripMenuItem;
    }
}