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
        /// <summary>
        /// 使用之前必须给这个
        /// </summary>
        public string IP
        {
            get
            {
                if (string.IsNullOrEmpty(IP))
                {
                    return "192.168.0.10";
                }
                return IP;
            }
            set
            {
                var temp = IPAddress.Parse(value);//抛异常,赋值变量肯定是空的
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

    public class CIPReturnMessageKind
    {
        //const修饰的常量在声明的时候必须初始化，readonly修饰的常量则可以延迟到构造函数中初始化。

        /// <summary>
        /// 状态正常（在报文里低位在前高位在后）
        /// </summary>
        public const int SUCCESS = 0x0000;
        public const int INVALID_OR_UNSUPPORTED_ENCAPSSULATION_COMMANDS = 
    }
}
