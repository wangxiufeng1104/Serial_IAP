﻿namespace Serial_IAP
{
    partial class Serial
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Serial));
            this.添加下载文件 = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.Com_PartName = new System.Windows.Forms.ComboBox();
            this.打开串口 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.下载 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AutoLoadCheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.com_baud = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DelSelect = new System.Windows.Forms.Button();
            this.ClearFile = new System.Windows.Forms.Button();
            this.List_LoadFile = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 添加下载文件
            // 
            this.添加下载文件.Enabled = false;
            this.添加下载文件.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.添加下载文件.Location = new System.Drawing.Point(14, 18);
            this.添加下载文件.Name = "添加下载文件";
            this.添加下载文件.Size = new System.Drawing.Size(93, 23);
            this.添加下载文件.TabIndex = 0;
            this.添加下载文件.Text = "添加下载文件";
            this.添加下载文件.UseVisualStyleBackColor = true;
            this.添加下载文件.Click += new System.EventHandler(this.选择下载文件_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.ReadBufferSize = 50;
            this.serialPort1.ReadTimeout = 1000;
            this.serialPort1.WriteTimeout = 1000;
            // 
            // Com_PartName
            // 
            this.Com_PartName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Com_PartName.FormattingEnabled = true;
            this.Com_PartName.Location = new System.Drawing.Point(118, 18);
            this.Com_PartName.Name = "Com_PartName";
            this.Com_PartName.Size = new System.Drawing.Size(80, 25);
            this.Com_PartName.Sorted = true;
            this.Com_PartName.TabIndex = 2;
            this.Com_PartName.SelectedIndexChanged += new System.EventHandler(this.Com_PartName_SelectedIndexChanged);
            this.Com_PartName.Click += new System.EventHandler(this.Com_PartName_Click);
            // 
            // 打开串口
            // 
            this.打开串口.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.打开串口.Location = new System.Drawing.Point(14, 19);
            this.打开串口.Name = "打开串口";
            this.打开串口.Size = new System.Drawing.Size(93, 23);
            this.打开串口.TabIndex = 3;
            this.打开串口.Text = "打开串口";
            this.打开串口.UseVisualStyleBackColor = true;
            this.打开串口.Click += new System.EventHandler(this.打开串口_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "所有文件|*.*|程序升级文件(*.bin)|*.bin|字库文件(*.dzk)|*.dzk|图标文件(*.ico)|*.ico";
            this.openFileDialog1.Multiselect = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(118, 18);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(339, 26);
            this.progressBar1.TabIndex = 4;
            // 
            // 下载
            // 
            this.下载.Enabled = false;
            this.下载.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.下载.Location = new System.Drawing.Point(14, 20);
            this.下载.Name = "下载";
            this.下载.Size = new System.Drawing.Size(93, 23);
            this.下载.TabIndex = 5;
            this.下载.Text = "下载";
            this.下载.UseVisualStyleBackColor = true;
            this.下载.Click += new System.EventHandler(this.下载_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AutoLoadCheck);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.com_baud);
            this.groupBox1.Controls.Add(this.Com_PartName);
            this.groupBox1.Controls.Add(this.打开串口);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(19, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 54);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "第一步：打开正确的串口";
            // 
            // AutoLoadCheck
            // 
            this.AutoLoadCheck.AutoSize = true;
            this.AutoLoadCheck.Enabled = false;
            this.AutoLoadCheck.Location = new System.Drawing.Point(380, 20);
            this.AutoLoadCheck.Name = "AutoLoadCheck";
            this.AutoLoadCheck.Size = new System.Drawing.Size(75, 21);
            this.AutoLoadCheck.TabIndex = 6;
            this.AutoLoadCheck.Text = "自动下载";
            this.AutoLoadCheck.UseVisualStyleBackColor = true;
            this.AutoLoadCheck.CheckedChanged += new System.EventHandler(this.AutoLoadCheck_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(209, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "波特率";
            // 
            // com_baud
            // 
            this.com_baud.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.com_baud.FormattingEnabled = true;
            this.com_baud.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "128000",
            "230400",
            "256000",
            "460800",
            "500000",
            "512000",
            "600000",
            "750000",
            "921600",
            ""});
            this.com_baud.Location = new System.Drawing.Point(271, 18);
            this.com_baud.Name = "com_baud";
            this.com_baud.Size = new System.Drawing.Size(98, 25);
            this.com_baud.TabIndex = 4;
            this.com_baud.SelectedIndexChanged += new System.EventHandler(this.com_baud_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DelSelect);
            this.groupBox2.Controls.Add(this.ClearFile);
            this.groupBox2.Controls.Add(this.List_LoadFile);
            this.groupBox2.Controls.Add(this.添加下载文件);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(19, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 118);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "第二步：选择文件";
            // 
            // DelSelect
            // 
            this.DelSelect.Enabled = false;
            this.DelSelect.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DelSelect.Location = new System.Drawing.Point(14, 50);
            this.DelSelect.Name = "DelSelect";
            this.DelSelect.Size = new System.Drawing.Size(93, 23);
            this.DelSelect.TabIndex = 3;
            this.DelSelect.Text = "删除选中文件";
            this.DelSelect.UseVisualStyleBackColor = true;
            this.DelSelect.Click += new System.EventHandler(this.DelSelect_Click);
            // 
            // ClearFile
            // 
            this.ClearFile.Enabled = false;
            this.ClearFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClearFile.Location = new System.Drawing.Point(14, 82);
            this.ClearFile.Name = "ClearFile";
            this.ClearFile.Size = new System.Drawing.Size(93, 23);
            this.ClearFile.TabIndex = 2;
            this.ClearFile.Text = "清空文件";
            this.ClearFile.UseVisualStyleBackColor = true;
            this.ClearFile.Click += new System.EventHandler(this.ClearFile_Click);
            // 
            // List_LoadFile
            // 
            this.List_LoadFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.List_LoadFile.FormattingEnabled = true;
            this.List_LoadFile.ItemHeight = 17;
            this.List_LoadFile.Location = new System.Drawing.Point(118, 18);
            this.List_LoadFile.Name = "List_LoadFile";
            this.List_LoadFile.ScrollAlwaysVisible = true;
            this.List_LoadFile.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.List_LoadFile.Size = new System.Drawing.Size(339, 72);
            this.List_LoadFile.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.下载);
            this.groupBox3.Controls.Add(this.progressBar1);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(19, 226);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(463, 54);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "第三步：下载";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 313);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(494, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(72, 17);
            this.toolStripStatusLabel1.Text = "LoadStatus";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // Serial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 335);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Serial";
            this.Text = "TDO IAP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Serial_FormClosing);
            this.Load += new System.EventHandler(this.Serial_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button 添加下载文件;
        private System.Windows.Forms.ComboBox Com_PartName;
        private System.Windows.Forms.Button 打开串口;
        private System.Windows.Forms.Button 下载;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button DelSelect;
        private System.Windows.Forms.Button ClearFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox com_baud;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        public System.Windows.Forms.ListBox List_LoadFile;
        public System.IO.Ports.SerialPort serialPort1;
        public System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.CheckBox AutoLoadCheck;
    }
}

