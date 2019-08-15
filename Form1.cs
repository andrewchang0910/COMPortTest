﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//include
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Reflection;

namespace COMPortTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }

        void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            String data = serialPort1.ReadLine();
            //Console.Write("datareceived: " + data);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ContentBox.ScrollBars = ScrollBars.Vertical;

            int Port_count = 0;
            string[] myPorts = SerialPort.GetPortNames(); //取得所有port的名字
            foreach (string port in myPorts)  //使用迴圈方式取得所有port的名字
            {
                Port_count += 1;
                if (Port_count <= 12)
                {
                    switch (Port_count)
                    {

                        case 1: textBox1.AppendText(port); break;
                        case 2: textBox2.AppendText(port); break;
                        case 3: textBox3.AppendText(port); break;
                        case 4: textBox4.AppendText(port); break;
                        case 5: textBox5.AppendText(port); break;
                        case 6: textBox6.AppendText(port); break;
                        case 7: textBox7.AppendText(port); break;
                        case 8: textBox8.AppendText(port); break;
                        case 9: textBox9.AppendText(port); break;
                        case 10: textBox10.AppendText(port); break;
                        case 11: textBox11.AppendText(port); break;
                        case 12: textBox12.AppendText(port); break;
                    }
                }
                else
                {
                    MessageBox.Show("Warning ! COM Port Out Of Limit");
                }
            }

            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            IniFile ini = new IniFile(str + "\\COMPortTest.ini");
            if (ini.Read("COM1")  == "1") { checkBox1.Checked =  true; } else { checkBox1.Checked  = false; }
            if (ini.Read("COM2")  == "1") { checkBox2.Checked =  true; } else { checkBox2.Checked  = false; }
            if (ini.Read("COM3")  == "1") { checkBox3.Checked =  true; } else { checkBox3.Checked  = false; }
            if (ini.Read("COM4")  == "1") { checkBox5.Checked =  true; } else { checkBox5.Checked  = false; }
            if (ini.Read("COM5")  == "1") { checkBox4.Checked =  true; } else { checkBox4.Checked  = false; }
            if (ini.Read("COM6")  == "1") { checkBox6.Checked =  true; } else { checkBox6.Checked  = false; }
            if (ini.Read("COM7")  == "1") { checkBox7.Checked =  true; } else { checkBox7.Checked  = false; }
            if (ini.Read("COM8")  == "1") { checkBox8.Checked =  true; } else { checkBox8.Checked  = false; }
            if (ini.Read("COM9")  == "1") { checkBox9.Checked =  true; } else { checkBox9.Checked  = false; }
            if (ini.Read("COM10") == "1") { checkBox10.Checked = true; } else { checkBox10.Checked = false; }
            if (ini.Read("COM11") == "1") { checkBox11.Checked = true; } else { checkBox11.Checked = false; }
            if (ini.Read("COM12") == "1") { checkBox12.Checked = true; } else { checkBox12.Checked = false; }
            if (ini.Read("AUTO")  == "1")
            {
                checkBox13.Checked = true;
                ContentBox.AppendText("   [Auto Test]\r\n");
                Test_Click(sender, e);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            string str1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            IniFile ini = new IniFile(str1 + "\\COMPortTest.ini");
            if (checkBox1.Checked  == true) { ini.Write("COM1", "1");  } else { ini.Write("COM1", "0"); }
            if (checkBox2.Checked  == true) { ini.Write("COM2", "1");  } else { ini.Write("COM2", "0"); }
            if (checkBox3.Checked  == true) { ini.Write("COM3", "1");  } else { ini.Write("COM3", "0"); }
            if (checkBox5.Checked  == true) { ini.Write("COM4", "1");  } else { ini.Write("COM4", "0"); }
            if (checkBox4.Checked  == true) { ini.Write("COM5", "1");  } else { ini.Write("COM5", "0"); }
            if (checkBox6.Checked  == true) { ini.Write("COM6", "1");  } else { ini.Write("COM6", "0"); }
            if (checkBox7.Checked  == true) { ini.Write("COM7", "1");  } else { ini.Write("COM7", "0"); }
            if (checkBox8.Checked  == true) { ini.Write("COM8", "1");  } else { ini.Write("COM8", "0"); }
            if (checkBox9.Checked  == true) { ini.Write("COM9", "1");  } else { ini.Write("COM9", "0"); }
            if (checkBox10.Checked == true) { ini.Write("COM10", "1"); } else { ini.Write("COM10","0"); }
            if (checkBox11.Checked == true) { ini.Write("COM11", "1"); } else { ini.Write("COM11","0"); }
            if (checkBox12.Checked == true) { ini.Write("COM12", "1"); } else { ini.Write("COM12","0"); }
            if (checkBox13.Checked == true) { ini.Write("AUTO", "1");  } else { ini.Write("AUTO", "0"); }
            ContentBox.AppendText("   Saved test configuration\r\n");
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ContentBox.Text = "";
        }

        private void Test_Click(object sender, EventArgs e)
        {
            ContentBox.AppendText("   [Test Start]\r\n");

            //add Time
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ContentBox.AppendText("     Time :" + date + "\r\n");

            //Test whether checkBox check or not
            int[] CheckBoxBool = new int[13];
            if (checkBox1.Checked)  { CheckBoxBool[1] = 1;  } else { CheckBoxBool[1]  = 0; }
            if (checkBox2.Checked)  { CheckBoxBool[2] = 1;  } else { CheckBoxBool[2]  = 0; }
            if (checkBox3.Checked)  { CheckBoxBool[3] = 1;  } else { CheckBoxBool[3]  = 0; }
            if (checkBox4.Checked)  { CheckBoxBool[4] = 1;  } else { CheckBoxBool[4]  = 0; }
            if (checkBox5.Checked)  { CheckBoxBool[5] = 1;  } else { CheckBoxBool[5]  = 0; }
            if (checkBox6.Checked)  { CheckBoxBool[6] = 1;  } else { CheckBoxBool[6]  = 0; }
            if (checkBox7.Checked)  { CheckBoxBool[7] = 1;  } else { CheckBoxBool[7]  = 0; }
            if (checkBox8.Checked)  { CheckBoxBool[8] = 1;  } else { CheckBoxBool[8]  = 0; }
            if (checkBox9.Checked)  { CheckBoxBool[9] = 1;  } else { CheckBoxBool[9]  = 0; }
            if (checkBox10.Checked) { CheckBoxBool[10] = 1; } else { CheckBoxBool[10] = 0; }
            if (checkBox11.Checked) { CheckBoxBool[11] = 1; } else { CheckBoxBool[11] = 0; }
            if (checkBox12.Checked) { CheckBoxBool[12] = 1; } else { CheckBoxBool[12] = 0; }


            int flag_testcheck = 0;//確認是否有勾選選項
            //測試Error Code 是否返回0(正常)
            for (int i = 1; i <= 12; i++)
            {
                //如果CheckBox有被勾選 
                if (CheckBoxBool[i] == 1)
                {
                    
                    flag_testcheck = 1;
                    //Test 預設值為錯誤
                    bool test = false;
                    string str;
                    switch (i)
                    {

                        case 1: textBox13.Text = COMSendReceiveMessageCheck(textBox1.Text.ToString()).ToString(); str = textBox13.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 2: textBox14.Text = COMSendReceiveMessageCheck(textBox2.Text.ToString()).ToString(); str = textBox14.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 3: textBox15.Text = COMSendReceiveMessageCheck(textBox3.Text.ToString()).ToString(); str = textBox15.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 4: textBox16.Text = COMSendReceiveMessageCheck(textBox4.Text.ToString()).ToString(); str = textBox16.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 5: textBox17.Text = COMSendReceiveMessageCheck(textBox5.Text.ToString()).ToString(); str = textBox17.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 6: textBox18.Text = COMSendReceiveMessageCheck(textBox6.Text.ToString()).ToString(); str = textBox18.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 7: textBox19.Text = COMSendReceiveMessageCheck(textBox7.Text.ToString()).ToString(); str = textBox19.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 8: textBox20.Text = COMSendReceiveMessageCheck(textBox8.Text.ToString()).ToString(); str = textBox20.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 9: textBox21.Text = COMSendReceiveMessageCheck(textBox9.Text.ToString()).ToString(); str = textBox21.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 10: textBox22.Text = COMSendReceiveMessageCheck(textBox10.Text.ToString()).ToString(); str = textBox22.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 11: textBox23.Text = COMSendReceiveMessageCheck(textBox11.Text.ToString()).ToString(); str = textBox23.Text.ToString(); if (str == "Match") { test = true; } break;
                        case 12: textBox24.Text = COMSendReceiveMessageCheck(textBox12.Text.ToString()).ToString(); str = textBox24.Text.ToString(); if (str == "Match") { test = true; } break;
                    }

                    //Test為是 更改圖片為成功
                    if (test == true)
                    {
                        switch (i)
                        {
                            case 1:  pictureBox1.Image =  Properties.Resources.bmp00006; break;
                            case 2:  pictureBox2.Image =  Properties.Resources.bmp00006; break;
                            case 3:  pictureBox3.Image =  Properties.Resources.bmp00006; break;
                            case 4:  pictureBox4.Image =  Properties.Resources.bmp00006; break;
                            case 5:  pictureBox5.Image =  Properties.Resources.bmp00006; break;
                            case 6:  pictureBox6.Image =  Properties.Resources.bmp00006; break;
                            case 7:  pictureBox7.Image =  Properties.Resources.bmp00006; break;
                            case 8:  pictureBox8.Image =  Properties.Resources.bmp00006; break;
                            case 9:  pictureBox9.Image =  Properties.Resources.bmp00006; break;
                            case 10: pictureBox10.Image = Properties.Resources.bmp00006; break;
                            case 11: pictureBox11.Image = Properties.Resources.bmp00006; break;
                            case 12: pictureBox12.Image = Properties.Resources.bmp00006; break;

                        }
                    }
                    //如為否 則更改圖片為失敗
                    else
                    {
                        switch (i)
                        {
                            case 1:  pictureBox1.Image  = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 2:  pictureBox2.Image  = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 3:  pictureBox3.Image  = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 4:  pictureBox4.Image  = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 5:  pictureBox5.Image  = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 6:  pictureBox6.Image  = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 7:  pictureBox7.Image  = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 8:  pictureBox8.Image  = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 9:  pictureBox9.Image  = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 10: pictureBox10.Image = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 11: pictureBox11.Image = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;
                            case 12: pictureBox12.Image = Properties.Resources.bmp00005; GlobalVarable.log_flag = 1; break;

                        }
                    }
                }
            }

            //add COM Port Total Result
            if (flag_testcheck == 0 && GlobalVarable.log_flag == 0)
            {
                MessageBox.Show("Warning ! Please Check At Least One COM Port !");
                Test.Enabled = true;
                //Scan_Click(sender, e);
            }
            else if (GlobalVarable.log_flag == 0)
            {
                ContentBox.AppendText("     Test  Result ----------------> PASS\r\n");
                Environment.ExitCode = 0;
            }
            else
            {
                ContentBox.AppendText("     Test  Result ----------------> FAIL\r\n");
                Environment.ExitCode = 1;
            }

            ContentBox.AppendText("   [Test End]\r\n\r\n");

            //製作結果檔
            CreateLogfile();

            //自動程式關閉
            string str1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            IniFile ini = new IniFile(str1 + "\\SATATest.ini");
            if (GlobalVarable.log_flag == 0 && ini.Read("AUTO") == "1")
            { timer1.Start(); timer1_Tick(sender, e); }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
            Environment.ExitCode = 0;
        }

        public string COMSendReceiveMessageCheck(string name)
        {
            serialPort1.Close();
            serialPort1.PortName = name;
            serialPort1.Parity = Parity.None;
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;

            try
            {
                serialPort1.Open();
            }
            catch (Exception)
            {
                ContentBox.AppendText("     ->"+serialPort1.PortName.ToString() + " \r\n     Can Not Be Opened\r\n");
                ContentBox.AppendText("     Result: Fail\r\n");
            }

            if (serialPort1.IsOpen)
            {

                try//~!@#$%^&*()_+ 
                {

                    serialPort1.Write("~!@#$%^&*()_+");
                    ContentBox.AppendText("     ->" + serialPort1.PortName + " Test\r\n");
                    ContentBox.AppendText("     Sleep 1 sec...\r\n");
                    Thread.Sleep(1000);
                    textBox25.Text = serialPort1.ReadExisting();
                    if (textBox25.Text == "~!@#$%^&*()_+")
                    {
                        ContentBox.AppendText("     Result: Pass\r\n");
                        return "Match";
                        
                    }
                    else 
                    {
                        ContentBox.AppendText("     Result: Fail\r\n");
                        return "Unmatch";
                        
                    }
                }
                catch { return "SE&RE Failed"; }
                finally { textBox25.Text = ""; }
            }

            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }

            return "Test Failed";
            
        }

        public class IniFile   // revision 11
        {
            string Path;
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public IniFile(string IniPath = null)
            {
                Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
            }

            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }

            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }

            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }

            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }

            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
        }

        public void CreateLogfile()
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string strTimeYear = string.Format("{0:D4}", currentTime.Year);
            string strTimeMonth = string.Format("{0:D2}", currentTime.Month);
            string strTimeDay = string.Format("{0:D2}", currentTime.Day);
            string strTimeHour = string.Format("{0:D2}", currentTime.Hour);
            string strTimeMinute = string.Format("{0:D2}", currentTime.Minute);
            string strTimeSecond = string.Format("{0:D2}", currentTime.Second);

            string logfilename = "COM_PortResult_" + strTimeYear + strTimeMonth + strTimeDay + ".log";
            if (false == System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\" + logfilename))
            {
                try
                {
                    StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\" + logfilename, true);
                    writer.Write("COM Port Test  : \r\n");
                    writer.Write("-------------------------------------- \r\n");
                    writer.Write("Time\t\t\t     Result\r\n");
                    writer.Close();

                }
                catch
                {
                    MessageBox.Show("Create File.log Error");
                }
            }

            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (GlobalVarable.log_flag == 0)
            {
                File.AppendAllText(logfilename, "\r\n" + date + "--------->PASS\r\n");
            }
            else
            {
                File.AppendAllText(logfilename, "\r\n" + date + "--------->FAIL\r\n");
            }

        }

        public class GlobalVarable
        {
            public static int log_flag = 0;
        }

        public void CleanAll()
        {
            ContentBox.Clear();
            CleanPicBox();
        }

        public void CleanPicBox()
        {
            pictureBox1.Image = Properties.Resources.bmp00002;
            pictureBox2.Image = Properties.Resources.bmp00002;
            pictureBox3.Image = Properties.Resources.bmp00002;
            pictureBox4.Image = Properties.Resources.bmp00002;
            pictureBox5.Image = Properties.Resources.bmp00002;
            pictureBox6.Image = Properties.Resources.bmp00002;
            pictureBox7.Image = Properties.Resources.bmp00002;
            pictureBox8.Image = Properties.Resources.bmp00002;
            pictureBox9.Image = Properties.Resources.bmp00002;
            pictureBox10.Image = Properties.Resources.bmp00002;
            pictureBox11.Image = Properties.Resources.bmp00002;
            pictureBox12.Image = Properties.Resources.bmp00002;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {if (checkBox1.Checked == true){if (textBox1.Text.ToString() == "") { checkBox1.Checked = false; }}}

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        { if (checkBox2.Checked == true) { if (textBox2.Text.ToString() == "") { checkBox2.Checked = false; } } }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        { if (checkBox3.Checked == true) { if (textBox3.Text.ToString() == "") { checkBox3.Checked = false; } } }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        { if (checkBox4.Checked == true) { if (textBox4.Text.ToString() == "") { checkBox4.Checked = false; } } }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        { if (checkBox5.Checked == true) { if (textBox5.Text.ToString() == "") { checkBox5.Checked = false; } } }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        { if (checkBox6.Checked == true) { if (textBox6.Text.ToString() == "") { checkBox6.Checked = false; } } }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        { if (checkBox7.Checked == true) { if (textBox7.Text.ToString() == "") { checkBox7.Checked = false; } } }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        { if (checkBox8.Checked == true) { if (textBox8.Text.ToString() == "") { checkBox8.Checked = false; } } }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        { if (checkBox9.Checked == true) { if (textBox9.Text.ToString() == "") { checkBox9.Checked = false; } } }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        { if (checkBox10.Checked == true) { if (textBox10.Text.ToString() == "") { checkBox10.Checked = false; } } }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        { if (checkBox11.Checked == true) { if (textBox11.Text.ToString() == "") { checkBox11.Checked = false; } } }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        { if (checkBox12.Checked == true) { if (textBox12.Text.ToString() == "") { checkBox12.Checked = false; } } }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            //Auto Run

            if (checkBox7.Checked) //設置開機自啟動  
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("JcShutdown", path);
                rk2.Close();
                rk.Close();

                //寫入ini檔為true 
                string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                IniFile ini = new IniFile(str + "\\COMPortTest.ini");
                ini.Write("AUTO", "1");
                if (checkBox1.Checked == true) { ini.Write("COM1", "1"); } else { ini.Write("COM1", "0"); }
                if (checkBox2.Checked == true) { ini.Write("COM2", "1"); } else { ini.Write("COM2", "0"); }
                if (checkBox3.Checked == true) { ini.Write("COM3", "1"); } else { ini.Write("COM3", "0"); }
                if (checkBox5.Checked == true) { ini.Write("COM4", "1"); } else { ini.Write("COM4", "0"); }
                if (checkBox4.Checked == true) { ini.Write("COM5", "1"); } else { ini.Write("COM5", "0"); }
                if (checkBox6.Checked == true) { ini.Write("COM6", "1"); } else { ini.Write("COM6", "0"); }
                if (checkBox7.Checked == true) { ini.Write("COM7", "1"); } else { ini.Write("COM7", "0"); }
                if (checkBox8.Checked == true) { ini.Write("COM8", "1"); } else { ini.Write("COM8", "0"); }
                if (checkBox9.Checked == true) { ini.Write("COM9", "1"); } else { ini.Write("COM9", "0"); }
                if (checkBox10.Checked == true) { ini.Write("COM10", "1"); } else { ini.Write("COM10", "0"); }
                if (checkBox11.Checked == true) { ini.Write("COM11", "1"); } else { ini.Write("COM11", "0"); }
                if (checkBox12.Checked == true) { ini.Write("COM12", "1"); } else { ini.Write("COM12", "0"); }

            }
            else //取消開機自啟動  
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.DeleteValue("JcShutdown", false);
                rk2.Close();
                rk.Close();

                //寫入ini檔為false
                string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                IniFile ini = new IniFile(str + "\\COMPortTest.ini");
                ini.Write("AUTO", "0");
            }
        }

        int timeLeft = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox13.Checked == false)
            {
                timeLeft = 1;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
            }
            else
            {
                //timer1.Stop();
                CloseWindow();
            }
        }

        public void CloseWindow()
        {

            Application.Exit();
        }

    }
}
