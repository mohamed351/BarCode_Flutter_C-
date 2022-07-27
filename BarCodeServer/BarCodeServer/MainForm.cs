using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarCodeServer
{

    public partial class MainForm : Form
    {
      
           
       
        public MainForm()
        {
            InitializeComponent();
         
         
        }


      

        private void MainForm_Load(object sender, EventArgs e)
        {
            string Myhost = System.Net.Dns.GetHostName();



            var myIP = System.Net.Dns.GetHostAddresses(Myhost).FirstOrDefault(a=> a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

        

            Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            pictureBox1.Image = qrcode.Draw($"{myIP.ToString()}:5000", 250);



        }
    }
}
