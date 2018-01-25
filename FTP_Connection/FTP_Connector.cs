using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTP_Connection {
    class FTP_Connector {

        private static string un = "korgua";
        private static string psw = "S|-|1TisVH";
        private static string host = "ftp://petrolcard.hu/";
        private static string downloadDir = @"C:\\vhcom\\";

        public FtpWebRequest connect(string fileName) {
            FtpWebRequest request;
            try {
                request = FtpWebRequest.Create(host+fileName) as FtpWebRequest;
                request.Credentials = new NetworkCredential(un, psw);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = true;
                return request;
            }
            catch(Exception e) {
                Console.WriteLine(String.Format("Nem sikerült csatlakozni a {0}: {1}", host, e.Message));
                return null;
            }
        }

        public bool DownloadFile(string fileName = "vh_update.txt") {
            int i = 1;
            bool error = false;
            FtpWebRequest request = connect(fileName);
            try {
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                i = 2;
                List<string> dir = new List<string>();
                i = 3;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                i = 4;
                Stream stream = response.GetResponseStream();
                i = 5;
                StreamReader reader = new StreamReader(stream);
                i = 6;
                MemoryStream memStream = new MemoryStream();
                byte[] buffer = new byte[1024]; //downloads in chuncks
                while(true) {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if(bytesRead == 0) {
                        break;
                    }
                    else {
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }
                reader.Close();
                response.Close();
                if(!Directory.Exists(downloadDir)) {
                    try {
                        Directory.CreateDirectory(downloadDir);
                    }
                    catch(Exception e) {
                        Console.WriteLine(String.Format("Cannot create directory: {0} because {1}",downloadDir, e.Message));
                        return false;
                    }
                }
                if(File.Exists(downloadDir+fileName) && !error) {
                    try {
                        File.Delete(downloadDir + fileName);
                    }
                    catch(Exception e) {
                        Console.WriteLine(String.Format("Cannot delete file: {0} because", downloadDir + fileName, e.Message));
                        return false;
                    }
                }
                try {
                    using(FileStream fs = File.Create(downloadDir+fileName)) {
                        memStream.Position = 0;
                        memStream.CopyTo(fs);
                        fs.Close();
                        memStream.Close();
                    }
                }
                catch(Exception e) {
                    Console.WriteLine(String.Format("Cannot download {0} to {1} because: ", host + fileName, downloadDir + fileName));
                    return false;
                }
                return true;
            }
            catch(Exception e) {
                Console.WriteLine(String.Format("[FtpDirList] Exception at {0}: {1}", i, e.Message));
            }
            return true;
        }

    }
}
