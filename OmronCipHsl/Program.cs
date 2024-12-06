using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication;
using HslCommunication.Profinet.Omron;

namespace OmronCipHsl
{
    public class Program
    {
        HslCommunication.Profinet.Omron.OmronConnectedCipNet plc = new HslCommunication.Profinet.Omron.OmronConnectedCipNet();
        HslCommunication.Profinet.Omron.OmronFinsUdp finsUdp = new OmronFinsUdp();
       
        public Program()
        {
            plc.CommunicationPipe = new HslCommunication.Core.Pipe.PipeTcpNet("192.168.10.40", 44818)
            {
                ConnectTimeOut = 5000,    // 连接超时时间，单位毫秒
                ReceiveTimeOut = 10000,    // 接收设备数据反馈的超时时间
                SleepTime = 0,// 获取或设置在正式接收对方返回数据前的时候，需要休息的时间，当设置为0的时候，不需要休息。
                SocketKeepAliveTime = 6000,//开启心跳检测,默认是小于0的-1获取或设置客户端的Socket的心跳时间信息，
                                           //这个是Socket底层自动实现的心跳包，不基于协议层实现。默认小于0，不开启心跳检测，如果需要开启，设置 60_000 比较合适，单位毫秒
                IsPersistentConnection = true,//获取或设置当前的管道是否是长连接，仅对于串口及TCP是有效的，默认都是长连接
            };
        }
        OmronCipNet cipClient = new OmronCipNet("192.168.10.40");
        static void Main(string[] args)
        {
            Program p1 = new Program();
            p1.cipClient.ConnectTimeOut = 500;
            var res = p1.cipClient.ConnectServer();
            if (res.IsSuccess)
            {
                var res1 = p1.cipClient.ReadBool("LC_Test_BoolArray",20);//这个方法可以实现读取bool数组
                if (res1.IsSuccess)
                {
                    bool[] bools = res1.Content;
                }
                var res2 = p1.cipClient.ReadBoolArray("LC_Test_BoolArray[0]");//这个方法的含义还是没有get到
                if (res2.IsSuccess)
                {
                    bool[] bools = res2.Content;
                }
                var res3 = p1.cipClient.ReadBoolArray("LC_Test_BoolArray[1]");
                if (res3.IsSuccess)
                {
                    bool[] bools = res3.Content;
                }
                var res4 = p1.cipClient.ReadBoolArray("LC_Test_BoolArray[2]");
                if (res4.IsSuccess)
                {
                    bool[] bools = res4.Content;
                }

                //读取字符串测试
                //如果字符串是一个数组ARRAY[0..2] OF String[256]这是PLC那边定义的。代表3个256的String

                OperateResult<string> res5 = p1.cipClient.ReadString("LC_Tes_StringArray[2]", 1, Encoding.ASCII);
                if (res5.IsSuccess)
                {
                    Console.WriteLine("Read [LC_Tes_StringArray[2]] Success, Value: " + res5.Content);
                }
                else
                {
                    Console.WriteLine("Read [LC_Tes_StringArray[2]] failed: " + res5.Message);
                }
            }
        }
    }

}
