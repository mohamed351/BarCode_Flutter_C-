using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarCodeServer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SocketApplication.thread.Start();
            SocketApplication.OnReciveMessage += SocketApplication_OnReciveMessage;
            SocketApplication.OnSuccessfull += SocketApplication_OnSuccessfull;
            Application.Run(new MainForm());
         
        }

        private static void SocketApplication_OnSuccessfull(System.Net.Sockets.Socket socket, ReciverDTO reciver)
        {
            MessageBox.Show("Connected");

        }
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        private static void SocketApplication_OnReciveMessage(System.Net.Sockets.Socket socket, string reciver)
        {
            Process p = Process.GetProcessesByName("notepad").FirstOrDefault();
            reciver = reciver.Replace("\0"," ").Trim();
            if (p != null)
            {
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);

                SendKeys.SendWait(reciver+"\n");
            }
        }

    }
}
