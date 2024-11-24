using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OrmonPLC_Comunication.CIP
{
    /// <summary>
    /// 
    /// </summary>
    public class OrmonCip_BKY
    {
        private byte[] Registercmd = new byte[28]
        {
　　        //--------------------------------------------------------Header 24byte-------------------------------------
　　        0x6F,0x00,//命令 2byte
　　        0x04,0x00,//Header后面数据的长度 2byte
　　        0x00,0x00,0x00,0x00,//会话句柄 4byte
　　        0x00,0x00,0x00,0x00,//状态默认0 4byte
　　        0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,//发送方描述默认0 8byte
　　        0x00,0x00,0x00,0x00,//选项默认0 4byte
           //-------------------------------------------------------CommandSpecificData 指令指定数据 4byte
　　        0x01,0x00,//协议版本 2byte
　　        0x00,0x00,//选项标记 2byte
        };

        private byte[] RefRegistercmd = new byte[28]
        {
        
        　　//--------------------------------------------------------Header 24byte-------------------------------------
        　　0x6F,0x00,//命令 2byte
        　　0x04,0x00,//CommandSpecificData的长度 2byte
        　　0x6B,0x01,0x01,0x00,//会话句柄 4byte 由PLC生成
        　　0x00,0x00,0x00,0x00,//状态默认0 4byte
        　　0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,//发送方描述默认0 8byte
        　　0x00,0x00,0x00,0x00,//选项默认0 4byte
        
           //-------------------------------------------------------CommandSpecificData 指令指定数据 4byte
        
        　　0x01,0x00,//协议版本 2byte
        　　0x00,0x00,//选项标记 2byte
        };

        public byte[] SessionHandle = new byte[4] { 0x6B, 0x01, 0x01, 0x00 };//从应答报文提取的会话ID

        public void RegisterSend()
        {
            byte[] bytes = new byte[4];
            //using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            //{
            //    //socket.ReceiveTimeout = 10000; // 设置接收超时时间为5秒（单位是毫秒）
            //    socket.Connect("192.168.10.40", 44818);
            //    if (socket.Connected)
            //    {
            //        using (NetworkStream ns = new NetworkStream(socket))
            //        {
            //            if (ns.CanRead)
            //            {
            //                ns.Write(Registercmd, 0, Registercmd.Length - 1);
            //                byte[] temp = new byte[28];
            //                ns.ReadTimeout = 500;
            //                int a = ns.Read(temp, 0, 28);//这里会报错
            //            }
            //        }
            //    }
            //}

            //using (TcpClient tcp = new TcpClient("192.168.10.40", 44818))
            //{
            //    using (NetworkStream ns = tcp.GetStream())
            //    {
            //        ns.Write(Registercmd, 0, Registercmd.Length - 1);
            //        byte[] temp = new byte[30];
            //        int A = ns.Read(temp, 0, 29);//远程主机强制关闭一个连接
            //    }
            //}

            //TcpClient tcp = new TcpClient();
            //tcp.BeginConnect("192.168.10.40", 44818, requestCallback, tcp);

            //using (Socket SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            //{
            //    SocketClient.ReceiveTimeout = 3000;
            //    SocketClient.Connect("192.168.10.40", 44818);

            //    SocketClient.Send(Registercmd);
            //    byte[] RecData = new byte[Registercmd.Length];
            //    int CNT = SocketClient.Receive(RecData);//和我当时在车间测试出来的结果一样，我只能收到24个字节
            //    var temp = RecData.Select(i => RecData[i].ToString("{x:2}")).ToArray();
            //    foreach (var b in temp)
            //    {
            //        Console.WriteLine(b);
            //    }
            //}

        }

        private void requestCallback(IAsyncResult ar)
        {
            TcpClient tcp = (TcpClient)ar.AsyncState;
            using (NetworkStream ns = tcp.GetStream())
            {
                ns.Write(Registercmd, 0, Registercmd.Length - 1);
                ns.Flush();
                int cache = ns.ReadByte();
                if (cache != -1)
                {
                    byte[] temp = new byte[30];
                    temp[0] = (byte)cache;
                    ns.Read(temp, 1, temp.Length - 1);
                }
            }
        }
    }
}
