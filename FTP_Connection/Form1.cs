using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTP_Connection {
    public partial class FTP_window : Form {

        FTP_Connector ftpConn = new FTP_Connector();

        public FTP_window() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Console.WriteLine("Button clicked");
            ftpConn.DownloadFile("Radio Hungary_1.6.1.apk");

        }
    }
}
