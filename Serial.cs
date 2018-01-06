using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace Serial_IAP
{
    public enum Restype
    {
        NONE = 0x00,
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
        ico = 0x50
    }



    public partial  class Serial : Form
    {
        private Thread C_Monitor_Thread;   //串口监听线程
        private bool IsLoading = false;
        static int ProgramOKNum = 0;     //成功下载程序的个数
        static int ProgramErrorNum = 0;  //失败下载程序的个数
        static List<FileInfo> filelist = new List<FileInfo> { };
        List<string> fileFailed = new List<string> { };
        string loadingfile = string.Empty;
        Restype restype = Restype.NONE;

        public Serial()
        {
            InitializeComponent();
        }

        private void Com_PartName_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
                打开串口.Text = "打开串口";
            }
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                //MessageBox.Show("No Serial", "Error");
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

        private void Check_Common()
        {
            string BeginString = "";
            Console.WriteLine("Check_Common\n");
            while(true)
            {
                if(IsLoading == false)
                {
                    Delay(300);
                    BeginString = serialPort1.ReadExisting();
                    Console.WriteLine($"BeginString = {BeginString}");
                    if (BeginString.Contains("IAPOK"))
                    {
                        IsLoading = true;
                        MyInvoke mi = new MyInvoke(Programing_Thread);
                        this.BeginInvoke(mi,true);
                        while (IsLoading == true) ;
                    }
                }
            }
        }
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
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {   
                foreach(string fi in openFileDialog1.FileNames)
                {
                    if(false == List_LoadFile.Items.Contains(fi))
                    {
                        List_LoadFile.Items.Add(fi);
                        FileInfo fileInfo = new FileInfo(fi);
                        if (fileInfo.Exists == true)
                            filelist.Add(fileInfo);
                    }  
                }
                int binnum = 0;
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
                下载.Enabled = true;
            }
        }
        //crc32校验
        public UInt32 CRC32(UInt32[] InputData, int len)
        {
            UInt32 dwPolynomial = 0x04c11db7;

            UInt32 xbit;
            UInt32 data;
            UInt32 crc_cnt = 0xFFFFFFFF; // init
            for (int i = 0; i < len; i++)
            {
                // xbit = 1 << 31;
                xbit = 0x80000000;
                data = InputData[i];
                for (int bits = 0; bits < 32; bits++)
                {
                    if ((crc_cnt & 0x80000000) > 0)
                    {
                        crc_cnt <<= 1;
                        crc_cnt ^= dwPolynomial;
                    }
                    else
                        crc_cnt <<= 1;
                    if ((data & xbit) > 0)
                        crc_cnt ^= dwPolynomial;
                    xbit >>= 1;
                }
            }
            return crc_cnt;
        }
        //byte[]数组转化为UInt32[]数组
        private UInt32[] ByteArrayToUInt32Array(byte[] bytes)
        {
            byte[] newbytes = new byte[bytes.Length + (4 - bytes.Length % 4)];
            for (int i = 0; i < bytes.Length; i++)
                newbytes[i] = bytes[i];
            UInt32[] u32 = new UInt32[newbytes.Length / 4];
            for (int i = 0; i < newbytes.Length / 4; i++)
                u32[i] = System.BitConverter.ToUInt32(newbytes, i * 4);
            return u32;
        }
        private UInt32[] ByteArrayToUInt32Array1(byte[] bytes)
        {
            byte[] newbytes = new byte[bytes.Length + (4 - bytes.Length % 4)];
            for (int i = 0; i < bytes.Length; i++)
                newbytes[i] = bytes[i];

            UInt32[] u32 = new UInt32[newbytes.Length / 4];
            for (int i = 0; i < newbytes.Length / 4; i++)
            {
                u32[i] = (UInt32)(newbytes[i * 4] << 24)| (UInt32)( newbytes[i * 4 + 1] << 16) | (UInt32)(newbytes[i * 4 + 2] << 8) | (UInt32)(newbytes[i * 4 + 3]);
            }
            return u32;
        }
        public static bool Delay(int delayTime)
        {
            DateTime now = DateTime.Now;
            int s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.Milliseconds;
                Application.DoEvents();
            }
            while (s < delayTime);
            return true;
        }
        private void 下载_Click(object sender, EventArgs e)
        {
            IsLoading = true;
            //C_Monitor_Thread.Suspend();
            Programing_Thread(false);
            //C_Monitor_Thread.Resume();
            IsLoading = false;

        }

        private void Download_progress(int count)
        {
            //progressBar1.Value = progressBar1.Value + count;
            progressBar1.Value = count;
            progressBar1.Refresh();
            
            Console.WriteLine($"progressBar1.Value = {progressBar1.Value}");
        }
        private void progressBar1_Max_Set(int max)
        {
            progressBar1.Maximum = max;
            Console.WriteLine($"progressBar1.Maximum = {progressBar1.Maximum}");
        }
        private void State_Text(string str,int i)
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
        public void Programing_Thread(bool IsAuto)
        {
            FileStream fileStream = null;
            string readstring = "";
            toolStripStatusLabel3.Text = "";
            int datalen = 0;

            foreach (FileInfo fi in filelist)
            {
                loadingfile = fi.FullName;
                toolStripStatusLabel2.Text = $"当前下载文件{fi.Name}";
                fileStream = new FileStream(loadingfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                progressBar1_Max_Set((int)fileStream.Length);
                byte[] buffur = new byte[fileStream.Length];
                fileStream.Read(buffur, 0, buffur.Length);
                // 设置当前流的位置为流的开始 
                fileStream.Seek(0, SeekOrigin.Begin);
                byte[] HeadInf = new byte[5];
                //第一步先判断文件类型
                if (fi.Extension != ".bin")
                {
                    restype = LoadFiletype(fi);
                    Byte funm = 0;
                    datalen = 256;
                    UInt32 tlen = (UInt32)fileStream.Length;
                    switch (restype)
                    {
                        case Restype.NONE:
                            continue;
                        case Restype.ico:
                            funm = 0x50;
                            break;
                        case Restype.ASCII16:
                            funm = 0x10;
                            break;
                        case Restype.ASCII24:
                            funm = 0x11;
                            break;
                        case Restype.ASCII32:
                            funm = 0x12;
                            break;
                        case Restype.ASCII48:
                            funm = 0x13;
                            break;
                        case Restype.GB2312_16:
                            funm = 0x20;
                            break;
                        case Restype.GB2312_24:
                            funm = 0x21;
                            break;
                        case Restype.GB2312_32:
                            funm = 0x22;
                            break;
                        case Restype.GB2312_48:
                            funm = 0x23;
                            break;
                        case Restype.KO_16:
                            funm = 0x30;
                            break;
                        case Restype.KO_24:
                            funm = 0x31;
                            break;
                        case Restype.KO_32:
                            funm = 0x32;
                            break;
                        case Restype.KO_48:
                            funm = 0x33;
                            break;
                        default:
                            break;
                    }
                    string incstr = string.Format($"DOW({funm},{tlen})\r\n");
                    serialPort1.Write(incstr);
                }
                else
                {
                    datalen = 2048;
                    if (IsAuto == false)
                    {
                        byte[] startInf = new byte[10];
                        startInf[0] = 0x52;
                        startInf[1] = 0x45;
                        startInf[2] = 0x49;
                        startInf[3] = 0x41;
                        startInf[4] = 0x50;
                        startInf[5] = 0x28;
                        startInf[6] = 0x29;
                        startInf[7] = 0x3b;
                        startInf[8] = 0x0d;
                        startInf[9] = 0x0a;
                        serialPort1.Write(startInf, 0, 10);
                    }
                    timer1.Start();
                    time = 0;
                    do
                    {
                        if (time >= 3 * 10)
                        {
                            State_Text($"超时0", 3);
                            time = 0;
                            timer1.Stop();
                            goto ERRORandOK;
                        }
                        readstring = serialPort1.ReadExisting();
                    } while (!readstring.Contains("IAPOK"));
                    UInt32 CRCResult = CRC32(ByteArrayToUInt32Array1(buffur), (int)fileStream.Length / 4);
                    
                    HeadInf[0] = 0x7f;
                    HeadInf[1] = (byte)(fileStream.Length >> 8);
                    HeadInf[2] = (byte)fileStream.Length;
                    HeadInf[3] = (byte)(CRCResult >> 8);
                    HeadInf[4] = (byte)(byte)CRCResult;
                    serialPort1.Write(HeadInf, 0, 5);
                }
                try
                {
                    if (loadingfile != "")
                    {
                        for (int i = 0; i < fileStream.Length; i += datalen)
                        {
                            Console.WriteLine(i);
                            Console.WriteLine(fileStream.Length);
                            Delay(300);
                            timer1.Start();
                            time = 0;
                            do
                            {
                                if (time >= 3 * 10)
                                {
                                    //MessageBox.Show("超时2", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                                    State_Text($"超时2", 3);
                                    ProgramErrorNum++;
                                    //State_Text($"更新失败，失败个数{ProgramErrorNum}", 2);
                                    time = 0;
                                    timer1.Stop();
                                    fileFailed.Add(fi.Name);
                                    goto ERRORandOK;
                                }
                                readstring = serialPort1.ReadExisting();
                            } while (readstring == "");
                            Console.WriteLine("收到的数据包 = {0}", readstring);
                            if (readstring.Contains("Wait data"))
                            {
                                time = 0;
                            }
                            else
                            {
                                time = 0;
                                timer1.Stop();
                                fileFailed.Add(fi.Name);
                                //MessageBox.Show($"数据错误！错误的数据%{readstring}%", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                                State_Text($"数据错误！错误的数据%{readstring}%", 3);
                                ProgramErrorNum++;
                                //State_Text($"更新失败，失败个数{ProgramErrorNum}", 2);

                                goto ERRORandOK;
                            }
                            int count = (int)fileStream.Length - i;
                            if (count > datalen)
                            {
                                count = datalen;
                            }
                            try
                            {
                                serialPort1.Write(buffur, i, count);
                                Download_progress(count+i);

                            }
                            catch (Exception ex)
                            {
                                State_Text($"Error:{ex.Message}", 3);
                                fileFailed.Add(fi.Name);
                                ProgramErrorNum++;
                                //State_Text($"更新失败，失败个数{ProgramErrorNum}", 2);
                            }
                            time = 0;
                            Delay(300);
                        }
                        
                        do
                        {
                            if (time >= 2 * 10)
                            {
                                //MessageBox.Show("超时", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                                State_Text($"超时", 3);
                                ProgramErrorNum++;
                                //State_Text($"更新失败，失败个数{ProgramErrorNum}", 2);
                                time = 0;
                                timer1.Stop();
                                fileFailed.Add(fi.Name);
                                goto ERRORandOK;
                            }
                            readstring = serialPort1.ReadExisting();
                            //Console.WriteLine("收到的数据包 = {0}", readstring);

                        } while (readstring == "");

                        readstring = readstring.Replace("\n", "");
                        //Console.WriteLine($"readstring = {readstring}");
                        if (readstring.Contains("Failed"))
                        {
                            time = 0;
                            timer1.Stop();
                            ProgramErrorNum++;
                            fileFailed.Add(fi.Name);
                            //State_Text($"更新失败，失败个数{ProgramErrorNum}", 2);
                        }
                        else if (readstring.Contains("SUCCESS"))
                        {
                            time = 0;
                            timer1.Stop();
                            ProgramOKNum++;
                            State_Text($"{ProgramOKNum}下载成功", 1);
                            progressBar1.Value = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    State_Text($"Error:{ex.Message}", 3);
                }
            }
            string filefa = string.Empty;
            if (fileFailed.Count >= 1)//有更新失败的文件
            {
                foreach(string s in fileFailed)
                {
                    filefa += s;
                    filefa += "\n";
                }
                MessageBox.Show(filefa,"更新失败列表",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                fileFailed.Clear();
            }
            else      //更新成功
            {
                serialPort1.Write($"{0x8f}");
            }
 ERRORandOK:
            Delay(300);
            
            readstring = serialPort1.ReadExisting();
            IsLoading = false;
            fileFailed.Clear();
            return;
        }
        int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
        }
        System.Timers.Timer timer1 = new System.Timers.Timer(100);//实例化Timer类，设置间隔时间为1000毫秒；
        private void Serial_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = "";
            toolStripStatusLabel3.Text = "";
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);//到时间的时候执行事件； 
            timer1.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
           // t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 

        }
        private void Serial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(C_Monitor_Thread!=null)
            {
                C_Monitor_Thread.Abort();
            }
            Application.Exit();
        }
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
        Restype LoadFiletype(FileInfo fileinfo)
        {
            switch(fileinfo.Extension)
            {
                case ".ico":
                    return Restype.ico;
                case ".dzk":
                    if(fileinfo.Name.Contains("ASCII"))
                    {
                        if(fileinfo.Name.Contains("16"))
                        {
                            return Restype.ASCII16;
                        }
                        else if (fileinfo.Name.Contains("24"))
                        {
                            return Restype.ASCII24;
                        }
                        else if (fileinfo.Name.Contains("32"))
                        {
                            return Restype.ASCII32;
                        }
                        else if (fileinfo.Name.Contains("48"))
                        {
                            return Restype.ASCII48;
                        }
                        else
                        {
                            return Restype.NONE;
                        }
                    }
                    else if(fileinfo.Name.Contains("GB2312"))
                    {
                        if (fileinfo.Name.Contains("16"))
                        {
                            return Restype.GB2312_16;
                        }
                        else if (fileinfo.Name.Contains("24"))
                        {
                            return Restype.GB2312_24;
                        }
                        else if (fileinfo.Name.Contains("32"))
                        {
                            return Restype.GB2312_32;
                        }
                        else if (fileinfo.Name.Contains("48"))
                        {
                            return Restype.GB2312_48;
                        }
                        else
                        {
                            return Restype.NONE;
                        }
                    }
                    else if(fileinfo.Name.Contains("KO"))
                    {
                        if (fileinfo.Name.Contains("16"))
                        {
                            return Restype.KO_16;
                        }
                        else if (fileinfo.Name.Contains("24"))
                        {
                            return Restype.KO_24;
                        }
                        else if (fileinfo.Name.Contains("32"))
                        {
                            return Restype.KO_32;
                        }
                        else if (fileinfo.Name.Contains("48"))
                        {
                            return Restype.KO_48;
                        }
                        else
                        {
                            return Restype.NONE;
                        }
                    }
                    return Restype.NONE; 
            }
            return Restype.NONE;
        }
    }
}
