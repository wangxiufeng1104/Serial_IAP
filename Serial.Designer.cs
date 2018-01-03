namespace Serial_IAP
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.List_LoadFile = new System.Windows.Forms.ListBox();
            this.ClearFile = new System.Windows.Forms.Button();
            this.DelSelect = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 添加下载文件
            // 
            this.添加下载文件.Enabled = false;
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
            this.Com_PartName.FormattingEnabled = true;
            this.Com_PartName.Location = new System.Drawing.Point(118, 20);
            this.Com_PartName.Name = "Com_PartName";
            this.Com_PartName.Size = new System.Drawing.Size(98, 20);
            this.Com_PartName.TabIndex = 2;
            this.Com_PartName.SelectedIndexChanged += new System.EventHandler(this.Com_PartName_SelectedIndexChanged);
            this.Com_PartName.Click += new System.EventHandler(this.Com_PartName_Click);
            // 
            // 打开串口
            // 
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
            this.openFileDialog1.Filter = "程序升级文件(*.bin)|*.bin|字库文件(*.dzk)|*.dzk|图标文件(*.ico)|*.ico|所有文件|*.*";
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
            this.groupBox1.Controls.Add(this.Com_PartName);
            this.groupBox1.Controls.Add(this.打开串口);
            this.groupBox1.Location = new System.Drawing.Point(19, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 54);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "第一步：打开正确的串口";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DelSelect);
            this.groupBox2.Controls.Add(this.ClearFile);
            this.groupBox2.Controls.Add(this.List_LoadFile);
            this.groupBox2.Controls.Add(this.添加下载文件);
            this.groupBox2.Location = new System.Drawing.Point(19, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 118);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "第二步：选择文件";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.下载);
            this.groupBox3.Controls.Add(this.progressBar1);
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
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(72, 17);
            this.toolStripStatusLabel1.Text = "LoadStatus";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // List_LoadFile
            // 
            this.List_LoadFile.FormattingEnabled = true;
            this.List_LoadFile.ItemHeight = 12;
            this.List_LoadFile.Location = new System.Drawing.Point(118, 18);
            this.List_LoadFile.Name = "List_LoadFile";
            this.List_LoadFile.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.List_LoadFile.Size = new System.Drawing.Size(339, 88);
            this.List_LoadFile.TabIndex = 1;
            // 
            // ClearFile
            // 
            this.ClearFile.Location = new System.Drawing.Point(14, 82);
            this.ClearFile.Name = "ClearFile";
            this.ClearFile.Size = new System.Drawing.Size(93, 23);
            this.ClearFile.TabIndex = 2;
            this.ClearFile.Text = "清空文件";
            this.ClearFile.UseVisualStyleBackColor = true;
            this.ClearFile.Click += new System.EventHandler(this.ClearFile_Click);
            // 
            // DelSelect
            // 
            this.DelSelect.Location = new System.Drawing.Point(14, 50);
            this.DelSelect.Name = "DelSelect";
            this.DelSelect.Size = new System.Drawing.Size(93, 23);
            this.DelSelect.TabIndex = 3;
            this.DelSelect.Text = "删除选中文件";
            this.DelSelect.UseVisualStyleBackColor = true;
            this.DelSelect.Click += new System.EventHandler(this.DelSelect_Click);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button 添加下载文件;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox Com_PartName;
        private System.Windows.Forms.Button 打开串口;
        private System.Windows.Forms.Button 下载;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox List_LoadFile;
        private System.Windows.Forms.Button DelSelect;
        private System.Windows.Forms.Button ClearFile;
    }
}

