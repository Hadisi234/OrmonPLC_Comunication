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
        OmronCipNet cipClient = new OmronCipNet("192.168.10.40");
        static void Main(string[] args)
        {
            Program p1 = new Program();
            p1.cipClient.ConnectTimeOut = 500;
            var res = p1.cipClient.ConnectServer();
            if (res.IsSuccess)
            {
                var res1 = p1.cipClient.ReadBoolArray("LC_Test_BoolArray");
                if (res1.IsSuccess)
                {
                    bool[] bools = res1.Content;
                }
            }
        }
    }

}
