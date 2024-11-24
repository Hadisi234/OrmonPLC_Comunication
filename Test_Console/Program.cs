using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrmonPLC_Comunication.CIP;

namespace Test_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Omron_Plc_BZ_CIP cip = new Omron_Plc_BZ_CIP("192.168.10.40",44818, new List<string>() { });
            cip.Connect();

            OrmonCip_BKY bky = new OrmonCip_BKY();
            bky.RegisterSend();
            //cip.ReadBool();
            Console.ReadLine();
        }
    }
}
