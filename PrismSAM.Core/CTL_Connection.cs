using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PrismSAM.Core
{
    public class CTL_Connection
    {
        #region Static Field
        public static string server_addr = "127.0.0.1";
        public static int server_port = 8688;
        public static string CTL_addr = "192.168.1.101";
        public static Socket client;
        public static bool socketConnectionStatus = false;
        public static string errmsg;

        //TODO: move to BS control module
        public static bool BS_Catch_enabled = false;
        public static bool BS_isCatched = false;
        public static double BS_Threshold = -40;
        #endregion

        #region Methods
        public static int Connect()
        {
            try
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(new IPEndPoint(IPAddress.Parse(server_addr), server_port));
                socketConnectionStatus = true;
                errmsg = null;
                return 1;
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return -1;
            } 
        }

        public static int Disconnect()
        {
            try
            {
                client.Close();
                socketConnectionStatus = false;
                return 1;
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return -1;
            }
        }

        public static int LaunchServer(string CTL_ip)
        {
            try
            {
                using (Process p = new Process())
                {
                    string scriptPath = "CTL_Server.py";
                    string sArguments = scriptPath + " " + CTL_ip;
                    p.StartInfo.FileName = "python.exe";
                    p.StartInfo.Arguments = sArguments;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    return 1;
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return -1;
            }
        }

        public static string SendCommand(string command)
        {
            client.Send(Encoding.UTF8.GetBytes(command));
            var feedback_bytes = new byte[128];
            int count = client.Receive(feedback_bytes);
            string feedback = Encoding.UTF8.GetString(feedback_bytes, 0, count);
            return feedback;
        }
        #endregion




    }
}
