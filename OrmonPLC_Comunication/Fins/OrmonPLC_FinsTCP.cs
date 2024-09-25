using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace OrmonPLC_Comunication.Fins
{
    internal struct FinsMessage
    {
        /// <summary>
        /// ASCII code:'Fins'
        /// </summary>
        byte[] Header;
        /// <summary>
        /// 20到2020字节，在Command后面的数据长度
        /// </summary>
        byte[] Length;
        byte[] Command;
        /// <summary>
        /// Not used,so does not require checking by at the receving end.
        /// </summary>
        byte[] ErrorCode;
        /// <summary>
        /// From Fins header ICF to end of Data
        /// </summary>
        byte[] FinsFrame;

        public byte[] GetBytes(int byteNumber)
        {
            byte[] bytes = new byte[byteNumber];
            if (Header == null || Length == null || Command == null || ErrorCode == null || FinsFrame == null)
            {
                throw new Exception("报文拼接失败！存在有报文是空值");
            }
            int index = 0;
            Header.CopyTo(bytes, index);
            index += Header.Length - 1;
            Length.CopyTo(bytes, index);
            index += Length.Length - 1;
            Command.CopyTo(bytes, index);
            index += Command.Length - 1;
            ErrorCode.CopyTo(bytes, index);
            index += ErrorCode.Length - 1;
            FinsFrame.CopyTo(bytes, index);//如果内容超出数组长度会抛出异常的
            return bytes;
        }

    }

    public class OrmonPLC_FinsTCP
    {
        string targetIP;
        int targetPort;
        private bool connected;

        /// <summary>
        /// true 为成功连接
        /// </summary>
        public bool Connected
        {
            get
            {
                return connected;
            }
        }

        public OrmonPLC_FinsTCP(string ip, int port)
        {
            IPAddress ipaddress;
            if (!IPAddress.TryParse(ip, out ipaddress))
            {
                throw new Exception("ip地址不正确");
            }
            if (port < 0 || port > int.MaxValue)
            {
                throw new Exception("端口号超出合理范围");
            }
            targetIP = ip;
            targetPort = port;
        }

        /// <summary>
        /// 使用TCP短连接，检测IP和端口是否能连接上
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            TcpClient tcpClient = new TcpClient(); 
            tcpClient.Connect(targetIP, targetPort);
            if (tcpClient.Connected)
            {
                connected = true;
                tcpClient.Close();
                return true;
            }
            else
            {
                connected = false;
                tcpClient.Close();
                return false;
            }
        }
    }
}
