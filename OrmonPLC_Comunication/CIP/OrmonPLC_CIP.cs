using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OrmonPLC_Comunication.CIP
{
    
    public class OrmonPLC_CIP
    {
        TcpClient client;
        public string IP
        {
            get {  return IP; }
            set
            {
                var temp = IPAddress.Parse(value);
                if (temp != null)
                {
                    IP = temp.ToString();
                }
            }
        }
        public int Port
        {
            get { return Port; }
            set
            {
                if (Port != value)
                {
                    Port = value;
                }
            }
        }
        /// <summary>
        ///  只通过这一个方法去判断是否连接成功
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            client = new TcpClient();
            client.Connect(IP, Port);
            if (client.Connected)
            {
                return true;
            }
            return false;
        }

        public bool DisConnect()
        {
            if (client != null)
            {
                client.Close();
                return true;
            }
            return false;
        }
        
        public OrmonPLC_CIP()
        {
            
        }
    }
}
