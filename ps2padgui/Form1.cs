using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace ps2padgui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct ButtonInfo
        {
            public byte ok;
            public byte mode;
            public ushort btns;

            public byte rjoy_h;
            public byte rjoy_v;
            public byte ljoy_h;
            public byte ljoy_v;

            public byte right_p;
            public byte left_p;
            public byte up_p;
            public byte down_p;
            public byte triangle_p;
            public byte circle_p;
            public byte cross_p;
            public byte square_p;
            public byte l1_p;
            public byte r1_p;
            public byte l2_p;
            public byte r2_p;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] unkn16;
        }
        TcpClient tcpClient;
        Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        NetworkStream tcpStream;
        byte[] data = new byte[32];
        ButtonInfo btnInfo = new ButtonInfo();
        IntPtr ptPoit = Marshal.AllocHGlobal(32);
        IPEndPoint iPEndPoint;
        Thread udpThread;
        private void Form1_Load(object sender, EventArgs e)
        {
            tcpClient = new TcpClient("192.168.0.10", 18196);
            iPEndPoint = new IPEndPoint(IPAddress.Any,18197);
            udpSocket.Bind(iPEndPoint);
            tcpStream = tcpClient.GetStream();

            StreamWriter sw = new StreamWriter(tcpStream);

            System.Threading.Thread.Sleep(1000);

            sw.Write("ps2pad");
            sw.Flush();

            
            btnInfo = (ButtonInfo)Marshal.PtrToStructure(ptPoit, typeof(ButtonInfo));

            udpThread = new Thread(new ThreadStart(delegate ()
            {
            ViGEmClient viGEmClient = new ViGEmClient();
            var ctrl = viGEmClient.CreateDualShock4Controller();
            ctrl.Connect();
            while (true)
            {
                if (udpSocket.Available < 32)
                {
                    System.Threading.Thread.Sleep(10);
                    continue;
                }

                udpSocket.Receive(data, 32, SocketFlags.None);

                Marshal.Copy(data, 0, ptPoit, 32);
                btnInfo = (ButtonInfo)Marshal.PtrToStructure(ptPoit, typeof(ButtonInfo));

                ctrl.SetAxisValue(DualShock4Axis.LeftThumbX, btnInfo.ljoy_h);
                ctrl.SetAxisValue(DualShock4Axis.LeftThumbY, btnInfo.ljoy_v);

                ctrl.SetAxisValue(DualShock4Axis.RightThumbX, btnInfo.rjoy_h);
                ctrl.SetAxisValue(DualShock4Axis.RightThumbY, btnInfo.rjoy_v);

                ctrl.SetButtonState(DualShock4Button.Cross, btnInfo.cross_p > 20);
                ctrl.SetButtonState(DualShock4Button.Circle, btnInfo.circle_p > 20);
                ctrl.SetButtonState(DualShock4Button.Triangle, btnInfo.triangle_p > 20);
                ctrl.SetButtonState(DualShock4Button.Square, btnInfo.square_p > 20);

                ctrl.SetButtonState(DualShock4Button.ThumbRight,(btnInfo.btns & 4) == 0);
                ctrl.SetButtonState(DualShock4Button.ThumbLeft, (btnInfo.btns & 2) == 0);

                ctrl.SetButtonState(DualShock4Button.Options, (btnInfo.btns & 1) == 0);
                ctrl.SetButtonState(DualShock4Button.Share, (btnInfo.btns & 8) == 0);

                ctrl.SetButtonState(DualShock4Button.ShoulderLeft, btnInfo.l1_p > 20);
                ctrl.SetButtonState(DualShock4Button.TriggerLeft, btnInfo.l2_p > 20);

                ctrl.SetButtonState(DualShock4Button.ShoulderRight, btnInfo.r1_p > 20);
                ctrl.SetButtonState(DualShock4Button.TriggerRight, btnInfo.r2_p > 20);

                System.Threading.Thread.Sleep(10);

                bool DPUP = (btnInfo.btns &     16) == 0;
                bool DPRIGHT = (btnInfo.btns &  32) == 0;
                bool DPDOWN = (btnInfo.btns &   64) == 0;
                bool DPLEFT = (btnInfo.btns &   128) == 0;
            
                if (DPUP)
                {
                    if (DPRIGHT)
                    {
                        ctrl.SetDPadDirection(DualShock4DPadDirection.Northeast);
                    }
                    else if (DPLEFT)
                    {
                        ctrl.SetDPadDirection(DualShock4DPadDirection.Northwest);
                    }
                    else
                    {
                        ctrl.SetDPadDirection(DualShock4DPadDirection.North);
                    }
                }
                else if (DPDOWN)
                {
                    if (DPRIGHT)
                    {
                        ctrl.SetDPadDirection(DualShock4DPadDirection.Southeast);
                    }
                    else if (DPLEFT)
                    {
                        ctrl.SetDPadDirection(DualShock4DPadDirection.Southwest);
                    }
                    else
                    {
                        ctrl.SetDPadDirection(DualShock4DPadDirection.South);
                    }
                }
                else if (DPLEFT)
                {
                    ctrl.SetDPadDirection(DualShock4DPadDirection.West);
                }
                else if (DPRIGHT)
                {
                    ctrl.SetDPadDirection(DualShock4DPadDirection.East);
                }
                else
                {
                    ctrl.SetDPadDirection(DualShock4DPadDirection.None);
                }

                }
            }));

            udpThread.Start();
        }
        

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           
        }
    }
}
