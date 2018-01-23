using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace Serial_IAP
{
    public enum Restype
    {
        NONE = 0x00,
        dzk,
        ASCII16 = 0x10,
        ASCII24,
        ASCII32,
        ASCII48,
        GB2312_16 = 0x20,
        GB2312_24,
        GB2312_32,
        GB2312_48,
        KO_16 = 0x30,
        KO_24,
        KO_32,
        KO_48,
        ico = 0x50,
        bin
    }
    public partial  class Serial : Form
    {
        private Thread C_Monitor_Thread;   //串口监听线程
        public bool IsLoading = false;
        public Thread ThreadDataHandle;
        Datahandle datahandle;
        public static List<FileInfo> filelist = new List<FileInfo> { };
        
        
        public Restype restype = Restype.NONE;
        public static Serial SerialSingle = null;
        public static Serial GetSingle()
        {
            if (SerialSingle == null)
            {
                SerialSingle = new Serial();

            }
            return SerialSingle;
        }
        public Serial()
        {
            SerialSingle = this;
            InitializeComponent();
        }

        private void Com_PartName_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                打开串口.Text = "打开串口";
            }

            //RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            //if(keyCom != null)
            //{
            //    string[] sSubKeys = keyCom.GetValueNames();
            //    Com_PartName.Items.Clear();
            //    foreach (string sName in sSubKeys)
            //    {
            //        string sValue = (string)keyCom.GetValue(sName);
            //        Com_PartName.Items.Add(sValue);
            //    }
            //}
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                toolStripStatusLabel3.Text = "Error: No Serial";
                return;
            }
            Com_PartName.Items.Clear();
            foreach (string s in str)
            {
                Com_PartName.Items.Add(s);
            }
            if (Com_PartName.Items.Count > 0)
            {
                Com_PartName.SelectedIndex = 0;
            }
        }
        private void 打开串口_Click(object sender, EventArgs e)
        {
            if(!serialPort1.IsOpen && Com_PartName.SelectedText != null)
            {
                try
                {
                    serialPort1.PortName = Com_PartName.Text;
                    serialPort1.Open();
                    打开串口.Text = "关闭串口";
                    添加下载文件.Enabled = true;
                    DelSelect.Enabled = true;
                    ClearFile.Enabled = true;
                    下载.Enabled = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //成功打开串口后创建串口监听线程
                //C_Monitor_Thread = new Thread(new ThreadStart(Check_Common));
                //C_Monitor_Thread.Start();
            }
            else
            {
                serialPort1.Close();
                if(serialPort1.IsOpen == false)
                    打开串口.Text = "打开串口";
                
                添加下载文件.Enabled = false;
                DelSelect.Enabled = false;
                ClearFile.Enabled = false;
                下载.Enabled = false;
            }
        }
        delegate void MyInvoke(bool IsAuto); //下载进度
        private void Com_PartName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen == false && Com_PartName.Items.Count > 0)
            {
                if(Com_PartName.SelectedText != "")
                    serialPort1.PortName = Com_PartName.SelectedText;
            }
        }
        private void 选择下载文件_Click(object sender, EventArgs e)
        {
            int fileID = -1;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {   
                foreach(string fi in openFileDialog1.FileNames)
                {
                    if(false == List_LoadFile.Items.Contains(fi))
                    {
                        FileInfo fileInfo = new FileInfo(fi);
                        restype = LoadFiletype(fileInfo,ref fileID);
                        if (restype == Restype.NONE)
                        {
                            MessageBox.Show($"不识别文件{fileInfo}", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        else
                        {
                            List_LoadFile.Items.Add(fi);
                            if (fileInfo.Exists == true)
                                filelist.Add(fileInfo);
                        } 
                    }  
                }
                int binnum = 0;
                //检查bin文件
                foreach(FileInfo fi in filelist)
                {
                    if (fi.Extension == ".bin")
                        binnum++;
                }
                if(binnum > 1)
                {
                    MessageBox.Show("不允许同时下载两个升级文件（.bin）,请确定后重新添加", "警告", MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                    filelist.Clear();
                    List_LoadFile.Items.Clear();
                    return;
                }
                file_sort(); //filelist进行排序
                progressBar1.Value = 0;
                restype = Restype.NONE;
                下载.Enabled = true;
            }
        }
        //crc32校验
        private void 下载_Click(object sender, EventArgs e)
        {
            IsLoading = true;
            //C_Monitor_Thread.Suspend();
            datahandle = new Datahandle(false);
            ThreadDataHandle = new Thread(new ThreadStart(datahandle.DataHandle_Thread));
            ThreadDataHandle.Start();
            //C_Monitor_Thread.Resume();
            IsLoading = false;
        }
        public void Download_progress(int count)
        {
            //progressBar1.Value = progressBar1.Value + count;
            progressBar1.Value = count;
            progressBar1.Refresh();
            Console.WriteLine($"progressBar1.Value = {progressBar1.Value}");
        }
        public void progressBar1_Max_Set(int max)
        {
            progressBar1.Maximum = max;
            Console.WriteLine($"progressBar1.Maximum = {progressBar1.Maximum}");
        }
        public void State_Text(string str,int i)
        {
            if(i == 1)
            {
                toolStripStatusLabel1.Text = str;
            }
            else if(i == 2)
            {
                toolStripStatusLabel2.Text = str;
            }
            else if(i == 3)
            {
                toolStripStatusLabel3.Text = str;
            }
        }
        public void Serial_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";
            toolStripStatusLabel3.Text = "";
            com_baud.SelectedIndex = 0;
            Control.CheckForIllegalCrossThreadCalls = false;    //取消线线程安全保护模式！
        }
        private void Serial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(C_Monitor_Thread!=null)
            {
                C_Monitor_Thread.Abort();
            }
            if(ThreadDataHandle != null)
            {
                ThreadDataHandle.Abort();//下载线程关闭
            }
            serialPort1.Close();//关闭串口，避免串口死掉
            Environment.Exit(0);
        }
        /****************************
         * 应用程序的几种退出方式
         * 1.this.Close();   只是关闭当前窗口，若不是主窗体的话，是无法退出程序的，另外若有托管线程（非主线程），也无法干净地退出；
         * 2.Application.Exit();  强制所有消息中止，退出所有的窗体，但是若有托管线程（非主线程），也无法干净地退出；
         * 3.Application.ExitThread(); 强制中止调用线程上的所有消息，同样面临其它线程无法正确退出的问题；
         * 4.System.Environment.Exit(0);   这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净。
         * *******************************/

        private void DelSelect_Click(object sender, EventArgs e)
        {
            while(List_LoadFile.SelectedItems.Count >= 1)
            {
                List_LoadFile.Items.Remove(List_LoadFile.SelectedItem);
            }
            filelist.Clear();
            foreach(string file in List_LoadFile.Items)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Exists == true)
                    filelist.Add(fileInfo);
            }
        }
        private void ClearFile_Click(object sender, EventArgs e)
        {
            if (List_LoadFile.Items.Count >= 1)
                List_LoadFile.Items.Clear();
            if(filelist.Count >= 1)
                filelist.Clear();
        }
        private void file_sort()  //对文件列表进行排序
        {
            FileInfo temp = null;
            int BinIndex = 0;    //bin文件所在的位置的索引
            int i = 0;
            bool BinExists = false;
            if (filelist.Count == 0)
                return;
           
            foreach(FileInfo fi in filelist)
            {

                if (fi.Extension == ".bin")
                {
                    BinIndex = i;//记录bin文件的位置
                    if (BinIndex == filelist.Count - 1)  //如果就在最后不用处理的
                        return;           
                    BinExists = true;//判断文件是否存在 
                    temp = fi;
                    break;
                }
                i++;
            }
            if (BinExists == false)
                return;            //不存在bin文件
            filelist.RemoveAt(BinIndex);
            filelist.Add(temp);
        }
        public Restype LoadFiletype(FileInfo fileinfo,ref int fileID)
        {
            string exten = fileinfo.Extension.ToLower();
            Restype res = Restype.NONE;
            fileID = -1;
            switch (exten)
            {
                case ".ico":
                    
                case ".dzk":
                    if(exten == ".ico")
                    {
                        res = Restype.ico;
                    }
                    else
                    {
                        res = Restype.dzk;
                    }
                    char[] namebyte = fileinfo.Name.ToCharArray();
                    if(namebyte.Length >= 5)
                    {
                        if(namebyte[0] < '0' || namebyte[0] > '9')
                        {
                            res = Restype.NONE;
                            break;
                        }
                        byte num = 0;
                        string filenum = string.Empty;
                        do
                        {
                            if (namebyte[num] > '0' && namebyte[num] < '9')
                            {
                                if (num >= 4)
                                {
                                    res = Restype.NONE;
                                    break;
                                }
                                filenum += namebyte[num];
                                num++;
                            }
                            else
                            {
                                if(num <= 3)
                                {
                                    //res = Restype.NONE;
                                    break;
                                }
                            }
                        } while (num < (namebyte.Length - 4));
                        if(filenum != string.Empty && num < 4)
                        {
                            fileID = Convert.ToInt32(filenum);
                            if(fileID < 0 || fileID > 255)
                            {
                                res = Restype.NONE;
                            }
                            break;
                        }
                    }
                    res = Restype.NONE;
                    break;
                case ".bin":
                    res = Restype.bin;
                    break;
                default:
                    res = Restype.NONE;
                    break;
            }
            return res;
        }
        private void com_baud_SelectedIndexChanged(object sender, EventArgs e)
        {
            String Sbaud = com_baud.Text;
            Console.WriteLine($"Sbaud = {Sbaud}");
            if (Sbaud != "")
            {
                serialPort1.BaudRate = Convert.ToInt32(Sbaud);
            }
        }
    }
}
