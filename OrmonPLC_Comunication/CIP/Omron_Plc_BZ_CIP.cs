using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace OrmonPLC_Comunication.CIP
{
    public class CipCommand
    {
        public CipCommand() { }

        public byte[] CipRegistercmd = new byte[28]
        {
            0x65,0x00,
　　        0x04,0x00,
　　        0x00,0x00,0x00,0x00,
　　        0x00,0x00,0x00,0x00,
　　        0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
　　        0x00,0x00,0x00,0x00,
　　        0x01,0x00,
　　        0x00,0x00,
        };

        public byte[] CipRefRegistercmd = new byte[28]
        {
　　        0x6F,0x00,
　　        0x04,0x00,
　　        0x6B,0x01,0x01,0x00,
　　        0x00,0x00,0x00,0x00,
　　        0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
　　        0x00,0x00,0x00,0x00,
　　        0x01,0x00,
　　        0x00,0x00,
        };

        public byte[] CipSessionHandle = new byte[4] { 0x6B, 0x01, 0x01, 0x00 };

        public byte[] CipHeader = new byte[24]
        {
            0x6F,0x00,
　　        0x28,0x00,
　　        0x6B,0x01,0x01,0x00,
　　        0x00,0x00,0x00,0x00,
　　        0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
　　        0x00,0x00,0x00,0x00,
        };

        public byte[] CipCommandSpecificData = new byte[16]
        {
            0x00,0x00,0x00,0x00,
　　        0x01,0x00,
　　        0x02,0x00,
　　        0x00,0x00,
　　        0x00,0x00,
　　        0xb2,0x00,
　　        0x18,0x00,
        };

        public byte[] CipMessage_Header = new byte[4]
        {
            0x4C,
            0x03,
            0x91,
            0x04,
        };

        public byte[] CipMessage_TagCode;

        public byte[] CipMessage_RCMD = new byte[2]//读指令
        {
            0x01,0x00,
        };

        public byte[] CipMessage_WCMD = new byte[6]//写指令
        {
            0xC1,0x00,
            0x01,0x00,
            0x00,0x00,
        };

        public byte[] CipMessage_WSTR;//写入的字符
    }
    public class Omron_Plc_BZ_CIP
    {
        #region 成员变量声明
        private string mIP;
        private int mPort;
        private bool mConnected;
        private List<string> mVariableNames;
        #endregion

        public Omron_Plc_BZ_CIP() { }
        public Omron_Plc_BZ_CIP(string ip, int port)
        {
            mIP = ip;
            mPort = port;
            mVariableNames = new List<string>();
        }
        public Omron_Plc_BZ_CIP(string ip, int port, List<string> VariableNames)
        {
            mIP = ip;
            mPort = port;
            mVariableNames = VariableNames;
        }
        public bool IsConnected
        {
            get
            {
                return mConnected;
            }
        }
        public bool Connect()//无具体实现，仅方便对接
        {
            lock (this)
            {
                try
                {
                    //进行连接
                    mConnected = true;
                    return true;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
                mConnected = false;
                return false;
            }
        }
        public void DisConnect()//无具体实现，仅方便对接
        {
            lock (this)
            {
                try
                {
                    //断开连接
                    mConnected = false;
                    return;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }
        public bool ReadBool(string TagName, out bool result, bool IsCheck = true)
        {
            try
            {
                string res_str;
                bool rtn_bl = ReadVariable(TagName, out res_str, IsCheck);
                if (res_str != "null")
                {
                    result = Convert.ToBoolean(res_str);
                    return true;
                }
                else
                {
                    result = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            result = false;
            return false;
        }
        public bool ReadInt(string TagName, out Int16 result, bool IsCheck = true)
        {
            try
            {
                string res_str;
                bool rtn_bl = ReadVariable(TagName, out res_str, IsCheck);
                if (res_str != "null")
                {
                    result = Convert.ToInt16(res_str);
                    return true;
                }
                else
                {
                    result = -9999;
                    return false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            result = -9999;
            return false;
        }
        public bool ReadDInt(string TagName, out Int32 result, bool IsCheck = true)
        {
            try
            {
                string res_str;
                bool rtn_bl = ReadVariable(TagName, out res_str, IsCheck);
                if (res_str != "null")
                {
                    result = Convert.ToInt32(res_str);
                    return true;
                }
                else
                {
                    result = -99999;
                    return false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            result = -99999;
            return false;
        }
        public bool ReadWord(string TagName, out UInt16 result, bool IsCheck = true)
        {
            try
            {
                string res_str;
                bool rtn_bl = ReadVariable(TagName, out res_str, IsCheck);
                if (res_str != "null")
                {
                    result = Convert.ToUInt16(res_str);
                    return true;
                }
                else
                {
                    result = 9999;
                    return false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            result = 9999;
            return false;
        }
        public bool ReadDWord(string TagName, out UInt32 result, bool IsCheck = true)
        {
            try
            {
                string res_str;
                bool rtn_bl = ReadVariable(TagName, out res_str, IsCheck);
                if (res_str != "null")
                {
                    result = Convert.ToUInt32(res_str);
                    return true;
                }
                else
                {
                    result = 99999;
                    return false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            result = 99999;
            return false;
        }
        public bool ReadString(string TagName, out string result, bool IsCheck = true)
        {
            try
            {
                string res_str;
                bool rtn_bl = ReadVariable(TagName, out res_str, IsCheck);
                if (res_str != "null")
                {
                    result = res_str;
                    return true;
                }
                else
                {
                    result = "-99999";
                    return false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            result = "-99999";
            return false;
        }
        public bool WriteBool(string TagName, bool value, bool IsCheck = true)
        {
            try
            {
                string ValStr = Convert.ToString(value);
                bool rtn_bl = false;
                int RetryTimes = 0;
                do
                {
                    if (RetryTimes >= 3)
                    {
                        break;
                    }
                    rtn_bl = WriteVariable(TagName, ValStr, "BOOL", IsCheck);
                    RetryTimes++;

                } while (!rtn_bl);

                return rtn_bl;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return false;
        }
        public bool WriteInt(string TagName, Int16 value, bool IsCheck = true)
        {
            try
            {
                string ValStr = Convert.ToString(value);
                bool rtn_bl = false;
                int RetryTimes = 0;
                do
                {
                    if (RetryTimes >= 3)
                    {
                        break;
                    }
                    rtn_bl = WriteVariable(TagName, ValStr, "INT", IsCheck);
                    RetryTimes++;

                } while (!rtn_bl);

                return rtn_bl;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return false;
        }
        public bool WriteDInt(string TagName, Int32 value, bool IsCheck = true)
        {
            try
            {
                string ValStr = Convert.ToString(value);
                bool rtn_bl = false;
                int RetryTimes = 0;
                do
                {
                    if (RetryTimes >= 3)
                    {
                        break;
                    }
                    rtn_bl = WriteVariable(TagName, ValStr, "DINT", IsCheck);
                    RetryTimes++;

                } while (!rtn_bl);

                return rtn_bl;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return false;
        }
        public bool WriteWord(string TagName, UInt16 value, bool IsCheck = true)
        {
            try
            {
                string ValStr = Convert.ToString(value);
                bool rtn_bl = false;
                int RetryTimes = 0;
                do
                {
                    if (RetryTimes >= 3)
                    {
                        break;
                    }
                    rtn_bl = WriteVariable(TagName, ValStr, "WORD", IsCheck);
                    RetryTimes++;

                } while (!rtn_bl);

                return rtn_bl;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return false;
        }
        public bool WriteDWord(string TagName, UInt32 value, bool IsCheck = true)
        {
            try
            {
                string ValStr = Convert.ToString(value);
                bool rtn_bl = false;
                int RetryTimes = 0;
                do
                {
                    if (RetryTimes >= 3)
                    {
                        break;
                    }
                    rtn_bl = WriteVariable(TagName, ValStr, "DWORD", IsCheck);
                    RetryTimes++;

                } while (!rtn_bl);

                return rtn_bl;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return false;
        }
        public bool WriteString(string TagName, string value, bool IsCheck = true)
        {
            try
            {
                string ValStr = value;

                bool rtn_bl = false;
                int RetryTimes = 0;
                do
                {
                    if (RetryTimes >= 3)
                    {
                        break;
                    }
                    rtn_bl = WriteVariable(TagName, ValStr, "STRING", IsCheck);
                    RetryTimes++;

                } while (!rtn_bl);

                return rtn_bl;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return false;
        }
        private bool ReadVariable(string TagName, out string Result, bool IsCheck)
        {
            Result = "null";
            if (TagName == "" || TagName ==String.Empty || TagName == null)
            {
                return false;
            }
            if (!mVariableNames.Contains(TagName) && IsCheck)
            {
                return false;
            }
            using (Socket SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    CipCommand CipCMD = new CipCommand();

                    SocketClient.ReceiveTimeout = 3000;
                    SocketClient.Connect(mIP, mPort);
                    if (SocketClient.Connected)
                    {
                        mConnected = true;
                    }
                    else
                    {
                        mConnected = false;
                        return false;
                    }

                    SocketClient.Send(CipCMD.CipRegistercmd);
                    byte[] RecData = new byte[CipCMD.CipRegistercmd.Length];
                    int CNT = SocketClient.Receive(RecData);

                    byte[] ReadCMD = CreatReadCode(TagName);//生成读命令
                    ReadCMD[4] = RecData[4]; ReadCMD[5] = RecData[5];
                    ReadCMD[6] = RecData[6]; ReadCMD[7] = RecData[7];

                    SocketClient.Send(ReadCMD);
                    byte[] RecDataBuff = new byte[ReadCMD.Length + 256 * 2];
                    CNT = SocketClient.Receive(RecDataBuff);
                    RecData = new byte[CNT];
                    Array.Copy(RecDataBuff, RecData, RecData.Length);
                    Result = GetValue(RecData);

                    SocketClient.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }

                mConnected = false;
                return false;
            }
        }
        private bool WriteVariable(string TagName, string Value, string Vtype, bool IsCheck)
        {
            if (TagName == "" || TagName == String.Empty || TagName == null)
            {
                return false;
            }
            if (!mVariableNames.Contains(TagName) && IsCheck)
            {
                return false;
            }
            using (Socket SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    CipCommand CipCMD = new CipCommand();

                    SocketClient.ReceiveTimeout = 3000;
                    SocketClient.Connect(mIP, mPort);
                    if (SocketClient.Connected)
                    {
                        mConnected = true;
                    }
                    else
                    {
                        mConnected = false;
                        return false;
                    }

                    SocketClient.Send(CipCMD.CipRegistercmd);
                    byte[] RecData = new byte[CipCMD.CipRegistercmd.Length];
                    int CNT = SocketClient.Receive(RecData);

                    byte[] WriteCMD = CreatWriteCode(TagName, Value, Vtype);//生成写命令
                    WriteCMD[4] = RecData[4]; WriteCMD[5] = RecData[5];
                    WriteCMD[6] = RecData[6]; WriteCMD[7] = RecData[7];

                    SocketClient.Send(WriteCMD);
                    byte[] RecDataBuff = new byte[WriteCMD.Length + 256 * 2];
                    CNT = SocketClient.Receive(RecDataBuff);
                    RecData = new byte[CNT];
                    Array.Copy(RecDataBuff, RecData, RecData.Length);
                    byte[] ResData = new byte[2];
                    Array.Copy(RecData, RecData.Length - 2 - 1, ResData, 0, ResData.Length);

                    SocketClient.Close();

                    if (ResData[0] == 0 && ResData[1] == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }

                mConnected = false;
                return false;
            }
        }
        private byte[] CreatReadCode(string TagName)//生成读命令
        {
            CipCommand CipCMD = new CipCommand();

            int Str2ByteLen;
            CipCMD.CipMessage_TagCode = Str2Bytes(TagName, out Str2ByteLen);
            CipCMD.CipMessage_Header[0] = 0x4C;
            CipCMD.CipMessage_Header[1] = (byte)((CipCMD.CipMessage_TagCode.Length + 2) / 2);
            CipCMD.CipMessage_Header[3] = (byte)Str2ByteLen;
            CipCMD.CipCommandSpecificData[14] = (byte)(CipCMD.CipMessage_Header.Length +
                                                       CipCMD.CipMessage_TagCode.Length +
                                                       CipCMD.CipMessage_RCMD.Length);

            CipCMD.CipHeader[2] = (byte)(CipCMD.CipCommandSpecificData.Length +
                                         CipCMD.CipMessage_Header.Length +
                                         CipCMD.CipMessage_TagCode.Length +
                                         CipCMD.CipMessage_RCMD.Length);

            CipCMD.CipHeader[12] = 0x00;//16

            List<byte> byte_list = new List<byte>();
            byte_list.AddRange(CipCMD.CipHeader);//24
            byte_list.AddRange(CipCMD.CipCommandSpecificData);//16
            byte_list.AddRange(CipCMD.CipMessage_Header);//4
            byte_list.AddRange(CipCMD.CipMessage_TagCode);//8
            byte_list.AddRange(CipCMD.CipMessage_RCMD);//2

            string Byte2Hex = "";
            foreach (var item in byte_list)
            {
                string hstr = Convert.ToString(item, 16);
                if (hstr.Length == 1)
                {
                    hstr = "0" + hstr;
                }
                Byte2Hex = Byte2Hex + hstr;
            }

            byte[] RearCode = byte_list.ToArray();
            return RearCode;
        }
        private byte[] CreatWriteCode(string TagName, string Value, string Vtype)
        {
            CipCommand CipCMD = new CipCommand();

            int Str2ByteLen, Value2ByteLen;
            CipCMD.CipMessage_TagCode = Str2Bytes(TagName, out Str2ByteLen);
            CipCMD.CipMessage_Header[0] = 0x4D;
            CipCMD.CipMessage_Header[1] = (byte)((CipCMD.CipMessage_TagCode.Length + 2) / 2);
            CipCMD.CipMessage_Header[3] = (byte)Str2ByteLen;
            CipCMD.CipCommandSpecificData[14] = (byte)(CipCMD.CipMessage_Header.Length +
                                                       CipCMD.CipMessage_TagCode.Length +
                                                       CipCMD.CipMessage_WCMD.Length);

            CipCMD.CipMessage_WSTR = new byte[] { };

            byte[] wdata;
            switch (Vtype.ToUpper())
            {
                case "BYTE":
                    break;

                case "BOOL":
                    wdata = BitConverter.GetBytes(Convert.ToBoolean(Value));
                    CipCMD.CipMessage_WCMD[0] = 0xC1;
                    CipCMD.CipMessage_WCMD[4] = wdata[0];
                    break;

                case "INT":
                    wdata = BitConverter.GetBytes(Convert.ToInt16(Value));
                    CipCMD.CipMessage_WCMD[0] = 0xC3;
                    CipCMD.CipMessage_WCMD[4] = wdata[0];
                    CipCMD.CipMessage_WCMD[5] = wdata[1];
                    break;

                case "DINT":
                    wdata = BitConverter.GetBytes(Convert.ToInt32(Value));
                    Array.Resize(ref CipCMD.CipMessage_WCMD, 8);
                    CipCMD.CipMessage_WCMD[0] = 0xC4;
                    CipCMD.CipMessage_WCMD[4] = wdata[0];
                    CipCMD.CipMessage_WCMD[5] = wdata[1];
                    CipCMD.CipMessage_WCMD[6] = wdata[2];
                    CipCMD.CipMessage_WCMD[7] = wdata[3];
                    break;

                case "WORD":
                    wdata = BitConverter.GetBytes(Convert.ToUInt16(Value));
                    CipCMD.CipMessage_WCMD[0] = 0xD2;
                    CipCMD.CipMessage_WCMD[4] = wdata[0];
                    CipCMD.CipMessage_WCMD[5] = wdata[1];
                    break;

                case "DWORD":
                    wdata = BitConverter.GetBytes(Convert.ToUInt32(Value));
                    Array.Resize(ref CipCMD.CipMessage_WCMD, 8);
                    CipCMD.CipMessage_WCMD[0] = 0xD3;
                    CipCMD.CipMessage_WCMD[4] = wdata[0];
                    CipCMD.CipMessage_WCMD[5] = wdata[1];
                    CipCMD.CipMessage_WCMD[6] = wdata[2];
                    CipCMD.CipMessage_WCMD[7] = wdata[3];
                    break;

                case "STRING":
                    CipCMD.CipMessage_WSTR = Str2Bytes(Value, out Value2ByteLen);
                    CipCMD.CipMessage_WCMD[0] = 0xD0;
                    CipCMD.CipMessage_WCMD[4] = (byte)CipCMD.CipMessage_WSTR.Length;
                    CipCMD.CipMessage_WCMD[5] = 0x00;
                    break;
            }

            CipCMD.CipHeader[2] = (byte)(CipCMD.CipCommandSpecificData.Length +
                                         CipCMD.CipMessage_Header.Length +
                                         CipCMD.CipMessage_TagCode.Length +
                                         CipCMD.CipMessage_WCMD.Length +
                                         CipCMD.CipMessage_WSTR.Length);

            CipCMD.CipCommandSpecificData[14] = (byte)(CipCMD.CipMessage_Header.Length +
                                                       CipCMD.CipMessage_TagCode.Length +
                                                       CipCMD.CipMessage_WCMD.Length +
                                                       CipCMD.CipMessage_WSTR.Length);

            List<byte> byte_list = new List<byte>();
            byte_list.AddRange(CipCMD.CipHeader);
            byte_list.AddRange(CipCMD.CipCommandSpecificData);
            byte_list.AddRange(CipCMD.CipMessage_Header);
            byte_list.AddRange(CipCMD.CipMessage_TagCode);
            byte_list.AddRange(CipCMD.CipMessage_WCMD);
            byte_list.AddRange(CipCMD.CipMessage_WSTR);

            string Byte2Hex = "";
            foreach (var item in byte_list)
            {
                string hstr = Convert.ToString(item, 16);
                if (hstr.Length == 1)
                {
                    hstr = "0" + hstr;
                }
                Byte2Hex = Byte2Hex + hstr;
            }

            byte[] RearCode = byte_list.ToArray();
            return RearCode;
        }
        private byte[] Str2Bytes(string Str, out int Str2ByteLen)
        {
            List<byte> byte_list = new List<byte>();
            byte[] TagCode = Encoding.UTF8.GetBytes(Str);
            Str2ByteLen = TagCode.Length;
            if (TagCode.Length % 2 == 0)
            {
                return TagCode;
            }
            else
            {
                byte_list.AddRange(TagCode);
                byte_list.Add(0x00);
                return byte_list.ToArray();
            }
        }
        private string GetValue(byte[] data)
        {
            string res_str = "null";
            try
            {
                byte[] rdata;
                int idx = 44;
                switch (data[idx])
                {
                    case 0xD1://BYTE
                        //res_str = data[idx + 2].ToString();
                        break;

                    case 0xC1://BOOL
                        rdata = new byte[] { data[idx + 2] };
                        bool res_bl = BitConverter.ToBoolean(rdata, 0);
                        res_str = res_bl.ToString();
                        break;

                    case 0xC3://INT(INT16)
                        rdata = new byte[] { data[idx + 2],data[idx + 3] };
                        Int16 res_int16 = BitConverter.ToInt16(rdata, 0);
                        res_str = res_int16.ToString();
                        break;

                    case 0xC4://DINT(INT32)
                        rdata = new byte[] { data[idx + 2],data[idx + 3],data[idx + 4],data[idx + 5] };
                        Int32 res_int32 = BitConverter.ToInt32(rdata, 0);
                        res_str = res_int32.ToString();
                        break;

                    case 0xD2://WORD（UINT16）
                        rdata = new byte[] { data[idx + 2],data[idx + 3]};
                        UInt16 res_uint16 = BitConverter.ToUInt16(rdata, 0);
                        res_str = res_uint16.ToString();
                        break;

                    case 0xD3://DWORD（UINT32）
                        rdata = new byte[] { data[idx + 2],data[idx + 3],data[idx + 4],data[idx + 5] };
                        UInt32 res_uint32 = BitConverter.ToUInt32(rdata, 0);
                        res_str = res_uint32.ToString();
                        break;

                    case 0xD0://STRING
                        rdata = new byte[data.Length - idx - 4];
                        Array.Copy(data, idx + 4, rdata, 0, rdata.Length);
                        res_str = Encoding.UTF8.GetString(rdata);
                        break;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return res_str;
        }

    }
}
