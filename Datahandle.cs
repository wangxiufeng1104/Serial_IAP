using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Serial_IAP
{
    public class Datahandle    //数据处理
    {
        public bool IsAuto;//自动下载
        string loadingfile = string.Empty;
        public int ProgramOKNum = 0;     //成功下载程序的个数
        public int ProgramErrorNum = 0;  //失败下载程序的个数
        List<string> fileFailed = new List<string> { };
        System.Timers.Timer timer1 = new System.Timers.Timer(100);//实例化Timer类，设置间隔时间为1000毫秒；
        public Datahandle(bool Isauto)
        {
            IsAuto = Isauto;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);//到时间的时候执行事件； 
            timer1.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            Control.CheckForIllegalCrossThreadCalls = false;    //取消线线程安全保护模式！
        }
        int time = 0;
        public void timer1_Tick(object sender, EventArgs e)
        {
            time++;
        }
        public UInt32 CRC32(UInt32[] InputData, int len)
        {
            UInt32 dwPolynomial = 0x04c11db7;

            UInt32 xbit;
            UInt32 data;
            UInt32 crc_cnt = 0xFFFFFFFF; // init
            for (int i = 0; i < len; i++)
            {
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
                u32[i] = (UInt32)(newbytes[i * 4] << 24) | (UInt32)(newbytes[i * 4 + 1] << 16) | (UInt32)(newbytes[i * 4 + 2] << 8) | (UInt32)(newbytes[i * 4 + 3]);
            }
            return u32;
        }
       
        public void DataHandle_Thread()
        {
            FileStream fileStream = null;
            string readstring = "";
            int LoadPosition = 0;//记录下载的位置
            int reloadrecord = 0;//记录重新下载次数
            Serial s1 = Serial.GetSingle();
            int prebaud = 0;
            
            s1.State_Text("", 3);
            int datalen = 0;
            Console.WriteLine($"s1.filelist.count = {Serial.filelist.Count}");
            prebaud = Serial.SerialSingle.serialPort1.BaudRate;
            try
            {
                foreach (FileInfo fi in Serial.filelist)
                {
                    loadingfile = fi.FullName;
                    s1.State_Text($"当前下载文件{fi.Name}", 2);
                    fileStream = new FileStream(loadingfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    s1.progressBar1_Max_Set((int)fileStream.Length);
                    byte[] buffur = new byte[fileStream.Length];
                    byte[] buffur1 = new byte[2048];
                    fileStream.Read(buffur, 0, buffur.Length);
                    // 设置当前流的位置为流的开始 
                    fileStream.Seek(0, SeekOrigin.Begin);
                    byte[] HeadInf = new byte[7];
                    //第一步先判断文件类型
                    string Exten = fi.Extension.ToLower();
                    if (Exten != ".bin")
                    {
                        int fileID = -1;
                        Serial.SerialSingle.serialPort1.BaudRate = prebaud;
                        s1.restype = s1.LoadFiletype(fi,ref fileID);
                        datalen = 512;
                        UInt32 tlen = (UInt32)fileStream.Length;
                        s1.State_Text($"正在擦除,请等待......", 2);
                        byte[] sendinfo = new byte[11];
                        sendinfo[0] = 0xaa;
                        sendinfo[1] = 0xb0;
                        sendinfo[2] = (byte)fileID;
                        sendinfo[3] = (byte)(tlen >> 24);
                        sendinfo[4] = (byte)(tlen >> 16);
                        sendinfo[5] = (byte)(tlen >> 8);
                        sendinfo[6] = (byte)(tlen);
                        sendinfo[7] = 0xcc;
                        sendinfo[8] = 0x33;
                        sendinfo[9] = 0xc3;
                        sendinfo[10] = 0x3c;
                        s1.serialPort1.Write(sendinfo, 0, 11);
                    }
                    else
                    {
                        datalen = 2048;
                        if (IsAuto == false)
                        {
                            byte[] startInf = new byte[8];
                            startInf[0] = 0xAA;
                            startInf[1] = 0x8E;
                            startInf[2] = 0x00;
                            startInf[3] = 0x8E;
                            startInf[4] = 0xCC;
                            startInf[5] = 0x33;
                            startInf[6] = 0xC3;
                            startInf[7] = 0x3C;
                            s1.serialPort1.Write(startInf, 0, 8);
                        }
                        //Serial.SerialSingle.serialPort1.Close();
                        Serial.SerialSingle.serialPort1.BaudRate = 115200;
                        //Serial.SerialSingle.serialPort1.Open();
                        timer1.Start();
                        time = 0;
                        Delay(30);//延时300ms
                        do
                        {

                            if (time >= 13 * 10)
                            {
                                s1.State_Text($"超时0", 3);
                                time = 0;
                                timer1.Stop();
                                goto ERRORandOK;
                            }
                            readstring = s1.serialPort1.ReadExisting();
                        } while (!readstring.Contains("TU"));   //TDO UART

                        UInt32 CRCResult = CRC32(ByteArrayToUInt32Array1(buffur), (int)fileStream.Length / 4);

                        HeadInf[0] = (byte)'T';
                        HeadInf[1] = (byte)'D';
                        HeadInf[2] = (byte)'O';
                        HeadInf[3] = (byte)(fileStream.Length >> 8);
                        HeadInf[4] = (byte)fileStream.Length;
                        HeadInf[5] = (byte)(CRCResult >> 8);
                        HeadInf[6] = (byte)(byte)CRCResult;
                        s1.serialPort1.Write(HeadInf, 0, 7);
                    }
                    if (loadingfile != "")
                    {
                        for (int i = 0; i < fileStream.Length; i += datalen)
                        {

                            if (i == datalen)
                                s1.State_Text($"当前下载文件{fi.Name}", 2);
                            timer1.Start();
                            time = 0;
                            do
                            {
                                if (time >= 13 * 10)
                                {
                                    s1.State_Text($"连接超时", 3);
                                    ProgramErrorNum++;
                                    time = 0;
                                    timer1.Stop();
                                    fileFailed.Add(fi.Name);
                                    goto ERRORandOK;
                                }
                                readstring = s1.serialPort1.ReadExisting();
                            } while (readstring == "");
                            s1.State_Text($"", 3);
                            if (readstring.Contains("W"))
                            {
                                reloadrecord = 0;
                                time = 0;
                            }
                            else if(readstring.Contains("C"))
                            {
                                reloadrecord++;
                                if(reloadrecord>=3)
                                {
                                    s1.State_Text($"检验错误，停止下载，请检查连接后重新下载", 3);
                                    goto ERRORandOK;
                                }
                                time = 0;
                                i = LoadPosition;
                            }
                            else
                            {
                                time = 0;
                                timer1.Stop();
                                fileFailed.Add(fi.Name);
                                s1.State_Text($"数据错误！错误的数据%{readstring}%", 3);
                                ProgramErrorNum++;
                                goto ERRORandOK;
                            }
                            int count = (int)fileStream.Length - i;
                            if (count > datalen) 
                            {
                                count = datalen;
                            }
                            try
                            {
                                UInt32 CRCResult1;
                                System.Buffer.BlockCopy(buffur,i, buffur1,0,count);
                                CRCResult1 = CRC32(ByteArrayToUInt32Array1(buffur1), count/4);
                                byte[] crcarray = new byte[2];
                                crcarray[0] = (byte)(CRCResult1 >> 8);
                                crcarray[1] = (byte)(byte)CRCResult1;
                                s1.serialPort1.Write(buffur, i, count);
                                s1.serialPort1.Write(crcarray, 0, 2);
                                s1.Download_progress(count + i);
                                LoadPosition = i;
                            }
                            catch (Exception ex)
                            {
                                s1.State_Text($"Error:{ex.Message}", 3);
                                fileFailed.Add(fi.Name);
                                ProgramErrorNum++;
                            }
                            time = 0;
                        }
                        do
                        {
                            if (time >= 3 * 10)
                            {
                                s1.State_Text($"超时", 3);
                                ProgramErrorNum++;
                                time = 0;
                                timer1.Stop();
                                fileFailed.Add(fi.Name);
                                goto ERRORandOK;
                            }
                            readstring = s1.serialPort1.ReadExisting();
                        } while (readstring == "");

                        readstring = readstring.Replace("\n", "");
                        if (readstring.Contains("F"))
                        {
                            time = 0;
                            timer1.Stop();
                            ProgramErrorNum++;
                            fileFailed.Add(fi.Name);
                        }
                        else if (readstring.Contains("S"))
                        {
                            time = 0;
                            timer1.Stop();
                            ProgramOKNum++;
                            s1.State_Text($"{ProgramOKNum}下载成功", 1);
                            s1.progressBar1.Value = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                s1.State_Text($"Error:{ex.Message}", 3);
                Delay(100);

                if (s1.serialPort1.IsOpen == false)
                {
                    try
                    {
                        s1.serialPort1.Open();
                    }
                    catch (Exception ex1)
                    {
                        s1.State_Text($"Error:{ex1.Message}", 3);
                    }
                }
            }
            string filefa = string.Empty;
            if (fileFailed.Count >= 1)//有更新失败的文件
            {
                foreach (string s in fileFailed)
                {
                    filefa += s;
                    filefa += "\n";
                }
                MessageBox.Show(filefa, "更新失败列表", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                fileFailed.Clear();
            }
            else      //更新成功
            {
                s1.State_Text($"全部下载完成", 3);
            }
ERRORandOK:
            Delay(300);
            Serial.SerialSingle.serialPort1.BaudRate = prebaud;
            readstring = s1.serialPort1.ReadExisting();
            s1.IsLoading = false;
            fileFailed.Clear();
            s1.ThreadDataHandle.Abort();
            return;
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
    }
}
